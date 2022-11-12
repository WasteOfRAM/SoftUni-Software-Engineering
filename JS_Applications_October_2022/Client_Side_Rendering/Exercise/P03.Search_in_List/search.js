import { html, render } from "./node_modules/lit-html/lit-html.js";
import { towns } from "./towns.js";

const root = document.getElementById("towns");
const result = document.getElementById("result");
document.querySelector("button").addEventListener("click", search);

const townsList = (data) => html`
    <ul>
        ${data.map(item => html`
                <li>${item}</li>
            `)
    }
    </ul>
`

render(townsList(towns), root);

function search() {
    const list = document.querySelector("ul");
    const serachParam = document.getElementById("searchText").value;
    let matches = 0;

    if(!serachParam){
        [...list.children].forEach(li => li.classList.remove("active"));
        result.textContent = "";
        render(townsList(towns), root);
        return;
    }

    [...list.children].forEach(li => {
        if(li.textContent.includes(serachParam)){
            li.classList.add("active");
            matches++;
        } else {
            li.classList.remove("active");
        }
    });

    result.textContent = `${matches} matches found`;

    render(townsList(towns), root);
}
