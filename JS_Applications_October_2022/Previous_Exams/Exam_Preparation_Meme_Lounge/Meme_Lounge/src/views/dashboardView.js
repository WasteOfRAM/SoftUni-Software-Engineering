import { getAllItems } from "../api/data.js";
import { html, repeat } from "../lib.js";

const itemCardTemplate = (item) => html`
    <div class="meme">
        <div class="card">
            <div class="info">
                <p class="meme-title">${item.title}</p>
                <img class="meme-image" alt="meme-img" src=${item.imageUrl}>
            </div>
            <div id="data-buttons">
                <a class="button" href="/details/${item._id}">Details</a>
            </div>
        </div>
    </div>
`;

const dashboardTemplate = (data) => html`
    <section id="meme-feed">
        <h1>All Memes</h1>
        ${data.length !== 0 ?
        html` <div id="memes"> ${repeat(data, (item) => item._id, itemCardTemplate)} </div>`
        : html`<p class="no-memes">No memes in database.</p>`}
    </section>
`

export async function dashboardView(ctx) {
    ctx.updateNavigation();

    const data = await getAllItems();

    ctx.render(dashboardTemplate(data));
}