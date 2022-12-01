import { register } from "../api/user.js";
import { html } from "../lib.js";
import { createSubmit } from "../util.js";

const registerTemplate = (onRegister) => html`
    <section id="register-page" class="content auth">
        <form @submit=${onRegister} id="register">
            <div class="container">
                <div class="brand-logo"></div>
                <h1>Register</h1>

                <label for="email">Email:</label>
                <input type="email" id="email" name="email" placeholder="maria@email.com">

                <label for="pass">Password:</label>
                <input type="password" name="password" id="register-password">

                <label for="con-pass">Confirm Password:</label>
                <input type="password" name="confirm-password" id="confirm-password">

                <input class="btn submit" type="submit" value="Register">

                <p class="field">
                    <span>If you already have profile click <a href="/login">here</a></span>
                </p>
            </div>
        </form>
    </section>
`;

export function registerView(ctx) {
    ctx.render(registerTemplate(createSubmit(onRegister)));

    async function onRegister(data) {

        if(!data.email || !data.password || !data["confirm-password"]){
            return alert("All fields are required");
        }

        if(data.password !== data["confirm-password"]){
            return alert("Passwords don\'t match");
        }

        await register(data.email, data.password);

        ctx.page.redirect("/");
    }
}