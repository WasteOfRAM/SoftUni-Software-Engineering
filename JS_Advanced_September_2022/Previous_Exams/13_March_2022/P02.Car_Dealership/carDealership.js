class CarDealership {
    constructor(name) {
        this.name = name;
        this.availableCars = [];
        this.soldCars = [];
        this.totalIncome = 0;
    }

    addCar(model, horsepower, price, mileage) {
        if (!model || horsepower < 0 || price < 0 || mileage < 0) {
            throw new Error("Invalid input!");
        };

        this.availableCars.push({
            model: model,
            horsepower: horsepower,
            price: Number(price),
            mileage: Number(mileage),

            toString: function () {
                return `---${this.model} - ${this.horsepower} HP - ${this.mileage.toFixed(2)} km - ${this.price.toFixed(2)}$`;
            }
        });

        return `New car added: ${model} - ${horsepower} HP - ${Number(mileage).toFixed(2)} km - ${Number(price).toFixed(2)}$`;
    }

    sellCar(model, desiredMileage) {
        let car = this.availableCars.find(c => c.model === model);

        if (car === undefined) {
            throw new Error(`${model} was not found!`);
        }

        let soldPrice = car.price;

        if (car.mileage > desiredMileage) {
            let difference = car.mileage - desiredMileage;

            if (difference <= 40000) {
                soldPrice = car.price * 0.95;
            } else {
                soldPrice = car.price * 0.90;
            }
        }

        this.totalIncome += soldPrice;
        this.soldCars.push({
            model: model,
            horsepower: car.horsepower,
            soldPrice: soldPrice,

            toString: function () {
                return `---${this.model} - ${this.horsepower} HP - ${this.soldPrice.toFixed(2)}$`;
            }
        });

        this.availableCars.splice(this.availableCars.indexOf(car), 1);

        return `${model} was sold for ${soldPrice.toFixed(2)}$`;
    }

    currentCar() {
        if (this.availableCars.length === 0) {
            return "There are no available cars";
        }

        let result = "-Available cars:\n";
        result += this.availableCars.join(`\n`);
        return result;
    }

    salesReport(criteria) {
        if (criteria === "horsepower") {
            this.soldCars.sort((a, b) => b.horsepower - a.horsepower);
        } else if (criteria === "model") {
            this.soldCars.sort((a, b) => a.model.localeCompare(b.model));
        } else {
            throw new Error("Invalid criteria!");
        }

        let result = `-${this.name} has a total income of ${this.totalIncome.toFixed(2)}$\n`;
        result += `-${this.soldCars.length} cars sold:\n`;
        result += this.soldCars.join("\n");

        return result;
    }
}

// let dealership = new CarDealership('SoftAuto');
// console.log(dealership.addCar('Toyota Corolla', 100, 3500, 190000));
// console.log(dealership.addCar('Mercedes C63', 300, 29000, 187000));
// console.log(dealership.addCar('', 120, 4900, 240000));

// New car added: Toyota Corolla - 100 HP - 190000.00 km - 3500.00$
// New car added: Mercedes C63 - 300 HP - 187000.00 km - 29000.00$
// Uncaught Error Error: Invalid input!

// let dealership = new CarDealership('SoftAuto');
// dealership.addCar('Toyota Corolla', 100, 3500, 190000);
// dealership.addCar('Mercedes C63', 300, 29000, 187000);
// dealership.addCar('Audi A3', 120, 4900, 240000);
// console.log(dealership.sellCar('Toyota Corolla', 230000));
// console.log(dealership.sellCar('Mercedes C63', 110000));

// Toyota Corolla was sold for 3500.00$
// Mercedes C63 was sold for 26100.00$

// let dealership = new CarDealership('SoftAuto');
// dealership.addCar('Toyota Corolla', 100, 3500, 190000);
// dealership.addCar('Mercedes C63', 300, 29000, 187000);
// dealership.addCar('Audi A3', 120, 4900, 240000);
// console.log(dealership.currentCar());

// -Available cars:
// ---Toyota Corolla - 100 HP - 190000.00 km - 3500.00$
// ---Mercedes C63 - 300 HP - 187000.00 km - 29000.00$
// ---Audi A3 - 120 HP - 240000.00 km - 4900.00$

let dealership = new CarDealership('SoftAuto');
dealership.addCar('Toyota Corolla', 100, 3500, 190000);
dealership.addCar('Mercedes C63', 300, 29000, 187000);
dealership.addCar('Audi A3', 120, 4900, 240000);
dealership.sellCar('Toyota Corolla', 230000);
dealership.sellCar('Mercedes C63', 110000);
console.log(dealership.salesReport('model'));

// -SoftAuto has a total income of 29600.00$
// -2 cars sold:
// ---Mercedes C63 - 300 HP - 26100.00$
// ---Toyota Corolla - 100 HP - 3500.00$
