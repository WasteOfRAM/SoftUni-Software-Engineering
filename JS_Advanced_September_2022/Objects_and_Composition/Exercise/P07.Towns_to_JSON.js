function townsToJson(input) {
    let [town, latitude, longitude] = input[0].split('|').filter(Boolean);
    town = town.trim();
    latitude = latitude.trim();
    longitude = longitude.trim();
    let towns = [];

    for (let i = 1; i < input.length; i++) {
        let [name, lat, lon] = input[i].split('|').filter(Boolean);
        name = name.trim();
        lat = Number(Number(lat).toFixed(2));
        lon = Number(Number(lon).toFixed(2));

        let obj = {
            [town]: name,
            [latitude]: lat,
            [longitude]: lon
        }

        towns.push(obj);
    }
    
    return JSON.stringify(towns);
}

console.log(townsToJson(['| Town | Latitude | Longitude |',
    '| Veliko Turnovo | 43.0757 | 25.6172 |',
    '| Monatevideo | 34.50 | 56.11 |']));

console.log(townsToJson(['| Town | Latitude | Longitude |',
    '| Sofia | 42.696552 | 23.32601 |',
    '| Beijing | 39.913818 | 116.363625 |']));