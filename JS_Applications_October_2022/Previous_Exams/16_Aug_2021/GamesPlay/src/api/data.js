import { get } from "./api.js";

const endpoints = {
    "getAllItems" : "/data/games?sortBy=_createdOn%20desc",
    "getItemById" : "/data/games/",
    "recentItems" : "/data/games?sortBy=_createdOn%20desc&distinct=category"
}

export async function getAllItems() {
    const data = await get(endpoints.getAllItems);
    return data;
}

export async function getItemById(id) {
    const data = await get(endpoints.getItemById + id);
    return data;
}

export async function getRecent() {
    const data = await get(endpoints.recentItems);
    return data;
}