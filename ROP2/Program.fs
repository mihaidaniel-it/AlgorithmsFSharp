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
    with ex -> Error (UnexpectedError ex.Message)

let pathFile = Path.GetFullPath("ROP2/products.txt")


let getFile (pathFile: string) =
    match File.Exists(pathFile) with
    | false -> Error (FileNotExist pathFile)
    | true -> Ok (File.ReadAllLines pathFile)

// Преобразование массива в список товаров
let parseFile (fileLines: string array) =
// Cразу array в list и далее делать фукнции
    match fileLines with 
    | [||] -> Error EmptyFile
    | _ ->
        fileLines
        |> Array.map (fun line ->
            let values = line.Split(';')

            {
                Name = values.[0]
                Price = Double.Parse(values.[1], CultureInfo.InvariantCulture)
                Quantity = Int32.Parse(values.[2])
            }
        )
        |> Array.toList
        |> Ok

// Добавить в rec printProducts, убрать вложенную функцию и сделать единну
let printProducts (productsList: Product list) = 
    let rec printList (values: Product list) =
        match values with
        | [] -> ()
        | head :: tail ->
            printfn "Name: %s | Price: %f | Quantity: %d"
                head.Name head.Price head.Quantity
            printList tail

    printList productsList

// let handleResult result =
//     match result with
//     | Ok () -> ()
//     | Error error -> handleError error

// mapError
let runProgramm (pathFile: string) = 
    Ok pathFile
    |> Result.bind (tryCatch getFile)
    |> Result.bind (tryCatch parseFile)
    |> Result.map printProducts
    |> ignore
 
runProgramm pathFile
// Убрать Ok и bind станет не нужным