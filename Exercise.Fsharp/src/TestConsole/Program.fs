open System

let printName() =
    printf "Enter your name: "
    let name = Console.ReadLine()
    printfn "Your name is %s" name

let printPi() = 
    printfn "PI : %f" Math.PI

let bindTest() =
    //mutable
    let mutable weight = 175
    weight <- 170
    printfn "Weight : %i" weight

    // ref
    let someRef = ref 10
    someRef := 50

    printfn "Some ref : %i" ! someRef

let someFunc()=
    let getSum (x:int, y:int) : int = x+y
    printfn "5 + 7 = %i" (getSum(5 ,7))

// Recrusion
let rec factorial x =
    if x < 1 then 1
    else x * factorial (x - 1)

let printFactorial x =
    printf "Factorial %i = %i" x (factorial(x))

let testList()=
    let randList = [1;2;3]
    let randList2 = List.map(fun x -> x * 2) randList
    printfn "Double list : %A" randList2

    // pipe operator
    [5;6;7;8]
    |> List.filter(fun v ->  v % 2 = 0)
    |> List.map(fun v -> v * 2)
    |> printfn "Even Doubles : %A"

    let multipleNumber x = x * 3
    let addNumber x = x + 5

    let mult_add = multipleNumber >> addNumber
    let add_mult = multipleNumber << addNumber

    printfn "mult_add : %i" (mult_add 10)
    printfn "add_mult : %i" (add_mult 10)

let getType()=
    let number = 2
    printfn "Type : %A" (number.GetType())

let string_test() = 
    let str1 = "Some random string"
    let str2 = @"Ignore backslashes"
    let str3 = """ " ignore double qoutest" """
    let str4 = str1 + " " + str2

    printfn "Length : %i" (String.length str4)
    printfn "1st word: : %s" (str1.[0..3])

    let upper_string = String.collect(fun c -> sprintf "%c, " c) "commas"
    printfn "Commas : %s" upper_string

    printfn "Any upper: %b" (String.exists(fun c -> Char.IsUpper(c)) str1)
    printfn "Number : %b" (String.forall(fun c -> Char.IsNumber(c)) "12345")

    let numbers = String.init 10 (fun i -> i.ToString())

    printfn "Numbers : %s" numbers

let loop_test_while() = 
    let num = "7"
    let mutable guess = ""

    while not (num.Equals(guess)) do 
        printf "Guess the number: "
        guess <- Console.ReadLine()

    printfn "You Guessed the Number"

let loop_test_for() = 
    for i = 1 to 10 do  
        printfn "%i" i

    for i = 10 downto 1 do
        printfn "%i" i

    for i in [2..12] do
        printfn "%i" i

    [1..10] |> List.iter(printfn "Num : %i")
    
    let sum = List.reduce (+) [1..10]

    printfn "Sum : %i" sum

let condition_test() =   
    let age = 8

    if age < 5 then
        printfn "preschool"
    elif age = 5 then
        printfn "kindergarten"
    elif (age > 5) && (age <= 18) then
        printfn "Go to grade %i" (age - 5)
    else 
        printfn "Go to College"

    let grade2: string = 
        match age  with
        | age when age < 5-> "Preschool"
        | 5 -> "Kindergarten"
        | age when ((age > 5) && (age <= 18)) -> (age - 5).ToString()
        | _ -> "Go to College"

    printfn "Grade2 :%s" grade2

let list_test() = 
    let list1 = [1;2;3;4]

    list1 |> List.iter(printfn "Num :  %i")

    printfn "%A" list1

    let list2 = 5::6::7::[]
    printfn "%A" list2

    let list3 = [1..5]
    printfn "%A" list3

    let list4 = ['a'..'g']
    printfn "%A" list4

    let list5 = List.init 5 (fun x -> x * 2)
    printfn "%A" list5

    let list6 = [ for a in 1..5 do yield (a * a)]
    printfn "%A" list6
    let list7 = [ for a in 1..20 do if a % 2 = 0 then yield a]
    printfn "%A" list7
    let list8 = [ for a in 1..3 do yield! [a .. a + 2]]
    printfn "%A" list8

type someEnum = 
| sap = 0
| navision = 1
| microsoft = 2

