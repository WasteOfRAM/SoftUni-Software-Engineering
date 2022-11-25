import { page } from "./lib.js";
import { render } from "./lib.js";
import { getUser } from "./util.js";
import { createView } from "./views/createView.js";
import { dashboardView } from "./views/dashboardView.js";
import { detailsView } from "./views/detailsVew.js";
import { editView } from "./views/editView.js";
import { homeView } from "./views/homeView.js";
import { loginView } from "./views/loginView.js";
import { navBar } from "./views/navBar.js";
import { registerView } from "./views/registerView.js";
import { searchView } from "./views/searchView.js";


const main = document.querySelector("main");

page(contextDecoration);
page("/", homeView);
page("/home", homeView);
page("/login", loginView);
page("/register", registerView);
page("/dashboard", dashboardView);
page("/details/:id", detailsView);
page("/create", createView);
page("/edit/:id", editView);
page("/search", searchView);

navBar();
page.start();

function contextDecoration(ctx, next){
    ctx.user = getUser("userData");
    ctx.render = (content) => render(content, main);
    ctx.updateNavigation = navBar;

    next();
}