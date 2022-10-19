class SummerCamp {
    constructor(organizer, location) {
        this.organizer = organizer;
        this.location = location;
        this.priceForTheCamp = { "child": 150, "student": 300, "collegian": 500 };
        this.listOfParticipants = [];
    }

    registerParticipant(name, condition, money) {
        if (!this.priceForTheCamp[condition]) {
            throw new Error("Unsuccessful registration at the camp.");
        }

        if (this.listOfParticipants.some(p => p.name === name)) {
            return `The ${name} is already registered at the camp.`;
        }

        if (money < this.priceForTheCamp[condition]) {
            return `The money is not enough to pay the stay at the camp.`;
        }

        this.listOfParticipants.push({
            name: name,
            condition: condition,
            power: 100,
            wins: 0,

            toString: function () {
                return `${this.name} - ${this.condition} - ${this.power} - ${this.wins}`;
            }
        });

        return `The ${name} was successfully registered.`;
    }

    unregisterParticipant(name) {
        let participant = this.listOfParticipants.find(p => p.name === name);
        if (participant === undefined) {
            throw new Error(`The ${name} is not registered in the camp.`);
        }

        this.listOfParticipants.splice(this.listOfParticipants.indexOf(participant), 1);

        return `The ${name} removed successfully.`;
    }

    timeToPlay(typeOfGame, participant1, participant2) {
        let playerOne, playerTwo;

        if (typeOfGame === "WaterBalloonFights") {
            playerOne = this.listOfParticipants.find(p => p.name === participant1);
            playerTwo = this.listOfParticipants.find(p => p.name === participant2);

            if (playerOne === undefined || playerTwo === undefined) {
                throw new Error(`Invalid entered name/s.`);
            }

            if (playerOne.condition !== playerTwo.condition) {
                throw new Error(`Choose players with equal condition.`);
            }

            let winner = playerOne.power > playerTwo.power ? playerOne : playerOne.power < playerTwo.power ? playerTwo : undefined;

            if (winner === undefined) {
                return `There is no winner.`;
            }

            winner.wins++;

            return `The ${winner.name} is winner in the game ${typeOfGame}.`;

        } else {
            playerOne = this.listOfParticipants.find(p => p.name === participant1);

            if (playerOne === undefined) {
                throw new Error(`Invalid entered name/s.`);
            }

            playerOne.power += 20;
            return `The ${playerOne.name} successfully completed the game ${typeOfGame}.`;
        }
    }

    toString() {
        let str = `${this.organizer} will take ${this.listOfParticipants.length} participants on camping to ${this.location}\n`;
        str += this.listOfParticipants.sort((a, b) => b.wins - a.wins).join("\n");

        return str;
    }
}

const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");

console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
console.log(summerCamp.registerParticipant("Goergi Petarson", "student", 300));
console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Goergi Petarson"));

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 200));
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.registerParticipant("Leila Wolfe", "childd", 200));

// The money is not enough to pay the stay at the camp.
// The Petar Petarson was successfully registered.
// The Petar Petarson is already registered at the camp.
// Uncaught Error: Unsuccessful registration at the camp.

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.unregisterParticipant("Petar"));
// console.log(summerCamp.unregisterParticipant("Petar Petarson"));

// The Petar Petarson was successfully registered.
// Uncaught Error: The Petar is not registered in the camp.
// The Petar Petarson removed successfully.

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.timeToPlay("Battleship", "Petar Petarson"));
// console.log(summerCamp.registerParticipant("Sara Dickinson", "child", 200));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Sara Dickinson"));
// console.log(summerCamp.registerParticipant("Dimitur Kostov", "student", 300));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Dimitur Kostov"));

// The Petar Petarson was successfully registered.
// The Petar Petarson successfully completed the game Battleship.
// The Sara Dickinson was successfully registered.
// Uncaught Error: Choose players with equal condition.
// The Dimitur Kostov was successfully registered.
// The Petar Petarson is winner in the game WaterBalloonFights.

// const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
// console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
// console.log(summerCamp.timeToPlay("Battleship", "Petar Petarson"));
// console.log(summerCamp.registerParticipant("Sara Dickinson", "child", 200));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Sara Dickinson"));
// console.log(summerCamp.registerParticipant("Dimitur Kostov", "student", 300));
// console.log(summerCamp.timeToPlay("WaterBalloonFights", "Petar Petarson", "Dimitur Kostov"));

// console.log(summerCamp.toString());

// The Petar Petarson was successfully registered.
// The Petar Petarson successfully completed the game Battleship.
// The Sara Dickinson was successfully registered.
// Uncaught Error: Choose players with equal condition.
// The Dimitur Kostov was successfully registered.
// The Petar Petarson is winner in the game WaterBalloonFights.
// Jane Austen will take 3 participants on camping to Pancharevo Sofia 1137, Bulgaria
// Petar Petarson - student - 120 - 1
// Sara Dickinson - child - 100 - 0
// Dimitur Kostov - student - 100 - 0
