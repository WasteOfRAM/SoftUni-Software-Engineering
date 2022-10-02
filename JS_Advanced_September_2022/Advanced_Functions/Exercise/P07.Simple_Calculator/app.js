function calculator() {
    let numOne;
    let numTwo;
    let result;

    return {
        init: (numOneId, numTwoId, resultId) => {
            numOne = document.querySelector(numOneId);
            numTwo = document.querySelector(numTwoId);
            result = document.querySelector(resultId);
        },
        add: () => {
            let sum = Number(numOne.value) + Number(numTwo.value);
            result.value = sum;
        },
        subtract: () => {
            let sum = Number(numOne.value) - Number(numTwo.value);
            result.value = sum;
        }
    }
}

const calculate = calculator();
calculate.init('#num1', '#num2', '#result');