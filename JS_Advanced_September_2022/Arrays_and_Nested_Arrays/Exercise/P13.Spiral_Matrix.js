function spiramMatrix(rows, cols) {
    let spiralMatrix = [];
    let value = 1;
    let col = 0;
    let row = 0;
    let colEnd = cols - 1;
    let rowEnd = rows - 1;

    for (let i = 0; i < rows; i++) {
        spiralMatrix[i] = [];
    }

    while (col <= colEnd && row <= rowEnd) {
        for (let i = col; i <= colEnd; i++) {
            spiralMatrix[row][i] = value;
            value++;
        }
        row++;

        for (let i = row; i <= rowEnd; i++) {
            spiralMatrix[i][colEnd] = value;
            value++;
        }
        colEnd--;

        for (let i = colEnd; i >= col; i--) {
            spiralMatrix[rowEnd][i] = value;
            value++;
        }
        rowEnd--;


        for (let i = rowEnd; i >= row; i--) {
            spiralMatrix[i][col] = value;
            value++;
        }
        col++;
    }

    spiralMatrix.forEach(arr => {
        console.log(arr.join(' '));
    });
}

spiramMatrix(3, 3);
spiramMatrix(5, 5);