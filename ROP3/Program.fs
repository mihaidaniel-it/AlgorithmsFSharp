open System
open System.IO
open System.Globalization

type Product = {
    Name: string
    Price: double
    Quantity: int
}

// Обработка ошибок 
type DomainErrors = 
    | FileNotExist of path: string
    | EmptyFile
    | UnexpectedError of message: string

let handleError error =
    match error with
    | FileNotExist path -> printfn "File not exist: %s" path
    | EmptyFile -> printfn "File has no values"
    | UnexpectedError message -> printfn "Unexpected error: %s" message

let tryCatch f x =
    try
        f x
    with 
        ex -> Error (UnexpectedError ex.Message)

// Путь до файла товаров
let pathFile = Path.GetFullPath("ROP3/products.txt")


// Получение массива строк из файла
let getFileArray (pathFile: string) =
    match File.Exists(pathFile) with
    | false -> Error (FileNotExist pathFile)
    | true -> Ok (File.ReadAllLines pathFile)

// Преобразование массива в список товаров 
// Создание объектов на основе Record
let parseFile (fileLines: string array) =
    let fileLinesList = Array.toList fileLines

    match fileLinesList with 
    | [] -> Error EmptyFile
    | _ ->
        fileLinesList
        |> List.map (fun line ->
            let values = line.Split(';')

            {
                Name = values.[0]
                Price = Double.Parse(values.[1], CultureInfo.InvariantCulture)
                Quantity = Int32.Parse(values.[2])
            }
        )
        |> Ok

// Форматированный вывод товаров
let rec printProducts (values: Product list) =
    match values with
    | [] -> ()
    | head :: tail ->
        printfn "Name: %s | Price: %f | Quantity: %d" head.Name head.Price head.Quantity
        printProducts tail

let runProgramm (pathFile: string) = 
    pathFile
    |> tryCatch getFileArray
    |> Result.bind (tryCatch parseFile)
    |> Result.mapError handleError
    |> Result.map printProducts
    |> ignore
 
runProgramm pathFile


(*
Понимание Result.bind:
Result.bind нужен для того, чтобы получить резульат Ok или Error. Если Ok - продолжить дальше шаги, 
если Error - остановиться. 
В runProgramm, в getFileArray мы получаем значение Ok c массивом (он завернутый), 
далее значение в Ok передаем в следющию функцию и если прошлая функция вернула Ok, 
то следующая функция возьмет результат Ok, обработает и вернет завернутый результат Ok/Error.
*)