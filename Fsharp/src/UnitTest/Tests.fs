module Tests

    open UnitTest
    open Xunit

    type TestClass() =
        
        [<Fact>]
        member this.``Adding numbers should work``() =
            let result = SomeMath.sum 12 5
            Assert.Equal(result, 17)
            
        [<Fact>]
        member this.``divide with 0 should return none``() =
            let result = SomeMath.divide 5 0
            Assert.Equal(result, None)
            
        [<Fact>]
        member this.``divide something should work``() =
            let result = SomeMath.divide 5 5
            Assert.Equal(result, Some 1)
            