function solve() {
    let addButton = document.getElementById("add");
    addButton.addEventListener("click", addToList);
    let resetButton = document.getElementById("reset");
    resetButton.addEventListener("click", resetInputs);

    let recipientField = document.getElementById("recipientName");
    let titleField = document.getElementById("title");
    let messageField = document.getElementById("message");

    let list = document.getElementById("list");
    let sentList = document.getElementsByClassName("sent-list")[0];
    let deleteList = document.getElementsByClassName("delete-list")[0];

    function addToList(e) {
        e.preventDefault();
        let recipientName = recipientField.value;
        let title = titleField.value;
        let message = messageField.value;

        if(!recipientName || !title || !message){
            return;
        }

        recipientField.value = "";
        titleField.value = "";
        messageField.value = "";

        let liElement = document.createElement("li");
        list.appendChild(liElement);

        let titleHeader = document.createElement("h4");
        titleHeader.textContent = "Title: " + title;
        liElement.appendChild(titleHeader);

        let recipientHeader = document.createElement("h4");
        recipientHeader.textContent = "Recipient Name: " + recipientName;
        liElement.appendChild(recipientHeader);

        let messageElement = document.createElement("span");
        messageElement.textContent = message;
        liElement.appendChild(messageElement);

        let listAction = document.createElement("div");
        listAction.classList.add("list-action");
        liElement.appendChild(listAction);

        let sendButton = document.createElement("button");
        sendButton.textContent = "Send";
        sendButton.setAttribute("type", "submit");
        sendButton.setAttribute("id", "send");
        listAction.appendChild(sendButton);

        let delButton = document.createElement("button");
        delButton.textContent = "Delete";
        delButton.setAttribute("type", "submit");
        delButton.setAttribute("id", "delete");
        listAction.appendChild(delButton);

        sendButton.addEventListener("click", sendMail);
        delButton.addEventListener("click", delMails);
    }

    function resetInputs(e) {
        e.preventDefault();

        recipientField.value = "";
        titleField.value = "";
        messageField.value = "";
    }

    function sendMail(e) {
        let currentListElement = e.target.parentElement.parentElement;
        let to = currentListElement.children[1].textContent.split(": ")[1];
        let title = currentListElement.children[0].textContent.split(": ")[1]

        let sentMailLiElement = document.createElement("li");
        sentList.appendChild(sentMailLiElement);

        let toElement = document.createElement("span");
        toElement.textContent = "To: " + to;
        sentMailLiElement.appendChild(toElement);

        let titleElement = document.createElement("span");
        titleElement.textContent = "Title: " + title;
        sentMailLiElement.appendChild(titleElement);

        let buttonDiv = document.createElement("div");
        buttonDiv.classList.add("btn");
        sentMailLiElement.appendChild(buttonDiv);

        let deleteButton = document.createElement("button");
        deleteButton.setAttribute("type", "submit");
        deleteButton.classList.add("delete");
        deleteButton.textContent = "Delete";
        buttonDiv.appendChild(deleteButton);
        deleteButton.addEventListener("click", delMails);

        currentListElement.remove();
    }

    function delMails(e) {
        let currentElement = e.target.parentElement.parentElement;

        let title;
        let recipient;

        if (currentElement.parentElement.id === "list") {
            recipient = currentElement.children[1].textContent.split(": ")[1];
            title = currentElement.children[0].textContent.split(": ")[1]
        }else {
            recipient = currentElement.children[0].textContent.split(": ")[1];
            title = currentElement.children[1].textContent.split(": ")[1]
        }

        let liElement = document.createElement("li");
        deleteList.appendChild(liElement);

        let recipientElement = document.createElement("span");
        recipientElement.textContent = "To: " + recipient;
        liElement.appendChild(recipientElement);

        let titleElement = document.createElement("span");
        titleElement.textContent = "Title: " + title;
        liElement.appendChild(titleElement);

        currentElement.remove();
    }
}
solve()