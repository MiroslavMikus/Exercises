namespace TimedOperation

open System

type Timed<'a> = 
    {
        Started : DateTimeOffset
        Stopped : DateTimeOffset
        Result : 'a
    }
    member this.Duration = this.Stopped - this.Started

module Untimed = 
    let map f timed = { Started = timed.Started; Stopped = timed.Stopped; Result = f timed.Result }

    let withResult newResult timed = map (fun _ -> newResult) timed

module Timed =
    let capture clock x =
        let now = clock()
        { Started = now; Stopped = now; Result = x }

    let map clock f x = 
        let result = f x.Result
        let stopped = clock()
        { Started = x.Started; Stopped = stopped; Result = result }

    let timeOn clock f x = x |> capture clock |> map clock f

module Clocks = 
    let machineClock () = DateTimeOffset.Now

    let acClock (start : DateTimeOffset) rate () =
        let now = DateTimeOffset.Now
        let ellapsed = now - start
        start.AddTicks(ellapsed.Ticks * rate)

    open System.Collections.Generic
    let qlock (q : DateTimeOffset Queue) = q.Dequeue

    let seqlock (l : DateTimeOffset seq) = Queue<DateTimeOffset> l |> qlock

module InteractiveTest = 
    let rnd = Random()

    let slowEcho x = 
        Async.Sleep(rnd.Next(500,2000)) |> Async.RunSynchronously
        x

    let result = Timed.timeOn Clocks.machineClock slowEcho 42

    let acceleratedResult = Timed.timeOn (Clocks.acClock DateTimeOffset.Now 10L) slowEcho 42
    

