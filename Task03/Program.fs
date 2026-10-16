let nums = [| 3; 4; 3; 3; 6; 5; 0; |]

let containsDuplicate (nums: int array) = 
    let mutable hasDuplicate = false

    for i in 0 .. nums.Length - 1 do
        for j in 1 .. nums.Length - 1 do
            if nums[i] = nums[j] && i <> j then
                hasDuplicate <- true

    hasDuplicate

let result = containsDuplicate nums
printfn "Result: %A" result