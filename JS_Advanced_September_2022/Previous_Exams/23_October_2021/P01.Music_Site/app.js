window.addEventListener('load', solve);

function solve() {
    document.getElementById("add-btn").addEventListener("click", add);

    let genreField = document.getElementById("genre");
    let nameField = document.getElementById("name");
    let authorField = document.getElementById("author");
    let dateField = document.getElementById("date");

    let allHitsContainer = document.querySelector(".all-hits-container");
    let savedContainer = document.querySelector(".saved-container");

    function add(e) {
        e.preventDefault();

        let genre = genreField.value;
        let name = nameField.value;
        let author = authorField.value;
        let date = dateField.value;

        if (!genre || !name || !author || !date) {
            return;
        }

        genreField.value = "";
        nameField.value = "";
        authorField.value = "";
        dateField.value = "";

        let hitsInfoElement = document.createElement("div");
        hitsInfoElement.classList.add("hits-info");
        allHitsContainer.appendChild(hitsInfoElement);

        let imgElement = document.createElement("img");
        imgElement.src = "./static/img/img.png";
        hitsInfoElement.appendChild(imgElement);

        let genreHeader = document.createElement("h2");
        genreHeader.textContent = "Genre: " + genre;
        hitsInfoElement.appendChild(genreHeader);

        let nameHeader = document.createElement("h2");
        nameHeader.textContent = "Name: " + name;
        hitsInfoElement.appendChild(nameHeader);

        let authorHeader = document.createElement("h2");
        authorHeader.textContent = "Author: " + author;
        hitsInfoElement.appendChild(authorHeader);

        let dateHeader = document.createElement("h3");
        dateHeader.textContent = "Date: " + date;
        hitsInfoElement.appendChild(dateHeader);

        let saveBtn = document.createElement("button");
        saveBtn.textContent = "Save song";
        saveBtn.classList.add("save-btn");
        hitsInfoElement.appendChild(saveBtn);

        let likeBtn = document.createElement("button");
        likeBtn.textContent = "Like song";
        likeBtn.classList.add("like-btn");
        hitsInfoElement.appendChild(likeBtn);

        let delBtn = document.createElement("button");
        delBtn.textContent = "Delete";
        delBtn.classList.add("delete-btn");
        hitsInfoElement.appendChild(delBtn);

        saveBtn.addEventListener("click", saveSong);
        likeBtn.addEventListener("click", likeSong);
        delBtn.addEventListener("click", delSong)
    }

    function saveSong(e){
        let currentContainer = e.target.parentElement;
        currentContainer.children[6].remove();
        currentContainer.children[5].remove();

        savedContainer.appendChild(currentContainer);
    }

    function likeSong(e){
        let likesElement = document.querySelector(".likes").firstElementChild;
        let totalLikes = Number(likesElement.textContent.split(": ")[1]);

        likesElement.textContent = "Total Likes: " + ++totalLikes;

        e.target.disabled = true;
    }

    function delSong(e){
        e.target.parentElement.remove();
    }
}