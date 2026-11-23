open System
open System.IO
open System.Globalization

type Product = {
    Name: string
    Price: double
    Quantity: int
}

let pathFile = Path.GetFullPath("ROP2/products.txt")

// Преобразование из массива в список товаров
let getFileLines (pathFile: string) : string array =
    File.ReadAllLines(pathFile)

let setProductsList (fileLines: string array) : Product list =
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

// Вывод списка
let printProducts (productsList: Product list) = 
    let rec printList (values: Product list) =
        match values with
        | [] -> ()
        | head :: tail ->
            printfn "Name: %s | Price: %f | Quantity: %d" head.Name head.Price head.Quantity
            printList tail

    printList productsList

// Запуск программы
let runProgramm (pathFile: string) = 
    pathFile
    |> getFileLines
    |> setProductsList
    |> printProducts

runProgramm pathFile