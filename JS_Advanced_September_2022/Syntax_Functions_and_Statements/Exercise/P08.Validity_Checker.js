function validityChecker(x1, y1, x2, y2) {
    
    let state1 = isValid(x1, y1, 0, 0) ? 'valid' : 'invalid';
    let result1 = `{${x1}, ${y1}} to {0, 0} is ${state1}`;
    console.log(result1);

    let state2 = isValid(x2, y2, 0, 0) ? 'valid' : 'invalid';
    let result2 = `{${x2}, ${y2}} to {0, 0} is ${state2}`;
    console.log(result2);

    let state3 = isValid(x1, y1, x2, y2) ? 'valid' : 'invalid';
    let result3 = `{${x1}, ${y1}} to {${x2}, ${y2}} is ${state3}`;
    console.log(result3);

    function isValid(x1, y1, x2, y2) {
        let distance = Math.sqrt(((x2 - x1) ** 2) + ((y2 - y1) ** 2))

        return Number.isInteger(distance); 
    }
}

validityChecker(3, 0, 0, 4);
validityChecker(2, 1, 1, 1);