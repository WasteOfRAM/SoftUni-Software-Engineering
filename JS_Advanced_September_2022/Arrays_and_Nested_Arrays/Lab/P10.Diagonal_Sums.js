function diagonalSums(matrix) {
    let firstDiagonalSum = 0;
    let secondDiagonalSum = 0;

    let firstIndex = 0;
    let secondIndex = matrix[0].length - 1;

    matrix.forEach(array => {
        firstDiagonalSum += array[firstIndex++];
        secondDiagonalSum += array[secondIndex--];
    });

    return firstDiagonalSum + ' ' + secondDiagonalSum;
}

console.log(diagonalSums([[20, 40], [10, 60]]));
console.log(diagonalSums([[3, 5, 17], [-1, 7, 14], [1, -8, 89]]));