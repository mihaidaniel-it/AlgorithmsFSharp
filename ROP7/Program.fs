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
  | UnexpectedError of string

let handleError error =
  match error with
  | FileNotExist path -> printfn "File not exist: %s" path
  | EmptyFile -> printfn "The file is empty"
  | UnexpectedError message -> printfn "Unexpected error: %s" message

let tryCatch f x =
  try
    Ok (f x)
  with
    ex -> Error (UnexpectedError ex.Message)

let readFile path =
  if File.Exists path then
    tryCatch File.ReadAllLines path
  else
    Error (FileNotExist path)

let ifFileEmpty (fileArray: string array) =
  match fileArray with
  | [||] -> Error EmptyFile
  | _ -> Ok (Array.toList fileArray)

let createProduct (values: string array) =
  {
    Name = values.[0]
    Price = Double.Parse(values.[1], CultureInfo.InvariantCulture)
    Quantity = Int32.Parse(values.[2])
  }

let collectProduct (line: string) =
  let values = line.Split(';')
  tryCatch createProduct values

let parseLine (products: Product list, productsError: string list, line: string) =
  match collectProduct line with
  | Ok product -> product :: products, productsError
  | Error _ -> products, line :: productsError

let parseFile (fileList: string list) =
  List.fold (fun (products, productsError) line ->
    parseLine (products, productsError, line)
  ) ([], []) fileList

let rec printProducts (products: Product list) =
  match products with
  | [] -> ()
  | head :: tail ->
    printfn "Name: %s | Price: %f | Quantity: %d" head.Name head.Price head.Quantity
    printProducts tail

let printResult (products: Product list, productsError: string list) =
  printfn "Parsed products:"
  printProducts products
  printfn "Lines with errors:"
  List.iter (printfn "%s") productsError

let runProgramm  =
  readFile
  >> Result.bind ifFileEmpty
  >> Result.map parseFile
  >> Result.map printResult
  >> Result.mapError handleError
  >> ignore
runProgramm "ROP7/products_empty.txt"
runProgramm "ROP7/products.txt"
runProgramm "ROP7/products_error.txt"