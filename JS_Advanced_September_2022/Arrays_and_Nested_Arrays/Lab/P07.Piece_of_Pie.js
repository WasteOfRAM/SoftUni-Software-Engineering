function piceOfPie(arr, start, end) {
    let startIndex = arr.findIndex(p => p === start);
    let endIndex = arr.findIndex(p => p === end);

    return arr.slice(startIndex, endIndex + 1);
}

console.log(piceOfPie(['Pumpkin Pie', 'Key Lime Pie', 'Cherry Pie', 'Lemon Meringue Pie', 'Sugar Cream Pie'], 'Key Lime Pie', 'Lemon Meringue Pie'));
console.log(piceOfPie(['Apple Crisp', 'Mississippi Mud Pie', 'Pot Pie', 'Steak and Cheese Pie', 'Butter Chicken Pie', 'Smoked Fish Pie'], 'Pot Pie', 'Smoked Fish Pie'));