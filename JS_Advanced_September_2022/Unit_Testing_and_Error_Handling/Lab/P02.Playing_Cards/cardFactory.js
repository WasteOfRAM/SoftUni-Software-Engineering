function cardFactory(face, suit) {
    let validCardFaces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];
    let validCardSuits = {
        'S': '\u2660',
        'H': '\u2665',
        'D': '\u2666',
        'C': '\u2663'
    }

    if(!validCardFaces.includes(face) || !validCardSuits[suit]){
        throw new Error('Error');
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

console.log(cardFactory('A', 'S').toString());
console.log(cardFactory('10', 'H').toString());
console.log(cardFactory('1', 'C').toString());
console.log(cardFactory('1', 'S').toString());