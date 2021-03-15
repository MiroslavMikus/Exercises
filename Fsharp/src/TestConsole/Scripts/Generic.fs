#if !INTERACTIVE
module Generics
#endif

let add1 input = input + 1
let times2 input = input * 2

let genericLogger anyFunc input = 
    printfn "input is %A" input   
    let result = anyFunc input    
    printfn "result is %A" result 
    result

let genericTimer anyFunc input = 
    let stopwatch = System.Diagnostics.Stopwatch()
    stopwatch.Start()
    let result = anyFunc input
    stopwatch.Stop()
    printfn "elapsed ms : %A"  stopwatch.ElapsedMilliseconds
    result

let add1WithLogging = genericLogger add1 
let times2WithLogging = genericLogger times2

add1WithLogging 3 |> ignore
times2WithLogging 4 |> ignore

let add3MeasureAndLog = (genericTimer >> genericLogger) add1

add3MeasureAndLog 33 |> ignore
