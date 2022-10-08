class Ticket {
    constructor(destination, price, status) {
        this.destination = destination,
        this.price = price,
        this.status = status
    }
}

function sovle(arr, criteria) {
    let sortedArr = [];
    
    for (const tiket of arr) {
        let tiketData = tiket.split("|");
        sortedArr.push(new Ticket(tiketData[0], Number(tiketData[1]), tiketData[2]));
    }

    if (criteria === 'price') {
        sortedArr.sort((a, b) => a.price - b.price);
    } else {
        sortedArr.sort((a, b) => a[criteria] > [criteria] ? 1 : b[criteria] > a[criteria] ? -1 : 0);
    }

    return sortedArr;
}

let test1 = sovle(['Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'],
    'destination');

let test2 = sovle(['Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'],
    'status');

console.log(test1);
console.log(test2);