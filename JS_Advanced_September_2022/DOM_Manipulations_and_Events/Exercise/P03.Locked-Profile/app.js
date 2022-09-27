function lockedProfile() {
    let main = document.getElementById('main');
    main.addEventListener('click', showMore);


    function showMore(e) {
        if (e.target.tagName !== "BUTTON") {
            return;
        }

        let isLocked = e.target.parentElement.children[2].checked;

        if (isLocked) {
            return;
        }

        let hidenField = e.target.previousElementSibling;

        if (hidenField.style.display !== 'block') {
            hidenField.style.display = 'block';
            e.target.textContent = 'Hide it';
        }else{
            hidenField.style.display = 'none';
            e.target.textContent = 'Show more';
        }
    }
}