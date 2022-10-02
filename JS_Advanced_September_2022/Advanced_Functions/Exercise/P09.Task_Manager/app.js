function solve() {
    let addButton = document.getElementById('add');
    addButton.setAttribute('type', 'button');
    addButton.addEventListener('click', addTask);

    function addTask(){
        let taskName = document.getElementById('task').value;
        let description = document.getElementById('description').value;
        let date = document.getElementById('date').value;

        if (taskName === '' || description === '' || date === '') {
            return;
        }

        let openSectionDiv = document.querySelector('.orange').parentElement.parentElement.lastElementChild;

        let article = document.createElement('article');
        let h3Element = document.createElement('h3');
        h3Element.textContent = taskName;
        article.appendChild(h3Element);
        let firstP = document.createElement('p');
        firstP.textContent = `Description: ${description}`;
        let secondP = document.createElement('p');
        secondP.textContent = `Due Date: ${date}`;
        article.appendChild(firstP);
        article.appendChild(secondP);

        let flexDiv = document.createElement('div');
        flexDiv.setAttribute('class', 'flex');

        let startButton = document.createElement('button');
        startButton.setAttribute('class', 'green');
        startButton.textContent = 'Start';
        flexDiv.appendChild(startButton);

        let deleteButton = document.createElement('button');
        deleteButton.setAttribute('class', 'red');
        deleteButton.textContent = 'Delete';
        flexDiv.appendChild(deleteButton);

        article.appendChild(flexDiv);
        openSectionDiv.appendChild(article);

        startButton.addEventListener('click', startTask);
        deleteButton.addEventListener('click', deleteTask);
    }

    function startTask(e) {
        let elementToMove = e.target.parentElement.parentElement;
        let buttonsDiv = e.target.parentElement;
        e.target.remove();

        let finishButton = document.createElement('button');
        finishButton.setAttribute('class', 'orange');
        finishButton.textContent = 'Finish';
        buttonsDiv.appendChild(finishButton);

        let inProgresSection = document.getElementById('in-progress');
        inProgresSection.appendChild(elementToMove);

        finishButton.addEventListener('click', finishTask);
    }

    function finishTask(e) {
        let articleToMove = e.target.parentElement.parentElement;
        let buttons = e.target.parentElement;

        buttons.remove();

        let completeSection = document.querySelectorAll('section')[3].lastElementChild;

        completeSection.appendChild(articleToMove);
    }

    function deleteTask(e) {
        e.target.parentElement.parentElement.remove();

    }
}