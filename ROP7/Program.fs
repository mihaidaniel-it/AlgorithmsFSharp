open System
open System.IO
open System.Globalization

type Product = {
    Name: string
    Price: double
    Quantity: int
}

type DomainErrors =
    | FileNotExist of path: string
    | UnexpectedError of string
    | OutOfRange 
    | NullReferenceException 

let handleError error =
    match error with
    | FileNotExist path -> printfn "File not exist: %s" path
    | UnexpectedError message -> printfn "Unexpected error: %s" message
    | OutOfRange -> printfn "Index of list is out of the range"
    | NullReferenceException -> printfn "The data is not exist or it's null"

let filePathsList = [
    Path.GetFullPath("ROP6/products.txt");
    Path.GetFullPath("test/products.txt");
    Path.GetFullPath("ROP4/products.txt")
]


    


// Запуск всех функций программы
let runProgramm = 
    Array.toList
    >> parseFile
    >> ignore

// Получение массива с строчками из файла
let rec getFile filePathsList =
    match File.Exists(List.head filePathsList) with
    | false -> getFile (List.tail filePathsList)
    | true -> File.ReadAllLines(List.head filePathsList) |> runProgramm
getFile filePathsList