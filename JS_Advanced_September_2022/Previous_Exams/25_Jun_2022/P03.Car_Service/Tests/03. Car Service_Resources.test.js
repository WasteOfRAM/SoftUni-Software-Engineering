const { assert, expect } = require("chai");
const { describe, it } = require("mocha");
const carService = require("../03. Car Service_Resources");

describe("Car service tests", () => {
    describe("isItExpensive function tests", () => {
        it("Engine input issue test", () => {
            let actual = carService.isItExpensive("Engine");
            let expected = `The issue with the car is more severe and it will cost more money`;

            assert.equal(actual, expected);
        });

        it("Transmission input issue test", () => {
            let actual = carService.isItExpensive("Transmission");
            let expected = `The issue with the car is more severe and it will cost more money`;

            assert.equal(actual, expected);
        });

        it("Test with other input", () => {
            let actual = carService.isItExpensive("Not Engine");
            let expected = `The overall price will be a bit cheaper`;

            assert.equal(actual, expected);
        });
    });

    describe("discount function tests", () => {
        it("numberOfParts is not a number throws error", () => {
            assert.throws(() => carService.discount("ASDf", 345), "Invalid input");
            //expect(() => carService.discount("ASDf", 345)).to.throw("Invalid input");
        });

        it("numberOfParts is not a number throws error", () => {
            assert.throws(() => carService.discount({}, 345), "Invalid input");
        });

        it("totalPrice is not a number throws error", () => {
            assert.throws(() => carService.discount(45, "345"), "Invalid input");
        });

        it("totalPrice is not a number throws error", () => {
            assert.throws(() => carService.discount(45, ["345"]), "Invalid input");
        });

        it("If numberOfParts <= 2", () => {
            assert.equal(carService.discount(2, 1), "You cannot apply a discount");
        });

        it("If numberOfParts === 3", () => {
            assert.equal(carService.discount(3, 1), "Discount applied! You saved 0.15$");
        });

        it("If numberOfParts === 7", () => {
            assert.equal(carService.discount(7, 2), "Discount applied! You saved 0.3$");
        });

        it("If numberOfParts > 7", () => {
            assert.equal(carService.discount(8, 10), "Discount applied! You saved 3$");
        });
    });

    describe("partsToBuy function tests", () => {
        it("partsCatalog is not an array", () => {
            assert.throws(() => carService.partsToBuy({}, []), "Invalid input");
        });

        it("neededParts is not an array", () => {
            assert.throws(() => carService.partsToBuy([], "not array"), "Invalid input");
        });

        it("If partsCatalog is empty, return 0", () => {
            let actual = carService.partsToBuy([], [{ part: "blowoff valve", price: 145 }]);
            let expected = 0;

            assert.equal(actual, expected);
        });

        it("Calculate total price", () => {
            let partsCatalog = [{ part: "blowoff valve", price: 145 }, { part: "coil springs", price: 230 }];
            let neededParts = ["blowoff valve"];

            let actual = carService.partsToBuy(partsCatalog, neededParts);
            let expected = 145;

            assert.equal(actual, expected);
        });

        
    });
});