const { assert, expect } = require('chai');
const { describe, it, beforeEach } = require('mocha');
const createCalculator = require('../addSubtract.js');

describe('Creating the object corectly', () => {

    it('Should create object with 3 propertys', () => {
        let calcolator = createCalculator();

        assert.equal(calcolator.hasOwnProperty('add'), true);
        assert.equal(calcolator.hasOwnProperty('subtract'), true);
        assert.equal(calcolator.hasOwnProperty('get'), true);
    });

    it('Should return inner value with default 0', () => {
        let calcolator = createCalculator();
        assert.equal(calcolator.get(), 0);
    });
});

describe('Test functionality with invalid input', () => {
    let calc;

    beforeEach(function () {
        calc = createCalculator();
    });

    it('Add function with input that cant be parsed to number. "not valid"', () => {
        calc.add("not valid");
        expect(calc.get()).to.be.NaN;
    });

    it('Subtract function with input that cant be parsed to number. "not valid"', () => {
        calc.subtract("not valid");
        expect(calc.get()).to.be.NaN;
    });

    it('Add function with input that cant be parsed to number. Empty object', () => {
        calc.add({});
        expect(calc.get()).to.be.NaN;
    });

    it('Subtract function with input that cant be parsed to number. Empty object', () => {
        calc.subtract({});
        expect(calc.get()).to.be.NaN;
    });
});

describe('Test functionality with valid input', () => {
    let calc;

    beforeEach(function () {
        calc = createCalculator();
    });

    it('Adding 1 should return 1', () => {
        calc.add(1);
        assert.equal(calc.get(), 1);
    });

    it('Subtracting 1 should return -1', () => {
        calc.subtract(1);
        assert.equal(calc.get(), -1);
    });

    it('Adding than subtracting 1 should return 0', () => {
        calc.add(1);
        calc.subtract(1);
        assert.equal(calc.get(), 0);
    });
});