function cooking(number, ...operations) {
    let func = {
        'chop': x => x / 2,
        'dice': x => Math.sqrt(x),
        'spice': x => ++x,
        'bake': x => x * 3,
        'fillet': x => x *= 0.80
    }

    operations.forEach(operation => {
        number = func[operation](number); 
        console.log(number)
    });
}

cooking('32', 'chop', 'chop', 'chop', 'chop', 'chop');
cooking('9', 'dice', 'spice', 'chop', 'bake', 'fillet');