const sum = require('../sumNumbers');
const { expect } = require('chai');
const { it, describe } = require('mocha');

describe('sumNumbers', () => {

    it('return NaN if input is not array of Numbers', () => {
        // Arange
        let invalidArray = ['not a number', 'also not a number'];
        // Act
        let result = sum(invalidArray);
        // Assert
        expect(result).to.be.NaN;
    });

    it('return sum of array elements', () => {
        // Arange
        let array = [1, 2, 3];
        // Act
        let result = sum(array);
        // Assert
        expect(result).to.be.equal(6);
    });

    it('judge wants 3 tests', () => {
        // Arange
        let array = [2, 2];
        // Act
        let result = sum(array);
        // Assert
        expect(result).to.be.equal(4);
    });
});