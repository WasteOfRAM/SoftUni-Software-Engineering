function attachEvents() {
    document.getElementById("btnLoad").addEventListener("click", loadEntries);
    document.getElementById("btnCreate").addEventListener("click", createEntrie);
}

async function loadEntries() {
    const phonebookList = document.getElementById("phonebook");

    const entries = await fetch("http://localhost:3030/jsonstore/phonebook").then(response => response.json());

    const items = Object.values(entries).map(({person, phone, _id}) => {
        const li = document.createElement("li");
        li.id = _id;
        li.textContent = `${person}: ${phone}`;

        const delBtn = document.createElement("button");
        delBtn.textContent = "Delete";
        li.appendChild(delBtn);

        delBtn.addEventListener("click", deleteEntrie);

        return li;
    });

    phonebookList.replaceChildren(...items);
}

async function createEntrie() {
    const personInputField = document.getElementById("person");
    const phoneInputField = document.getElementById("phone");

    const person = personInputField.value;
    const phone = phoneInputField.value;

    personInputField.value = "";
    phoneInputField.value = "";

    await fetch("http://localhost:3030/jsonstore/phonebook", {
        method: "POST",
        headers: {
            "Content-type": "application/json"
        },
        body: JSON.stringify({"person": person, "phone": phone})
    });

    loadEntries();
}

async function deleteEntrie(e) {
    const elementId = e.target.parentElement.id;

    await fetch(`http://localhost:3030/jsonstore/phonebook/${elementId}`, {
        method: "DELETE",
        headers: {
            "Content-type": "application/json"
        }
    });

    loadEntries();
}

attachEvents();