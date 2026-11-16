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
let reverseList = List.fold (fun acc x -> x :: acc) [] numbers
let reverseSum = List.fold (fun acc x -> acc + x) 0 reverseList
printfn "Reverse sum: %i" reverseSum