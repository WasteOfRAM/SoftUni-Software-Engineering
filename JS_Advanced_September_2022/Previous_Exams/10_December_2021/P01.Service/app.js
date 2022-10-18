window.addEventListener('load', solve);

function solve() {
    document.querySelector("button[type='submit']").addEventListener("click", sendForm);

    let productTypeField = document.getElementById("type-product");
    let descriptionField = document.getElementById("description");
    let clientNameField = document.getElementById("client-name");
    let clientPhoneField = document.getElementById("client-phone");

    let receivedOrders = document.getElementById("received-orders");
    let completedOrders = document.getElementById("completed-orders");

    let clearBtn = document.getElementsByClassName("clear-btn")[0];
    clearBtn.addEventListener("click", removeCompleted);

    function sendForm(e) {
        e.preventDefault();

        let productType = productTypeField.value;
        let description = descriptionField.value;
        let clientName = clientNameField.value;
        let clientPhone = clientPhoneField.value;

        if (!description || !clientPhone || !clientName) {
            return;
        }

        descriptionField.value = "";
        clientNameField.value = "";
        clientPhoneField.value = "";

        let containerDiv = document.createElement("div");
        containerDiv.classList.add("container");
        receivedOrders.appendChild(containerDiv);

        let productTypeHeader = document.createElement("h2");
        productTypeHeader.textContent = "Product type for repair: " + productType;
        containerDiv.appendChild(productTypeHeader);

        let clientInfo = document.createElement("h3");
        clientInfo.textContent = `Client information: ${clientName}, ${clientPhone}`;
        containerDiv.appendChild(clientInfo);

        let problemDescription = document.createElement("h4");
        problemDescription.textContent = "Description of the problem: " + description;
        containerDiv.appendChild(problemDescription);

        let startButton = document.createElement("button");
        startButton.classList.add("start-btn");
        startButton.textContent = "Start repair";
        containerDiv.appendChild(startButton);

        let finishButton = document.createElement("button");
        finishButton.classList.add("finish-btn");
        finishButton.textContent = "Finish repair";
        finishButton.disabled = true;
        containerDiv.appendChild(finishButton);

        startButton.addEventListener("click", startRepair);
        finishButton.addEventListener("click", finishRepair);
    }

    function startRepair(e) {
        let currentOrderButtons = Array.from(e.target.parentElement.children).filter(el => el.tagName === "BUTTON");

        currentOrderButtons[0].disabled = true;
        currentOrderButtons[1].disabled = false;
    }

    function finishRepair(e) {
        let currentOrder = e.target.parentElement;

        Array.from(e.target.parentElement.children).filter(el => el.tagName === "BUTTON").map(btn => btn.remove());

        completedOrders.appendChild(currentOrder);
    }

    function removeCompleted(e) {
        let completedSection = e.target.parentElement;

        Array.from(completedSection.children).filter(el => el.classList.contains("container")).map(el => el.remove());
    }
}