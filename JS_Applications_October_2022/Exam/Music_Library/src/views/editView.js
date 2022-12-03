import { put } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const editItemTemplate = (data, onEdit) => html`
<section id="edit">
<div class="form">
  <h2>Edit Album</h2>
  <form @submit=${onEdit} class="edit-form">
    <input type="text" name="singer" id="album-singer" placeholder="Singer/Band" .value=${data.singer} />
    <input type="text" name="album" id="album-album" placeholder="Album" .value=${data.album} />
    <input type="text" name="imageUrl" id="album-img" placeholder="Image url" .value=${data.imageUrl} />
    <input type="text" name="release" id="album-release" placeholder="Release date" .value=${data.release} />
    <input type="text" name="label" id="album-label" placeholder="Label" .value=${data.label} />
    <input type="text" name="sales" id="album-sales" placeholder="Sales" .value=${data.sales} />

    <button type="submit">post</button>
  </form>
</div>
</section>
`

export async function editView(ctx) {
    const data = await getItemById(ctx.params.id);

    ctx.render(editItemTemplate(data, createSubmit(onEdit)));

    async function onEdit(data) {
        if (Object.values(data).some(item => item === "")) {
            return alert("All field are requierd");
        }

        await put("/data/albums/" + ctx.params.id, data);

        ctx.page.redirect("/details/" + ctx.params.id);
    }
}