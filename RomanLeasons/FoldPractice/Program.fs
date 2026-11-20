module Programm

open PersonUtility

let numbers = [ 1; 2; 6; 4; 5 ] 

let sum = List.fold (fun acc x -> acc + x) 0 numbers
printfn "Result of sum: %i" sum  

let multiply = List.fold (fun acc x -> acc + (x * 2)) 0 numbers
printfn "Result of multiply: %i" multiply 

let biggestNumber =
    List.fold (fun acc x ->
        match acc < x with
        | true -> x
        | false -> acc
    ) numbers.Head numbers

printfn "The biggest number in list is: %i" biggestNumber


// Reverse sum
let reverseSum numbers = 
    numbers
    |> List.fold (fun acc x -> x :: acc) [] 
    |> List.fold (fun acc x -> acc + x) 0 
printfn "Reverse sum: %i" (reverseSum numbers)

// Reverse sum without fold
let rec sumWithReverseList numbers =
    let rec reverseList nums =
        match nums with
        | [] -> []
        | head :: tail -> reverseList tail @ [head]

    let rec sumList nums =
        match nums with
        | [] -> 0
        | head :: tail -> head + sumList tail

    let reversed = reverseList numbers
    sumList reversed 

// Reverse list
let reverseList numbers = 
    let reverseList = List.fold (fun acc x -> x :: acc) [] numbers
    reverseList
printfn "reverseList: %A" (reverseList numbers)



let printHello = printf "Hello World"
printHello








// Functions for type from module
let person = {
    name = "Daniel"
    age = 22
    country = "Moldova"
    isStudy = true
    isWork = true
}

person.personInfo
person.personCountry
