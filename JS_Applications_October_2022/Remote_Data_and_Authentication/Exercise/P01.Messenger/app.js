function attachEvents() {
    document.getElementById("submit").addEventListener("click", submitMessage);
    document.getElementById("refresh").addEventListener("click", refreshMessage);
}

async function submitMessage() {
    const name = document.querySelector('input[name="author"]').value;
    const message = document.querySelector('input[name="content"]').value;

    const msgJson = JSON.stringify({ "author": name, "content": message });

    await fetch("http://localhost:3030/jsonstore/messenger", {
        method: "POST",
        headers: {
            "Content-type": "aplication/json"
        },
        body: msgJson
    });
}

async function refreshMessage() {
    const messagesArea = document.getElementById("messages");
    messagesArea.textContent = "";
    const allMessages = await fetch("http://localhost:3030/jsonstore/messenger").then(response => response.json());

    Object.values(allMessages).forEach(({author, content}) => {
        messagesArea.textContent += `${author}: ${content}\n`;
    });

    messagesArea.textContent = messagesArea.textContent.trimEnd();
}

attachEvents();