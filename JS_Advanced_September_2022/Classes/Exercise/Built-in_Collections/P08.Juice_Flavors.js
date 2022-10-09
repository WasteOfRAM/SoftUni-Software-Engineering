function solve(inputArr) {
    let juiceQuantity  = new Map();
    let bottles = new Map();

    for (const juice of inputArr) {
        let juiceData = juice.split(" => ");
        let juiceType = juiceData[0];
        let juiceAmount = Number(juiceData[1]);

        if(!juiceQuantity[juiceType]){
            juiceQuantity[juiceType] = 0;
        }

        juiceQuantity[juiceType] += juiceAmount;

        while (juiceQuantity[juiceType] >= 1000) {
            if (!bottles[juiceType]) {
                bottles[juiceType] = 0;
            }

            juiceQuantity[juiceType] -= 1000;
            bottles[juiceType]++;
        }
    }

    for (const [type, count] of Object.entries(bottles)) {
        console.log(`${type} => ${count}`);
    }
}

solve(['Orange => 2000',
'Peach => 1432',
'Banana => 450',
'Peach => 600',
'Strawberry => 549']);

solve(['Kiwi => 234',
'Pear => 2345',
'Watermelon => 3456',
'Kiwi => 4567',
'Pear => 5678',
'Watermelon => 6789']);