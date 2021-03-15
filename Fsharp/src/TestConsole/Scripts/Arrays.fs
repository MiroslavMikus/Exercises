#if !INTERACTIVE
module Arrays
#endif

// array
let arr = [|1;2;3|]
// list
let list = [ 5..10 ]


let numbers = [| for i in 0..99 do yield i * i|]

let squares =[| for i in 0..99 do yield i * i |]

let IsEven n = n % 2 = 0 

// c# where
let evenSquares = Array.filter IsEven squares


// c# select
let printSquares min max =
    let square n = n * n
    [|min..max|]
    |> Array.map square

let Sum min max = 
    [|min..max|]
    |> Array.map (fun a -> a + 1)
    |> Array.reduce ( fun a b-> a + b)

