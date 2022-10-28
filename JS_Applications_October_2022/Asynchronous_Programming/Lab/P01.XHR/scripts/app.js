function loadRepos() {
   const xhr = new XMLHttpRequest();
   const method = "GET";
   const url = "https://api.github.com/users/testnakov/repos";

   let divElement = document.getElementById("res");

   xhr.open(method, url, true);

   xhr.onreadystatechange = () => {
      

      if(xhr.readyState === 4){
         divElement.textContent = xhr.responseText;
      }
   }

   xhr.send();
}