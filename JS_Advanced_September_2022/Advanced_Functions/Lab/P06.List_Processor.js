function listProcesor(commandsList) {
    let collection = [];

    let comands = {
        add: (str) => collection.push(str),
        remove: (str) => collection = collection.filter((s) => s !== str),
        print: () => console.log(collection.join(','))
    }

    for (const command of commandsList) {
        let commandArg = command.split(' ');
        let proces = commandArg[0];
        let value = commandArg[1];

        comands[proces](value);
    }
}

listProcesor(['add hello', 'add again', 'remove hello', 'add again', 'print']);
listProcesor(['add pesho', 'add george', 'add peter', 'remove peter','print']);