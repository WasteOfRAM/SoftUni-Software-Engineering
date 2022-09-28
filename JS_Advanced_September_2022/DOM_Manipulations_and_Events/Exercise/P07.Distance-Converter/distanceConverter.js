function attachEventsListeners() {
    let button = document.getElementById('convert');
    button.addEventListener('click', convert);

    const conversionTable = {
        "km": 1000,
        "m": 1,
        "cm": 0.01,
        "mm": 0.001,
        "mi": 1609.34,
        "yrd": 0.9144,
        "ft": 0.3048,
        "in": 0.0254
    }

    function convert() {
        let inputOption = document.getElementById('inputUnits').value;
        let inputValue = document.getElementById('inputDistance').value;
        let outputOption = document.getElementById('outputUnits').value;

        let inputValueToMeters = conversionToMeters(inputOption, inputValue);

        document.getElementById('outputDistance').value = conversionToOutputUnit(outputOption, inputValueToMeters);

    }

    function conversionToMeters(unitType, value) {
        return value * conversionTable[unitType];
    }

    function conversionToOutputUnit(unitType, value) {
        return value / conversionTable[unitType];
    }
}