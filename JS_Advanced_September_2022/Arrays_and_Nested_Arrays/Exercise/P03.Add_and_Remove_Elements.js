function addRemoveElements(commands) {
    let result = [];

    commands.forEach((command, index) => {
        command === 'add' ? result.push(index + 1) : result.pop();
    });

    return result.length === 0 ? 'Empty' : result.join('\n');
}

console.log(addRemoveElements(['add',
    'add',
    'add',
    'add']
));
console.log(addRemoveElements(['add',
    'add',
    'remove',
    'add',
    'add']));
console.log(addRemoveElements(['remove',
    'remove',
    'remove']));