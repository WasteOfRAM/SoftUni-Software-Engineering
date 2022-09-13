function roadRadar(carSpeed, area) {
    let speedLimit = {'motorway': 130, 'interstate': 90, 'city': 50, 'residential': 20};

    let result;

    if(carSpeed <= speedLimit[area]){
        result = `Driving ${carSpeed} km/h in a ${speedLimit[area]} zone`
    }else{
        let overSpeed = carSpeed - speedLimit[area];
        let status;

        if (overSpeed > 40) {
            status = 'reckless driving';
        } else if (overSpeed > 20 ) {
            status = 'excessive speeding';
        } else {
            status = 'speeding';
        }

        result = `The speed is ${carSpeed - speedLimit[area]} km/h faster than the allowed speed of ${speedLimit[area]} - ${status}`;
    }

    return result;
}

console.log(roadRadar(40, 'city'));
console.log(roadRadar(21, 'residential'));
console.log(roadRadar(120, 'interstate'));
console.log(roadRadar(200, 'motorway'));