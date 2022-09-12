function solve(startNum, endNum) {
    let num1 = Number(startNum);
    let num2 = Number(endNum);

    let result = 0;

    for (let num = num1; num <= num2; num++) {
        result += num;
    }

    console.log(result);
}

solve('1', '5');
solve('-8', '20');