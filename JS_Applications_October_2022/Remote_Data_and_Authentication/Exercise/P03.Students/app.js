window.addEventListener("load", attachEvents);

function attachEvents() {
    document.getElementById("form").addEventListener("submit", createStudent);

    extractStudents();
}

async function extractStudents() {
    const tableBody = document.getElementsByTagName("tbody")[0];

    const studentData = await fetch("http://localhost:3030/jsonstore/collections/students").then(response => response.json());

    const items = Object.values(studentData).map(({firstName, lastName, facultyNumber, grade}) => {
        const row = document.createElement("tr");

        const firstNameElem = document.createElement("td");
        firstNameElem.textContent = firstName;
        row.appendChild(firstNameElem);

        const lastNameElem = document.createElement("td");
        lastNameElem.textContent = lastName;
        row.appendChild(lastNameElem);

        const facultyNumberElem = document.createElement("td");
        facultyNumberElem.textContent = facultyNumber;
        row.appendChild(facultyNumberElem);

        const gradeElem = document.createElement("td");
        gradeElem.textContent = Number(grade).toFixed(2);
        row.appendChild(gradeElem);

        return row;
    });

    tableBody.replaceChildren(...items);
}

async function createStudent(formElement) {
    formElement.preventDefault();
    const data = new FormData(formElement.target);
    const entries = [...data.values()];

    if (entries.some(entry => entry === "")) {
        return;
    }

    const entryObj = {
        firstName: entries[0],
        lastName: entries[1],
        facultyNumber: entries[2],
        grade: entries[3]
    }

    await fetch("http://localhost:3030/jsonstore/collections/students", {
        method: "POST",
        headers: {
            "Content-type" : "application/json"
        },
        body: JSON.stringify(entryObj)
    });

    extractStudents();
}