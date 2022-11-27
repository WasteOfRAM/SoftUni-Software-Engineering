import { del, get, post } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html, nothing } from "../lib.js";

const createDetailsTemplate = (data, userId, onDelete) => html`
    <section id="meme-details">
        <h1>Meme Title: ${data.title}</h1>
        <div class="meme-details">
            <div class="meme-img">
                <img alt="meme-alt" src=${data.imageUrl}>
            </div>
            <div class="meme-description">
                <h2>Meme Description</h2>
                <p>
                    ${data.description}
                </p>

                <!-- Buttons Edit/Delete should be displayed only for creator of this meme  -->
                ${userId === data._ownerId ? html`
                    <a class="button warning" href="/edit/${data._id}">Edit</a>
                    <button @click=${onDelete} class="button danger">Delete</button>` :
                    nothing}
            </div>
        </div>
    </section>
`;

export async function detailsView(ctx) {
    const data = await getItemById(ctx.params.id);
    const userId = ctx.user ? ctx.user._id : "";


    ctx.render(createDetailsTemplate(data, userId, onDelete));

    async function onDelete() {
        const removeItem = confirm("Are you shure you want to permanetly delete the item?");

        if (removeItem) {
            await del("/data/memes/" + ctx.params.id);
            ctx.page.redirect("/dashboard");
        }
    }
}