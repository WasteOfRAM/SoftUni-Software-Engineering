import { logout } from "../api/user.js";
import { render, html } from "../lib.js";
import { getUser } from "../util.js";
import { page } from "../lib.js";

const nav = document.querySelector("nav");

const navigationTemplate = (isLoged) => html`
    <a href="/">Theater</a>
    <ul>
    ${ isLoged ? html`
        <!--Only users-->
        <li><a href="/profile">Profile</a></li>
        <li><a href="/create">Create Event</a></li>
        <li><a @click=${onLogout} href="javascript:void(0)">Logout</a></li>`
        : html`
        <li><a href="/login">Login</a></li>
        <li><a href="/register">Register</a></li>`
    }
    </ul>`;

export function navBar() {
    const user = getUser();
    render(navigationTemplate(user), nav);
}

function onLogout() {
    logout();
    page.redirect("/");
}