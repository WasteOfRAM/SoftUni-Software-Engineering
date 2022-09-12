function solve(strOne, strTwo, strThree){

    let strOneLength = strOne.length;
    let strTwoLength = strTwo.length;
    let strThreeLength = strThree.length;

    let sum = strOneLength + strTwoLength + strThreeLength;
    let average = Math.floor(sum / 3);

    console.log(sum);
    console.log(average);
}

solve('chocolate', 'ice cream', 'cake'); // 22 7
solve('pasta', '5', '22.3'); // 10 3