function colorize() {
    let tableRows = document.querySelectorAll('table tr');

    for (let row = 1; row < tableRows.length; row += 2) {
        tableRows[row].style.backgroundColor = 'teal';
    }
}