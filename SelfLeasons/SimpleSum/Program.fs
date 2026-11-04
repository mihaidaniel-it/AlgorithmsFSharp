let sumOfNumbers n = 
    [1 .. n]
    |> List.sum
printfn "Sum of numbers: %i" (sumOfNumbers 5)

let square x = x * x
let sumOfSquares n =
    [1 .. n]
    |> List.map square
    |> List.sum 

printfn "Sum of squares: %i" (sumOfSquares 5)



let numbers = [2; 4; 6; 8; 3; 10; 12; 5; 14]

let sumOfNumbersMoreThenFive numbers =
    numbers
    |> List.filter (fun x -> x > 5)
    |> List.map square
    |> List.sum
printfn "Sum of squares of numbers more than five: %i" (sumOfNumbersMoreThenFive numbers)