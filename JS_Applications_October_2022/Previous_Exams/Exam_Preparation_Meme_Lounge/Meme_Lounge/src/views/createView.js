import { post } from "../api/api.js";
import { errorNotifications } from "../api/notifications.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const createItemTemplate = (onCreate) => html`
  <section id="create-meme">
    <form @submit=${onCreate} id="create-form">
        <div class="container">
            <h1>Create Meme</h1>
            <label for="title">Title</label>
            <input id="title" type="text" placeholder="Enter Title" name="title">
            <label for="description">Description</label>
            <textarea id="description" placeholder="Enter Description" name="description"></textarea>
            <label for="imageUrl">Meme Image</label>
            <input id="imageUrl" type="text" placeholder="Enter meme ImageUrl" name="imageUrl">
            <input type="submit" class="registerbtn button" value="Create Meme">
        </div>
    </form>
  </section>
`;

export async function createView(ctx) {
    ctx.render(createItemTemplate(createSubmit(onCreate)));

    async function onCreate(data) {
        if (Object.values(data).some(item => item === "")) {
            return errorNotifications("All field are requierd");
        }

        await post("/data/memes", data);

        ctx.page.redirect("/dashboard");
    }
}