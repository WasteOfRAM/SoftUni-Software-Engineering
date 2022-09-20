function calorieObj(elements) {
    let obj = {};

    for (let i = 0; i < elements.length; i += 2) {
        obj[elements[i]] = Number(elements[i + 1]);
    }

    return obj;
}

console.log(calorieObj(['Yoghurt', '48', 'Rise', '138', 'Apple', '52']));
console.log(calorieObj(['Potato', '93', 'Skyr', '63', 'Cucumber', '18', 'Milk', '42']));