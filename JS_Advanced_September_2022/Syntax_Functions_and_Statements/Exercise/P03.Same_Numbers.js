function sameNumbers(number) {
    let num = number;
    let controlNumber = num % 10;
    let areSame = true;
    let sum = 0;

    while (num !== 0) {
        let lastNum = num % 10;
        num = Math.trunc(num / 10);

        if (lastNum !== controlNumber) {
            areSame = false;
        }

        sum += lastNum;
    }

    console.log(areSame);
    console.log(sum);
}

sameNumbers(2222222);
sameNumbers(1234);