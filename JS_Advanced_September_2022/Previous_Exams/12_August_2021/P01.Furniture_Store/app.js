window.addEventListener('load', solve);

function solve() {
    document.getElementById("add").addEventListener("click", addFurniture);

    let modelField = document.getElementById("model");
    let yearlField = document.getElementById("year");
    let descriptionField = document.getElementById("description");
    let pricelField = document.getElementById("price");

    let furnitureList = document.getElementById("furniture-list");
    
    let totalPriceElement = document.querySelector(".total-price");

    function addFurniture(e) {
        e.preventDefault();

        let model = modelField.value;
        let year = yearlField.value;
        let description = descriptionField.value;
        let price = pricelField.value;

        modelField.value = "";
        yearlField.value = "";
        descriptionField.value = "";
        pricelField.value = "";

        if (!model || !year || !description || !price || year < 0 || price < 0) {
            return;
        }

        let infoRow = document.createElement("tr");
        infoRow.classList.add("info");
        furnitureList.appendChild(infoRow);

        let modelElement = document.createElement("td");
        modelElement.textContent = model;
        infoRow.appendChild(modelElement);

        let priceElement = document.createElement("td");
        priceElement.textContent = Number(price).toFixed(2);
        infoRow.appendChild(priceElement);

        let buttonsElement = document.createElement("td");
        infoRow.appendChild(buttonsElement);

        let infoButton = document.createElement("button");
        infoButton.textContent = "More Info";
        infoButton.classList.add("moreBtn");
        buttonsElement.appendChild(infoButton);

        let buyButton = document.createElement("button");
        buyButton.textContent = "Buy it";
        buyButton.classList.add("buyBtn");
        buttonsElement.appendChild(buyButton);

        infoButton.addEventListener("click", moreInfo);
        buyButton.addEventListener("click", buyIt);

        let descriptionRow = document.createElement("tr");
        descriptionRow.classList.add("hide");
        furnitureList.appendChild(descriptionRow);

        let yearElement = document.createElement("td");
        yearElement.textContent = "Year: " + year;
        descriptionRow.appendChild(yearElement);

        let descriptionElement = document.createElement("td");
        descriptionElement.setAttribute("colspan", "3");
        descriptionElement.textContent = "Description: " + description;
        descriptionRow.appendChild(descriptionElement);
    }

    function moreInfo(e) {
        if (e.target.textContent === "More Info") {
            e.target.textContent = "Less Info";
            e.target.parentElement.parentElement.nextElementSibling.style.display = "contents";
        } else {
            e.target.textContent = "More Info";
            e.target.parentElement.parentElement.nextElementSibling.style.display = "none";
        }
    }

    function buyIt(e) {
        let totalPrice = Number(totalPriceElement.textContent);
        let productPrice = Number(e.target.parentElement.parentElement.children[1].textContent);

        totalPrice += productPrice;
        totalPriceElement.textContent = totalPrice.toFixed(2);

        let infoRow = e.target.parentElement.parentElement;
        let hidenRow = e.target.parentElement.parentElement.nextElementSibling;

        infoRow.remove();
        hidenRow.remove();
    }
}
