function sumFirstLast(input) {
    let first = input.shift();
    let last = input.pop();

    return Number(first) + Number(last);
}

console.log(sumFirstLast(['20', '30', '40']));
console.log(sumFirstLast(['5', '10']));