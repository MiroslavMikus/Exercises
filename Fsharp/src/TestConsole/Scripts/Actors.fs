#if !INTERACTIVE
module Actors
#endif

open System
open System.Threading

// slow console example

let slowConsoleWrite msg =
    msg |> String.iter (fun ch->
        System.Threading.Thread.Sleep(3)
        System.Console.Write ch
        )

let makeTask logger taskId = async {
    let name = sprintf "Task%i" taskId
    for i in [1..3] do
        let msg = sprintf "-%s:Loop%i-" name i
        logger msg
    }

type UnserializedLogger() = 
    // public interface
    member this.Log msg = slowConsoleWrite msg

type SerializedLogger() =
    // create the mailbox processor
    let agent = MailboxProcessor.Start(fun inbox ->
        // the message processing function
        let rec messageLoop () = async{
            // read a message
            let! msg = inbox.Receive()
            // write it to the log
            slowConsoleWrite msg
            // loop to top
            return! messageLoop()
            }
        // start the loop
        messageLoop()
        )
    // public interface
    member this.Log msg = agent.Post msg

let unserializedExample =
    let logger = UnserializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

let serializedExample =
    let logger = SerializedLogger()
    [1..5]
        |> List.map (fun i -> makeTask logger.Log i)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

// agent with response example
type EchoMessage = { Name: string; Channel: string AsyncReplyChannel }

let echo = MailboxProcessor.Start(fun inbox -> async {
    while true do 
        let! msg = inbox.Receive()
        msg.Channel.Reply("Hello " + msg.Name)
    })

let result = echo.PostAndAsyncReply(fun ch -> { Name = "World"; Channel = ch }) |> Async.RunSynchronously

