open System
open System.IO
open System.Globalization

type Product = {
    Name: string
    Price: double
    Quantity: int
}

type DomainError =
    | FileNotExist of path: string
    | FileNotAvailable
    | InvalidProductName
    | InvalidProductPrice
    | InvalidProductQuantity
    | DataNotAvailable

// Обработка ошибок
let handleError error =
    match error with
    | FileNotExist path -> printfn "File not exist: %s" path
    | FileNotAvailable -> printfn "File has no values"
    | InvalidProductName -> printfn "Product name is empty"
    | InvalidProductPrice -> printfn "Product price is incorrect"
    | InvalidProductQuantity -> printfn "Product quantity is incorrect"
    | DataNotAvailable -> printfn "The data is not set"

// Путь до файла
let pathFile = Path.GetFullPath("ROP/products.txt")




// Проверка имени продукта
let checkProductName name =
    match String.IsNullOrWhiteSpace(name) with
    | true -> Error InvalidProductName
    | false -> Ok name

// Проверка цены продукта
let checkProductPrice (price: string) =
    match Double.TryParse(price, NumberStyles.Float, CultureInfo.InvariantCulture) with
    | (false, _) -> Error InvalidProductPrice
    | (true, parsedPrice) ->
        match parsedPrice < 0.0 with
        | true -> Error InvalidProductPrice
        | false -> Ok parsedPrice

// Проверка количества продукта
let checkProductQuantity quantity =
    match Int32.TryParse(quantity: string) with
    | (false, _) -> Error InvalidProductQuantity
    | (true, parsedQuantity) -> Ok parsedQuantity


 

// Создание продукта
let createProduct array =
    match array with
    | [| name; price; quantity |] ->
        checkProductName name
        |> Result.bind (fun validName ->
            checkProductPrice price
            |> Result.bind (fun validPrice ->
                checkProductQuantity quantity
                |> Result.map (fun validQuantity ->
                    {
                        Name = validName
                        Price = validPrice
                        Quantity = validQuantity
                    }
                )
            )
        )
    | _ -> Error DataNotAvailable



// Проверка существования файла
let checkFileExist pathFile =
    match File.Exists(pathFile) with
    | false -> Error (FileNotExist pathFile)
    | true -> Ok pathFile

// Массив строк из файла 
let readFile pathFile = Ok (File.ReadAllLines(pathFile))

// Проверка пустой ли файл
let checkFileNotEmpty lines =
    match lines with
    | [||] -> Error FileNotAvailable
    | _ -> Ok lines



// Конвейр создания товаров
let parseFile lines =
    lines
    |> Array.map (fun (line: string) -> line.Split([| ';' |]))
    |> Array.map createProduct
    |> Array.toList

// Общая проверка и обработка файла
let validateFile pathFile =
    Ok pathFile
    |> Result.bind checkFileExist
    |> Result.bind readFile
    |> Result.bind checkFileNotEmpty
    |> Result.map parseFile



// Форматированный вывод и расчёт
let calculateProduct product =
    sprintf "%s: (%s * %d) = %f"
        product.Name
        (product.Price.ToString())
        product.Quantity
        (product.Price * double product.Quantity)

// Преобразование продукта через map
let processProduct product = Result.map calculateProduct product

// Вывод продуктов
let printProducts products =
    products
    |> List.iteri (fun index product ->
        match processProduct product with
        | Ok value -> printfn "Line %d: %s" (index + 1) value
        | Error error ->
            printf "Line %d: " (index + 1)
            handleError error
    )



// Запуск программы
let runProgram pathFile =
    match validateFile pathFile with
    | Error error ->
        handleError error
    | Ok products ->
        printProducts products
runProgram pathFile