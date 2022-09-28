function solve() {
  let exercise = document.getElementById('exercise');
  let textAreas = exercise.querySelectorAll('textarea');
  let inputTextArea = textAreas[0];
  let outputTextArea = textAreas[1];
  let buttons = exercise.querySelectorAll('button');
  let generateButton = buttons[0];
  let buyButton = buttons[1];

  let tableBody = document.querySelector('tbody');

  generateButton.addEventListener('click', generate);
  buyButton.addEventListener('click', buy);

  function generate() {
    let itemsArr = JSON.parse(inputTextArea.value);

    for (const item of itemsArr) {
      tableBody.innerHTML +=
        `<tr>` +
            `<td>` +
                `<img src="${item.img}">` +
            `</td>` +
            `<td>` +
                `<p>${item.name}</p>` +
            `</td>` +
            `<td>` +
                `<p>${item.price}</p>` +
            `</td>` +
            `<td>` +
                `<p>${item.decFactor}</p>` +
            `</td>` +
            `<td>` +
                `<input type="checkbox" />` +
            `</td>` +
        `</tr>`
    }
  }

  function buy() {
    let checkedItems = [];

    for (let i = 0; i < tableBody.rows.length; i++) {
      if (tableBody.rows[i].cells[4].children[0].checked) {
        let paragraphs = tableBody.rows[i].querySelectorAll('p');

        checkedItems.push({
          name: paragraphs[0].textContent,
          price: Number(paragraphs[1].textContent),
          decFactor: Number(paragraphs[2].textContent)
        });
      }
    }

    let productNames = checkedItems.map(p => p.name);
    let totalPrice = checkedItems.reduce((total, item) => total + item.price, 0);
    let decAverage = checkedItems.reduce((total, item) => total + item.decFactor, 0) / checkedItems.length;

    outputTextArea.textContent = `Bought furniture: ${productNames.join(', ')}\n` +
                                `Total price: ${totalPrice.toFixed(2)}\n` +
                                `Average decoration factor: ${decAverage}`
  }
}