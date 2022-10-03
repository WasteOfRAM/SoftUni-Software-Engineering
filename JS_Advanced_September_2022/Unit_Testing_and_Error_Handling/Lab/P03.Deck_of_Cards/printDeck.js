function printDeckOfCards(cards) {
    function cardFactory(face, suit) {
        let validCardFaces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];
        let validCardSuits = {
            'S': '\u2660',
            'H': '\u2665',
            'D': '\u2666',
            'C': '\u2663'
        }

        if (!validCardFaces.includes(face) || !validCardSuits[suit]) {
            throw new Error(`Invalid card: ${face + suit}`);
        }

        suit = validCardSuits[suit];

        return {
            face,
            suit,
            toString() {
                return this.face + this.suit;
            }
        }
    }

    let deck = [];

    for (const cardInput of cards) {
        let face;
        let suit;
        if (cardInput.length === 3) {
            face = cardInput[0] + cardInput[1];
            suit = cardInput[2];
        } else {
            face = cardInput[0];
            suit = cardInput[1];
        }

        try {
            let card = cardFactory(face, suit);
            deck.push(card.toString());

        } catch (error) {
            console.log(error.message);
            return;
        }
    }

    console.log(deck.join(' '));
}

printDeckOfCards(['AS', '10D', 'KH', '2C']);
printDeckOfCards(['5S', '3D', 'QD', '1C']);