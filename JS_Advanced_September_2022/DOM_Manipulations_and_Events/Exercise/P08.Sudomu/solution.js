function solve() {
    let checkButton = document.querySelectorAll('button')[0];
    let clearButton = document.querySelectorAll('button')[1];
    let tableBody = document.querySelector('tbody');
    let table = document.querySelector('table');
    let resultMsg = document.getElementById('check').children[0];

    checkButton.addEventListener('click', quickCheck);
    clearButton.addEventListener('click', clear);

    function quickCheck() {
        let sudomuMatrix = [];

        matrixFill(sudomuMatrix);

        let lines = [];

        for (let i = 0; i < sudomuMatrix.length; i++) {
            lines.push(sudomuMatrix[i]);

            let col = [];
            for (let j = 0; j < sudomuMatrix[i].length; j++) {
                col.push(sudomuMatrix[j][i]);
            }

            lines.push(col);
        }

        let invalid = lines.some(line => line.some((element, index) => line.indexOf(element) !== index));

        if (invalid) {
            table.style.border = "2px solid red";
            resultMsg.style.color = 'red';
            resultMsg.textContent = "NOP! You are not done yet...";
            return;
        }

        table.style.border = "2px solid green";
        resultMsg.style.color = 'green';
        resultMsg.textContent = "You solve it! Congratulations!"
    }

    function clear() {
        table.style.border = "none";
        resultMsg.textContent = '';

        for (const row of tableBody.rows) {
            for (const data of row.children) {
                data.children[0].value = '';
            }
        }
    }

    function matrixFill(sudomuMatrix) {
        for (let row = 0; row < tableBody.rows.length; row++) {
            sudomuMatrix[row] = [];

            for (let cell = 0; cell < tableBody.rows[row].cells.length; cell++) {
                let inputValue = tableBody.rows[row].cells[cell].children[0].value;

                sudomuMatrix[row][cell] = inputValue;
            }
        }
    }


}