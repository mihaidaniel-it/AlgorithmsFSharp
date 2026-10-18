let nums = [| 3; 8; 12; 4; 7; 15; 2; 9 |]
let target = 18

let twoSum (nums:int array) (target: int) =
    let mutable result = [||]

    for i in 0 .. nums.Length - 1 do
        for j in 1 .. nums.Length - 1 do
            let mutable mathAction = nums[i] + nums[j]

            if mathAction = target then
                result <- [| i; j |]
    
    result

let functionResult = twoSum nums target
printfn "Result without recursion: %A" functionResult


// With recursion
let twoSumRecursion (nums: int array) target = 
    let rec findI i = 
        match i >= nums.Length with
        | true -> [||]
        | false -> 
            let rec findJ j = 
                match j >= nums.Length with 
                | true -> findI (i + 1)
                | false -> 
                    match nums[i] + nums[j] = target with
                    | true -> [| i; j |]
                    | false -> findJ (j + 1)
            findJ (i + 1)

    findI 0

printfn "Result with recursion: %A" (twoSumRecursion nums target)


// 1. Если i НЕ больше длины массива - ищем j
// 2. Если j НЕ больше длины сверяем на target, иначе ищем опять i
// 3. Если target найден - возврат индексов, иначе ищем j