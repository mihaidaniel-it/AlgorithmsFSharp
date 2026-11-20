module PersonUtility

    type Person = {
        name: string
        age: int
        country: string
        isStudy: bool
        isWork: bool
    } with 
        member this.personInfo = 
            printfn "Name: %s, age: %i" this.name this.age

    type Person with 
        member this.personCountry =
            printfn "%s's country is %s" this.name this.country
