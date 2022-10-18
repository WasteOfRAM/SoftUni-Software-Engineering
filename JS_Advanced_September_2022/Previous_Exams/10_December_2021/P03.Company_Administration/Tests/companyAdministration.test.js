const { assert } = require("chai");
const { describe, it } = require("mocha");
const companyAdministration = require("../companyAdministration");

describe("companyAdministration tests", () => {
    describe("hiringEmployee function tests", () => {
        it("position pram != Programmer", () => {
            assert.throws(() => companyAdministration.hiringEmployee("Petur", "NotProgrammer", 14),
                `We are not looking for workers for this position.`);
        });

        it("valid input with yearsExperience = 3", () => {
            let actual = companyAdministration.hiringEmployee("Petur", "Programmer", 3);
            let expected = `Petur was successfully hired for the position Programmer.`;

            assert.equal(actual, expected);
        });

        it("valid input with yearsExperience > 3", () => {
            let actual = companyAdministration.hiringEmployee("Petur", "Programmer", 14);
            let expected = `Petur was successfully hired for the position Programmer.`;

            assert.equal(actual, expected);
        });

        it("valid input with yearsExperience < 3", () => {
            let actual = companyAdministration.hiringEmployee("Petur", "Programmer", 2);
            let expected = `Petur is not approved for this position.`;

            assert.equal(actual, expected);
        });
    });

    describe("calculateSalary function test", () => {
        it("Invalid input type. String", () => {
            assert.throws(() => companyAdministration.calculateSalary("5200"),"Invalid hours");
        });

        it("Invalid input hours < 0.", () => {
            assert.throws(() => companyAdministration.calculateSalary(-1),"Invalid hours");
        });

        it("Valid input under 160 hours", () => {
            let actual = companyAdministration.calculateSalary(159);
            let expected = 2385;

            assert.equal(actual, expected);
        });

        it("Valid input 0 hours", () => {
            let actual = companyAdministration.calculateSalary(0);
            let expected = 0;

            assert.equal(actual, expected);
        });

        it("Valid input with 160 hours", () => {
            let actual = companyAdministration.calculateSalary(160);
            let expected = 2400;

            assert.equal(actual, expected);
        });

        it("Valid input over 160 hours", () => {
            let actual = companyAdministration.calculateSalary(161);
            let expected = 3415;

            assert.equal(actual, expected);
        });
    });

    describe("firedEmployee function tests", () => {
        it("Invalid input for employees != Array, Object", () => {
            assert.throws(() => companyAdministration.firedEmployee({}, 0), "Invalid input");
        });

        it("Invalid input for index != Number. String", () => {
            assert.throws(() => companyAdministration.firedEmployee(["Petar", "Ivan", "George"], "0"), "Invalid input");
        });

        it("Invalid input for index < 0", () => {
            assert.throws(() => companyAdministration.firedEmployee(["Petar", "Ivan", "George"], -1), "Invalid input");
        });

        it("Invalid input for index = array.length", () => {
            let employees = ["Petar", "Ivan", "George"];

            assert.throws(() => companyAdministration.firedEmployee(employees, employees.length), "Invalid input");
        });

        it("Valid inputs index 0", () => {
            let employees = ["Petar", "Ivan", "George"];

            let actual = companyAdministration.firedEmployee(employees, 0);
            let expected = "Ivan, George";

            assert.equal(actual, expected);
        });
    });
});