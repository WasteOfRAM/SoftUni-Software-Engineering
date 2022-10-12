function solution() {
    class Balloon {
        constructor(color, hasWeight) {
            this.color = color;
            this.hasWeight = Number(hasWeight);
        }
    }

    class PartyBalloon extends Balloon {
        constructor(color, hasWeight, ribbonColor, ribbonLength) {
            super(color, hasWeight);
            this.ribbonColor = ribbonColor;
            this.ribbonLength = ribbonLength;
            this._ribbon = { color: ribbonColor, length: ribbonLength };
        }

        get ribbon() {
            return this._ribbon;
        }
    }

    class BirthdayBalloon extends PartyBalloon{
        constructor(ribbonColor, ribbonLength, color, hasWeight, text){
            super(ribbonColor, ribbonLength, color, hasWeight);
            this._text = text;
        }

        get text(){
            return this._text;
        }
    }

    return {
        Balloon,
        PartyBalloon,
        BirthdayBalloon
    }
}


let classes = solution();
let testBalloon = new classes.Balloon("yellow", 20.5);
let testPartyBalloon = new classes.PartyBalloon("yellow", 20.5, "red", 10.25);
let ribbon = testPartyBalloon.ribbon;
console.log(testBalloon);
console.log(testPartyBalloon);
console.log(ribbon);
