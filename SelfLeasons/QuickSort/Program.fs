// igonre - function 
let f1 i j =
    let sum = i + j
    ignore sum


// Split tuple by indexes
let parsName (name: string) =
    let parts =       name.Split(' ')
    let firstName = parts.[0]
    let lastName = parts.[1]
    firstName, lastName

printfn "%A" (parsName "Daniel Mihai") 
let firstName, lastName = parsName "Daniel Mihai"
printfn "First name: %s, Last name: %s" firstName lastName


// Nested tuples
let nestedTuple = ("Daniel", "Mihai"), (21, 3)
let name, personNumbers = nestedTuple
let firstName2, lastName2 = name
let age, grade = personNumbers
printfn "Name: %s %s, Age: %d, Grade: %d" firstName2 lastName2 age grade

// Records
type Address = {
    street: string
    city: string
}

type Person = {
    name: string
    age: int
    address: Address
}

let customer = {
    name = "Daniel Mihai"
    age = 21
    address = {
        street = "Strada Mihai Eminescu"
        city = "Bucharest"
    }
}

// Referential and Structural equality
type Customer = {
    name: string
    orderSum: int
}

let customer1 = { 
    name = "Daniel"
    orderSum = 100 
}
let customer2 = { 
    name = "Daniel" 
    orderSum = 100 
}

// Referential equality, equality by the VALUE of the object
printfn "Referential equality: %b" (obj.ReferenceEquals(customer1, customer2))
// Structural equality, equality by the DATA TYPE of the fields
printfn "Structural equality: %b" (customer1 = customer2)

// Update the object 
let updateCustomer1 = {
    customer1 with 
        orderSum = 200
}


// Pipe 
let sum a b = a + b

let result = 5 |> sum 10




// Collections
type Players = {
    Team: string
    AwayGoals: int
    HomeGoals: int
}

// Constror for type
let constructorPlayers (team, hg, ag) = 
    {
        Team = team
        HomeGoals = hg
        AwayGoals = ag
    }

let team = 
    [
       constructorPlayers ("Zarya", 2, 1)
       constructorPlayers ("Lokomativ", 1, 3)
    
    ]

printfn "First player: %A" team.[0]
printfn "Second player: %A" team.[1]

printfn "----------------------"

// Second Leason
type Nums<'a> = 
    | Some of int
    | None
let sumNums (x: int Nums) (y: int Nums) =
    match x, y with
    | Some x1, Some x2 -> x1 + x2
    | None, Some x1 -> 0
    | Some x1, None -> 0
    | None, None -> 0
// printfn "sumNums %A" sumNums (Some 5, Some 10)

type Result<'a, 'b> = 
    | Ok of 'a 
    | Error of 'b 

let resultOfType = Ok 200, Error 500
printf "%A" resultOfType
