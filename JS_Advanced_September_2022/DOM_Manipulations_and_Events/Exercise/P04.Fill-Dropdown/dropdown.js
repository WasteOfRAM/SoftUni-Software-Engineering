function addItem() {
    let menu = document.getElementById('menu');
    let itemText = document.getElementById('newItemText');
    let itemValue = document.getElementById('newItemValue');

    let newOption = document.createElement('option');
    newOption.textContent = itemText.value;
    newOption.value = itemValue.value;

    menu.appendChild(newOption);
    
    itemText.value = '';
    itemValue.value = '';
}