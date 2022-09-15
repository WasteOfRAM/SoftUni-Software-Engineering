function biggestElement(matrix) {
    let maxElement = Number.NEGATIVE_INFINITY;

    matrix.forEach(row => {
        row.forEach(element => { if(element > maxElement){ maxElement = element} });
    });

    return maxElement;
}

console.log(biggestElement([[20, 50, 10], [8, 33, 145]]));
console.log(biggestElement([[3, 5, 7, 12], [-1, 4, 33, 2], [8, 3, 0, 4]]));