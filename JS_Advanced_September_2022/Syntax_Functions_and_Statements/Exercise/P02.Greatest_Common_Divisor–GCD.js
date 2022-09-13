function GCD(a, b){
    while (a !== 0 && b !== 0) {
        if (a > b) {
            a %= b;
        }
        else{
            b %= a;
        }
    }

    return a | b;
}

console.log(GCD(15, 5));
console.log(GCD(2154, 458));
