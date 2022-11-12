import { httpRequest } from "./httpRequest.js";
import { html, render } from "./node_modules/lit-html/lit-html.js";

const serverUrl = "http://localhost:3030/jsonstore/advanced/dropdown";
const selectElement = document.getElementById("menu");
const form = document.querySelector("form");
form.addEventListener("submit", addItem);

const dropdownOptions = (data) => html`${Object.values(data).map(item => html`<option .value=${item._id}>${item.text}</option>`)}`;

update()

async function addItem(formEvent) {
    formEvent.preventDefault();

    const text = document.getElementById("itemText").value;
    document.getElementById("itemText").value = "";

    await httpRequest(serverUrl, "POST", { text: text });

    update();
}

async function update() {
    const data = await httpRequest(serverUrl, "GET");
    render(dropdownOptions(data), selectElement);
}