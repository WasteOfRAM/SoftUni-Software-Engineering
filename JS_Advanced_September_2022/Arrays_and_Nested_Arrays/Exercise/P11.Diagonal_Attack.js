function diagonalAttack(matrix) {
    let principalDiagSum = 0;
    let secondaryDiagSum = 0;

    let numbersMatrix = matrix.map(str => [] = str.split(' '));

    for (let i = 0; i < numbersMatrix.length; i++) {
        principalDiagSum += Number(numbersMatrix[i][i]);
        secondaryDiagSum += Number(numbersMatrix[(numbersMatrix.length - 1) - i][i]);
    }

    if(principalDiagSum !== secondaryDiagSum){
        matrix.forEach(str => {
            console.log(str);
        });
    } else {
        for (let i = 0; i < numbersMatrix.length; i++) {
            for (let j = 0; j < numbersMatrix.length; j++) {
                if (i !== j && (i + j) !== (numbersMatrix.length - 1)) {
                    numbersMatrix[i][j] = principalDiagSum;
                }
            }
        }

        numbersMatrix.forEach(numbers => {
            console.log(numbers.join(' '));
        });
    }
}

diagonalAttack(
    ['5 3 12 3 1',
    '11 4 23 2 5',
    '101 12 3 21 10',
    '1 4 5 2 2',
    '5 22 33 11 1']);

diagonalAttack(
        ['1 1 1',
         '1 1 1',
         '1 1 0']
);