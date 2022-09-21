function solve(elements) {
    let result = [];

    let operationsEnum = {
        '+': (a, b) => a + b,
        '-': (a, b) => a - b,
        '*': (a, b) => a * b,
        '/': (a, b) => a / b
    }

    for (const elem of elements) {
        if (typeof elem === 'number') {
            result.push(elem);
        } else {
            if(result.length < 2){
                return "Error: not enough operands!";
            } else {
                let b = result.pop();
                let a = result.pop();

                result.push(operationsEnum[elem](a, b));
            }
        }
    }

    return result.length === 1 ? result[0] : "Error: too many operands!";
}

console.log(solve([7, 33, 8, '-']));
console.log(solve([15, '/']));
console.log(solve([5, 3, 4, '*', '-']));
console.log(solve([3, 4, '+']));