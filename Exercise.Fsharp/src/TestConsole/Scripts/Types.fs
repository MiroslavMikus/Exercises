#if !INTERACTIVE
module Types
#endif

////// Record
type Person = { FirstName : string; LastName : string }

let person = { FirstName = "SomeName"; LastName = "AlsoLastName" }

// creates new person
let person2 = { person with FirstName = "Miro" } 

let person3 = { FirstName = "SomeName"; LastName = "AlsoLastName" }

printfn "Structural equality %b" (person = person3)


////// Discriminated Union
type Shape = 
    | Square of float
    | Rectangle of float * float
    | Circle of float
    member this.Add a b = a + b // shape member

let square = Square 3.4
let rectangle = Rectangle (2.2, 1.9)
let circle = Circle 1.0

printfn "%i" (circle.Add 3 2)

let drawings = [|square; rectangle; circle |]

let PrintDrawing (shape : Shape) = 
    match shape with
    | Square f -> sprintf "We have square %f" f 
    | Rectangle (f, s) -> sprintf "We have Rectangle %f %f" f s 
    | Circle f -> sprintf "We have circle %f" f 

let d = drawings |> Array.map (fun drawing -> PrintDrawing drawing)

////// Class
type Customer(forename : string, surename : string) =
    do
        printfn "Some ctor logic here"

    member this.Forename = forename
    member this.Surename = surename

let someTuple = ("Some", "Customer")
let customer = Customer someTuple

type MutableCustomer(forename : string, surename : string) =
    member val Forename = forename with get, set
    member val Surename = surename with get, set

////// Interface
type ICustomer =
    abstract member Forename : string
    abstract member Surename : string
    abstract member Fullname : string

type CustomerWithInterface(forename : string, surename : string) =
    
    interface ICustomer with
        member __.Forename = forename
        member __.Surename = surename
        member __.Fullname = sprintf "%s %s" forename surename

let customerInterface = CustomerWithInterface("some", "customer")

let fullName = (customerInterface :> ICustomer).Fullname

////// Extension method / interfaces

type IAnimal = 
   abstract member MakeNoise : unit -> string

let showTheNoiseAnAnimalMakes (animal:IAnimal) = 
   animal.MakeNoise() |> printfn "Making noise %s"

type Cat = Felix | Socks
type Dog = Butch | Lassie 

type Cat with
    member this.AsAnimal = 
         { new IAnimal 
           with member a.MakeNoise() = "Meow" }

type Dog with
    member this.AsAnimal = 
         { new IAnimal 
           with member a.MakeNoise() = "Woof" }

let dog = Lassie
showTheNoiseAnAnimalMakes (dog.AsAnimal)

let cat = Felix
showTheNoiseAnAnimalMakes (cat.AsAnimal)

////// Structs
type Point2D =
   struct
      val X: float
      val Y: float
      new(x: float, y: float) = { X = x; Y = y }
   end

//test
let p = Point2D()  // zero initialized
let p2 = Point2D(2.0,3.0)  // explicitly initialized

