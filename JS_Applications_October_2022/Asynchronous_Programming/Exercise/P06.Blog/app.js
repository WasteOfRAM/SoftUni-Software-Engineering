window.addEventListener("load", attachEvents());

function attachEvents() {
    const loadPostsBtn = document.getElementById("btnLoadPosts");
    loadPostsBtn.addEventListener("click", getPosts);
    const viewPostBtn = document.getElementById("btnViewPost");
    viewPostBtn.addEventListener("click", viewPost);
}

async function viewPost() {
    const postTitleElem = document.getElementById("post-title");
    const postBodyElem = document.getElementById("post-body");
    const postsElement = document.getElementById("posts");
    const postCommentsList = document.getElementById("post-comments");
    
    const postsUrl = "http://localhost:3030/jsonstore/blog/posts";
    const posts = await fetch(postsUrl).then(promise => promise.json());
    const post = Object.values(posts).find(post => post.id === postsElement.value);

    postTitleElem.textContent = postsElement[postsElement.selectedIndex].textContent;
    postBodyElem.textContent = post.body;

    const commentsUrl = `http://localhost:3030/jsonstore/blog/comments`;
    let postComments = await fetch(commentsUrl).then(promise => promise.json());
    postComments = Object.values(postComments).filter(c => c.postId === postsElement.value);

    const commentsList = postComments.map(comment => {
        const li = document.createElement("li");
        li.id = comment.id;
        li.textContent = comment.text;

        return li;
    });

    postCommentsList.replaceChildren(...commentsList);
}

async function getPosts() {
    const postsElement = document.getElementById("posts");
    const postsUrl = "http://localhost:3030/jsonstore/blog/posts";

    const options = await fetch(postsUrl).then(promise => promise.json());

    const optionsElements = createElements(options);
    postsElement.replaceChildren(...optionsElements);
}

function createElements(options) {
    const elements = Object.values(options).map(data => {
        const option = document.createElement("option");
        option.value = data.id;
        option.textContent = data.title;

        return option;
    });

    return elements;
}