module DesigningWithTypes

// Распеределение типов
type EmailVerificationState =
    | Verified
    | NotVerified

type EmailInfo = { 
    EmailAddress: string
    IsVerifyedEmail: EmailVerificationState
}

type PhoneInfo = 
    | PersonalNumber of int
    | WorkNumber of int
    | HomeNumber of int

type AddressInfo = 
    | HoneAdress of string
    | ApatmentNumber of int
    | Floor of int
    | Zip of int


type ContactMethod = 
    | Email of EmailInfo
    | Phone of PhoneInfo
    | Address of AddressInfo

type Person = {
    Name: string

    // list для хранения нескольких методов связи
    ContactMethod: ContactMethod list
}

// Валидация на этапе создания объекта
let emailIncorrect = "daniel.com"
let emailCorrect = "daniel@gmail.com"
let validateEmail (email: string) =
    match System.Text.RegularExpressions.Regex.IsMatch(email, @"^\S+@\S+\.\S+$") with
    | true -> Some { EmailAddress = email }
    | false -> None


// Смена конечного автомата
let changeEmailVerification state = 
    match state with 
    | Verified -> Verified
    | NotVerified -> Verified