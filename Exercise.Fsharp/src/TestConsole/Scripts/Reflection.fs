#if !INTERACTIVE
module Reflection
#endif

open System.Reflection
open Microsoft.FSharp.Reflection

// create a record type...
type Account = {Id: int; Name: string}

// ... and show the fields
let fields = 
    FSharpType.GetRecordFields(typeof<Account>)
    |> Array.map (fun propInfo -> propInfo.Name, propInfo.PropertyType.Name)

// create a union type...
type Choices = | A of int | B of string

// ... and show the choices
let choices = 
    FSharpType.GetUnionCases(typeof<Choices>)
    |> Array.map (fun choiceInfo -> choiceInfo.Name)