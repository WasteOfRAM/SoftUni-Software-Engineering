function fruit(fruitType, weightInGr, pricePerKg){
    let weightInKg = weightInGr / 1000;
    let price = weightInKg * pricePerKg;

    console.log(`I need $${price.toFixed(2)} to buy ${weightInKg.toFixed(2)} kilograms ${fruitType}.`);
}

fruit('orange', 2500, 1.80);
fruit('apple', 1563, 2.35);