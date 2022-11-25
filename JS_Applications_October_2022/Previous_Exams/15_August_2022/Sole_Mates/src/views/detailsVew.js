import { del } from "../api/api.js";
import { getAddByID } from "../api/data.js";
import { html, nothing } from "../lib.js";

const createDetailsTemplate = (data, userId, onDelete) => html`
    <section id="details">
        <div id="details-wrapper">
        <p id="details-title">Shoe Details</p>
        <div id="img-wrapper">
            <img src=${data.imageUrl} alt="example1" />
        </div>
        <div id="info-wrapper">
            <p>Brand: <span id="details-brand">${data.brand}</span></p>
            <p>
            Model: <span id="details-model">${data.model}</span>
            </p>
            <p>Release date: <span id="details-release">${data.release}</span></p>
            <p>Designer: <span id="details-designer">${data.designer}</span></p>
            <p>Value: <span id="details-value">${data.value}</span></p>
        </div>

        ${ data._ownerId === userId ? html`
        <!--Edit and Delete are only for creator-->
        <div id="action-buttons">
            <a href="/edit/${data._id}" id="edit-btn">Edit</a>
            <a @click=${onDelete} href="javascript:void(0)" id="delete-btn">Delete</a>
        </div>
        ` : nothing}
        </div>
    </section>
`;

export async function detailsView(ctx) {
    const data = await getAddByID(ctx.params.id);
    const userId = ctx.user ? ctx.user._id : "";
    ctx.render(createDetailsTemplate(data, userId, onDelete));

    async function onDelete() {
        await del("/data/shoes/" + ctx.params.id);

        ctx.page.redirect("/dashboard");
    }
}