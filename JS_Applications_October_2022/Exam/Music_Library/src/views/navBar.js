import { logout } from "../api/user.js";
import { render, html } from "../lib.js";
import { getUser } from "../util.js";
import { page } from "../lib.js";

const nav = document.querySelector("header");

const navigationTemplate = (isLoged) => html`
    <!-- Navigation -->
    <a id="logo" href="/"><img id="logo-img" src="./images/logo.png" alt="" /></a>

    <nav>
        <div>
            <a href="/dashboard">Dashboard</a>
        </div>

        ${isLoged ? html`
            <div class="user">
                <a href="/create">Add Album</a>
                <a @click=${onLogout} href="javascript:void(0)">Logout</a>
            </div>` 
            : html`
            <div class="guest">
                <a href="/login">Login</a>
                <a href="/register">Register</a>
            </div>
            `}
    </nav>
`;

export function navBar() {
    const user = getUser();
    render(navigationTemplate(user), nav);
}

function onLogout() {
    logout();
    page.redirect("/dashboard");
}