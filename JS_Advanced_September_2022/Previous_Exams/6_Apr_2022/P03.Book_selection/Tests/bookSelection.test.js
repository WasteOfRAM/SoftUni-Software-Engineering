const { assert, expect } = require("chai");
const { describe, it } = require("mocha");
const bookSelection = require("../bookSelection");

describe("Book Selection tests", () => {
    describe("isGenreSuitable function tests", () => {
        it("inputs: Thriller, 12", () => {
            let actuial = bookSelection.isGenreSuitable("Thriller", 12);
            let expected = `Books with Thriller genre are not suitable for kids at 12 age`;

            assert.equal(actuial, expected);
        });

        it("inputs: Horror, 12", () => {
            let actuial = bookSelection.isGenreSuitable("Horror", 12);
            let expected = `Books with Horror genre are not suitable for kids at 12 age`;

            assert.equal(actuial, expected);
        });

        it("inputs: Thriller, 13", () => {
            let actuial = bookSelection.isGenreSuitable("Thriller", 13);
            let expected = `Those books are suitable`;

            assert.equal(actuial, expected);
        });

        it("inputs: Horror, 13", () => {
            let actuial = bookSelection.isGenreSuitable("Horror", 13);
            let expected = `Those books are suitable`;

            assert.equal(actuial, expected);
        });

        it("inputs: Comedy, 13", () => {
            let actuial = bookSelection.isGenreSuitable("Comedy", 7);
            let expected = `Those books are suitable`;

            assert.equal(actuial, expected);
        });
    });

    describe("isItAffordable function tests", () => {
        it("Input validation with wrong price input. Input must be Number.", () => {
            assert.throws(() => bookSelection.isItAffordable("24345", 424), "Invalid input");
            assert.throws(() => bookSelection.isItAffordable([24345], 424), "Invalid input");
            assert.throws(() => bookSelection.isItAffordable([24345], {}), "Invalid input");
        });

        it("Input validation with wrong budget input. Input must be Number.", () => {
            assert.throws(() => bookSelection.isItAffordable(24345, "424"), "Invalid input");
            assert.throws(() => bookSelection.isItAffordable(24345, [424]), "Invalid input");
            assert.throws(() => bookSelection.isItAffordable(24345, {}), "Invalid input");
        });

        it("buget < price", () => {
            let actual = bookSelection.isItAffordable(2, 1);
            let expected = "You don't have enough money";

            assert.equal(actual, expected);
        });

        it("buget = price", () => {
            let actual = bookSelection.isItAffordable(2, 2);
            let expected = `Book bought. You have 0$ left`;;

            assert.equal(actual, expected);
        });

        it("buget > price", () => {
            let actual = bookSelection.isItAffordable(2, 5);
            let expected = `Book bought. You have 3$ left`;;

            assert.equal(actual, expected);
        });
    });

    describe("suitableTitles function tests", () => {
        it("Invalid input for books array", () => {
            assert.throws(() => bookSelection.suitableTitles({}, "Something"), "Invalid input");
            assert.throws(() => bookSelection.suitableTitles(14, "Something"), "Invalid input");
            assert.throws(() => bookSelection.suitableTitles(true, "Something"), "Invalid input");
        });

        it("Invalid input for wantedGenre string", () => {
            assert.throws(() => bookSelection.suitableTitles([], 14), "Invalid input");
            assert.throws(() => bookSelection.suitableTitles([], ["14"]), "Invalid input");
            assert.throws(() => bookSelection.suitableTitles([], {}), "Invalid input");
        });

        it("Correct inputs", () => {
            let inputArr = [{ title: "The Da Vinci Code", genre: "Thriller" }, { title: "Good omens", genre: "Fantasy" }];
            let expected = ["Good omens"];
            let actual = bookSelection.suitableTitles(inputArr, "Fantasy");

            expect(actual).deep.equal(expected);
        });
    });
});