const { assert } = require("chai");
const { describe, it } = require("mocha");
const isOddOrEven = require("../P02.Even_Or_Odd");

describe("Tests", () => {

    it("Testing with incorect type. Number", () => {
        assert.equal(isOddOrEven(14), undefined);
    });

    it("Testing with incorect type. ['string', '14']", () => {
        assert.equal(isOddOrEven(['string', '14']), undefined);
    });

    it("Testing with even string", () => {
        assert.equal(isOddOrEven("Four"), "even");
    });

    it("Testing with odd string", () => {
        assert.equal(isOddOrEven("Two"), "odd");
    });
});