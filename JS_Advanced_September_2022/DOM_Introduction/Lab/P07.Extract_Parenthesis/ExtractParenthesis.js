function extract(elementId) {
    let content = document.getElementById(elementId).textContent;
    let pattern = /\(([^)]+)\)/g;
    let extracted = content.matchAll(pattern);
    let matches = [];

    for (const text of extracted) {
        matches.push(text[1]);
    }

    return matches.join("; ");
}

console.log(extract("content"));