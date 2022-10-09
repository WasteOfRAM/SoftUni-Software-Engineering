function register(inputArr) {
    let carRegister = new Map();

    for (const input of inputArr) {
        let carData = input.split(" | ");
        let carBrand = carData[0];
        let carModel = carData[1];
        let producedCars = Number(carData[2]);

        if (!carRegister[carBrand]) {
            carRegister[carBrand] = new Map();
        }

        if (!carRegister[carBrand][carModel]) {
            carRegister[carBrand][carModel] = 0;
        }

        carRegister[carBrand][carModel] += producedCars;
    }

    for (const [brand, models] of Object.entries(carRegister)) {
        console.log(brand);
        for (const [model, quantity] of Object.entries(models)) {
            console.log(`###${model} -> ${quantity}`);
        }
    }
}

register(['Audi | Q7 | 1000',
    'Audi | Q6 | 100',
    'BMW | X5 | 1000',
    'BMW | X6 | 100',
    'Citroen | C4 | 123',
    'Volga | GAZ-24 | 1000000',
    'Lada | Niva | 1000000',
    'Lada | Jigula | 1000000',
    'Citroen | C4 | 22',
    'Citroen | C5 | 10']);