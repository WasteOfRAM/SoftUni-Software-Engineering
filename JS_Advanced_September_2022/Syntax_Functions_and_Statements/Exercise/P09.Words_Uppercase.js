function words(inputStr) {
    
    return inputStr.match(/\w+/g).join(', ').toUpperCase();
}

console.log(words('Hi, how are you?'));
console.log(words('hello'));