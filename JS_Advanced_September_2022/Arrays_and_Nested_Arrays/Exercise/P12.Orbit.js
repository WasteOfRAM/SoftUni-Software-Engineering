function orbit(params) {
    let width = params[0];
    let heigth = params[1];
    let startRow = params[2];
    let startCol = params[3];

    let matrix = [];

    for (let i = 0; i < heigth; i++) {
        matrix[i] = [];     
    }

    for (let row = 0; row < heigth; row++) {
        for (let col = 0; col < width; col++) {
            matrix[row][col] = Math.max(Math.abs(row - startRow), Math.abs(col - startCol)) + 1;
        }       
    }

    matrix.forEach(element => {
        console.log(element.join(' '));
    });
}

orbit([4, 4, 0, 0]);
orbit([5, 5, 2, 2]);
orbit([3, 3, 2, 2]);