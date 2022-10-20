const { assert } = require("chai");
const { describe, it } = require("mocha");
const cinema = require("../cinema");

describe("Tests", () => {
    describe("showMovies function tests", () => {
        it("Test with empty array", () => {
            let movieArr = [];

            let actual = cinema.showMovies(movieArr);
            let expected = 'There are currently no movies to show.';

            assert.equal(actual, expected);
        });

        it("Test array with elemnets", () => {
            let movieArr = ["King Kong", "The Tomorrow War", "Joker"];

            let actual = cinema.showMovies(movieArr);
            let expected = 'King Kong, The Tomorrow War, Joker';

            assert.equal(actual, expected);
        });
    });

    describe("ticketPrice function tests", () => {
        it("Invalid projectionType", () => {
            assert.throws(() => cinema.ticketPrice("normal"), 'Invalid projection type.');
        });

        it("Correct projection type Premiere", () => {
            let actual = cinema.ticketPrice("Premiere");
            let expected = 12.00;

            assert.equal(actual, expected);
        });

        it("Correct projection type Normal", () => {
            let actual = cinema.ticketPrice("Normal");
            let expected = 7.50;

            assert.equal(actual, expected);
        });

        it("Correct projection type", () => {
            let actual = cinema.ticketPrice("Discount");
            let expected = 5.50;

            assert.equal(actual, expected);
        });
    });

    describe("swapSeatsInHall function tests", () => {
        it("Valid swap", () => {
            let actual = cinema.swapSeatsInHall(1, 2);
            let expected = "Successful change of seats in the hall.";

            assert.equal(actual, expected);
        });

        it("firstPlace = secondPlace", () => {
            let actual = cinema.swapSeatsInHall(2, 2);
            let expected = "Unsuccessful change of seats in the hall.";

            assert.equal(actual, expected);
        });

        describe("firstPlace validations", () => {
            it("equal 20", () => {
                let actual = cinema.swapSeatsInHall(20, 2);
                let expected = "Successful change of seats in the hall.";
    
                assert.equal(actual, expected);
            });

            it("Not integer", () => {
                let actual = cinema.swapSeatsInHall(1.5, 2);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });

            it("equal to 0", () => {
                let actual = cinema.swapSeatsInHall(0, 2);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });

            it("less than 0", () => {
                let actual = cinema.swapSeatsInHall(-1, 2);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });

            it("larger than 20", () => {
                let actual = cinema.swapSeatsInHall(21, 2);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });
        });

        describe("secondPlace validations", () => {
            it("equal 20", () => {
                let actual = cinema.swapSeatsInHall(1, 20);
                let expected = "Successful change of seats in the hall.";
    
                assert.equal(actual, expected);
            });

            it("Not integer", () => {
                let actual = cinema.swapSeatsInHall(1, 2.5);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });

            it("equal to 0", () => {
                let actual = cinema.swapSeatsInHall(1, 0);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });

            it("less than 0", () => {
                let actual = cinema.swapSeatsInHall(1, -2);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });

            it("larger than 20", () => {
                let actual = cinema.swapSeatsInHall(1, 22);
                let expected = "Unsuccessful change of seats in the hall.";

                assert.equal(actual, expected);
            });
        });
    });
});