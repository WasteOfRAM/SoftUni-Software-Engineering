import { del, get, post } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html, nothing } from "../lib.js";

const createDetailsTemplate = (data, userId, totalLikes, onDelete, onLike, userLiked) => html`
    <section id="detailsPage">
        <div id="detailsBox">
            <div class="detailsInfo">
                <h1>Title: ${data.title}</h1>
                <div>
                    <img src=${data.imageUrl} />
                </div>
            </div>

            <div class="details">
                <h3>Theater Description</h3>
                <p>${data.description}</p>
                <h4>Date: ${data.date}</h4>
                <h4>Author: ${data.author}</h4>
                <div class="buttons">
                    ${data._ownerId === userId ? html`     
                    <a @click=${onDelete} class="btn-delete" href="javascript:void(0)">Delete</a>
                    <a class="btn-edit" href="/edit/${data._id}">Edit</a>`
                    : userId != "" && !userLiked ? html`
                    <a @click=${onLike} class="btn-like" href="javascript:void(0)">Like</a>` : nothing }
                </div>
                <p class="likes">Likes: ${totalLikes}</p>
            </div>
        </div>
    </section>
`;

export async function detailsView(ctx) {
    const data = await getItemById(ctx.params.id);
    const totalLikes = await get(`/data/likes?where=theaterId%3D%22${data._id}%22&distinct=_ownerId&count`);
    const userId = ctx.user ? ctx.user._id : "";

    const userLiked = await get(`/data/likes?where=theaterId%3D%22${ctx.params.id}%22%20and%20_ownerId%3D%22${userId}%22&count`);

    ctx.render(createDetailsTemplate(data, userId, totalLikes, onDelete, onLike, !!userLiked));

    async function onDelete() {
        const removeItem = confirm("Are you shure you want to permanetly delete the item?");

        if (removeItem) {
            await del("/data/theaters/" + ctx.params.id);
            ctx.page.redirect("/profile");
        }
    }

    async function onLike() {
        await post("/data/likes", { "theaterId": data._id });
        ctx.page.redirect(`/details/${data._id}`);
    }
}