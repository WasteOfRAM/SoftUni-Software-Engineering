function subSum(array, startIndex, endIndex) {
    if (!Array.isArray(array)) {
        return NaN;
    }

    if (startIndex < 0) {
        startIndex = 0;
    }

    if (endIndex >= array.length) {
        endIndex = array.length - 1;
    }

    return array.slice(startIndex, endIndex + 1).map(Number).reduce((sum, current) => sum + current, 0);
}

console.log(subSum([10, 20, 30, 40, 50, 60], 3, 300)); // 150
console.log(subSum([1.1, 2.2, 3.3, 4.4, 5.5], -3, 1)); // 3.3
console.log(subSum([10, 'twenty', 30, 40], 0, 2));     // NaN
console.log(subSum([], 1, 2));                         // 0
console.log(subSum('text', 0, 2));                     // NaN