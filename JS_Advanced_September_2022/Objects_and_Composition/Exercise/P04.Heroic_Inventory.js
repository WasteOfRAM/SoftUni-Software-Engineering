function invetory(heroes) {
    let heroesRegister = [];

    heroes.forEach(hero => {
        let [name, level, inventory] = hero.split(' / ');
        let heroInfo = {
            name: name,
            level: Number(level),
            items: inventory ? inventory.split(', ') : []
        }
        
        heroesRegister.push(heroInfo);
    });

    return JSON.stringify(heroesRegister);
}

console.log(invetory(['Isacc / 25 / Apple, GravityGun',
    'Derek / 12 / BarrelVest, DestructionSword',
    'Hes / 1 / Desolator, Sentinel, Antara']));

console.log(invetory(['Jake / 1000 / Gauss, HolidayGrenade']));

console.log(invetory(['Jake / 1000 / ']));