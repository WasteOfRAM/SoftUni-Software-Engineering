import { render, html, nothing } from "./node_modules/lit-html/lit-html.js";
import { editBook, onDelete } from "./app.js";

export const createElement = {

    button: (textContent, clickEvent, id) => html`
        <button id=${id ? id : nothing} @click=${clickEvent ? clickEvent : nothing}>${textContent}</button>
    `,

    table: (head, body) => html`
        <table>
            ${head}
            ${body}
        </table>
    `,

    tableHead: (...headers) => html`
        <thead>
            <tr>
                ${headers.map(th => html`<th>${th}</th>`)}
            </tr>
        </thead>
    `,

    tableBody: (data) => html`
        <tbody>
            ${Object.entries(data).map(item => html`
                <tr .id=${item[0]}>
                    <td>${item[1].title}</td>
                    <td>${item[1].author}</td>
                    <td>
                        ${createElement.button("Edit", editBook)}
                        ${createElement.button("Delete", onDelete)}
                    </td>
                </tr>
            `)}
        </tbody>
    `,

    form: (id, headerTag, onSubmit, value) => html`
        <form id=${id ? id : nothing} @submit=${onSubmit}>
            <h3>${headerTag}</h3>
            <label>TITLE</label>
            <input type="text" name="title" placeholder="Title...">
            <label>AUTHOR</label>
            <input type="text" name="author" placeholder="Author...">
            <input type="submit" value=${value}>
        </form>
    `
}

export function renderElements(appendTo, ...elements) {
    render(elements, appendTo);
}