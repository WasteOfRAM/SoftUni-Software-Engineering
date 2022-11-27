import { html } from "../lib.js";

const homeViewTemplate = () => html`
    <section id="welcome">
        <div id="welcome-container">
            <h1>Welcome To Meme Lounge</h1>
            <img src="/images/welcome-meme.jpg" alt="meme">
            <h2>Login to see our memes right away!</h2>
            <div id="button-div">
                <a href="/login" class="button">Login</a>
                <a href="/register" class="button">Register</a>
            </div>
        </div>
    </section>

    <footer class="footer">
            <p>Created by SoftUni Delivery Team</p>
    </footer>
`;

export function homeView(ctx) {
    ctx.updateNavigation();
    ctx.render(homeViewTemplate());
}