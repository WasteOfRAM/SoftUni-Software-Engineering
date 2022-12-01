import { put } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const editItemTemplate = (data, onEdit) => html`
  <section id="edit-page" class="auth">
    <form @submit=${onEdit} id="edit">
        <div class="container">

            <h1>Edit Game</h1>
            <label for="leg-title">Legendary title:</label>
            <input type="text" id="title" name="title" .value=${data.title}>

            <label for="category">Category:</label>
            <input type="text" id="category" name="category" .value=${data.category}>

            <label for="levels">MaxLevel:</label>
            <input type="number" id="maxLevel" name="maxLevel" min="1" .value=${data.maxLevel}>

            <label for="game-img">Image:</label>
            <input type="text" id="imageUrl" name="imageUrl" .value=${data.imageUrl}>

            <label for="summary">Summary:</label>
            <textarea name="summary" id="summary">${data.summary}</textarea>
            <input class="btn submit" type="submit" value="Edit Game">

        </div>
    </form>
  </section>
`

export async function editView(ctx) {
    const data = await getItemById(ctx.params.id);

    ctx.render(editItemTemplate(data, createSubmit(onEdit)));

    async function onEdit(data) {
        if (Object.values(data).some(item => item === "")) {
            return alert("All field are requierd");
        }

        await put("/data/games/" + ctx.params.id, data);

        ctx.page.redirect("/details/" + ctx.params.id);
    }
}