function validate() {
    let email = document.getElementById('email');

    email.addEventListener('change', validation);

    function validation(event) {
        let mailFormat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

        if (mailFormat.test(event.target.value)) {
            event.target.classList.remove('error');
        }else{
            event.target.classList.add('error');
        }
    }
}