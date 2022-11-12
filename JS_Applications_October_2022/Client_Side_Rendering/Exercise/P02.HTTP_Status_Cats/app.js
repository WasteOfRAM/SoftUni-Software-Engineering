import { render, html } from "./node_modules/lit-html/lit-html.js";
import { cats } from "./catSeeder.js";

const root = document.getElementById("allCats");

const showStatusMsg = "Show status code";
const hideStatusMsg = "Hide status code";

const catsCards = (data) => html`
    <ul>
        ${
            data.map(item => html`
            <li>
                <img src=${"./images/" + item.imageLocation + ".jpg"} width="250" height="250" alt="Card image cap">
                <div class="info">
                    <button class="showBtn" @click=${onToggle.bind(null, item.id)}>Show status code</button>
                    <div class="status" style="display: none" id=${item.id}>
                        <h4>Status Code: ${item.statusCode}</h4>
                        <p>${item.statusMessage}</p>
                    </div>
                </div>
            </li>
            `)
        }
    </ul>
`;

render(catsCards(cats), root);

function onToggle(id) {
    const element = document.getElementById(id);
    const btnElement = element.previousElementSibling;

    if(element.style.display === "none"){
        element.style.display = "block";
        btnElement.textContent = hideStatusMsg;
    } else {
        element.style.display = "none";
        btnElement.textContent = showStatusMsg;
    }

    render(catsCards(cats), root);
}