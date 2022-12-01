import { del, get, post } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html, nothing, repeat } from "../lib.js";
import { createSubmit } from "../util.js";

const commentTemplate = (item) => html`
    <li class="comment">
        <p>Content: ${item.comment}</p>
    </li>
`

const createDetailsTemplate = (data, userId, onDelete, comments, onComment) => html`
    <section id="game-details">
        <h1>Game Details</h1>
        <div class="info-section">

            <div class="game-header">
                <img class="game-img" src=${data.imageUrl} />
                <h1>${data.title}</h1>
                <span class="levels">MaxLevel: ${data.maxLevel}</span>
                <p class="type">${data.category}</p>
            </div>

            <p class="text">${data.summary}</p>

            <div class="details-comments">
                <h2>Comments:</h2>
                ${comments.length === 0 ? html`<p class="no-comment">No comments.</p>`
                : html`<ul>${repeat(comments, commnet => commnet._id, commentTemplate)}</ul>`
                }
            </div>

            ${userId === data._ownerId ? html`
                <div class="buttons">
                    <a href="/edit/${data._id}" class="button">Edit</a>
                    <a @click=${onDelete} href="javascript:void(0)" class="button">Delete</a>
                </div>` : nothing}
        </div>

        ${userId !== "" && userId !== data._ownerId ? html`
        <article class="create-comment">
            <label>Add new comment:</label>
            <form @submit=${onComment} class="form">
                <textarea name="comment" placeholder="Comment......"></textarea>
                <input class="btn submit" type="submit" value="Add Comment">
            </form>
        </article>` : nothing}

    </section>`;

export async function detailsView(ctx) {
    const data = await getItemById(ctx.params.id);
    const userId = ctx.user ? ctx.user._id : "";
    
    const comments = await get(`/data/comments?where=gameId%3D%22${ctx.params.id}%22`)

    ctx.render(createDetailsTemplate(data, userId, onDelete, comments, createSubmit(onComment)));

    async function onDelete() {
        const removeItem = confirm("Are you shure you want to permanetly delete the item?");

        if (removeItem) {
            await del("/data/games/" + ctx.params.id);
            ctx.page.redirect("/");
        }
    }

    async function onComment(data) {
        data.gameId = ctx.params.id;

        await post("/data/comments", data);
        ctx.page.redirect(`/details/${ctx.params.id}`);
    }
}