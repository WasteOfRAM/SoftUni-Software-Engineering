import { get } from "./api.js";

const endpoints = {
    "getAllItems" : "/data/albums?sortBy=_createdOn%20desc",
    "getItemById" : "/data/albums/"
}

export async function getAllItems() {
    const data = await get(endpoints.getAllItems);
    return data;
}

export async function getItemById(id) {
    const data = await get(endpoints.getItemById + id);
    return data;
}
