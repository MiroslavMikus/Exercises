namespace UnitTest

open API

module SomeMath =
    let sum a b = a + b

    let divide a b =
        match b with
        | 0 -> None
        | _ -> Some(a / b)

module Say =
    let hello name =
        printfn "Hello %s" name
        printfn "some sum is %O" (SomeMath.sum 1 23)

    type Connection() =
        interface IConnection with
            member _.Message() = "some message"
            member _.Sum(a: int, b: int) = SomeMath.sum a b
