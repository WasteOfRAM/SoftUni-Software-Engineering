function attachEvents() {
    const getButton = document.getElementById("submit");
    getButton.addEventListener("click", getLocation);
    const forecast = document.getElementById("forecast");

    const weatherSymbols = {
        "Sunny": `&#x2600`,
        "Partly sunny": `&#x26C5`,
        "Overcast": `&#x2601`,
        "Rain": `&#x2614`,
        "Degrees": `&#176`
    }

    async function getLocation(e) {
        const locationInput = document.getElementById("location").value;

        try {
            const locationsList = await fetch(`http://localhost:3030/jsonstore/forecaster/locations/`)
                .then(response => response.json())
                .then(data => { return data; });

            const location = locationsList.find(loc => loc.name === locationInput);

            if (location === undefined) {
                throw new Error("No location");
            }

            const todayForcast = await fetch(`http://localhost:3030/jsonstore/forecaster/today/${location.code}`)
                .then(response => response.json())
                .then(data => { return data; });

            const upcomingForcast = await fetch(`http://localhost:3030/jsonstore/forecaster/upcoming/${location.code}`)
                .then(response => response.json())
                .then(data => { return data; });



            forecast.style.display = "block";
            forecast.textContent = "";

            const currentElement = current(todayForcast);
            const upcomingElement = upcoming(upcomingForcast);

            forecast.appendChild(currentElement);
            forecast.appendChild(upcomingElement);

        } catch (err) {
            forecast.style.display = "block";
            forecast.textContent = "Error";
        }
    }

    function current(todayForcast) {
        const currentDiv = document.createElement("div");
        currentDiv.setAttribute("id", "current");

        const lableDiv = document.createElement("div");
        lableDiv.classList.add("label");
        lableDiv.textContent = "Current conditions";
        currentDiv.appendChild(lableDiv);

        const forecastsDiv = document.createElement("div");
        forecastsDiv.classList.add("forecasts");
        currentDiv.appendChild(forecastsDiv);

        const symbolSpan = document.createElement("span");
        symbolSpan.classList.add("condition", "symbol");
        symbolSpan.innerHTML = weatherSymbols[todayForcast.forecast.condition];
        forecastsDiv.appendChild(symbolSpan);

        const conditionsSpan = document.createElement("span");
        conditionsSpan.classList.add("condition");
        forecastsDiv.appendChild(conditionsSpan);

        const locationName = document.createElement("span");
        locationName.classList.add("forecast-data");
        locationName.textContent = todayForcast.name;
        conditionsSpan.appendChild(locationName);

        const temperatureSpan = document.createElement("span");
        temperatureSpan.classList.add("forecast-data");
        temperatureSpan.innerHTML = `${todayForcast.forecast.low}${weatherSymbols["Degrees"]}/${todayForcast.forecast.high}${weatherSymbols["Degrees"]}`;
        conditionsSpan.appendChild(temperatureSpan);

        const condition = document.createElement("span");
        condition.classList.add("forecast-data");
        condition.textContent = todayForcast.forecast.condition;
        conditionsSpan.appendChild(condition);

        return currentDiv;
    }

    function upcoming(upcomingForcast) {
        const upcomingDiv = document.createElement("div");
        upcomingDiv.setAttribute("id", "upcoming");

        const lableDiv = document.createElement("div");
        lableDiv.classList.add("label");
        lableDiv.textContent = "Three-day forecast";
        upcomingDiv.appendChild(lableDiv);

        const forecastsDiv = document.createElement("div");
        forecastsDiv.classList.add("forecast-info");
        upcomingDiv.appendChild(forecastsDiv);

        const items = upcomingForcast.forecast.map(data => {
            const conditionsSpan = document.createElement("span");
            conditionsSpan.classList.add("upcoming");

            const symbol = document.createElement("span");
            symbol.classList.add("symbol");
            symbol.innerHTML = weatherSymbols[data.condition];
            conditionsSpan.appendChild(symbol);

            const temperature = document.createElement("span");
            temperature.classList.add("forecast-data");
            temperature.innerHTML = `${data.low}${weatherSymbols["Degrees"]}/${data.high}${weatherSymbols["Degrees"]}`;
            conditionsSpan.appendChild(temperature);

            const condition = document.createElement("span");
            condition.classList.add("forecast-data");
            condition.textContent = data.condition;
            conditionsSpan.appendChild(condition);

            return conditionsSpan;
        })

        forecastsDiv.replaceChildren(...items);

        return upcomingDiv;
    }
}

attachEvents();