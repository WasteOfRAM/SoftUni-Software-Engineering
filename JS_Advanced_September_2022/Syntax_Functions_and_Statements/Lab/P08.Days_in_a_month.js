function solve(month, year) {
    return new Date(year, month, 0).getDate();
}

console.log(solve(1, 2012));
console.log(solve(2, 2021));