function solve(number = 5) {
    let str = '* '.repeat(number).trimEnd();

    for (let index = 0; index < number; index++) {
        console.log(str);
    }
}

solve();
solve(7);