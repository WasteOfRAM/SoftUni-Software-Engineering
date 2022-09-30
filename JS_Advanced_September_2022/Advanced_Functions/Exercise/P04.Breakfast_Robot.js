function solution() {
    let recipesLib = {
        apple: { 'carbohydrate': 1, 'flavour': 2 },
        lemonade: { 'carbohydrate': 10, 'flavour': 20 },
        burger: { 'carbohydrate': 5, 'fat': 7, 'flavour': 3 },
        eggs: { 'protein': 5, 'fat': 1, 'flavour': 1 },
        turkey: { 'protein': 10, 'carbohydrate': 10, 'fat': 10, 'flavour': 10 }
    }

    let storage = {
        protein: 0,
        carbohydrate: 0,
        fat: 0,
        flavour: 0
    }

    return (commandInput) => {
        let commandSplit = commandInput.split(' ');

        let command = commandSplit[0];

        let commands = {
            restock: (microelement, quantity) => {
                storage[microelement] += Number(quantity);
                return 'Success';
            },
            prepare: (recipeInput, quantity) => {
                let recipe = recipesLib[recipeInput];
                for (const [ingredient, count] of Object.entries(recipe)) {
                    if(count * quantity > storage[ingredient]){
                        return `Error: not enough ${ingredient} in stock`
                    }
                }

                for (const [ingredient, count] of Object.entries(recipe)) {
                    storage[ingredient] -= count * quantity;
                }

                return 'Success';
            },
            report: () => { 
                return `protein=${storage['protein']} carbohydrate=${storage['carbohydrate']} fat=${storage['fat']} flavour=${storage['flavour']}`; 
            }
        }

        return commands[command](commandSplit[1], commandSplit[2]);
    }
}

let manager = solution();
// console.log(manager("restock flavour 50")); // Success 
// console.log(manager("prepare lemonade 4")); // Error: not enough carbohydrate in stock 
// console.log(manager("report"));

console.log(manager("restock flavour 50"));
console.log(manager("prepare lemonade 4"));
console.log(manager("restock carbohydrate 10"));
console.log(manager("restock flavour 10"));
console.log(manager("prepare apple 1"));
console.log(manager("restock fat 10"));
console.log(manager("prepare burger 1"));
console.log(manager("report"));