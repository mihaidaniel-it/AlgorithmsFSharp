let nums = [| 3; 8; 12; 4; 7; 15; 2; 9 |]

let runningSum (nums: int array) =
    let result = Array.zeroCreate nums.Length
    let mutable tempNumber = 0

    for i in 0 .. nums.Length - 1 do
        tempNumber <- tempNumber + nums[i]
        result[i] <- tempNumber

    result

let resultArray = runningSum nums
printfn "Result: %A" resultArray