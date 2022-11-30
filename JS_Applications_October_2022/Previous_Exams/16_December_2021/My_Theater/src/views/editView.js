import { put } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const editItemTemplate = (data, onEdit) => html`
  <section id="editPage">
    <form @submit=${onEdit} class="theater-form">
        <h1>Edit Theater</h1>
        <div>
            <label for="title">Title:</label>
            <input id="title" name="title" type="text" placeholder="Theater name" value=${data.title}>
        </div>
        <div>
            <label for="date">Date:</label>
            <input id="date" name="date" type="text" placeholder="Month Day, Year" value=${data.date} >
        </div>
        <div>
            <label for="author">Author:</label>
            <input id="author" name="author" type="text" placeholder="Author"
            value=${data.author} >
        </div>
        <div>
            <label for="description">Theater Description:</label>
            <textarea id="description" name="description"
                placeholder="Description" >${data.description}</textarea>
        </div>
        <div>
            <label for="imageUrl">Image url:</label>
            <input id="imageUrl" name="imageUrl" type="text" placeholder="Image Url" value=${data.imageUrl}>
        </div>
        <button class="btn" type="submit">Submit</button>
    </form>
  </section>
`

export async function editView(ctx) {
    const data = await getItemById(ctx.params.id);
    console.log(data);

    ctx.render(editItemTemplate(data, createSubmit(onEdit)));

    async function onEdit(data) {
        if (Object.values(data).some(item => item === "")) {
            return alert("All field are requierd");
        }

        await put("/data/theaters/" + ctx.params.id, data);

        ctx.page.redirect("/details/" + ctx.params.id);
    }
}