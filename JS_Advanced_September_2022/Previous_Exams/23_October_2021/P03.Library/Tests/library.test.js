const { assert, expect } = require("chai");
const { describe, it } = require("mocha");
const library = require("../library");

describe("Tests", () => {
    describe("calcPriceOfBook function", () => {
        it("nameOfBook input validation. If not String throw Error", () => {
            expect(() => library.calcPriceOfBook(2000, 2000)).throws("Invalid input");
        });

        it("year input validation. If not Number throw Error. String", () => {
            assert.throws(() => library.calcPriceOfBook("Name", "2000"));
        });

        it("year input validation. If not Number throw Error. Float", () => {
            assert.throws(() => library.calcPriceOfBook("Name", 14.86684));
        });

        it("Valid inputs year > 1980", () => {
            let actual = library.calcPriceOfBook("Torronto", 1981);
            let expected = "Price of Torronto is 20.00";

            expect(actual).to.be.equal(expected);
        });

        it("Valid inputs year = 1980", () => {
            let actual = library.calcPriceOfBook("Torronto", 1980);
            let expected = "Price of Torronto is 10.00";

            expect(actual).to.be.equal(expected);
        });

        it("Valid inputs year <=> 1980", () => {
            let actual = library.calcPriceOfBook("Torronto", 1777);
            let expected = "Price of Torronto is 10.00";

            expect(actual).to.be.equal(expected);
        });
    });

    describe("findBook function tests", () => {
        it("empty arr throws error", () => {
            expect(() => library.findBook([], "Torronto")).throws("No books currently available");
        });

        it("Book not present in the arr", () => {
            let testBookArr = ["Troy", "Life Style", "Torronto",];

            let actual = library.findBook(testBookArr, "A Real book name");
            let expected = "The book you are looking for is not here!";

            expect(actual).to.be.equal(expected);
        });

        it("Book present in the arr", () => {
            let testBookArr = ["Troy", "Life Style", "Torronto",];

            let actual = library.findBook(testBookArr, "Life Style");
            let expected = "We found the book you want.";

            expect(actual).to.be.equal(expected);
        });
    });

    describe("arrangeTheBooks function tests", () => {
        it("countBooks is not a number", () => {
            expect(() => library.arrangeTheBooks("5")).throws("Invalid input");
        });

        it("countBooks is a negative number", () => {
            expect(() => library.arrangeTheBooks(-1)).throws("Invalid input");
        });

        it("countBooks is a float", () => {
            expect(() => library.arrangeTheBooks(14.333333333)).throws("Invalid input");
        });

        it("No books", () => {
            let actual = library.arrangeTheBooks(0);
            let expected = "Great job, the books are arranged.";

            expect(actual).to.be.equal(expected);
        });

        it("All books are arranged", () => {
            let actual = library.arrangeTheBooks(40);
            let expected = "Great job, the books are arranged.";

            expect(actual).to.be.equal(expected);
        });

        it("Insufficient space", () => {
            let actual = library.arrangeTheBooks(41);
            let expected = "Insufficient space, more shelves need to be purchased.";

            expect(actual).to.be.equal(expected);
        });
    });
});