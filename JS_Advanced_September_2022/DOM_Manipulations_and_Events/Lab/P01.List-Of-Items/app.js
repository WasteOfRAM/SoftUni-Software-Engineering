function addItem() {
    let ul = document.getElementById('items');
    let input = document.getElementById('newItemText').value;
    
    let listItem = document.createElement('li');
    listItem.textContent = input;
    //ul.append(listItem);

    // append() in Judge (use only appendChild())
    ul.appendChild(listItem);
}