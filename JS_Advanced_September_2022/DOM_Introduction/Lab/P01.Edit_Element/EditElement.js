function editElement(element, match, replacer) {
    let elementText = element.innerHTML;
    element.innerHTML = elementText.replace(new RegExp(match, 'g'), replacer);
}