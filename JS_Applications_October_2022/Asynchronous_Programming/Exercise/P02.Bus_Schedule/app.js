function solve() {
    const departButton = document.getElementById("depart");
    const arriveButton = document.getElementById("arrive");
    const infoBox = document.getElementsByClassName("info")[0];

    let nextStopId = "depot";
    let currentStopName;

    async function depart() {

        try {
            const response = await fetch(`http://localhost:3030/jsonstore/bus/schedule/${nextStopId}`);

            if (response.ok === false || response.status === 204) {
                throw new Error("Error");
            }

            const data = await response.json();

            infoBox.textContent = `Next stop ${data.name}`;
            currentStopName = data.name;
            nextStopId = data.next;


            departButton.disabled = true;
            arriveButton.disabled = false;

        } catch (error) {
            infoBox.textContent = "Error";

            departButton.disabled = true;
            arriveButton.disabled = true;
        }

    }

    function arrive() {
        departButton.disabled = false;
        arriveButton.disabled = true;

        infoBox.textContent = `Arriving at ${currentStopName}`;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();