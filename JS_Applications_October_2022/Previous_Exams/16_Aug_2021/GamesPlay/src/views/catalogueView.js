import { getAllItems } from "../api/data.js";
import { html, repeat } from "../lib.js";

const itemCardTemplate = (item) => html`
    <div class="allGames">
        <div class="allGames-info">
            <img src=".${item.imageUrl}">
            <h6>${item.category}</h6>
            <h2>${item.title}</h2>
            <a href="/details/${item._id}" class="details-button">Details</a>
        </div>
    </div>
`;

const catalogueTemplate = (data) => html`
    <section id="catalog-page">
        <h1>All Games</h1>
        ${data.length !== 0 ?
        html`${repeat(data, item => item._id, itemCardTemplate)}`
        : html`<h3 class="no-articles">No articles yet</h3>`}
    </section>
`

export async function catalogueView(ctx) {
    ctx.updateNavigation();

    const data = await getAllItems();

    ctx.render(catalogueTemplate(data));
}