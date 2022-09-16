function sortingNumbers(numbersArr) {
    let result = [];
    numbersArr.sort((a, b) => a - b);

    while (numbersArr.length > 0) {
        result.push(numbersArr.shift());
        result.push(numbersArr.pop());
    }

    return result;
}

console.log(sortingNumbers([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));