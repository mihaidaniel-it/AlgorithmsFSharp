let studentGrade = 9.7

if studentGrade > 7.0 then 
    printfn "Student can be admited with scholarship"
else if studentGrade > 5.0 then
    printfn "Student can be admited"
else
    printfn "Student cannot be admited"

let studentYear = 3
let studentYearString = 
    if studentYear = 1 then 
        "First year"
    elif studentYear = 2 then 
        "Second year"
    elif studentYear = 3 then 
        "Third year"
    else 
        "Unknown year"
printfn "Student year: %s" studentYearString



// Циклы
for i in 0 .. 2 .. 4 do
    printfn "Number: %i" i

let nums = [| 1; 2; 3; 4; 5 |]
for i in 0 .. 1 .. nums.Length -1 do
    printfn "Number: %i" nums[i]


let mutable number = 5
while number > 0 do
    printfn "Number (while): %i" number
    number <- number - 1