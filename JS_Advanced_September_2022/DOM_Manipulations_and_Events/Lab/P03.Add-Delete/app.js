function addItem() {
    let ul = document.getElementById('items');
    let input = document.getElementById('newItemText').value;
    
    let listItem = document.createElement('li');
    listItem.textContent = input;
    //ul.append(listItem);

    // append() in Judge (use only appendChild())
    
    let remove = document.createElement('a');
    let linkText = document.createTextNode('[Delete]');
    remove.appendChild(linkText);
    remove.href = "#";
    
    remove.addEventListener('click', deleteItem);
    
    listItem.appendChild(remove);
    ul.appendChild(listItem);

    function deleteItem() {
        listItem.remove();
    }
}