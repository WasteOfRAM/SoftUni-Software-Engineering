import { clearUser, setUser } from "../util.js";
import { get, post } from "./api.js"

const endpoints = {
    "login": "/users/login",
    "register": "/users/register",
    "logout": "/users/logout"
}

export async function login(email, password) {
    const {_id, email: resultEmail, accessToken, gender, username} = await post(endpoints.login, {email, password});
    setUser({_id, email: resultEmail, accessToken, gender, username});

}

export async function register(username, email, password, gender) {
    const {_id, email: resultEmail, accessToken} = await post(endpoints.register, {username, email, password, gender});
    setUser({_id, email: resultEmail, accessToken, gender});
}

export async function logout() {
    get(endpoints.logout);
    clearUser();
}