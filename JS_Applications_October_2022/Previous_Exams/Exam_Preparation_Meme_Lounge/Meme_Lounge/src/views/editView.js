import { put } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { errorNotifications } from "../api/notifications.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const editItemTemplate = (data, onEdit) => html`
  <section id="edit-meme">
    <form @submit=${onEdit} id="edit-form">
        <h1>Edit Meme</h1>
        <div class="container">
            <label for="title">Title</label>
            <input id="title" type="text" placeholder="Enter Title" name="title" .value=${data.title}>
            <label for="description">Description</label>
            <textarea id="description" placeholder="Enter Description" name="description" .value=${data.description}>
            </textarea>
            <label for="imageUrl">Image Url</label>
            <input id="imageUrl" type="text" placeholder="Enter Meme ImageUrl" name="imageUrl" .value=${data.imageUrl}>
            <input type="submit" class="registerbtn button" value="Edit Meme">
        </div>
    </form>
  </section>
`

export async function editView(ctx) {
    const data = await getItemById(ctx.params.id);

    ctx.render(editItemTemplate(data, createSubmit(onEdit)));

    async function onEdit(data) {
        if (Object.values(data).some(item => item === "")) {
            return errorNotifications("All field are requierd");
        }

        await put("/data/memes/" + ctx.params.id, data);

        ctx.page.redirect("/details/" + ctx.params.id);
    }
}