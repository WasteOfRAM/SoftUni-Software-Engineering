function doubleSort(array) {
    return array.sort((a, b) => a.length - b.length || a.localeCompare(b)).join('\n');
}

console.log(doubleSort(['alpha',
    'beta',
    'gamma']));

console.log(doubleSort(['Isacc',
    'Theodor',
    'Jack',
    'Harrison',
    'George']));

console.log(doubleSort(['test',
    'Deny',
    'omen',
    'Default']));