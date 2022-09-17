function magicCheck(matrix) {
    let newMatrix = [...matrix];
    let controlSum = matrix[0].reduce((sum, current) => sum + current, 0);

    for (let col = 0; col < matrix[0].length; col++) {
        let newRow = [];
        for (let row = 0; row < matrix.length; row++) {
            newRow.push(matrix[row][col]);
        }

        newMatrix.push(newRow);
    }

    return newMatrix.every(arr => arr.reduce((sum, current) => sum + current, 0) === controlSum);
}

console.log(magicCheck([[4, 5, 6],
[6, 5, 4],
[5, 5, 5]]));

console.log(magicCheck([[11, 32, 45],
[21, 0, 1],
[21, 1, 1]]));

console.log(magicCheck([[1, 0, 0],
[0, 0, 1],
[0, 1, 0]]));