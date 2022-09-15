function numbers(arr) {
    let result = [];

    arr.forEach(element => {
        element < 0 ? result.unshift(element) : result.push(element);
    });

    return result.join('\n');
}

console.log(numbers([7, -2, 8, 9]));
console.log(numbers([3, -2, 0, -1]));