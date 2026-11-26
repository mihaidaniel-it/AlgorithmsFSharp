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

let filePathsList = [
    Path.GetFullPath("ROP6/products.txt");
    Path.GetFullPath("test/products.txt");
    Path.GetFullPath("ROP4/products.txt")
]


let collectProducts (lines: string list) : Product list =
    List.map (fun (line: string) ->
        let values: string array = line.Split(';')

        {
            Name = values.[0]
            Price = Double.Parse(values.[1], CultureInfo.InvariantCulture)
            Quantity = Int32.Parse(values.[2])
        }
    ) lines
let rec printProducts (values: Product list) =
    match values with
    | [] -> ()
    | head :: tail ->
        printfn "Name: %s | Price: %f | Quantity: %d" head.Name head.Price head.Quantity
        printProducts tail

let runProgramm =
    Array.toList
    >> collectProducts
    >> Ok
    >> Result.map printProducts
    >> Result.mapError handleError
    >> ignore

let rec getFile filePathsList =
    match File.Exists(List.head filePathsList) with
    | false -> getFile (List.tail filePathsList)
    | true ->
        File.ReadAllLines(List.head filePathsList)
        |> runProgramm
getFile filePathsList