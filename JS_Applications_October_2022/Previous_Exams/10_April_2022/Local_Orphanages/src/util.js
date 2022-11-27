export function getUser() {
    return JSON.parse(sessionStorage.getItem("userData"));
}

export function setUser(userData) {
    sessionStorage.setItem("userData", JSON.stringify(userData));
}

export function clearUser() {
    sessionStorage.removeItem("userData");
}

export function createSubmit(callback){
    return function (event) {
        event.preventDefault();
        const formData = new FormData(event.target);
        const data = Object.fromEntries(formData);

        callback(data, event.target);
    }
}