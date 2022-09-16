function increasingSubsequence(array) {
    let result = [];

    array.reduce((biggest, current) => {
        if (biggest <= current) {
            result.push(current);
            return biggest = current;
        }
        return biggest;
    }, array[0]);

    return result;
}

console.log(increasingSubsequence([1,
    3,
    8,
    4,
    10,
    12,
    3,
    2,
    24]));

console.log(increasingSubsequence([1,
    2,
    3,
    4]));

console.log(increasingSubsequence([20,
    3,
    2,
    15,
    6,
    1]));