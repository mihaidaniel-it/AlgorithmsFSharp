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
    | NullReferenceException
    | InvalidDataType

let handleError error =
    match error with
    | FileNotExist path -> printfn "File not exist: %s" path
    | EmptyFile -> printfn "File has no values"
    | UnexpectedError message -> printfn "Unexpected error: %s" message
    | NullReferenceException -> printfn "Null Reference Exception: File must not contain any empty entries"
    | InvalidDataType -> printfn "Invali dData Type: The type of data is incorrect"

let tryCatch f x =
    try
        f x
    with 
        ex -> Error (UnexpectedError ex.Message)

// Путь до файла товаров
let pathFile = Path.GetFullPath("ROP4/products.txt")


// Получение массива строк из файла
let getFileArray (pathFile: string) =
    match File.Exists(pathFile) with
    | false -> Error (FileNotExist pathFile)
    | true -> Ok (File.ReadAllLines pathFile)

// Преоброзование массива в строки
// Создание объектов на основе Record
let ifFileNotEmpty (fileLines: string list) =
    match fileLines with
    | [] -> Error EmptyFile
    | _ -> Ok fileLines


// Проверка на null и разделение значений
let splitLines (fileLines: string list) =
    fileLines
    |> List.map (fun line ->
        match line with
        | null -> Error InvalidDataType
        | "" -> Error InvalidDataType
        | _ -> Ok (line.Split(';'))
    )

let isValuesCountValid (values: string array) =
    match values.Length with
    | 3 -> Ok values
    | _ -> Error InvalidDataType

let parseValues (values: string array) =
    match values.[0],
          Double.TryParse(values.[1], NumberStyles.Float, CultureInfo.InvariantCulture),
          Int32.TryParse(values.[2])
    with
    | null, _, _ -> Error InvalidDataType
    | "", _, _ -> Error InvalidDataType
    | _, (false, _), _ -> Error InvalidDataType
    | _, _, (false, _) -> Error InvalidDataType
    | name, (true, price), (true, quantity) ->
        Ok {
            Name = name
            Price = price
            Quantity = quantity
        }

// Обработка каждой строки и создание Product
let processLines lines =
    lines
    |> List.map (fun line ->
        line
        |> Result.bind isValuesCountValid
        |> Result.bind parseValues
    )

// Преобразование результата в один список
let collectProducts products =
    products
    |> List.fold (fun state current ->
        match state, current with
        | Ok products, Ok product -> Ok (product :: products)
        | Error error, _ -> Error error
        | _, Error error -> Error error
    ) (Ok [])

let parseFile =
    Array.toList 
    >> ifFileNotEmpty
    >> Result.map splitLines
    >> Result.map processLines
    >> Result.bind collectProducts

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