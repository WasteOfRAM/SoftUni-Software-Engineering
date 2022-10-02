function solve() {
    let onScreenButton = document.getElementById('container').getElementsByTagName('button')[0];
    onScreenButton.setAttribute('type', 'button');
    onScreenButton.addEventListener('click', addMovie);

    let clearButton = document.querySelector('#archive button');
    clearButton.addEventListener('click', clearAll);
    
    function addMovie() {
        let parentDiv = document.getElementById('container');
        let inputs = parentDiv.querySelectorAll('input');
        let movieName = inputs[0].value;
        let hall = inputs[1].value;
        let ticketPrice = inputs[2].value;

        if (movieName === '' || hall === '' || ticketPrice === '' || isNaN(ticketPrice)) {
            return;
        }

        let moviesOnScreenUl = document.querySelector('#movies ul');
        
        let listElement = document.createElement('li');
        let nameSpanElement = document.createElement('span');
        nameSpanElement.textContent = movieName;
        listElement.appendChild(nameSpanElement);

        let hallNameStrong = document.createElement('strong');
        hallNameStrong.textContent = `Hall: ${hall}`;
        listElement.appendChild(hallNameStrong);

        let divElement = document.createElement('div');
        let priceElement = document.createElement('strong');
        priceElement.textContent = `${Number(ticketPrice).toFixed(2)}`;
        divElement.appendChild(priceElement);

        let ticketsSoldInput = document.createElement('input');
        ticketsSoldInput.setAttribute('placeholder', 'Tickets Sold');
        divElement.appendChild(ticketsSoldInput);

        let archiveButton = document.createElement('button');
        archiveButton.textContent = 'Archive';
        divElement.appendChild(archiveButton);

        listElement.appendChild(divElement);
        moviesOnScreenUl.appendChild(listElement);

        archiveButton.addEventListener('click', archive);

        inputs[0].value = '';
        inputs[1].value = '';
        inputs[2].value = '';
    }

    function archive(e) {
        let ticketsSold = e.target.previousElementSibling.value;

        if (ticketsSold === '' || isNaN(ticketsSold)) {
            return;
        }

        let onScreenSectionLi = e.target.parentElement.parentElement;
        let movieName = onScreenSectionLi.children[0].textContent;
        let ticketPrice = onScreenSectionLi.children[2].children[0].textContent;

        let archive = document.querySelector('#archive ul');

        let archiveSectionLi = document.createElement('li');
        let name = document.createElement('span');
        name.textContent = movieName;
        archiveSectionLi.appendChild(name);

        let totalAmount = document.createElement('strong');
        totalAmount.textContent = `Total amount: ${Number(ticketPrice * ticketsSold).toFixed(2)}`;
        archiveSectionLi.appendChild(totalAmount);

        let deleteButton = document.createElement('button');
        deleteButton.textContent = 'Delete';
        archiveSectionLi.appendChild(deleteButton);
        deleteButton.addEventListener('click', deleteElement);

        onScreenSectionLi.remove();
        archive.appendChild(archiveSectionLi);
    }

    function deleteElement(e) {
        e.target.parentElement.remove();
    }

    function clearAll() {
        let archiveList = document.querySelector('#archive ul');

        archiveList.textContent = '';
        // better way but not suported by judge system
        //archiveList.replaceChildren();
    }
}