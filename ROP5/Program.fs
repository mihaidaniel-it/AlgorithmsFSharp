
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

let handleError error =
    match error with
    | FileNotExist path ->
        printfn "File not exist: %s" path
    | UnexpectedError message ->
        printfn "Unexpected error: %s" message

let tryCatch f x =
    try
        Ok (f x)
    with
        ex -> Error (UnexpectedError ex.Message)

let pathFile = Path.GetFullPath("ROP5/products.txt")



let collectProduct (line: string) =
    let values = line.Split(';')

    {
        Name = values.[0]
        Price = Double.Parse(values.[1], CultureInfo.InvariantCulture)
        Quantity = Int32.Parse(values.[2])
    }

let collectProducts =
    Array.toList >> List.map collectProduct

let rec printProducts (values: Product list) =
    match values with
    | [] -> ()
    | head :: tail ->
        printfn "Name: %s | Price: %f | Quantity: %d" head.Name head.Price head.Quantity
        printProducts tail

let runProgramm =
    pathFile
    |> tryCatch File.ReadAllLines
    |> Result.bind (tryCatch collectProducts)
    |> Result.map printProducts
    |> Result.mapError handleError
    |> ignore
runProgramm