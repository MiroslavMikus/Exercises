#if !INTERACTIVE
module ActivePattern
#endif

// Create your own 'Matcher'
let (|Int|_|) (str:string) = 
    match System.Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

let (|Bool|_|) (str:string) = 
    match System.Boolean.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

let testParse str = 
    match str with 
    | Int nr -> printfn "String is a number: %i" nr
    | Bool b -> printfn "String is a bool: %b" b
    | _ -> printfn "Can't recognize"

testParse "135"
testParse "true"
testParse "654sd"

let (|Digit|Letter|Whitespace|Other|) ch = 
   if System.Char.IsDigit(ch) then Digit
   else if System.Char.IsLetter(ch) then Letter
   else if System.Char.IsWhiteSpace(ch) then Whitespace
   else Other

let printChar ch = 
  match ch with
  | Digit -> printfn "%c is a Digit" ch
  | Letter -> printfn "%c is a Letter" ch
  | Whitespace -> printfn "%c is a Whitespace" ch
  | _ -> printfn "%c is something else" ch

// print a list
['a';'b';'1';' ';'-';'c'] |> List.iter printChar