import { post, put } from "../api/api.js";
import { getAddByID } from "../api/data.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const editPairTemplate = (data, onEdit) => html`
<section id="edit">
          <div class="form">
            <h2>Edit item</h2>
            <form @submit=${onEdit} class="edit-form">
              <input
                type="text"
                name="brand"
                id="shoe-brand"
                placeholder="Brand"
                .value=${data.brand}
              />
              <input
                type="text"
                name="model"
                id="shoe-model"
                placeholder="Model"
                .value=${data.model}
              />
              <input
                type="text"
                name="imageUrl"
                id="shoe-img"
                placeholder="Image url"
                .value=${data.imageUrl}
              />
              <input
                type="text"
                name="release"
                id="shoe-release"
                placeholder="Release date"
                .value=${data.release}
              />
              <input
                type="text"
                name="designer"
                id="shoe-designer"
                placeholder="Designer"
                .value=${data.designer}
              />
              <input
                type="text"
                name="value"
                id="shoe-value"
                placeholder="Value"
                .value=${data.value}
              />

              <button type="submit">post</button>
            </form>
          </div>
        </section>
`

export async function editView(ctx) {
    const data = await getAddByID(ctx.params.id);

    ctx.render(editPairTemplate(data, createSubmit(onEdit)));

    async function onEdit(data) {
        if (Object.values(data).some(item => item === "")) {
            return alert("All field are requierd");
        }

        await put("/data/shoes/" + ctx.params.id, data);

        ctx.page.redirect("/details/" + ctx.params.id);
    }
}