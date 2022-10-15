window.addEventListener("load", solve);

function solve() {
  document.querySelector("#publish").addEventListener("click", addTableRow);

  let makeField = document.getElementById("make");
  let modelField = document.getElementById("model");
  let yearField = document.getElementById("year");
  let fuelField = document.getElementById("fuel");
  let originalCostField = document.getElementById("original-cost");
  let sellingPriceField = document.getElementById("selling-price");

  let table = document.getElementById("table-body");

  let soldCarsListElem = document.getElementById("cars-list");

  let profitElement = document.getElementById("profit");

  function addTableRow(e) {
    e.preventDefault();
    
    let make = makeField.value;
    let model = modelField.value;
    let year = yearField.value;
    let fuel = fuelField.value;
    let originalCost = originalCostField.value;
    let sellingPrice = sellingPriceField.value;
    
    if(!make || !model || !year || !fuel || !originalCost || !sellingPrice || Number(originalCost) > Number(sellingPrice)){
      return;
    }

    let infoList = [];
    infoList.push(make);
    infoList.push(model);
    infoList.push(year);
    infoList.push(fuel);
    infoList.push(originalCost);
    infoList.push(sellingPrice);

    makeField.value = "";
    modelField.value = "";
    yearField.value = "";
    fuelField.value = "";
    originalCostField.value = "";
    sellingPriceField.value = "";

    let tableRow = document.createElement("tr");
    tableRow.classList.add("row");
    table.appendChild(tableRow);

    for (const carInfo of infoList) {
      let td = document.createElement("td");
      td.textContent = carInfo;
      tableRow.appendChild(td);
    }

    let buttonsTd = document.createElement("td");
    let editButton = document.createElement("button");
    let sellButton = document.createElement("button");

    editButton.classList.add("action-btn");
    editButton.classList.add("edit");
    editButton.textContent = "Edit";
    sellButton.classList.add("action-btn");
    sellButton.classList.add("sell");
    sellButton.textContent = "Sell";

    buttonsTd.appendChild(editButton);
    buttonsTd.appendChild(sellButton);

    tableRow.appendChild(buttonsTd);

    editButton.addEventListener("click", edit);
    sellButton.addEventListener("click", sell);
  }


  function edit(e) {
    let infoPost = e.target.parentElement.parentElement;

    makeField.value = infoPost.children[0].textContent;
    modelField.value = infoPost.children[1].textContent;
    yearField.value = infoPost.children[2].textContent;
    fuelField.value = infoPost.children[3].textContent;
    originalCostField.value = infoPost.children[4].textContent;
    sellingPriceField.value = infoPost.children[5].textContent;

    infoPost.remove();
  }

  function sell(e) {
    let infoPost = e.target.parentElement.parentElement;
    
    let liElem = document.createElement("li");
    liElem.classList.add("each-list");
    soldCarsListElem.appendChild(liElem);

    let carMakeModel = document.createElement("span");
    let carYear = document.createElement("span");
    let sellProfit = document.createElement("span");

    carMakeModel.textContent = infoPost.children[0].textContent + " " + infoPost.children[1].textContent;
    carYear.textContent = infoPost.children[2].textContent;
    sellProfit.textContent = Number(infoPost.children[5].textContent) - Number(infoPost.children[4].textContent);

    liElem.appendChild(carMakeModel);
    liElem.appendChild(carYear);
    liElem.appendChild(sellProfit);

    infoPost.remove();

    let totalProfit = Number(profitElement.textContent);

    totalProfit += Number(sellProfit.textContent);

    profitElement.textContent = totalProfit.toFixed(2);
  }
}