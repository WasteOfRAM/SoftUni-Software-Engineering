import { getAllItems } from "../api/data.js";
import { html, repeat } from "../lib.js";

const shoeCardTemplate = (data) => html`
    <div class="offer">
        <img src=${data.imageUrl} alt="example1" />
        <p>
          <strong>Title: </strong><span class="title">${data.title}</span>
        </p>
        <p><strong>Salary:</strong><span class="salary">${data.salary}</span></p>
        <a class="details-btn" href="/details/${data._id}">Details</a>
    </div>
`;

const dashboardTemplate = (data) => html`
    <section id="dashboard">
      <h2>Job Offers</h2>
        ${data.length !== 0 ? 
            html`${repeat(data, (item) => item._id, shoeCardTemplate)}` 
            : html`<h2>No offers yet.</h2>`}
    </section>
`

export async function dashboardView(ctx){
    ctx.updateNavigation();

    const data = await getAllItems();
    
    ctx.render(dashboardTemplate(data));
}