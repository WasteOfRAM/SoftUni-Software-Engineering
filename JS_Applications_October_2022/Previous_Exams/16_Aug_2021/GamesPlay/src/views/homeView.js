import { getRecent } from "../api/data.js";
import { html, repeat } from "../lib.js";

const itemCard = (item) => html`
    <div class="game">
        <div class="image-wrap">
            <img src=".${item.imageUrl}">
        </div>
        <h3>${item.title}</h3>
        <div class="rating">
            <span>☆</span><span>☆</span><span>☆</span><span>☆</span><span>☆</span>
        </div>
        <div class="data-buttons">
            <a href="/details/${item._id}" class="btn details-btn">Details</a>
        </div>
    </div>
`;

const homeViewTemplate = (data) => html`
    <section id="welcome-world">

        <div class="welcome-message">
            <h2>ALL new games are</h2>
            <h3>Only in GamesPlay</h3>
        </div>
        <img src="./images/four_slider_img01.png" alt="hero">

        <div id="home-page">
            <h1>Latest Games</h1>

            ${data.length === 0 ? html`<p class="no-articles">No games yet</p>` 
            : html`${repeat(data, item => item._id, itemCard)}`}   

        </div>
    </section>
`;

export async function homeView(ctx) {
    ctx.updateNavigation();

    const data = await getRecent();

    ctx.render(homeViewTemplate(data));
}