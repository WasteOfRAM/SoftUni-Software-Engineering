import { post } from "../api/api.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const createItemTemplate = (onCreate) => html`
    <section id="create">
      <div class="form">
        <h2>Create Offer</h2>
        <form @submit=${onCreate} class="create-form">
          <input
            type="text"
            name="title"
            id="job-title"
            placeholder="Title"
          />
          <input
            type="text"
            name="imageUrl"
            id="job-logo"
            placeholder="Company logo url"
          />
          <input
            type="text"
            name="category"
            id="job-category"
            placeholder="Category"
          />
          <textarea
            id="job-description"
            name="description"
            placeholder="Description"
            rows="4"
            cols="50"
          ></textarea>
          <textarea
            id="job-requirements"
            name="requirements"
            placeholder="Requirements"
            rows="4"
            cols="50"
          ></textarea>
          <input
            type="text"
            name="salary"
            id="job-salary"
            placeholder="Salary"
          />
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

        await post("/data/offers", data);

        ctx.page.redirect("/dashboard");
    }
}