function catalogue(elements) {
    let productCatalogue = {};

    for (const product of elements) {
        let [name, price] = product.split(' : ');
        let initial = name[0];

        if(!productCatalogue.hasOwnProperty(initial)){
            productCatalogue[initial] = [];
        }

        productCatalogue[initial].push({name, price});
    }

    const sortedKeys = Object.keys(productCatalogue).sort();

    for (const letter of sortedKeys) {
        console.log(letter);
        let sortedProducts = productCatalogue[letter].sort((a, b) => a.name > b.name ? 1 : b.name > a.name ? -1 : 0);
        for (const product of sortedProducts) {
            console.log(`  ${product.name}: ${product.price}`);
        }
    }
}

catalogue(['Appricot : 20.4',
'Fridge : 1500',
'TV : 1499',
'Deodorant : 10',
'Boiler : 300',
'Apple : 1.25',
'Anti-Bug Spray : 15',
'T-Shirt : 10']
);

catalogue(['Banana : 2',
`Rubic's Cube : 5`,
'Raspberry P : 4999',
'Rolex : 100000',
'Rollon : 10',
'Rali Car : 2000000',
'Pesho : 0.000001',
'Barrel : 10']);