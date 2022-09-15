function everyNth(array, step) {
    let result = [];
    let resultIndex = 0;
    
    for (let index = 0; index < array.length; index += step) {
        result[resultIndex++] = array[index];
    }

    return result;
}

console.log(everyNth(['5', '20', '31', '4', '20'], 2));
console.log(everyNth(['dsa','asd', 'test', 'tset'], 2));
console.log(everyNth(['1', '2','3', '4', '5'], 6));