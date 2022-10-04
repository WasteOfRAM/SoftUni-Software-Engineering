const { assert } = require("chai");
const { describe, it } = require("mocha");
//const { addFive, subtractTen, sum } = require("../P04.Math_Enforcer");
const mathEnforcer = require("../P04.Math_Enforcer");

describe("mathEnforcer", () => {
    describe("addFive", () => {
        it("With invalid parameter. String", () => {
            assert.equal(mathEnforcer.addFive("Not a number"), undefined);
        });

        it("With invalid parameter. function", () => {
            assert.equal(mathEnforcer.addFive(function () { 5 + 5 }), undefined);
        });

        it("With valid parameter. Int", () => {
            assert.equal(mathEnforcer.addFive(14), 19);
        });

        it("With valid parameter. float", () => {
            assert.closeTo(mathEnforcer.addFive(5.14), 10.14, 0.1);
        });

        it("Adding negative number Int", () => {
            assert.equal(mathEnforcer.addFive(-14), -9);
        });

        it("Adding negative number float", () => {
            assert.closeTo(mathEnforcer.addFive(-7.55), -2.55, 0.1);
        });
    });

    describe("subtractTen", () => {
        it("With invalid parameter. String", () => {
            assert.equal(mathEnforcer.subtractTen("Not a number"), undefined);
        });

        it("With invalid parameter. function", () => {
            assert.equal(mathEnforcer.subtractTen(function () { 5 + 5 }), undefined);
        });

        it("With valid parameter. Int", () => {
            assert.equal(mathEnforcer.subtractTen(14), 4);
        });

        it("With valid parameter. float", () => {
            assert.closeTo(mathEnforcer.subtractTen(10.14), 0.14, 0.1);
        });

        it("Subtracting negative number Int", () => {
            assert.equal(mathEnforcer.subtractTen(-14), -24);
        });

        it("Subtracting negative number float", () => {
            assert.closeTo(mathEnforcer.subtractTen(-14.57), -24.57, 0.1);
        });
    });

    describe("sum", () => {
        it("Invalid first parameter type", () => {
            assert.equal(mathEnforcer.sum("Not a number", 14), undefined);
        });

        it("Invalid second parameter type", () => {
            assert.equal(mathEnforcer.sum(7, { number: 14 }), undefined);
        });

        it("Bouth invalid parameter types", () => {
            assert.equal(mathEnforcer.sum({ number: 14 }, "Not a number"), undefined);
        })

        it("Should add to numbers and return corect result. Int", () => {
            assert.equal(mathEnforcer.sum(33, 8), 41);
        });

        it("Should add to negative numbers and return corect result. Int", () => {
            assert.equal(mathEnforcer.sum(-33, -8), -41);
        });

        it("Should add to numbers and return corect result. float", () => {
            assert.closeTo(mathEnforcer.sum(33.55, 8.033), 41.583, 0.1);
        });

        it("Should add to negative numbers and return corect result. float", () => {
            assert.closeTo(mathEnforcer.sum(-33.55, -8.033), -41.583, 0.1);
        });

        it("First parameter positive second negative. int", () => {
            assert.equal(mathEnforcer.sum(10, -10), 0);
        });

        it("First parameter positive second negative. float", () => {
            assert.closeTo(mathEnforcer.sum(10.5454, -10.5454), 0, 0.1);
        });

        it("Second parameter positive first negative. int", () => {
            assert.equal(mathEnforcer.sum(-10, 10), 0);
        });

        it("Second parameter positive first negative. float", () => {
            assert.closeTo(mathEnforcer.sum(-10.5454, 10.5454), 0, 0.1);
        });
    });
});