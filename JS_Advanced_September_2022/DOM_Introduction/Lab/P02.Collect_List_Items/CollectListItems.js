function extractText() {
    let list = document.getElementById('items');
    listItems = list.querySelectorAll('li');
    let textArea = document.getElementById('result');

    for (const item of listItems) {
        textArea.value += item.textContent + '\n';
    }
}