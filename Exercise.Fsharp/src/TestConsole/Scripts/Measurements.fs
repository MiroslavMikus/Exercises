#if !INTERACTIVE
module Measurements
#endif
    
[<Measure>]
type cm

[<Measure>]
type inches

[<Measure>]
type feet =
    static member toInches(feet: float<feet>) : float<inches> = 
        feet * 12.0<inches/feet>

let meter = 100.0<cm>
let yard = 3.0<feet>

let yardInInches = feet.toInches(yard)

let moreYards = yard + 1.0<_> // wildcard
