function getInfo() {
    const stopId = document.getElementById("stopId").value;

    fetch(`http://localhost:3030/jsonstore/bus/businfo/${stopId}`)
        .then(responseHandler)
        .then(dataHandler)
        .catch(errorHandler);
}

function responseHandler(response) {
    if (response.status === 204) {
        throw new Error("Error");
    }

    return response.json();
}

function dataHandler(data) {
    document.getElementById("stopName").textContent = data.name;

    const busesList = document.getElementById("buses");

    const liElements = Object.entries(data.buses).map(busInfo => {
        const li = document.createElement("li");
        li.textContent = `Bus ${busInfo[0]} arrives in ${busInfo[1]} minutes`;

        return li;
    });

    busesList.replaceChildren(...liElements);
}

function errorHandler(error) {
    document.getElementById("buses").textContent = "";
    document.getElementById("stopName").textContent = "Error";
}