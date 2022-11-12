export async function httpRequest(url, method, body) {
    const headers = {
        method,
        headers: {}
    }

    if (body) {
        headers.headers["Content-Type"] = "application/json";
        headers.body = JSON.stringify(body);
    }
    
    const response = await fetch(url, headers);

    return await response.json();
}