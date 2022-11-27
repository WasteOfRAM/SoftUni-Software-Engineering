import { page } from "./lib.js";
import { render } from "./lib.js";
import { getUser } from "./util.js";
import { createView } from "./views/createView.js";
import { dashboardView } from "./views/dashboardView.js";
import { detailsView } from "./views/detailsVew.js";
import { editView } from "./views/editView.js";
import { loginView } from "./views/loginView.js";
import { navBar } from "./views/navBar.js";
import { registerView } from "./views/registerView.js";
import { userItemsView } from "./views/userPostsView.js";


const main = document.querySelector("main");

page(contextDecoration);
page("/", dashboardView);
page("/login", loginView);
page("/register", registerView);
page("/myposts", userItemsView);
page("/create", createView);
page("/details/:id", detailsView);
page("/edit/:id", editView);

navBar();
page.start();

function contextDecoration(ctx, next){
    ctx.user = getUser("userData");
    ctx.render = (content) => render(content, main);
    ctx.updateNavigation = navBar;

    next();
}