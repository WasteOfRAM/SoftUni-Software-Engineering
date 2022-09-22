function sumTable() {
    document.getElementById('sum').textContent = '';
    let table = document.querySelectorAll('table tr');
    let total = 0;

    for (let i = 1; i < table.length; i++) {
        let cols = table[i].children;
        let price = cols[cols.length - 1].textContent;
        total += Number(price);
    }

    document.getElementById('sum').textContent = total;
}