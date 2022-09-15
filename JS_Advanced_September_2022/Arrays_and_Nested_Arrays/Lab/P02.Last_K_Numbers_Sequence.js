function lastKNumbers(n, k) {
    let sum = 1;
    let start = 0;
    let end = 1;
    let sequence = [1];

    while (end < n) {
        if (end - start > k) {
            sum -= sequence[start];
            start++;
        }

        sequence[end] = sum;
        sum += sequence[end];
        end++;
    }

    return sequence;
}

console.log(lastKNumbers(6, 3));
console.log(lastKNumbers(8, 2));