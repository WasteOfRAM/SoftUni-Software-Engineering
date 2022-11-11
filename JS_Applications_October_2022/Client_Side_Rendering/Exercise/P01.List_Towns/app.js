import { render, html } from "./node_modules/lit-html/lit-html.js";

document.querySelector("form.content").addEventListener("submit", getData);
const root = document.getElementById("root");

function renderList(data) {
    const list = () => html`
        <ul>
            ${
                data.map(item => html`<li>${item}</li>`)
            }
        </ul>
    `

    render(list(), root);
}

function getData(formEvent, data) {
    formEvent.preventDefault();

    const formData = new FormData(formEvent.target);
    data = formData.get("towns").split(", ");

    renderList(data);
}