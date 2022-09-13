function previousDay(year, month, day) {
    let date = new Date(year, month - 1, day - 1);

    let previousDayDate = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;

    return previousDayDate;
}

console.log(previousDay(2016, 9, 30));
console.log(previousDay(2016, 10, 1));