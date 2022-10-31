async function solution() {
    const main = document.getElementById("main");

    const articlesListUrl = "http://localhost:3030/jsonstore/advanced/articles/list/";
    const articlesList = await fetch(articlesListUrl).then(response => response.json());

    const acordionElements = createAccordionHtml(articlesList);
    main.replaceChildren(...acordionElements);
}

function createAccordionHtml(articlesList) {
    const elements = Object.values(articlesList).map(articleData => {
        const accordion = document.createElement("div");
        accordion.classList.add("accordion");

        const head = document.createElement("div");
        head.classList.add("head");
        accordion.appendChild(head);

        const titleSpan = document.createElement("span");
        titleSpan.textContent = articleData.title;
        head.appendChild(titleSpan);

        const button = document.createElement("button");
        button.classList.add("button");
        button.textContent = "More";
        button.id = articleData._id;
        head.appendChild(button);
        button.addEventListener("click", toggle);

        const extra = document.createElement("div");
        extra.classList.add("extra");
        accordion.appendChild(extra);

        const articleText = document.createElement("p");
        extra.appendChild(articleText);

        return accordion;
    });

    return elements;
}


async function toggle(e) {
    const extra = e.target.parentElement.nextElementSibling;
    const button = document.getElementById(`${e.target.id}`);
    const articleUrl = "http://localhost:3030/jsonstore/advanced/articles/details/";

    if (extra.style.display === 'none' || extra.style.display === '') {
        const articleText = await fetch(articleUrl + e.target.id).then(response => response.json());
        extra.children[0].textContent = articleText.content;

        extra.style.display = 'block';
        button.textContent = 'Less';
    } else {
        extra.style.display = 'none';
        button.textContent = 'More';
    }
}

solution();