function getArticleGenerator(articles) {
    let articleLines = Array.from(articles);

    return () => {

        if(articleLines.length > 0){
            let contentDiv = document.getElementById('content');
            let article = articleLines.shift();

            contentDiv.innerHTML += `<article>${article}</article>`;
        }
        
    }
}