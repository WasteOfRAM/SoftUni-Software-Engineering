function add(firstNum) {
    let sum = firstNum;

    function inner(secondNum) {
        sum += secondNum;
        return inner;
    }

    inner.toString = () => { return sum };

    return inner;
}

console.log(add(1).toString());
console.log(add(1)(6)(-3).toString());