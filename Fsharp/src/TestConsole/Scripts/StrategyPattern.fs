#if !INTERACTIVE
module StrategyPattern
#endif

type Animal(makeNoiseStrategy) =
    member this.MakeNoise =
        makeNoiseStrategy() |> printfn "Noise: %s" 
    
let meowing () = "Meow"
let cat = Animal(meowing)

let bark () = "Bark"
let dog = Animal(bark)
let fish = Animal(fun () -> "No noise here")

cat.MakeNoise
dog.MakeNoise
fish.MakeNoise