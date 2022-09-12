function solve(elements) {

    aggregate(elements, 0, (x, y) => x + y);
    aggregate(elements, 0, (x, y) => x + 1 / y);
    aggregate(elements, '', (x, y) => x + y);

    function aggregate(arr, initValue, func){
        let val = initValue;
        
        for (let i = 0; i < arr.length; i++) {
            val = func(val, arr[i]);
        }

        console.log(val);
    }
}

solve([1, 2, 3]);
solve([2, 4, 8, 16]);