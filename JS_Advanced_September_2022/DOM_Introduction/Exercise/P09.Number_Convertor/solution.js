function solve() {
    document.querySelector("button").addEventListener('click', onClick);
    
    let result = document.getElementById('result');

    let selectTo = document.getElementById("selectMenuTo");
    let binary = document.createElement("option");
    binary.value = 'binary';
    binary.textContent = 'Binary';

    let hexadecimal = document.createElement("option");
    hexadecimal.value = 'hexadecimal';
    hexadecimal.textContent = 'Hexadecimal';

    selectTo.appendChild(binary);
    selectTo.appendChild(hexadecimal);


    function onClick() {
        let input = Number(document.getElementById("input").value);
        let selectedTo = selectTo.value;
        let conversionResult = '';

        switch (selectedTo) {
            case 'binary':
                conversionResult = input.toString(2);
                break;
            case 'hexadecimal':
                conversionResult = input.toString(16);
                break;
            default:
                break;
        }

        result.value = conversionResult.toUpperCase();
    }
}