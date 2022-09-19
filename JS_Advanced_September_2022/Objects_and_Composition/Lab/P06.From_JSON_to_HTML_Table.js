function json(jsonString) {
    let objArray = JSON.parse(jsonString);
    let tableColumnNames = Object.keys(objArray[0]);
    let result = `<table>\r\n`;

    result += `<tr>`;
    for (const columnName of tableColumnNames) {
        result += `<th>${columnName}</th>`;
    }
    result += '</tr>\r\n';

    let values = objArray.map(obj => Object.values(obj));
    
    for (const value of values) {
        result += `<tr>`;
        for (const v of value) {
            result += `<td>${v}</td>`;
        }
        result += '</tr>\r\n';
    }

    result += `</table>`;

    result.replace('<', "&lt;")
                .replace('>', "&gt;")
                .replace('"', "&quot;")
                .replace("'", "&#39;")
                .replace('&', "&amp;")
                .replace("/", "&#x2F;");

    return result;
}

console.log(json(`[{"Name":"Stamat", "Score":5.5}, {"Name":"Rumen", "Score":6}]`));

console.log('-----------------------');

console.log(json(`[{"Name":"Pesho",
"Score":4,
"Grade":8},
{"Name":"Gosho",
"Score":5,
"Grade":8},
{"Name":"Angel",
"Score":5.50,
"Grade":10}]`));