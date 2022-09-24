function generateReport() {
    let output = document.getElementById("output");
    let checkBoxList = document.querySelectorAll('input[type=checkbox]');
    checkBoxList = Array.from(checkBoxList);
    let rowsData = document.querySelectorAll("tbody tr");
    let resultArray = [];
    let selected = [];
    
    for (let index = 0; index < checkBoxList.length; index++) {
        if(checkBoxList[index].checked){
            selected.push(index);
        }
    }
    
    for (const row of rowsData) {
        let obj = {};

        for (const index of selected) {
            obj[checkBoxList[index].name] = row.cells[index].textContent;
        }

        resultArray.push(obj);
    }

    let jsonString = JSON.stringify(resultArray);

    output.textContent = jsonString;
}