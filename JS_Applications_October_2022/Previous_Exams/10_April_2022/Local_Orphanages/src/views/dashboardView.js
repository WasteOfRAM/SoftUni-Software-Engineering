import { getAllItems } from "../api/data.js";
import { html, repeat } from "../lib.js";

const itemCardTemplate = (data) => html`
    <div class="post">
        <h2 class="post-title">${data.title}</h2>
        <img class="post-image" src=${data.imageUrl} alt="Material Image">
        <div class="btn-wrapper">
            <a href="/details/${data._id}" class="details-btn btn">Details</a>
        </div>
    </div>
`;

const dashboardTemplate = (data) => html`
    <section id="dashboard-page">
        <h1 class="title">All Posts</h1>
        ${data.length !== 0 ?
        html` <div class="all-posts"> ${repeat(data, (item) => item._id, itemCardTemplate)} </div>`
        : html`<h1 class="title no-posts-title">No posts yet!</h1>`}
    </section>
`

export async function dashboardView(ctx) {
    ctx.updateNavigation();

    const data = await getAllItems();

    ctx.render(dashboardTemplate(data));
}