function timeToWalk(steps, stepLength, speed) {
    let distanceInMeters = steps * stepLength;
    let metersInSec = speed / 3.6;
    let time = distanceInMeters / metersInSec;
    let restCount = Math.floor(distanceInMeters / 500);

    let timeInMin = Math.floor(time / 60);
    let timeInSec = Number((time - (timeInMin * 60)).toFixed(0));
    let timeInHour = Math.floor(time / 3600);
    timeInMin += restCount;
    timeInHour += Math.floor(timeInMin / 60);
    timeInMin = timeInMin % 60;

    let hours = timeInHour < 10 ? `0${timeInHour}` : `${timeInHour}`;
    let min = timeInMin < 10 ? `0${timeInMin}` : `${timeInMin}`;
    let sec = timeInSec < 10 ? `0${timeInSec}` : `${timeInSec}`;

    console.log(`${hours}:${min}:${sec}`);
}

timeToWalk(4000, 0.60, 5);
timeToWalk(2564, 0.70, 5.5);