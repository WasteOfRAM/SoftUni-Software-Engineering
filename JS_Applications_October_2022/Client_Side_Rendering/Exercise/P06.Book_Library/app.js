import { createElement, renderElements } from "./templates.js";

const body = document.querySelector("body");

const loadAllBtn = createElement.button("LOAD ALL BOOKS", loadAll, "loadBooks");

const tableHead = createElement.tableHead("Title", "Author", "Action");

let table = createElement.table(tableHead);

const addForm = createElement.form("add-form", "Add book", onAdd, "Submit");
const editForm = createElement.form("edit-form", "Edit book", onEdit, "Save");

renderElements(body, [loadAllBtn, table, addForm]);

async function loadAll() {
    const response = await fetch("http://localhost:3030/jsonstore/collections/books");
    const data = await response.json();

    const tableBody = createElement.tableBody(data);
    table = createElement.table(tableHead, tableBody);

    renderElements(body, [loadAllBtn, table, addForm]);
    document.getElementById("add-form").reset();
}

export function editBook(e) {
    renderElements(body, [loadAllBtn, table, editForm]);
    document.querySelector('form#edit-form input[name="title"]').value = e.target.parentElement.parentElement.children[0].textContent;
    document.querySelector('form#edit-form input[name="author"]').value = e.target.parentElement.parentElement.children[1].textContent;
    sessionStorage.setItem("elementId", e.target.parentElement.parentElement.id);
}

export async function onDelete(e) {
    const elementId = e.target.parentElement.parentElement.id;

    await fetch("http://localhost:3030/jsonstore/collections/books/" + elementId, {
        method: "DELETE"
    });

    loadAll();
}

async function onAdd(formEvent) {
    formEvent.preventDefault();

    const formData = new FormData(formEvent.target);
    const title = formData.get("title");
    const author = formData.get("author");

    if (!title || !author) {
        return;
    }

    document.getElementById("add-form").reset();

    await fetch("http://localhost:3030/jsonstore/collections/books", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ author, title })
    });

    loadAll();
}

async function onEdit(formEvent) {
    formEvent.preventDefault();

    const formData = new FormData(formEvent.target);
    const title = formData.get("title");
    const author = formData.get("author");

    if (!title || !author) {
        return;
    }

    document.getElementById("edit-form").reset();

    await fetch("http://localhost:3030/jsonstore/collections/books/" + sessionStorage.getItem("elementId"), {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ author, title })
    });

    sessionStorage.clear();

    loadAll();
}