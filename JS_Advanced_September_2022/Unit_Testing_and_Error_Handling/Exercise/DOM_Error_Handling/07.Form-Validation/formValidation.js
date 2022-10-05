function validate() {
    let isCompaniCheckBox = document.getElementById("company");
    let companyInfo = document.getElementById("companyInfo");
    let submitButton = document.getElementById("submit");

    isCompaniCheckBox.addEventListener("change", showCompanyInfo);
    submitButton.addEventListener("click", submit);

    let usernamePattern = /^[a-zA-Z0-9]{3,20}$/g;
    let passwordPattern = /^[a-zA-Z0-9_]{5,15}$/g;
    //let emailPattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    let emailPattern = /^.*@.*\..*$/;

    function submit(e) {
        e.preventDefault();

        let username = document.getElementById("username");
        let email = document.getElementById("email");

        let password = document.getElementById("password");
        let confirmPassword = document.getElementById("confirm-password");
        let passwordMatch = password.value === confirmPassword.value;

        let companyNumber = document.getElementById("companyNumber");

        let isNameValid = nameValidation();
        let isEmailValid = emailValidation();
        let isPasswordValid = passwordValidation();
        let isCompanyValid = companyValidation();

        if (isNameValid && isEmailValid && isPasswordValid && isCompanyValid) {
            document.getElementById("valid").style.display = "block";
        }

        function companyValidation() {
            if (!isCompaniCheckBox.checked) {
                return true;
            }

            if (companyNumber.value < 1000 || companyNumber.value > 9999) {
                companyNumber.style.borderColor = "red";
                companyNumber.style.borderColor = "red";
                return false;

            } else {
                companyNumber.style.border = "none";
                companyNumber.style.border = "none";
                return true;
            }
        }

        function passwordValidation() {
            if (passwordMatch && passwordPattern.test(password.value)) {
                password.style.border = "none";
                confirmPassword.style.border = "none";
                return true;
            } else {
                password.style.borderColor = "red";
                confirmPassword.style.borderColor = "red";
                return false;
            }
        }

        function emailValidation() {
            if (!emailPattern.test(email.value)) {
                email.style.borderColor = "red";
                return false;
            } else {
                email.style.border = "none";
                return true;
            }
        }

        function nameValidation() {
            if (!usernamePattern.test(username.value)) {
                username.style.borderColor = "red";
                return false;
            } else {
                username.style.border = "none";
                return true;
            }
        }
    }

    function showCompanyInfo() {
        let current = companyInfo.style.display;
        companyInfo.style.display = current === "none" ? "block" : "none";
    }
}