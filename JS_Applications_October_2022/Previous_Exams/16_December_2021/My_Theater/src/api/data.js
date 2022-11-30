import { get } from "./api.js";

const endpoints = {
    "getAllItems" : "/data/theaters?sortBy=_createdOn%20desc&distinct=title",
    "getItemById" : "/data/theaters/"
}

export async function getAllItems() {
    const data = await get(endpoints.getAllItems);
    return data;
}

export async function getItemById(id) {
    const data = await get(endpoints.getItemById + id);
    return data;
}
