let numbersList = [1; 5; 3; 4; 5; 6; 7; 9; 15]
let rec sum nums =
    match nums with
    | [] -> 0        
    | head :: tail -> head + sum tail


let x = ()
 



let t1 = 
    [ 
        1, 2, 3
        4, 5, 6
        7, 8, 9
    ]

let tupleSum (x, y, z) = 
    x + y + z

let tupleProduct (x, y, z) = 
    x * y * z  

let rec tOneGeneric (transform: int * int * int -> int) (t1: list<int * int * int>) = 
    match t1 with
    | [] -> 0       
    | head :: tail -> 
      (transform head) + tOneGeneric transform tail

let rec tOneGeneric2 (transform: 'a -> int) (t1: list<'a>) = 
    match t1 with
    | [] -> 0       
    | head :: tail -> 
      (transform head) + tOneGeneric2 transform tail
let s = tOneGeneric2 (fun i -> i) [1; 3; 4; 5]

let tOneSum2 (t1: list<int * int * int>) = 
    tOneGeneric tupleSum t1

let tOneProductOne (t1: list<int * int * int>) = 
      tOneGeneric tupleSum t1

let rec tOneSum (t1: list<int * int * int>) = 
    match t1 with
    | [] -> 0        
    | head :: tail -> 
      let a, b, c = head 
      a + b + c + tOneSum tail

let rec tOneProduct (t1: list<int * int * int>) = 
    match t1 with
    | [] -> 0        
    | head :: tail -> 
      let a, b, c = head 
      a * b * c + tOneSum tail








let sum2 x y = x + y
let plus5 a = sum2 5 a
let plus5' = sum2 5


let sum x y = z + y 

let addFive = sum 5

let finalCall = addFive 10

`// 5 + 10 = 15`




// For matrix
let sumMatrix = tOneGeneric2 (tOneGeneric2 id)






