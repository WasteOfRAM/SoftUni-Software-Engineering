function attachEventsListeners() {
    let main = document.getElementsByTagName('main')[0];

    main.addEventListener('click', convert);

    

    function convert(e) {
        if (e.target.value !== 'Convert') {
            return;
        }

        let timeUnit = e.target.previousElementSibling.id;
        let input = e.target.previousElementSibling.value;

        switch (timeUnit) {
            case 'days':
                converter(input);
                break;
            case 'hours':
                converter(input / 24);
                break;
            case 'minutes':
                converter(input / 60 / 24);
                break;
            case 'seconds':
                converter(input / 60 / 60 / 24);
                break;
            default:
                break;
        }
    }

    function converter(input) {
        let textElements = document.querySelectorAll('input[type=text]');

        textElements[0].value = input;
        textElements[1].value = input * 24;
        textElements[2].value = input * 60 * 24;
        textElements[3].value = input * 60 * 60 * 24;
    }
}