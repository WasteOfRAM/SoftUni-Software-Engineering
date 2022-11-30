import { login } from "../api/user.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const loginTemplate = (onLogin) => html`
    <section id="loginaPage">
        <form @submit=${onLogin} class="loginForm">
            <h2>Login</h2>
            <div>
                <label for="email">Email:</label>
                <input id="email" name="email" type="text" placeholder="steven@abv.bg" value="">
            </div>
            <div>
                <label for="password">Password:</label>
                <input id="password" name="password" type="password" placeholder="********" value="">
            </div>

            <button class="btn" type="submit">Login</button>

            <p class="field">
                <span>If you don't have profile click <a href="/register">here</a></span>
            </p>
        </form>
    </section>
`
 
export function loginView(ctx) {
    ctx.render(loginTemplate(createSubmit(onLogin)));

    async function onLogin({email, password}) {
        if(!email || !password){
            return alert("All fields are required");
        }

        await login(email, password);

        ctx.page.redirect("/");
    }
}