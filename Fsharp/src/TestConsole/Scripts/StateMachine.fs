namespace TimedOperation

open System

module StateMachine = 

    type MessageHandler = unit -> Timed<unit>

    // state data
    type ReadyData = Timed<TimeSpan list>

    type ReceivedMessageData = Timed<TimeSpan list *  MessageHandler>
    
    type NoMessageData = Timed<TimeSpan list>

    // states
    type PollingConsumer = 
    | ReadyState of ReadyData
    | ReceivedMessageState of ReceivedMessageData
    | NoMessageState of NoMessageData
    | StoppedState

    let transformFromStopped  = StoppedState

    let transitionFromNoMessage shouldIdle idle (nm : NoMessageData) = 
        if shouldIdle nm then 
            idle () |> Untimed.withResult nm.Result |> ReadyState
        else StoppedState

    let transitionFromReady shouldPoll poll (r: ReadyData) =
        if shouldPoll r then 
            let msg = poll ()
            match msg.Result with
            | Some h -> msg |> Untimed.withResult (r.Result, h) |> ReceivedMessageState
            | None -> msg |> Untimed.withResult r.Result |>  NoMessageState
        else StoppedState

    let transitionFromReceived (rm : ReceivedMessageData) = 
        let list, handler = rm.Result
        let t = handler ()
        let totalDuration = rm.Duration + t.Duration
        t |> Untimed.withResult (totalDuration :: list) |> ReadyState

    let rec run trans state = 
        let nexState = trans state
        match nexState with 
        | StoppedState -> StoppedState
        | _ -> run trans nexState

    let transition shouldPoll poll shouldIdle idle state = 
        match state with 
        | ReadyState r -> transitionFromReady shouldPoll poll r
        | ReceivedMessageState rm -> transitionFromReceived rm
        | NoMessageState nm -> transitionFromNoMessage shouldIdle idle nm
        | StoppedState -> transformFromStopped

    let shouldIdle idleduration stopBefore (nm : NoMessageData ) = 
        nm.Stopped + idleduration < stopBefore

    let idle (idleDuration : TimeSpan) = 
        let s () = 
            idleDuration.TotalMilliseconds
            |> int
            |> Async.Sleep
            |> Async.RunSynchronously
        printfn "sleeping"
        Timed.timeOn Clocks.machineClock s ()

    let shouldPoll calculateExpectedDuration stopBefore (r : ReadyData) = 
        let durations = r.Result
        let expectedHandleDuration = calculateExpectedDuration durations
        r.Stopped + expectedHandleDuration < stopBefore

    let poll pollForMessage handle clock () = 
        let p () = 
            match pollForMessage () with
            | Some msg ->
                let h () = Timed.timeOn clock (handle >> ignore) msg
                Some (h : MessageHandler)
            | None -> None
        Timed.timeOn clock p ()

    let calculateAverage (durations : TimeSpan list) = 
        if durations.IsEmpty then
            None
        else
            durations
            |> List.averageBy (fun x -> float x.Ticks)
            |> int64
            |> TimeSpan.FromTicks
            |> Some

    let calculateAverageAndStardardDeviation durations = 
        let stdDev(avg : TimeSpan) =
            durations
            |> List.averageBy (fun x -> ((x - avg).Ticks |> float) ** 2.)
            |> sqrt
            |> int64
            |> TimeSpan.FromTicks
        durations |> calculateAverage |> Option.map (fun avg -> avg, stdDev avg)

    let calculateExpectedDuration estimatedDuration durations = 
        match calculateAverageAndStardardDeviation durations with
        | None -> estimatedDuration
        | Some (avg, stdDev) -> avg + stdDev + stdDev + stdDev

