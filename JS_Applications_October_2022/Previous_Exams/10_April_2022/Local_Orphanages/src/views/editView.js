import { put } from "../api/api.js";
import { getItemById } from "../api/data.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const editPairTemplate = (data, onEdit) => html`
        <section id="edit-page" class="auth">
          <form @submit=${onEdit} id="edit">
              <h1 class="title">Edit Post</h1>
              <article class="input-group">
                  <label for="title">Post Title</label>
                  <input type="title" name="title" id="title" .value=${data.title}>
              </article>
              <article class="input-group">
                  <label for="description">Description of the needs </label>
                  <input type="text" name="description" id="description" .value=${data.description}>
              </article>
              <article class="input-group">
                  <label for="imageUrl"> Needed materials image </label>
                  <input type="text" name="imageUrl" id="imageUrl" .value=${data.imageUrl}>
              </article>
              <article class="input-group">
                  <label for="address">Address of the orphanage</label>
                  <input type="text" name="address" id="address" .value=${data.address}>
              </article>
              <article class="input-group">
                  <label for="phone">Phone number of orphanage employee</label>
                  <input type="text" name="phone" id="phone" .value=${data.phone}>
              </article>
              <input type="submit" class="btn submit" value="Edit Post">
          </form>
        </section>
`

export async function editView(ctx) {
  const data = await getItemById(ctx.params.id);

  ctx.render(editPairTemplate(data, createSubmit(onEdit)));

  async function onEdit(data) {
    if (Object.values(data).some(item => item === "")) {
      return alert("All field are requierd");
    }

    await put("/data/posts/" + ctx.params.id, data);

    ctx.page.redirect("/details/" + ctx.params.id);
  }
}