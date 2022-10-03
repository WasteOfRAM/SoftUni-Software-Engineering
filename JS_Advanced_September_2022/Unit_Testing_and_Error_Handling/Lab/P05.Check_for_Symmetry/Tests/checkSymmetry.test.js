const { expect } = require('chai');
const { describe, it } = require('mocha');
const isSymmetric = require('../checkForSymmetry');

describe('Check array symetry', () => {
    it('Check if input is an array', () => {
       // Arrange
       let notArray = 14;
       // Act
       let result = isSymmetric(notArray);
       // Assert
       expect(result).to.be.false; 
    });

    it('Should return false with non symetric array', () => {
        // Arrange
        let nonSymetricArray = [1, 2, 4, 5, 6];
        // Act
        let result = isSymmetric(nonSymetricArray);
        // Assert
        expect(result).to.be.false;
    });

    it('Should return true with symetric array', () => {
        // Arrange
        let symetricArray = [1, 2, 4, 2, 1];
        // Act
        let result = isSymmetric(symetricArray);
        // Assert
        expect(result).to.be.true;
    });

    it('Should return true with one element array', () => {
        // Arrange
        let symetricArray = ['array of one'];
        // Act
        let result = isSymmetric(symetricArray);
        // Assert
        expect(result).to.be.true;
    });

    it('Empty array should return true', () => {
        // Arrange
        let emptyArr = [];
        // Act
        let result = isSymmetric(emptyArr);
        // Assert
        expect(result).to.be.true;
    });

    it('Shude return false symmetric number string mix array', () => {
        // Arrange
        let arr = ['14', 14];
        // Act
        let result = isSymmetric(arr);
        // Assert
        expect(result).to.be.false;
    });
});