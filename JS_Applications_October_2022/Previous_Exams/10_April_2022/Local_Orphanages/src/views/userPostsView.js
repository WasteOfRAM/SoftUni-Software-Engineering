import { get } from "../api/api.js";
import { html, repeat } from "../lib.js";

const userItemsTemplate = (data) => html`
    <section id="my-posts-page">
        <h1 class="title">My Posts</h1>

        ${
            data.length === 0 ? html`<h1 class="title no-posts-title">You have no posts yet!</h1>` 
            : html`<div class="my-posts"> ${repeat(data, (data) => data._id, userItemCard)} </div>`
        }

    </section>
`; 

const userItemCard = (item) => html`
    <div class="post">
        <h2 class="post-title">${item.title}</h2>
        <img class="post-image" src=${item.imageUrl} alt="Material Image">
        <div class="btn-wrapper">
            <a href="/details/${item._id}" class="details-btn btn">Details</a>
        </div>
    </div>
`

export async function userItemsView(ctx) {
    const data = await get(`/data/posts?where=_ownerId%3D%22${ctx.user._id}%22&sortBy=_createdOn%20desc`);

    ctx.render(userItemsTemplate(data));
}