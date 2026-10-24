// Пример 1
let rec counter x =
    if x <= 0 then
        printfn "Number: 0"
    else 
        printfn "Number: %i" x
        counter (x - 1)

counter 3

// Пример 2
let rec sum x = 
    if x <= 0 then
        0
    else 
        x + sum (x - 1)

printfn "Result: %i" (sum 5)

// Пример 3
let rec sumList list = 
    match list with
    | [] -> 0 // Базовый случай
    | head :: tail -> head + sumList tail

printfn "Sum of list: %i" (sumList [1; 2; 3])


// Tail recursion
let sumTail n = 
    let rec loop n acc = 
        if n <= 0 then  
            acc 
        else 
            loop (n - 1) (acc + n)
    loop n 0 // n - значение полученое из SumTail, 0 - это значение для acc (врем. хранилище)

printfn "Tail recursion result: %i" (sumTail 5)

let factorial n =
    let rec loop n acc = 
        if n <= 0 then
            acc 
        else 
            loop (n - 1) (acc * n)
    loop n 1

printfn "Result factorial: %i" (factorial 5)

let listLength numbers =
    let rec loop numbers acc = 
        match numbers with
        | [] -> acc
        | head :: tail -> loop tail (acc + 1)
    loop numbers 0
printfn "List length: %i" (listLength [1; 4;])


