function lowestPrices(input) {
    let products = {};

    for (const data of input) {
        let [city, product, price] = data.split(' | ');
        price = Number(price);

        if (products.hasOwnProperty(product)) {
            if(products[product].price > price){
                products[product] = {city, price};
            }
        } else {
            products[product] = {city, price};
        }
    }

    for (const product in products) {
        console.log(`${product} -> ${products[product].price} (${products[product].city})`)
    }
}

lowestPrices(['Sample Town | Sample Product | 1000',
    'Sample Town | Orange | 2',
    'Sample Town | Peach | 1',
    'Sofia | Orange | 3',
    'Sofia | Peach | 2',
    'New York | Sample Product | 1000.1',
    'New York | Burger | 10']);