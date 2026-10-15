// Объявление НЕ изменяемой переменной
let number = 10;
let username = "Daniel Mihai";

// Объявление изменяемой переменной
let mutable studentAge = 21;
let mutable studentName = "Jhon Deer";


// Списки
let numbersOne = [3; 4; 5]
let numbersTwo = [6; 2]
let oneToNumber = 1 :: numbersOne
let finalNumber = numbersOne @ numbersTwo


// Функции
let square x = x * x
let squareResult = square 5

let Multiply x y =
    let multiplyResult = x * y
    printfn "Multiply result is: %d" multiplyResult
Multiply 5 10

let isPositiveNumber x = 
    let isPositiveNumber = x > 0
    printfn "Is %d positive? %b" x isPositiveNumber
isPositiveNumber 5

let addNumberToFive x =
    let result = x + 5
    printfn "Result: %d " result
addNumberToFive 23423

// Вложенная функция
let numbers = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
let even numbers =
    let isEven x = x % 2 = 0
    List.filter isEven numbers

let evenNumbers = even numbers
printfn "Even numbers: %A" evenNumbers


// Функция с аргументом результата другой функции
let sumOfSquares = 
    List.sum (List.map (fun x -> x * x) numbers)
printfn "Sum of squares: %d" sumOfSquares

let sumOfCubs= 
    List.sum (List.map (fun x -> x * x * x) numbers)
printfn "Sum of cubs: %d" sumOfCubs

// Функция с конвейером
let sumOfSquarePipe = 
    [1; 2; 3; 4; 5; 6; 7; 8; 9; 10] // Объявили список (либо можно указать переменную)
    |> List.map (fun x -> x * x) // Первое действие 
    |> List.sum // Второе действие, которое приняло в качестве аргумента результат первого действия
printfn "Sum of squares (pipe): %d" sumOfSquarePipe 

let simpleSumOfNumbers = 
    [1; 2]
    |> List.sum
printfn "Simple sum of numbers: %d" simpleSumOfNumbers


// match ... with (Сопоставление с образцом)
let checkNumber =
    let number = 23
    match number with 
    | 24 -> printfn "It's not my birthday"
    | 23 -> printfn "It's my birthday"
    | _ -> printfn "The number is something else"


let ifExist number =
    match number with 
    | Some i -> printfn "The number is: %d" i
    | None -> printfn "The number is not exist"
ifExist (Some 5) 

let isEven number = 
    let result  = number % 2 = 0
    match result with 
    | true -> printfn "The number is even"
    | false -> printfn "The number is odd"
isEven 4


// Кортеж
let tuple = ("Daniel", 21, true)
let name, _, isStudent = tuple
let _, age, _ = tuple
printfn "Name: %s, Age: %d, Is student: %b" name age isStudent


// Записи
// Основной Record
type student = { 
    StudentName: string;
    StudentYear: int;
    StudentGrade: double;
} 

// Запись
let studentOne = {
    StudentName = "Daniel";
    StudentYear = 3;
    StudentGrade = 8.5;
}

let nameStudent = studentOne.StudentName
printfn "Student name: %s" nameStudent

// Descriminated union
type Temperature = 
    | TempC of float
    | TempF of float
    | TempK of float

let temp = TempC 36.6

let whichTemperature Temperature = 
    match Temperature with
    | TempC x -> printfn "Temperature in Celsius: %f" x
    | TempF x -> printfn "Temperature in Fahrenheit: %f" x
    | TempK x -> printfn "Temperature in Kelvin: %f" x 
whichTemperature temp

type Grade = 
    | NotDouble of int
    | Double of double

let doubleGrade = Double 5.4
printfn "Double grade: %A" doubleGrade

// Объединение записей (типов)
type Person = {
    name: string;
    position: string;
}

type Employee = 
    | Worker of Person
    | Manager of Employee list

let person1 = {name="Daniel"; position="Developer"}
let person2 = {name="Alex"; position="Manager"}

let danielDeveloper = Worker person1
let alexManager = Worker person2
let manager = Manager [danielDeveloper; alexManager]

let definitionOfEmployee Employee = 
    match Employee with
    | Worker x -> printfn "Worker: %s, Position: %s" x.name x.position
    | Manager x -> printfn "Manager with %d employees" (List.length x)
definitionOfEmployee danielDeveloper
definitionOfEmployee alexManager
definitionOfEmployee manager


// Вывод данных
let outputName = "Daniel"
let outputAge = 21
printfn "Name is %s and age is %d" outputName outputAge


// Массивы
let nums = [| 1; 2; 3 |]
let firstArrayIndex = nums[0]
nums[1] <- 42