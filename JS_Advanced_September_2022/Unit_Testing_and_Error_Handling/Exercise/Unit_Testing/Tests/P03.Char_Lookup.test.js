const { assert } = require("chai");
const { describe, it } = require("mocha");
const lookupChar = require("../P03.Char_Lookup");

describe("Tests", () => {
    it("Testing with invalid first parametar type", () => {
        assert.equal(lookupChar(14, 14), undefined);
    });

    it("Testing with invalid second parametar type", () => {
        assert.equal(lookupChar("String", "String"), undefined);
    });

    it("Testing second parametar with float", () => {
        assert.equal(lookupChar("String", 89.14), undefined);
    });

    it("Testing index < 0", () => {
        assert.equal(lookupChar("String", -1), "Incorrect index");
    });

    it("Testing index > string.length", () => {
        assert.equal(lookupChar("String", 14), "Incorrect index");
    });

    it("Testing index === string.length", () => {
        assert.equal(lookupChar("String", 6), "Incorrect index");
    });

    it("Testing with index === 0", () => {
        assert.equal(lookupChar("String", 0), "S");
    });

    it("Testing with index === string.length - 1", () => {
        assert.equal(lookupChar("String", 5), "g");
    });
});