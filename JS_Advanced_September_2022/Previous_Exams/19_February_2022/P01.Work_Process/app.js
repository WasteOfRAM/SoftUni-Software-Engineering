function solve() {
    document.getElementById("add-worker").addEventListener("click", hireWorker);
    let firstNameField = document.getElementById("fname");
    let lastNameField = document.getElementById("lname");
    let emailField = document.getElementById("email");
    let birthDateField = document.getElementById("birth");
    let positionField = document.getElementById("position");
    let salaryField = document.getElementById("salary");

    let hiredTable = document.getElementById("tbody");

    let salaryBudgetNeeded = document.getElementById("sum");

    function hireWorker(e) {
        e.preventDefault();

        let firstName = firstNameField.value;
        let lastName = lastNameField.value;
        let email = emailField.value;
        let birthDate = birthDateField.value;
        let position = positionField.value;
        let salary = salaryField.value;

        if(!firstName || !lastName || !email || !birthDate || !position || !salary){
            return;
        }

        firstNameField.value = "";
        lastNameField.value = "";
        emailField.value = "";
        birthDateField.value = "";
        positionField.value = "";
        salaryField.value = "";

        let inputData = [];
        inputData.push(firstName);
        inputData.push(lastName);
        inputData.push(email);
        inputData.push(birthDate);
        inputData.push(position);
        inputData.push(salary);

        let tableRow = document.createElement("tr");
        hiredTable.appendChild(tableRow);

        for (const value of inputData) {
            let td = document.createElement("td");
            td.textContent = value;
            tableRow.appendChild(td);
        }

        let tdButtons = document.createElement("td");
        tableRow.appendChild(tdButtons);

        let firedButton = document.createElement("button");
        firedButton.classList.add("fired");
        firedButton.textContent = "Fired";
        
        let editButton = document.createElement("button");
        editButton.classList.add("edit");
        editButton.textContent = "Edit";

        tdButtons.appendChild(editButton);
        tdButtons.appendChild(firedButton);

        editButton.addEventListener("click", edit);
        firedButton.addEventListener("click", fired);

        let budget = Number(salaryBudgetNeeded.textContent);

        budget += Number(salary);

        salaryBudgetNeeded.textContent = budget.toFixed(2);
    }

    function edit(e) {
        let currentRow = e.target.parentElement.parentElement;

        let valuesArr = [];
        for (let i = 0; i < 6; i++) {
            valuesArr.push(currentRow.cells[i].textContent);
        }


        firstNameField.value = valuesArr[0];
        lastNameField.value = valuesArr[1];
        emailField.value = valuesArr[2];
        birthDateField.value = valuesArr[3];
        positionField.value = valuesArr[4];
        salaryField.value = valuesArr[5];

        let budget = Number(salaryBudgetNeeded.textContent);

        budget -= Number(valuesArr[5]);

        salaryBudgetNeeded.textContent = budget.toFixed(2);

        currentRow.remove();
    }

    function fired(e) {
        let currentRow = e.target.parentElement.parentElement;

        let budget = Number(salaryBudgetNeeded.textContent);

        budget -= Number(currentRow.cells[5].textContent);

        salaryBudgetNeeded.textContent = budget.toFixed(2);

        currentRow.remove();
    }
}
solve()