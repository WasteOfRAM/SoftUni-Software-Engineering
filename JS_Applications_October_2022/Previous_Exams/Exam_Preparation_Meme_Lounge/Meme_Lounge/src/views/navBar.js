import { logout } from "../api/user.js";
import { render, html, nothing } from "../lib.js";
import { getUser } from "../util.js";
import { page } from "../lib.js";

const nav = document.querySelector("nav");

const navigationTemplate = (isLoged) => html`
    <a href="/dashboard">All Memes</a>

    ${
        isLoged ? html`
        <div class="user">
            <a href="/create">Create Meme</a>
            <div class="profile">
                <span>Welcome, ${isLoged ? isLoged.email : nothing}</span>
                <a href="/profile">My Profile</a>
                <a @click=${onLogout} href="javascript:void(0)">Logout</a>
            </div>
        </div>`
        : html`
        <div class="guest">
            <div class="profile">
                <a href="/login">Login</a>
                <a href="/register">Register</a>
            </div>
            <a class="active" href="/">Home Page</a>
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