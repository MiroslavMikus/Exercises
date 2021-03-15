#if !INTERACTIVE
module AsyncWorkflow
#endif
open System

let fileWriteWithAsync = 
    use stream = new System.IO.FileStream("test.txt", System.IO.FileMode.Create)

    printfn "Start"

    let asyncResult = stream.BeginWrite(Array.empty, 0, 0, null, null)

    let async = Async.AwaitIAsyncResult(asyncResult)

    printfn "Some parallel work here"

    Async.RunSynchronously async |> ignore

    printfn "Done"

let simpleSleepWorkflow = async {
    printfn "Start workflow at %O" DateTime.Now.TimeOfDay
    do! Async.Sleep 2000
    printfn "End workflow at %O" DateTime.Now.TimeOfDay
    }

let nestedWorkflow = async {
    printfn "Start parent workflow"
    let! childWorkflow = Async.StartChild simpleSleepWorkflow

    do! Async.Sleep 100
    printfn "Do something in parallel"

    let! result = childWorkflow

    printfn "Done"
    }

let runNestedWorkflow = 
    use tokenSource = new System.Threading.CancellationTokenSource()
    Async.Start(nestedWorkflow, tokenSource.Token)

let sleepWorkflow ms = async {
    printfn "%i ms workflow started" ms
    do! Async.Sleep ms
    printfn "%i ms workflow finished" ms
    }

let workflowInSeries = async{
    let! sleep1 = sleepWorkflow 100
    let! sleep2 = sleepWorkflow 200
    printfn "Done"
    }

let workflowInParallel = 
    let sleep1 = sleepWorkflow 100
    let sleep2 = sleepWorkflow 200

    [sleep1; sleep2]
        |> Async.Parallel
        |> Async.RunSynchronously

// child task example
let childExample() = async {
    let startTime =  DateTime.Now.TimeOfDay

    let! sleepWork1 = sleepWorkflow 2000 |> Async.StartChild
    let! sleepWork2 = sleepWorkflow 5000 |> Async.StartChild
    let! sleepWork3 = sleepWorkflow 5000 |> Async.StartChild
    let! sleepWork4 = sleepWorkflow 5000 |> Async.StartChild

    do! sleepWork1
    do! sleepWork2
    do! sleepWork3
    do! sleepWork4
    
    printfn "Done"

    printfn "End workflow in %O" (DateTime.Now.TimeOfDay - startTime)
    }

childExample() |> Async.RunSynchronously