import { get } from "../api/api.js";
import { html, render, nothing } from "../lib.js";
import { createSubmit } from "../util.js";

const searchTemplate = (onSearch) => html`
    <section id="search">
    <h2>Search by Brand</h2>

    <form @submit=${onSearch} class="search-wrapper cf">
    <input
        id="#search-input"
        type="text"
        name="search"
        placeholder="Search here..."
        required
    />
    <button type="submit">Search</button>
    </form>

    <h3>Results:</h3>

    <div id="search-container">
    
    </div>
    </section>
`;

const searchResultTemplate = (result, isLoged) => html`
    ${result.length > 0 ? html`${itemCardTemplate(result, isLoged)}` : html`<h2>There are no results found.</h2>`}
`

const itemCardTemplate = (data, isLoged) => html`
    <ul class="card-wrapper">
        ${html`
            ${data.map(item => itemCard(item, isLoged))}
        `}
    </ul>
`
const itemCard = (item, isLoged) => html`
    <li class="card">
        <img src=${item.imageUrl} alt="travis" />
        <p>
        <strong>Brand: </strong><span class="brand">${item.brand}</span>
        </p>
        <p>
        <strong>Model: </strong
        ><span class="model">${item.model}</span>
        </p>
        <p><strong>Value:</strong><span class="value">${item.value}</span>$</p>
        ${isLoged ? html`<a class="details-btn" href="/details/${item._id}">Details</a>` : nothing}
    </li>
`

export function searchView(ctx) {
    ctx.render(searchTemplate(createSubmit(onSearch)));

    async function onSearch({search}, event) {
        const result = await get(`/data/shoes?where=brand%20LIKE%20%22${search}%22`);
        
        render(searchResultTemplate(result, Boolean(ctx.user)), document.getElementById("search-container"));
    }
}