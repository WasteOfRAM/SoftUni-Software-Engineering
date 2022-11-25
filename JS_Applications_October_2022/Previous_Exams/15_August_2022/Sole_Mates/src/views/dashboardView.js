import { getAllAdds } from "../api/data.js";
import { html, repeat } from "../lib.js";

const shoeCardTemplate = (data) => html`
    <li class="card">
        <img src=${data.imageUrl} alt="travis" />
        <p>
            <strong>Brand: </strong><span class="brand">${data.brand}</span>
        </p>
        <p>
            <strong>Model: </strong> <span class="model">${data.model}</span>
        </p>
        <p><strong>Value:</strong><span class="value">${data.value}</span>$</p>
        <a class="details-btn" href="/details/${data._id}">Details</a>
    </li>
`;

const dashboardTemplate = (data) => html`
    <section id="dashboard">
        <h2>Collectibles</h2>
            ${data.length !== 0 ? 
                html`<ul class="card-wrapper"> ${repeat(data, (item) => item._id, shoeCardTemplate)} </ul>` 
                : html`<h2>There are no items added yet.</h2>`}
    </section>
`

export async function dashboardView(ctx){
    ctx.updateNavigation();

    const data = await getAllAdds();
    
    ctx.render(dashboardTemplate(data));
}