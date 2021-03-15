#if !INTERACTIVE
module Sequences
#endif

open System.IO

let GetWindowsBigFiles() = 
    Directory.EnumerateFiles(@"c:\windows")
    |> Seq.map (fun name -> FileInfo name)
    |> Seq.filter (fun file -> file.Length > 1000000L)
    |> Seq.map (fun file -> file.Name)

let PrintFiles() =
    for file in GetWindowsBigFiles() do 
        printfn "Our file is %s" file
