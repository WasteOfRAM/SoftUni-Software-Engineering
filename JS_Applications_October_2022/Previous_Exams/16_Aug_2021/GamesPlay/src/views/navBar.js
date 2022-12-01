import { logout } from "../api/user.js";
import { render, html } from "../lib.js";
import { getUser } from "../util.js";
import { page } from "../lib.js";

const header = document.querySelector("header");

const navigationTemplate = (isLoged) => html`
    <h1><a class="home" href="/">GamesPlay</a></h1>
    <nav>
    <a href="/catalogue">All games</a>
    ${ isLoged ? html`
        <div id="user">
            <a href="/create">Create Game</a>
            <a @click=${onLogout} href="javascript:void(0)">Logout</a>
        </div>`
        : html`
        <div id="guest">
            <a href="/login">Login</a>
            <a href="/register">Register</a>
        </div>`}
    </nav>
`;

export function navBar() {
    const user = getUser();
    render(navigationTemplate(user), header);
}

function onLogout() {
    logout();
    page.redirect("/");
}