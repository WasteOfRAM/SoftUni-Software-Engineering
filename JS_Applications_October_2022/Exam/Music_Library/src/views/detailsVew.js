import { del, get, post } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html, nothing } from "../lib.js";

const createDetailsTemplate = (data, totalLikes, onDelete, onLike) => html`
    <section id="details">
        <div id="details-wrapper">
            <p id="details-title">Album Details</p>
            <div id="img-wrapper">
                <img src=${data.imageUrl} alt="example1" />
            </div>
            <div id="info-wrapper">
                <p><strong>Band:</strong><span id="details-singer">${data.singer}</span></p>
                <p>
                <strong>Album name:</strong><span id="details-album">${data.album}</span>
                </p>
                <p><strong>Release date:</strong><span id="details-release">${data.release}</span></p>
                <p><strong>Label:</strong><span id="details-label">${data.label}</span></p>
                <p><strong>Sales:</strong><span id="details-sales">${data.sales}</span></p>
            </div>
            <div id="likes">Likes: <span id="likes-count">${totalLikes}</span></div>

            <div id="action-buttons">
                <a @click=${onLike} href="javascript:void(0)" id="like-btn">Like</a>

                <a href="/edit/${data._id}" id="edit-btn">Edit</a>
                <a @click=${onDelete} href="javascript:void(0)" id="delete-btn">Delete</a>
            </div>
        </div>
    </section>
`;

export async function detailsView(ctx) {
    const data = await getItemById(ctx.params.id);
    const userId = ctx.user ? ctx.user._id : "";
    const totalLikes = await get(`/data/likes?where=albumId%3D%22${ctx.params.id}%22&distinct=_ownerId&count`);
    const userLiked = await get(`/data/likes?where=albumId%3D%22${ctx.params.id}%22%20and%20_ownerId%3D%22${userId}%22&count`);

    ctx.render(createDetailsTemplate(data, totalLikes, onDelete, onLike));

    userLikedOrOwner(!!userLiked, userId, data._ownerId)

    async function onDelete() {
        const removeItem = confirm("Are you shure you want to permanetly delete the item?");

        if (removeItem) {
            await del("/data/albums/" + ctx.params.id);
            ctx.page.redirect("/dashboard");
        }
    }

    async function onLike() {
        await post("/data/likes", {albumId: ctx.params.id})

        ctx.page.redirect(`/details/${ctx.params.id}`);
    }

    function userLikedOrOwner(userLiked, userId, ownerId) {
        const likeBtn = document.getElementById("like-btn");
        const editBtn = document.getElementById("edit-btn");
        const delBtn = document.getElementById("delete-btn");

        if (userLiked || userId === ownerId) {
            likeBtn.style.display = "none";
        } else {
            likeBtn.style.display = "block";
        }

        if (userId !== ownerId) {
            editBtn.style.display = "none";
            delBtn.style.display = "none";
        } else {
            editBtn.style.display = "block";
            delBtn.style.display = "block";
        }
    }
}