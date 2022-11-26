import { del, get, post } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html, nothing } from "../lib.js";

const createDetailsTemplate = (data, userId, totalAplications, onDelete, onApply, userApplyed) => html`
    <section id="details">
        <div id="details-wrapper">
        <img id="details-img" src=${data.imageUrl} />
        <p id="details-title">${data.title}</p>
        <p id="details-category">
            Category: <span id="categories">${data.category}</span>
        </p>
        <p id="details-salary">
            Salary: <span id="salary-number">${data.salary}</span>
        </p>
        <div id="info-wrapper">
            <div id="details-description">
            <h4>Description</h4>
            <span>${data.description}</span>
            </div>
            <div id="details-requirements">
            <h4>Requirements</h4>
            <span>${data.requirements}</span>
            </div>
        </div>
        <p>Applications: <strong id="applications">${totalAplications}</strong></p>

        <div id="action-buttons">
        ${data._ownerId === userId ? html`
              <a href="/edit/${data._id}" id="edit-btn">Edit</a>
              <a @click=${onDelete} href="javascript:void(0)" id="delete-btn">Delete</a>`
        : userId != "" && !userApplyed? html`<a @click=${onApply} href="javascript:void(0)" id="apply-btn">Apply</a>` : nothing}
        </div>
    </section>
`;

export async function detailsView(ctx) {
    const data = await getItemById(ctx.params.id);
    const totalAplications = await get(`/data/applications?where=offerId%3D%22${data._id}%22&distinct=_ownerId&count`);
    const userId = ctx.user ? ctx.user._id : "";
    
    const userApplyed = await get(`/data/applications?where=offerId%3D%22${ctx.params.id}%22%20and%20_ownerId%3D%22${userId}%22&count`);

    ctx.render(createDetailsTemplate(data, userId, totalAplications, onDelete, onApply, !!userApplyed));

    async function onDelete() {
        const removeItem = confirm("Are you shure you want to permanetly delete the item?");

        if (removeItem) {
            await del("/data/offers/" + ctx.params.id);
            ctx.page.redirect("/dashboard");
        }
    }

    async function onApply() {
        await post("/data/applications", { "offerId" : data._id});
        ctx.page.redirect(`/details/${data._id}`);
    }
}