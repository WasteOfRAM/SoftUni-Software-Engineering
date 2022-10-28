async function loadCommits() {
    const userName = document.getElementById("username").value;
    const repository = document.getElementById("repo").value;
    const commitsList = document.getElementById("commits");

    try {
        const responce = await fetch(`https://api.github.com/repos/${userName}/${repository}/commits`);

        if (responce.ok === false) {
            throw new Error(`Error: ${responce.status} (Not Found)`);
        }

        const data = await responce.json();

        const commits = data.map(repo => {
            const li = document.createElement("li");
            li.textContent = `${repo.commit.author.name}: ${repo.commit.message}`;

            return li;
        });

        commitsList.replaceChildren(...commits);

    } catch (error) {
        commitsList.innerHTML = `<li>${error.message}</li>`;
    }
}