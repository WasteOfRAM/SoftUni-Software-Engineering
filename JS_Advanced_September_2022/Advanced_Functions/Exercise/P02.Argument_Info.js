function solve(...params) {
    let typesCount = {};

    for (const value of params) {
        let type = typeof value;
        console.log(`${type}: ${value}`);

        if (!typesCount[type]) {
            typesCount[type] = 0;
        }

        typesCount[type]++;
    }

    let sortedTypes = Object.entries(typesCount).sort((a, b) => b[1] - a[1]);

    for (const [type, count] of sortedTypes) {
        console.log(`${type} = ${count}`);
    }
}

solve('cat', 42, function () { console.log('Hello world!'); });
console.log('---------------------------');
solve({ name: 'bob'}, 3.333, 9.999);