let enum_test() = 
    let enum = someEnum.microsoft

    match enum with
    | sap -> printfn "SAP"
    | navision -> printfn "Navision"
    | microsoft -> printfn "Microsoft"

let options_test() =
    let divide x y =
        match y with
        | 0 -> None
        | _ -> Some(x/y)

    let result = divide 5 0
    if result.IsSome then
        printfn "Result is : %i" result.Value
    else
        printfn "There is no result"

let tuple_test () =
    let avg (w, x, y, z) = 
        (w + x + y + z) / 4.0

    printfn "Avg : %f" (avg (1.0, 2.0, 3.0, 4.0))

    let data =  ("Miro", 30, 7.89)

    let (name, age, _) = data

    printf "Name : %s" name

type customer = { Name : string; Balance : float }

let record_test() = 
    let bob  = { Name = "Bob Smith"; Balance = 101.50 }
    printfn "%s owes us %.2f" bob.Name bob.Balance

let seq_test() =
    let seq1 = seq { 1..100 }
    let seq2 = seq { 00.. 2 .. 50 }
    let seq3 = seq { 50 .. 1 }

    printfn "%A" seq1

    Seq.toList seq2 |> List.iter(printfn "Num: %i")

    let is_prime n =
        let rec check i =
            i > n/2 || (n % i <> 0  && check (i + 1))
        check 2

    let prime_sequence = seq {for n in 1..500 do if is_prime n then yield n }

    printfn "%A" prime_sequence

    Seq.toList prime_sequence |> List.iter(printfn "Prime: %i")

let map_test() = 
    let bob = "Bob Smith"
    let customers = 
        Map.empty.
            Add(bob, 100.50).
            Add("Sally Marks", 50.25)

    printfn "# of Customers %i" customers.Count

    let cust = customers.TryFind bob

    match cust with 
    | Some x -> printfn "Balance : %.2f" x
    | None -> printfn "Not Found"

    printfn "Customers : %A" customers

    if customers.ContainsKey bob then
        printfn "Found"

    printfn "Bobs balance : %.2f" customers.[bob]

    let cust2 = Map.remove bob customers

    printfn "# of Customers %i" cust2.Count

let add_stuff<'T> x y =
    printfn "%A" (x + y)

let generics_test() = 
    add_stuff<int> 5 2
    //add_stuff<float> 5.5 6.6
    
let exp_test() =
    let divide x y =
        try 
            printfn "%.2f / %.2f = %.2f" x y (x / y)
        with
            | :? System.DivideByZeroException -> printfn "Can't divide by zero"
    divide 5.0 4.0
    divide 5.0 0.0

type Rectangle = struct
    val Length : float
    val Width : float

    new (length, width)=
    { Length = length; Width = width}
end

let struct_test() =
    let area(shape: Rectangle) = 
        shape.Length * shape.Width
        
    let rect = new Rectangle(5.0, 6.0)

    let rect_area = area rect 

    printfn "Area: %.2f" rect_area

type Animal = class
    val Name : string
    val Height : float
    val Weight : float

    new (name, height, weight) = 
        { Name = name; Height = height; Weight = weight }

    member x.Run = 
        printfn "%s Runs" x.Name
end

type Dog(name, height, weight) = 
    inherit Animal (name, height, weight)

    member x.Bark = 
        printfn "%s Barks" x.Name

let class_test() = 
    let spot = new Animal("Spot", 20.5, 40.5)
    spot.Run
    let bowser = new Dog("Bowser", 20.5, 40.5)
    bowser.Run
    bowser.Bark


let fuc_argument() = 

    let apply (f: int -> int -> int) x y = f x y

    let mul x y = x * y

    apply mul 10 20

let composition_test() =
    let add x y = x + y
    let times x y = x * y

    let result = 100 |> add 3 |> times 12
    printfn "The composition result is %i" result

    let addAndMultiply = add 3 >> times 15
    let result = addAndMultiply 100
    printfn "The composition result is %i" result


[<EntryPoint>]
let main argv =
    //printName()
    //printPi()
    //bindTest()
    //printFactorial(5)
    //someFunc()
    //testList()
    //getType()
    //string_test()

    //loop_test_for()
    //condition_test()
    //list_test()
    //options_test()
    //tuple_test()
    //record_test()
    //seq_test()
    //map_test()
    //composition_test()

    0 // return an integer exit code

