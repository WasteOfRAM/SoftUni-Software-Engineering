const { assert } = require("chai");
const { describe, it } = require("mocha");
const flowerShop = require("../flowerShop");

describe("flowerShop tests", () => {
    describe("calcPriceOfFlowers function tests", () => {
        it("Invalid input for flower. Number", () => {
            assert.throws(() => flowerShop.calcPriceOfFlowers(14, 35, 35));
        });

        it("Invalid input for price. Array", () => {
            assert.throws(() => flowerShop.calcPriceOfFlowers("rose", [35], 35));
        });

        it("Invalid input for quantity. Object", () => {
            assert.throws(() => flowerShop.calcPriceOfFlowers("rose", 14, { quantity: 35 }));
        });

        it("Valid inputs", () => {
            let actual = flowerShop.calcPriceOfFlowers("Rose", 14, 2);
            let expected = "You need $28.00 to buy Rose!";

            assert.equal(actual, expected);
        });
    });

    describe("checkFlowersAvailable", () => {
        it("Available flowers", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];

            let actual = flowerShop.checkFlowersAvailable("Rose", gardenArr);
            let expected = "The Rose are available!";

            assert.equal(actual, expected);
        });

        it("Not available flowers", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];

            let actual = flowerShop.checkFlowersAvailable("Oak", gardenArr);
            let expected = "The Oak are sold! You need to purchase more!";

            assert.equal(actual, expected);
        });
    });

    describe("sellFlowers", () => {
        it("Invalid input for gardenArr. String", () => {
            assert.throws(() => flowerShop.sellFlowers("gardenArr", 1), "Invalid input!");
        });

        it("Invalid input for space. Not a number", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];
            assert.throws(() => flowerShop.sellFlowers(gardenArr, "14"), "Invalid input!");
        });

        it("Invalid input for space. Less than 0", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];
            assert.throws(() => flowerShop.sellFlowers(gardenArr, -1), "Invalid input!");
        });

        it("Invalid input for space. Equal to gardenArr.length", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];
            assert.throws(() => flowerShop.sellFlowers(gardenArr, 3), "Invalid input!");
        });

        it("Invalid input for space. Greater than gardenArr.length", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];
            assert.throws(() => flowerShop.sellFlowers(gardenArr, 14), "Invalid input!");
        });

        it("Valid inputs", () => {
            let gardenArr = ["Rose", "Lily", "Orchid"];
            let actual = flowerShop.sellFlowers(gardenArr, 2);
            let expected = "Rose / Lily";

            assert.equal(actual, expected);
        });
    });
});