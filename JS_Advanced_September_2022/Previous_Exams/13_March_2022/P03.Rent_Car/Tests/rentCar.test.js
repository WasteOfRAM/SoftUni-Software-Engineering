const rentCar = require("../rentCar");
const { assert } = require("chai");
const { describe, it } = require("mocha");

describe("rentCar tests", () => {
    describe("searchCar function tests", () => {
        it("shop array invalid input. Object", () => {
            assert.throws(() => rentCar.searchCar({}, "model"), "Invalid input!");
        });

        it("shop array invalid input. Number", () => {
            assert.throws(() => rentCar.searchCar(14, "model"), "Invalid input!");
        });

        it("model string invalid input, Array", () => {
            assert.throws(() => rentCar.searchCar([], ["model"]), "Invalid input!");
        });

        it("model string invalid input. Object", () => {
            assert.throws(() => rentCar.searchCar(14, { "model": "147" }), "Invalid input!");
        });

        it("valid input. No matching element", () => {
            assert.throws(() => rentCar.searchCar(["Volkswagen", "BMW", "Audi"], "Not Audi"), "There are no such models in the catalog!");
        });

        it("valid input. Matching element", () => {
            let shop = ["Volkswagen", "BMW", "Audi"];
            let actual = rentCar.searchCar(shop, "BMW");
            let expected = "There is 1 car of model BMW in the catalog!";

            assert.equal(actual, expected);
        });

        it("valid input. 2 matching element", () => {
            let shop = ["Volkswagen", "BMW", "Audi", "BMW"];
            let actual = rentCar.searchCar(shop, "BMW");
            let expected = "There is 2 car of model BMW in the catalog!";

            assert.equal(actual, expected);
        });
    });

    describe("calculatePriceOfCar", () => {
        it("Invalid input model. Number", () => {
            assert.throws(() => rentCar.calculatePriceOfCar(14, 17), "Invalid input!");
        });

        it("Invalid input model. Array", () => {
            assert.throws(() => rentCar.calculatePriceOfCar(["BWM"], 17), "Invalid input!");
        });

        it("Invalid input days. String", () => {
            assert.throws(() => rentCar.calculatePriceOfCar("BWM", "17"), "Invalid input!");
        });

        it("Invalid input days. Object", () => {
            assert.throws(() => rentCar.calculatePriceOfCar("BWM", {days: 14}), "Invalid input!");
        });

        it("Valid input with no model match", () => {
            assert.throws(() => rentCar.calculatePriceOfCar("Alfa Romeo", 14), "No such model in the catalog!");
        });

        it("Valid input with model match. BMW", () => {
            let actual = rentCar.calculatePriceOfCar("BMW", 14);
            let expected = "You choose BMW and it will cost $630!";

            assert.equal(actual, expected);
        });

        it("Valid input with model match. Volkswagen", () => {
            let actual = rentCar.calculatePriceOfCar("Volkswagen", 0);
            let expected = "You choose Volkswagen and it will cost $0!";

            assert.equal(actual, expected);
        });
    });

    describe("checkBudget", () => {
        it("Invalid costPerDay input. String.", () => {
            assert.throws(() => rentCar.checkBudget("14", 14, 14));
        });

        it("Invalid costPerDay input. Array.", () => {
            assert.throws(() => rentCar.checkBudget([14], 14, 14));
        });

        it("Invalid days input. Object.", () => {
            assert.throws(() => rentCar.checkBudget(14, {days: 14}, 14));
        });

        it("Invalid days input. True.", () => {
            assert.throws(() => rentCar.checkBudget(14, true, 14));
        });

        it("Invalid budget input. String.", () => {
            assert.throws(() => rentCar.checkBudget(14, 14, "14"));
        });

        it("Invalid budget input. Object.", () => {
            assert.throws(() => rentCar.checkBudget(14, 14, {}));
        });

        it("Valid inputs with cost = budget", () => {
            let actual = rentCar.checkBudget(14, 1, 14);
            let expected = "You rent a car!";

            assert.equal(actual, expected);
        });

        it("Valid inputs with cost < budget", () => {
            let actual = rentCar.checkBudget(14, 1, 30);
            let expected = "You rent a car!";

            assert.equal(actual, expected);
        });

        it("Valid inputs with cost 1 biger than budget", () => {
            let actual = rentCar.checkBudget(14, 1, 13);
            let expected = "You need a bigger budget!";

            assert.equal(actual, expected);
        });

        it("Valid inputs with cost > budget", () => {
            let actual = rentCar.checkBudget(14, 15, 13);
            let expected = "You need a bigger budget!";

            assert.equal(actual, expected);
        });
    });
});