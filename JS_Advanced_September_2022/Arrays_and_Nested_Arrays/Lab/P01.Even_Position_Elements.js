function evenPosition(input) {
    return input.filter((el, index) => index % 2 === 0).join(' ');
}

console.log(evenPosition(['20', '30', '40', '50', '60']));
console.log(evenPosition(['5', '10']));