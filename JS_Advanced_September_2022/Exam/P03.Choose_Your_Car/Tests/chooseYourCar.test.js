const { assert } = require("chai");
const { describe, it } = require("mocha");
const chooseYourCar = require("../chooseYourCar");

describe("Tests", () => {
    describe("choosingType function tests", () => {
        it("year < 1900", () => {
            assert.throws(() => chooseYourCar.choosingType("Sedan", "Pink", 1899), `Invalid Year!`);
        });

        it("year > 2022", () => {
            assert.throws(() => chooseYourCar.choosingType("Sedan", "Pink", 2023), `Invalid Year!`);
        });

        it("Not Sedan", () => {
            assert.throws(() => chooseYourCar.choosingType("NotSedan", "Pink", 2010), `This type of car is not what you are looking for.`);
        });

        it("year < 2010", () => {
            let actual = chooseYourCar.choosingType("Sedan", "Pink", 2009);
            let expected = `This Sedan is too old for you, especially with that Pink color.`;

            assert.equal(actual, expected);
        });

        it("year = 2010", () => {
            let actual = chooseYourCar.choosingType("Sedan", "Pink", 2010);
            let expected = `This Pink Sedan meets the requirements, that you have.`;

            assert.equal(actual, expected);
        });
    });

    describe("brandName function tests", () => {
        it("brands is not array", () => {
            assert.throws(() => chooseYourCar.brandName(1, 0),"Invalid Information!");
        });

        it("index is not integer", () => {
            let brandsArr = ["BMW", "Toyota", "Peugeot"];
            assert.throws(() => chooseYourCar.brandName(brandsArr, "Index"),"Invalid Information!");
        });

        it("index is float", () => {
            let brandsArr = ["BMW", "Toyota", "Peugeot"];
            assert.throws(() => chooseYourCar.brandName(brandsArr, 1.4),"Invalid Information!");
        });

        it("index is less than 0", () => {
            let brandsArr = ["BMW", "Toyota", "Peugeot"];
            assert.throws(() => chooseYourCar.brandName(brandsArr, -1),"Invalid Information!");
        });

        it("index is bigger than array length", () => {
            let brandsArr = ["BMW", "Toyota", "Peugeot"];
            assert.throws(() => chooseYourCar.brandName(brandsArr, 4),"Invalid Information!");
        });

        it("index is equal to array length", () => {
            let brandsArr = ["BMW", "Toyota", "Peugeot"];
            assert.throws(() => chooseYourCar.brandName(brandsArr, 3),"Invalid Information!");
        });

        it("Valid input", () => {
            let brandsArr = ["BMW", "Toyota", "Peugeot"];
            let actual = chooseYourCar.brandName(brandsArr, 1);
            let expected = "BMW, Peugeot";

            assert.equal(actual, expected);
        });
    });

    describe("carFuelConsumption function tests", () => {
        it("distanceInKilometers not a number", () => {
            assert.throws(() => chooseYourCar.carFuelConsumption("number", 5),"Invalid Information!");
        });

        it("distanceInKilometers < 0", () => {
            assert.throws(() => chooseYourCar.carFuelConsumption(-1, 5),"Invalid Information!");
        });

        it("distanceInKilometers = 0", () => {
            assert.throws(() => chooseYourCar.carFuelConsumption(0, 5),"Invalid Information!");
        });

        it("consumptedFuelInLiters not a number", () => {
            assert.throws(() => chooseYourCar.carFuelConsumption(5, "number"),"Invalid Information!");
        });

        it("consumptedFuelInLiters < 0", () => {
            assert.throws(() => chooseYourCar.carFuelConsumption(5, -1),"Invalid Information!");
        });

        it("consumptedFuelInLiters = 0", () => {
            assert.throws(() => chooseYourCar.carFuelConsumption(5, 0),"Invalid Information!");
        });

        it("litersPerHundredKm < 7", () => {
            let actual = chooseYourCar.carFuelConsumption(150, 8);
            let expected = "The car is efficient enough, it burns 5.33 liters/100 km.";

            assert.equal(actual, expected);
        });

        it("litersPerHundredKm = 7", () => {
            let actual = chooseYourCar.carFuelConsumption(114.3, 8);
            let expected = "The car is efficient enough, it burns 7.00 liters/100 km.";

            assert.equal(actual, expected);
        });

        it("litersPerHundredKm > 7", () => {
            let actual = chooseYourCar.carFuelConsumption(50, 8);
            let expected = "The car burns too much fuel - 16.00 liters!";

            assert.equal(actual, expected);
        });
    });
});