class footballTeam {
    constructor(clubName, country) {
        this.clubName = clubName;
        this.country = country;
        this.invitedPlayers = [];
    }

    newAdditions(footballPlayers) {
        let players = [];

        for (const playerInput of footballPlayers) {
            let playerData = playerInput.split("/");
            let name = playerData[0];
            let age = playerData[1];
            let playerValue = playerData[2];

            let currentPlayer = this.invitedPlayers.find(p => p.name === name);
            if (currentPlayer === undefined) {
                this.invitedPlayers.push({
                    name,
                    age,
                    playerValue, 

                    toString: function () {
                        return `Player ${this.name}-${this.playerValue}`;
                    }
                });

                players.push(name);
            } else {
                if (currentPlayer.playerValue < playerValue) {
                    currentPlayer.playerValue = playerValue;
                }
            }
        }

        return "You successfully invite " + players.join(", ") + ".";
    }

    signContract(selectedPlayer) {
        let selectedName = selectedPlayer.split("/")[0];
        let offer = selectedPlayer.split("/")[1];

        let currentPlayer = this.invitedPlayers.find(p => p.name === selectedName);

        if (currentPlayer === undefined) {
            throw new Error(`${selectedName} is not invited to the selection list!`);
        }

        if (offer < currentPlayer.playerValue) {
            throw new Error(`The manager's offer is not enough to sign a contract with ${selectedName}, ${currentPlayer.playerValue - offer} million more are needed to sign the contract!`);
        }

        currentPlayer.playerValue = "Bought";

        return `Congratulations! You sign a contract with ${selectedName} for ${offer} million dollars.`;
    }

    ageLimit(name, age) {
        let currentPlayer = this.invitedPlayers.find(p => p.name === name);
        if (currentPlayer === undefined) {
            throw new Error(`${name} is not invited to the selection list!`);
        }

        if (currentPlayer.age < age) {
            let ageDifference = age - currentPlayer.age;

            if (ageDifference < 5) {
                return `${name} will sign a contract for ${ageDifference} years with ${this.clubName} in ${this.country}!`
            }

            return `${name} will sign a full 5 years contract for ${this.clubName} in ${this.country}!`;
        }

        return `${name} is above age limit!`;
    }

    transferWindowResult() {
        this.invitedPlayers.sort((a, b) => a.name.localeCompare(b.name));

        let str = `Players list:\n`;
        
        str += this.invitedPlayers.join("\n");

        return str;
    }
}

let fTeam = new footballTeam("Barcelona", "Spain");
console.log(fTeam.newAdditions(["Kylian Mbappé/23/160", "Lionel Messi/35/50", "Pau Torres/25/52"]));
console.log(fTeam.signContract("Kylian Mbappé/240"));
console.log(fTeam.ageLimit("Kylian Mbappé", 30));
console.log(fTeam.transferWindowResult());
