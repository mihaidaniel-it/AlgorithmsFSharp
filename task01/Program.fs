let nums = [| 3; 8; 12; 4; 7; 15; 2; 9 |]
let target = 11

let twoSum (nums:int array) (target: int) =
    let mutable result = [||]

    for i in 0 .. nums.Length - 1 do
        for j in 1 .. nums.Length - 1 do
            let mutable mathAction = nums[i] + nums[j]

            if mathAction = target then
                result <- [| i; j |]
    
    result

let functionResult = twoSum nums target
printfn "Result: %A" functionResult