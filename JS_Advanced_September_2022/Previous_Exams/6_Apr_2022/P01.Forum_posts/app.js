window.addEventListener("load", solve);

function solve() {
  let publishButton = document.getElementById("publish-btn");
  publishButton.addEventListener("click", publishPost);

  let titleField = document.getElementById("post-title");
  let categoryField = document.getElementById("post-category");
  let contentField = document.getElementById("post-content");

  let reviewList = document.getElementById("review-list");
  let publishedList = document.getElementById("published-list");

  let clearButton = document.getElementById("clear-btn");
  clearButton.addEventListener("click", clearPosts);

  function publishPost() {
    let title = titleField.value;
    let category = categoryField.value;
    let content = contentField.value;

    if (!title || !category || !content) {
      return;
    }

    category = "Category: " + categoryField.value;
    content = "Content: " + contentField.value;

    titleField.value = "";
    categoryField.value = "";
    contentField.value = "";


    let liElement = document.createElement("li");
    liElement.classList.add("rpost");

    reviewList.appendChild(liElement);

    let articleElement = document.createElement("article");

    let headerElement = document.createElement("h4");
    headerElement.textContent = title;

    let pCategoryElement = document.createElement("p");
    pCategoryElement.textContent = category;

    let pContenetElement = document.createElement("p");
    pContenetElement.textContent = content;

    articleElement.appendChild(headerElement);
    articleElement.appendChild(pCategoryElement);
    articleElement.appendChild(pContenetElement);

    liElement.appendChild(articleElement);

    let editButton = document.createElement("button");
    editButton.textContent = "Edit";
    editButton.classList.add("action-btn", "edit");
    let approveButton = document.createElement("button");
    approveButton.textContent = "Approve";
    approveButton.classList.add("action-btn", "approve");

    liElement.appendChild(approveButton);
    liElement.appendChild(editButton);

    approveButton.addEventListener("click", approve);
    editButton.addEventListener("click", edit);
  }

  function edit(e) {
    let post = e.target.parentElement;
    titleField.value = post.firstElementChild.children[0].textContent;
    categoryField.value = post.firstElementChild.children[1].textContent.split(": ")[1];
    contentField.value = post.firstElementChild.children[2].textContent.split(": ")[1];
    
    post.remove();
  }

  function approve(e) {
    let post = e.target.parentElement;

    let buttons = document.getElementsByClassName("action-btn");
    buttons[1].remove();
    buttons[0].remove();

    publishedList.appendChild(post);
  }

  function clearPosts(e) {
    //publishedList.textContent = "";

    for (let i = publishedList.children.length - 1; i >= 0; i--) {
      publishedList.children[i].remove();
    }
  }
}