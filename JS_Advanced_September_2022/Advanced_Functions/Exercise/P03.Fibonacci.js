function getFibonator() {
    let numOne = 0;
    let numTwo = 1;

    return () => {
        let sum = numOne + numTwo;
        numOne = numTwo;
        numTwo = sum;
        return numOne;
    }
}

let fib = getFibonator();
console.log(fib()); // 1
console.log(fib()); // 1
console.log(fib()); // 2
console.log(fib()); // 3
console.log(fib()); // 5
console.log(fib()); // 8
console.log(fib()); // 13