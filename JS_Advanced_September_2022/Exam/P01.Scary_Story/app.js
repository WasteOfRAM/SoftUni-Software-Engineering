window.addEventListener("load", solve);

function solve() {
  let publishBtn = document.getElementById("form-btn");
  publishBtn.addEventListener("click", publish);

  let firstNameField = document.getElementById("first-name");
  let lastNameField = document.getElementById("last-name");
  let ageField = document.getElementById("age");
  let titleField = document.getElementById("story-title");
  let genreField = document.getElementById("genre");
  let storyField = document.getElementById("story");

  let previewList = document.getElementById("preview-list");

  function publish(e) {
    let firstName = firstNameField.value;
    let lastName = lastNameField.value;
    let age = ageField.value;
    let title = titleField.value;
    let genre = genreField.value;
    let story = storyField.value;

    if (!firstName || !lastName || !age || !title || !story) {
      return;
    }

    firstNameField.value = "";
    lastNameField.value = "";
    ageField.value = "";
    titleField.value = "";
    storyField.value = "";

    publishBtn.disabled = true;

    let liElement = document.createElement("li");
    liElement.classList.add("story-info");
    previewList.appendChild(liElement);

    let articleElement = document.createElement("article");
    liElement.appendChild(articleElement);

    let headerElement = document.createElement("h4");
    headerElement.textContent = `Name: ${firstName} ${lastName}`;
    articleElement.appendChild(headerElement);

    let ageParagraph = document.createElement("p");
    ageParagraph.textContent = "Age: " + age;
    articleElement.appendChild(ageParagraph);

    let titleParagraph = document.createElement("p");
    titleParagraph.textContent = "Title: " + title;
    articleElement.appendChild(titleParagraph);

    let genreParagraph = document.createElement("p");
    genreParagraph.textContent = "Genre: " + genre;
    articleElement.appendChild(genreParagraph);

    let storyParagraph = document.createElement("p");
    storyParagraph.textContent = story;
    articleElement.appendChild(storyParagraph);

    let saveButton = document.createElement("button");
    saveButton.classList.add("save-btn");
    saveButton.textContent = "Save Story";
    liElement.appendChild(saveButton);

    let editButton = document.createElement("button");
    editButton.classList.add("edit-btn");
    editButton.textContent = "Edit Story";
    liElement.appendChild(editButton);

    let delButton = document.createElement("button");
    delButton.classList.add("delete-btn");
    delButton.textContent = "Delete Story";
    liElement.appendChild(delButton);

    saveButton.addEventListener("click", saveStory);
    editButton.addEventListener("click", editStory);
    delButton.addEventListener("click", delStory);


    function saveStory(e) {
      let mainElement = document.getElementById("main");
      mainElement.innerHTML = `<h1>Your scary story is saved!</h1>`;
    }

    function editStory(e) {
      let storyInfoElement = document.querySelector(".story-info article");
      let storyInfo = storyInfoElement.children;

      let fullName = storyInfo[0].textContent.split(": ")[1];
      let firstName = fullName.split(" ")[0];
      let lastName = fullName.split(" ")[1];
      let age = storyInfo[1].textContent.split(": ")[1];
      let title = storyInfo[2].textContent.split(": ")[1];
      let gnere = storyInfo[3].textContent.split(": ")[1];
      let stroy = storyInfo[4].textContent;

      firstNameField.value = firstName;
      lastNameField.value = lastName;
      ageField.value = age;
      titleField.value = title;
      genreField.value = gnere;
      storyField.value = stroy;

      publishBtn.disabled = false;

      storyInfoElement.parentElement.remove();
    }

    function delStory(e) {
      let storyInfoElement = document.querySelector(".story-info");
      publishBtn.disabled = false;
      storyInfoElement.remove();
    }
  }
}
