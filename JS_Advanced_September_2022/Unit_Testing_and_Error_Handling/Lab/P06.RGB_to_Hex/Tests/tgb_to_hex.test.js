const { expect } = require('chai');
const { describe, it } = require('mocha');
const rgbToHexColor = require('../rgb-to-hex');

describe('Tests', () => {
    it('Call with incorect red value should return "undefined"', () => {
        let red = -1;
        let green = 100;
        let blue = 110;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal(undefined);
    });

    it('Call with incorect green value should return "undefined"', () => {
        let red = 1;
        let green = 256;
        let blue = 110;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal(undefined);
    });

    it('Call with incorect blue value should return "undefined"', () => {
        let red = 1;
        let green = 255;
        let blue = 2545525541225;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal(undefined);
    });

    it('Call with incorect red type should return "undefined"', () => {
        let red = 'red';
        let green = 100;
        let blue = 110;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal(undefined);
    });

    it('Call with incorect green type should return "undefined"', () => {
        let red = 1;
        let green = true;
        let blue = 110;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal(undefined);
    });

    it('Call with incorect blue type should return "undefined"', () => {
        let red = 1;
        let green = 255;
        let blue = {red: 2, green: 14, blue: 'bus'};

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal(undefined);
    });

    it('Corect input values should return coresponding hex value as string', () => {
        let red = 232;
        let green = 232;
        let blue = 232;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal('#E8E8E8');
    });

    it('Hex for white', () => {
        let red = 255;
        let green = 255;
        let blue = 255;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal('#FFFFFF');
    });

    it('Hex for black', () => {
        let red = 0;
        let green = 0;
        let blue = 0;

        let result = rgbToHexColor(red, green, blue);

        expect(result).to.be.equal('#000000');
    });
});