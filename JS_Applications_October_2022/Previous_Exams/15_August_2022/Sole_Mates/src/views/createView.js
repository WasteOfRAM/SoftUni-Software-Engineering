import { post } from "../api/api.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const createPairTemplate = (onCreate) => html`
<section id="create">
<div class="form">
  <h2>Add item</h2>
  <form @submit=${onCreate} class="create-form">
    <input
      type="text"
      name="brand"
      id="shoe-brand"
      placeholder="Brand"
    />
    <input
      type="text"
      name="model"
      id="shoe-model"
      placeholder="Model"
    />
    <input
      type="text"
      name="imageUrl"
      id="shoe-img"
      placeholder="Image url"
    />
    <input
      type="text"
      name="release"
      id="shoe-release"
      placeholder="Release date"
    />
    <input
      type="text"
      name="designer"
      id="shoe-designer"
      placeholder="Designer"
    />
    <input
      type="text"
      name="value"
      id="shoe-value"
      placeholder="Value"
    />

    <button type="submit">post</button>
  </form>
</div>
</section>
`

export async function createView(ctx) {
    ctx.render(createPairTemplate(createSubmit(onCreate)));

    async function onCreate(data) {
        if (Object.values(data).some(item => item === "")) {
            return alert("All field are requierd");
        }

        await post("/data/shoes", data);

        ctx.page.redirect("/dashboard");
    }
}