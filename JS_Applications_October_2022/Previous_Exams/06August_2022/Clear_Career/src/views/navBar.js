import { logout } from "../api/user.js";
import { render, html } from "../lib.js";
import { getUser } from "../util.js";
import { page } from "../lib.js";

const nav = document.querySelector("nav");

const navigationTemplate = (isLoged) => html`
    <div>
        <a href="/dashboard">Dashboard</a>
    </div>

    ${
        isLoged ? html`
        <div class="user">
            <a href="/create">Create Offer</a>
            <a @click=${onLogout} href="javascript:void(0)">Logout</a>
          </div>`
        : html`
        <div class="guest">
            <a href="/login">Login</a>
            <a href="/register">Register</a>
          </div>`
    }
`;

export function navBar() {
    const user = getUser();
    render(navigationTemplate(user), nav);
}

function onLogout() {
    logout();
    page.redirect("/");
}