class VegetableStore {
    constructor(owner, location) {
        this.owner = owner;
        this.location = location;
        this.availableProducts = [];
    }

    loadingVegetables(vegetables) {
        for (const vegetable of vegetables) {
            let vegetableData = vegetable.split(" ");

            let currentVegetable = this.availableProducts.find(v => v.type === vegetableData[0]);
            if (currentVegetable === undefined) {
                this.availableProducts.push({
                    type: vegetableData[0],
                    quantity: Number(vegetableData[1]),
                    price: Number(vegetableData[2]),

                    toString: function() {
                        return `${this.type}-${this.quantity}-$${this.price}`;
                    }
                })
            } else {
                currentVegetable.quantity += Number(vegetableData[1]);

                if (Number(vegetableData[2]) > currentVegetable.price) {
                    currentVegetable.price = Number(vegetableData[2]);
                }
            }
        }

        let productNames = this.availableProducts.map(pr => pr.type);

        return `Successfully added ${productNames.join(", ")}`;
    }

    buyingVegetables(selectedProducts) {
        let totalPrice = 0;

        for (const selectedProduct of selectedProducts) {
            let selectedProductData = selectedProduct.split(" ");
            let type = selectedProductData[0];
            let quantity = Number(selectedProductData[1]);

            let product = this.availableProducts.find(v => v.type === type);
            if (product === undefined) {
                throw new Error(`${type} is not available in the store, your current bill is $${totalPrice.toFixed(2)}.`);
            }

            if (product.quantity < quantity) {
                throw new Error(`The quantity ${quantity} for the vegetable ${type} is not available in the store, your current bill is $${totalPrice.toFixed(2)}.`)
            }

            totalPrice += product.price * quantity;
            product.quantity -= quantity;

        }

        return `Great choice! You must pay the following amount $${totalPrice.toFixed(2)}.`;
    }

    rottingVegetable(type, quantity) {
        let product = this.availableProducts.find(v => v.type === type);

        if (product === undefined) {
            throw new Error(`${type} is not available in the store.`);
        }

        if (product.quantity < quantity) {
            product.quantity = 0;
            return `The entire quantity of the ${type} has been removed.`;
        }

        product.quantity -= quantity;
        return `Some quantity of the ${type} has been removed.`;
    }

    revision() {
        let str = `Available vegetables:\n`;

        str += this.availableProducts.sort((a, b) => a.price - b.price).join(`\n`);

        str += `\nThe owner of the store is ${this.owner}, and the location is ${this.location}.`;

        return str;
    }
}

// let vegStore = new VegetableStore("Jerrie Munro", "1463 Pette Kyosheta, Sofia");
// console.log(vegStore.loadingVegetables(["Okra 2.5 3.5", "Beans 10 2.8", "Celery 5.5 2.2", "Celery 0.5 2.5"]));

//Successfully added Okra, Beans, Celery

// let vegStore = new VegetableStore("Jerrie Munro", "1463 Pette Kyosheta, Sofia");
// console.log(vegStore.loadingVegetables(["Okra 2.5 3.5", "Beans 10 2.8", "Celery 5.5 2.2", "Celery 0.5 2.5"]));
// console.log(vegStore.buyingVegetables(["Okra 1"]));
// console.log(vegStore.buyingVegetables(["Beans 8", "Okra 1.5"]));
// console.log(vegStore.buyingVegetables(["Banana 1", "Beans 2"]));

// Successfully added Okra, Beans, Celery 
// Great choice! You must pay the following amount $3.50. 
// Great choice! You must pay the following amount $27.65.
// Uncaught Error: Banana is not available in the store, your current bill is $0.00.

// let vegStore = new VegetableStore("Jerrie Munro", "1463 Pette Kyosheta, Sofia");
// console.log(vegStore.loadingVegetables(["Okra 2.5 3.5", "Beans 10 2.8", "Celery 5.5 2.2", "Celery 0.5 2.5"]));
// console.log(vegStore.rottingVegetable("Okra", 1));
// console.log(vegStore.rottingVegetable("Okra", 2.5));
// console.log(vegStore.buyingVegetables(["Beans 8", "Okra 1.5"]));

// Successfully added Okra, Beans, Celery
// Some quantity of the Okra has been removed.
// The entire quantity of the Okra has been removed.
// Uncaught Error: The quantity 1.5 for the vegetable Okra is not available in the store, your current bill is $22.40.

let vegStore = new VegetableStore("Jerrie Munro", "1463 Pette Kyosheta, Sofia");
console.log(vegStore.loadingVegetables(["Okra 2.5 3.5", "Beans 10 2.8", "Celery 5.5 2.2", "Celery 0.5 2.5"]));
console.log(vegStore.rottingVegetable("Okra1", 1));
console.log(vegStore.rottingVegetable("Okra", 2.5));
console.log(vegStore.buyingVegetables(["Beans 8", "Celery 1.5"]));
console.log(vegStore.revision());

// Successfully added Okra, Beans, Celery
// Some quantity of the Okra has been removed.
// The entire quantity of the Okra has been removed.
// Great choice! You must pay the following amount $26.15.
// Available vegetables:
// Celery-4.5-$2.5
// Beans-2-$2.8
// Okra-0-$3.5
// The owner of the store is Jerrie Munro, and the location is 1463 Pette Kyosheta, Sofia.