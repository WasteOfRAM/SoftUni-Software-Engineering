function deleteByEmail() {
    let tableBody = document.querySelectorAll('tbody tr');
    let input = document.getElementsByName('email')[0].value;
    let result = document.getElementById('result');

    for (const row of tableBody) {
        let rowToDel;
        if (row.children[1].textContent === input) {
            rowToDel = row;
            rowToDel.parentElement.removeChild(rowToDel);
            result.textContent = 'Deleted.'
            return;
        }
    }

    result.textContent = 'Not found.'
}