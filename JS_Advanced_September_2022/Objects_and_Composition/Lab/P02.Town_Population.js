function townPopulation(input) {
    let register = input.map(element => {
        let data = element.split(' <-> ');
        let town = { name: data[0], population: Number(data[1]) };
        return town;
    }).reduce((result, town) => {
        if (result[town.name] === undefined) {
            result[town.name] = town.population;
        } else {
            result[town.name] += town.population;
        }

        return result;
    }, {});

    Object.entries(register).forEach(([name, population]) => console.log(`${name} : ${population}`));
}

townPopulation(
    ['Sofia <-> 1200000',
        'Montana <-> 20000',
        'New York <-> 10000000',
        'Washington <-> 2345000',
        'Las Vegas <-> 1000000']
);

townPopulation(
    ['Istanbul <-> 100000',
        'Honk Kong <-> 2100004',
        'Jerusalem <-> 2352344',
        'Mexico City <-> 23401925',
        'Istanbul <-> 1000']
);