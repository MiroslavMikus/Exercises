#if !INTERACTIVE
module Options
#endif

type Company = { Name : string; TaxNumber : int option }

let company = { Name = "Some company"; TaxNumber = Some 123 }

let companyWithNoTaxNr = { Name = "Some company"; TaxNumber = None }

let PrintCompany (company : Company) =
    let taxNr = 
        match company.TaxNumber with
        | Some n -> sprintf "[%i]" n
        | None ->"[No number]"

    printfn"%s %s" company.Name taxNr
