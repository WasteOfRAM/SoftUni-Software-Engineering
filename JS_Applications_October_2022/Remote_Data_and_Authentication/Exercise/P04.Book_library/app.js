window.addEventListener("load", attachEvents);

let id;

function attachEvents() {
    document.getElementsByTagName("tbody")[0].textContent = "";
    document.getElementById("loadBooks").addEventListener("click", loadAllBooks);
    document.getElementsByTagName("form")[0].addEventListener("submit", createBook);
}

async function loadAllBooks() {
    const tableBody = document.getElementsByTagName("tbody")[0];

    const books = await fetch("http://localhost:3030/jsonstore/collections/books").then(response => response.json());

    const elements = Object.entries(books).map(book => {
        const row = document.createElement("tr");
        row.id = book[0];

        const title = document.createElement("td");
        title.textContent = book[1].title;
        row.appendChild(title);

        const author = document.createElement("td");
        author.textContent = book[1].author;
        row.appendChild(author);

        const actions = document.createElement("td");
        row.appendChild(actions);

        const editBtn = document.createElement("button");
        editBtn.textContent = "Edit";
        actions.appendChild(editBtn);

        const deleteBtn = document.createElement("button");
        deleteBtn.textContent = "Delete";
        actions.appendChild(deleteBtn);

        editBtn.addEventListener("click", editBook);
        deleteBtn.addEventListener("click", deleteBook);

        return row;
    });

    tableBody.replaceChildren(...elements);
}

async function createBook(formElement) {
    formElement.preventDefault();

    const data = new FormData(formElement.target);
    const title = data.get("title");
    const author = data.get("author");

    formElement.target.reset();


    if (!title || !author) {
        return;
    }

    const body = {
        author,
        title
    }

    await fetch("http://localhost:3030/jsonstore/collections/books", {
        method: "POST",
        headers: {
            "Content-Type": "appication/json"
        },
        body: JSON.stringify(body)
    });

    loadAllBooks();
}

function editBook(e) {
    id = e.target.parentElement.parentElement.id;
    const author = e.target.parentElement.previousElementSibling.textContent;
    const title = e.target.parentElement.previousElementSibling.previousElementSibling.textContent;

    const submitForm = document.querySelector("form");

    submitForm.querySelector('input[name="title"]').value = title;
    submitForm.querySelector('input[name="author"]').value = author;

    submitForm.removeEventListener("submit", createBook);

    submitForm.addEventListener("submit", updateInfo);

    document.querySelector("form h3").textContent = "Edit FORM";
    document.querySelector("form button").textContent = "Save";
}

async function updateInfo(formElement) {
    formElement.preventDefault();

    const data = new FormData(formElement.target);
    const title = data.get("title");
    const author = data.get("author");

    if (!title || !author) {
        formElement.target.reset();

        formElement.target.removeEventListener("submit", updateInfo);
        formElement.target.addEventListener("submit", createBook);

        document.querySelector("form h3").textContent = "FORM";
        document.querySelector("form button").textContent = "Submit";
        return;
    }

    const body = {
        author,
        title
    }

    await fetch("http://localhost:3030/jsonstore/collections/books/" + id, {
        method: "PUT",
        headers: {
            'Content-type': 'application/json'
        },
        body: JSON.stringify(body)
    });

    loadAllBooks();
    formElement.target.reset();

    formElement.target.removeEventListener("submit", updateInfo);
    formElement.target.addEventListener("submit", createBook);

    document.querySelector("form h3").textContent = "FORM";
    document.querySelector("form button").textContent = "Submit";
}

async function deleteBook(e) {
    const bookId = e.target.parentElement.parentElement.id;

    await fetch(`http://localhost:3030/jsonstore/collections/books/${bookId}`, {
        method: "DELETE"
    });

    loadAllBooks();
}