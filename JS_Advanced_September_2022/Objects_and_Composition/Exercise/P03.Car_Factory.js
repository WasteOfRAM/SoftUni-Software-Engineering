function carFactory(requirements) {
    let car = { model: requirements.model};

    const engineTypes = {
        'small': { power: 90, volume: 1800 },
        'normal': { power: 120, volume: 2400 },
        'monster': { power: 200, volume: 3500 }
    }

    const carriageTypes = {
        'hatchback': { type: 'hatchback', color: undefined },
        'coupe': { type: 'coupe', color: undefined }
    }

    if (requirements.power <= 90) {
        car.engine = engineTypes.small;
    } else if (requirements.power <= 120) {
        car.engine = engineTypes.normal;
    } else {
        car.engine = engineTypes.monster;
    }

    car.carriage = carriageTypes[requirements.carriage];
    car.carriage.color = requirements.color;

    let wSize = requirements.wheelsize % 2 === 0 ? requirements.wheelsize - 1 : requirements.wheelsize;
    const wheelsSet = Array(4).fill(wSize);
    car.wheels = wheelsSet;

    return car;
}

console.log(carFactory({
    model: 'VW Golf II',
    power: 90,
    color: 'blue',
    carriage: 'hatchback',
    wheelsize: 14
}));

console.log(carFactory({
    model: 'Opel Vectra',
    power: 110,
    color: 'grey',
    carriage: 'coupe',
    wheelsize: 17
}));

console.log(carFactory({
    model: 'Opel Vectra Turbo',
    power: 121,
    color: 'grey',
    carriage: 'coupe',
    wheelsize: 17
}));