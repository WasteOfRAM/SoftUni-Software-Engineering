function loadRepos() {
	const userName = document.getElementById("username").value;

	fetch(`https://api.github.com/users/${userName}/repos`)
		.then(responseHandler)
		.then(dataHandler)
		.catch(errorHandler);
}

function responseHandler(responce) {
	if(responce.ok == false){
		// Chrome does not give responce.statusText. Mozila does
		throw new Error(`${responce.status} ${responce.statusText}`);
	}

	return responce.json();
}

function dataHandler(data) {
	const list = document.getElementById("repos");

	const items = data.map(repo => {
		const li = document.createElement("li");
		const a = document.createElement("a");
		a.href = repo.html_url;
		a.textContent = repo.full_name;

		li.appendChild(a);

		return li;
	});

	list.replaceChildren(...items);
}

function errorHandler(err) {
	const list = document.getElementById("repos");

	list.textContent = err.message;
}