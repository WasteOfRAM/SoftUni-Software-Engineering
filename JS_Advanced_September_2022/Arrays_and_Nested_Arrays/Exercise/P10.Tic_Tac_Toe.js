function pointles(moves) {
    let field = [[false, false, false],
    [false, false, false],
    [false, false, false]];

    let player = true;
    let isWinner = false;
    let movesCount = 0;

    for (const move of moves) {
        [row, col] = move.split(' ');
        let currentPlayer = player ? 'X' : 'O';

        if (!field[row][col]) {
            field[row][col] = currentPlayer;

            if(rowWin(field, row, currentPlayer) || colWin(field, col, currentPlayer) || diagonalWin(field, currentPlayer)){
                console.log(`Player ${currentPlayer} wins!`);
                isWinner = true;
                break;
            }
            
        } else {
            console.log('This place is already taken. Please choose another!');
            continue;
        }

        movesCount++;
        if(movesCount === 9){
            break;
        }

        player = !player;
    }

    if(!isWinner){
        console.log('The game ended! Nobody wins :(');
    }

    field.forEach(element => {
        console.log(element.join('\t'));
    });

    function rowWin(field, row, currentPlayer) {

        if (field[row][0] === currentPlayer &&
            field[row][1] === currentPlayer &&
            field[row][2] === currentPlayer) { return true; };

        return false;
    }

    function colWin(field, col, currentPlayer) {
        if (field[0][col] === currentPlayer &&
            field[1][col] === currentPlayer &&
            field[2][col] === currentPlayer) { return true; };

        return false;
    }

    function diagonalWin(field, currentPlayer) {
        if (field[0][0] === currentPlayer &&
            field[1][1] === currentPlayer &&
            field[2][2] === currentPlayer ||
            field[2][0] === currentPlayer &&
            field[1][1] === currentPlayer &&
            field[0][2] === currentPlayer) { return true; };

        return false;
    }
}

pointles(["0 1",
    "0 0",
    "0 2",
    "2 0",
    "1 0",
    "1 1",
    "1 2",
    "2 2",
    "2 1",
    "0 0"]);

pointles(["0 0",
    "0 0",
    "1 1",
    "0 1",
    "1 2",
    "0 2",
    "2 2",
    "1 2",
    "2 2",
    "2 1"]);

pointles(["0 1",
    "0 0",
    "0 2",
    "2 0",
    "1 0",
    "1 2",
    "1 1",
    "2 1",
    "2 2",
    "0 0"]);