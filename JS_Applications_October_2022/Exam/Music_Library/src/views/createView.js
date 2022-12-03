import { post } from "../api/api.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const createItemTemplate = (onCreate) => html`
  <section id="create">
    <div class="form">
      <h2>Add Album</h2>
      <form @submit=${onCreate} class="create-form">
        <input type="text" name="singer" id="album-singer" placeholder="Singer/Band" />
        <input type="text" name="album" id="album-album" placeholder="Album" />
        <input type="text" name="imageUrl" id="album-img" placeholder="Image url" />
        <input type="text" name="release" id="album-release" placeholder="Release date" />
        <input type="text" name="label" id="album-label" placeholder="Label" />
        <input type="text" name="sales" id="album-sales" placeholder="Sales" />

        <button type="submit">post</button>
      </form>
    </div>
  </section>
`;

export async function createView(ctx) {
    ctx.render(createItemTemplate(createSubmit(onCreate)));

    async function onCreate(data) {
        if (Object.values(data).some(item => item === "")) {
            return alert("All field are requierd");
        }

        await post("/data/albums", data);

        ctx.page.redirect("/dashboard");
    }
}