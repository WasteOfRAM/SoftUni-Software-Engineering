import { get } from "./api.js";

const endpoints = {
    "getAllAdds": "/data/shoes?sortBy=_createdOn%20desc",
    "getAddById": "/data/shoes/"
}

export async function getAllAdds() {
    return await get(endpoints.getAllAdds);
}

export async function getAddByID(id) {
    return await get(endpoints.getAddById + id);
}