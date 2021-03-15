#if !INTERACTIVE
module TypeProviders
#endif

// note -> change your path here..
//#r "C:/Users/miros/.nuget/packages/fsharp.data/3.3.3/lib/net45/FSharp.Data.dll"

open FSharp.Data

let wb = WorldBankData.GetDataContext()
printfn "%A" wb.Countries.``Slovak Republic``.Code

type ToDo = { UserId : int ; Title : string ; Completed : bool }

type Todos = JsonProvider<"https://jsonplaceholder.typicode.com/todos">
type FileTodos = JsonProvider<"E:\Projects\_GitHub\Exercises\Exercise.Fsharp\src\data.json">

let castFileTodos() = 
    FileTodos.GetSamples()
    |> Array.map (fun a -> {UserId = a.UserId; Title = a.Title; Completed = a.Completed })

let castApiTodos() = 
    Todos.GetSamples()
    |> Array.map (fun a -> { UserId = a.UserId; Title = a.Title; Completed = a.Completed }) 


let printTodos todos = 
    for todo in  todos
        |> Array.filter (fun a -> a.Completed)
        |> Array.take 2 do 
        printfn "%A" todo

printfn "API"
castApiTodos() |> printTodos
printfn "File"
castFileTodos() |> printTodos



