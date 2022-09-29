function solve(commandsArray) {
    let carsList = {};

    let commands = {
        create: (carName, inherits, carToInherit) => carsList[carName] = inherits !== undefined ?  Object.create(carsList[carToInherit]) : {},
        set: (carName, key, value) => carsList[carName][key] = value,
        print: (carName) => {
            let properties = [];
            for (const property in carsList[carName]) {
                properties.push(`${property}:${carsList[carName][property]}`);
            }

            console.log(properties.join(','));
        }
    }

    for (const commandLine of commandsArray) {
        let [command, carName, paramOne, paramTree] = commandLine.split(' ');

        commands[command](carName, paramOne, paramTree);
    }
}

solve(['create c1',
    'create c2 inherit c1',
    'set c1 color red',
    'set c2 model new',
    'print c1',
    'print c2']
);