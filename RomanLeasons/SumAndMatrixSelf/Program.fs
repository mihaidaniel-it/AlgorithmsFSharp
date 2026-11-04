// Sum of list 
let numbersList = [1; 2; 3; 4; 5]

let rec sumList i numbersList = 
    match numbersList with
        | [] -> 0
        | head :: tail -> 
            printfn "Current head [%i]: %A" i head
            head + sumList (i + 1) tail
printfn "Sum of list: %A" (sumList 0 numbersList)

printfn "----------------------------"

// Tuples
let tuplesWithLists = 
    [
        1, 2, 3
        4, 5, 6
        7, 8, 9
    ]

// Basic operations with tuple
let tupleSum (x, y, z) = 
    x + y + z

let tupleMultiplication (x, y, z) = 
    x * y * z

let x, y, z = List.item 1 tuplesWithLists 
printfn "Numbers in the list in tuple: %i, %i, %i" x y z
printfn "Sum of list in tuple: %i" (tupleSum (List.item 1 tuplesWithLists))
printfn "Multiplication of list in tuple: %i" (tupleMultiplication (List.item 1 tuplesWithLists))

printfn "----------------------------"

(*
    Sum of all lists in tuple:
       - In this case we get a RESULT of all lists in tuple which we conveyed at the second argument
       - But, the result can be deifrent depending on the argument which we conveyed at the first argument
       - If we convey a function which return a sum of the list - we get a sum, if multiplication - we get a multiplication, etc
*)
let rec tupleGeneric (transform: int * int * int -> int) (tuplesWithLists: list<int * int * int>) = 
    match tuplesWithLists with
    | [] -> 0
    | head :: tail -> transform head + tupleGeneric transform tail

// 'a - means that we can use any type of data for bring this function
let rec tupleGeneric2 (transform: 'a -> int) (tuplesWithLists: list<'a>) = 
    match tuplesWithLists with
    | [] -> 0
    | head :: tail -> transform head + tupleGeneric2 transform tail


// Wrapper for generic functions
let tupleGenericSum (tuple: list<int * int * int>) =
    tupleGeneric tupleSum tuple

let tupleGenericMultiplication (tuple: list<'a>) =
    tupleGeneric2 tupleMultiplication tuple


printfn "Sum of all lists in tuple: %i" (tupleGenericSum tuplesWithLists)
printfn "Multiplication of all lists in tuple: %i" (tupleGenericMultiplication tuplesWithLists)

printfn "----------------------------"