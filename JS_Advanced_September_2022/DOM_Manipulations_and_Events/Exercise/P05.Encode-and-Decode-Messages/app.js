function encodeAndDecodeMessages() {
    let main = document.getElementById('main');

    let encodeDiv = main.getElementsByTagName('div')[0];
    let decodeDiv = main.getElementsByTagName('div')[1];

    let encodeButton = encodeDiv.getElementsByTagName('button')[0];
    encodeButton.addEventListener('click', encode);

    let decodeButton = decodeDiv.getElementsByTagName('button')[0];
    decodeButton.addEventListener('click', decode);

    function encode() {
        let textToEncode = encodeDiv.getElementsByTagName('textarea')[0].value;
        let result = '';

        for (let ch of textToEncode) {
            result += String.fromCharCode(ch.charCodeAt() + 1);
        }

        decodeDiv.getElementsByTagName('textarea')[0].value = result;
        encodeDiv.getElementsByTagName('textarea')[0].value = '';
    }

    function decode() {
        let textToDecode = decodeDiv.getElementsByTagName('textarea')[0].value;
        let result = '';

        for (let ch of textToDecode) {
            result += String.fromCharCode(ch.charCodeAt() - 1);
        }

        decodeDiv.getElementsByTagName('textarea')[0].value = result;
    }
}