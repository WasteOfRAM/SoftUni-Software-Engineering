function lockedProfile() {
    const main = document.getElementById("main");
    main.textContent = "";

    const url = `http://localhost:3030/jsonstore/advanced/profiles`;

    fetch(url).then(response => response.json())
        .then(data => {
            const items = Object.entries(data).map((profile, index) => {
                const profileDiv = document.createElement("div");
                profileDiv.classList.add("profile");

                appendElements(profileDiv, profile, index + 1);

                return profileDiv;
            });

            main.replaceChildren(...items);
        });
}

function appendElements(appendTo, profile, index) {
    const icon = document.createElement("img");
    icon.src = "./iconProfile2.png";
    icon.classList.add("userIcon");
    appendTo.appendChild(icon);

    const lockLabel = document.createElement("label");
    lockLabel.textContent = "Lock";
    appendTo.appendChild(lockLabel);

    const lockRadio = document.createElement("input");
    lockRadio.type = "radio";
    lockRadio.name = `user${index}Locked`;
    lockRadio.value = "lock";
    lockRadio.checked = true;
    appendTo.appendChild(lockRadio);
    
    const unlockLabel = document.createElement("label");
    unlockLabel.textContent = "Unlock";
    appendTo.appendChild(unlockLabel);

    const unlockRadio = document.createElement("input");
    unlockRadio.type = "radio";
    unlockRadio.name = `user${index}Locked`;
    unlockRadio.value = "unlock";
    appendTo.appendChild(unlockRadio);

    const br = document.createElement("br");
    appendTo.appendChild(br);
    const hr = document.createElement("hr");
    appendTo.appendChild(hr);

    const userNameLabel = document.createElement("label");
    userNameLabel.textContent = "Username";
    appendTo.appendChild(userNameLabel);

    const userNameInput = document.createElement("input");
    userNameInput.type = "text";
    userNameInput.name = `user${index}Username`;
    userNameInput.value = `${profile[1].username}`;
    userNameInput.disabled = true;
    userNameInput.readOnly = true;
    appendTo.appendChild(userNameInput);

    const hiddenFields = document.createElement("div");
    hiddenFields.setAttribute("id", `user${index}HiddenFields`);
    hiddenFields.style.display = "none";
    appendTo.appendChild(hiddenFields);

    const hr2 = document.createElement("hr");
    hiddenFields.appendChild(hr2);

    const emailLabel = document.createElement("label");
    emailLabel.textContent = "Email:";
    hiddenFields.appendChild(emailLabel);

    const emailInput = document.createElement("input");
    emailInput.type = "email";
    emailInput.name = `user${index}Email`;
    emailInput.value = profile[1].email;
    emailInput.disabled = true;
    emailInput.readOnly = true;
    hiddenFields.appendChild(emailInput);

    const ageLabel = document.createElement("label");
    ageLabel.textContent = "Age:";
    hiddenFields.appendChild(ageLabel);

    const ageInput = document.createElement("input");
    ageInput.type = "email";
    ageInput.name = `user${index}Age`;
    ageInput.value = `${profile[1].age}`;
    ageInput.disabled = true;
    ageInput.readOnly = true;
    hiddenFields.appendChild(ageInput);

    const showBtn = document.createElement("button");
    showBtn.textContent = "Show more";
    appendTo.appendChild(showBtn);

    showBtn.addEventListener("click", (e) => {
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
    });
}