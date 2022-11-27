import { del, get, post } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html, nothing } from "../lib.js";

const createDetailsTemplate = (data, userId, onDelete, totalDonations, userDonated, onDonation) => html`
    <section id="details-page">
        <h1 class="title">Post Details</h1>

        <div id="container">
            <div id="details">
                <div class="image-wrapper">
                    <img src=${data.imageUrl} alt="Material Image" class="post-image">
                </div>
                <div class="info">
                    <h2 class="title post-title">${data.title}</h2>
                    <p class="post-description">Description: ${data.description}</p>
                    <p class="post-address">Address: ${data.address}</p>
                    <p class="post-number">Phone number: ${data.phone}</p>
                    <p class="donate-Item">Donate Materials: ${totalDonations}</p>

                    ${userId != "" ? html`
                        <div class="btns">
                            ${userId === data._ownerId ? html`
                            <a href="/edit/${data._id}" class="edit-btn btn">Edit</a>
                            <a @click=${onDelete} href="javascript:void(0)" class="delete-btn btn">Delete</a>` 
                            : !userDonated ? html`
                            <a @click=${onDonation} href="javascript:void(0)" class="donate-btn btn">Donate</a>` : nothing}
                        </div>` : nothing}
                </div>
            </div>
        </div>
    </section>
`;

export async function detailsView(ctx) {
    const data = await getItemById(ctx.params.id);
    const totalDonations =
        await get(`/data/donations?where=postId%3D%22${data._id}%22&distinct=_ownerId&count`);

    const userId = ctx.user ? ctx.user._id : "";

    const userDonated =
        await get(`/data/donations?where=postId%3D%22${data._id}%22%20and%20_ownerId%3D%22${userId}%22&count`);

    
    ctx.render(createDetailsTemplate(data, userId, onDelete, totalDonations, !!userDonated, onDonation));

    async function onDelete() {
        const removeItem = confirm("Are you shure you want to permanetly delete the item?");

        if (removeItem) {
            await del("/data/posts/" + ctx.params.id);
            ctx.page.redirect("/");
        }
    }

    async function onDonation() {
        await post("/data/donations", { "postId": data._id });
        ctx.page.redirect(`/details/${data._id}`);
    }
}