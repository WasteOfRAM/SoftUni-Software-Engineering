function create(words) {
   let content = document.getElementById("content");
   content.addEventListener('click', displayElem);

   for (const word of words) {
      let newDiv = document.createElement('div');
      newDiv.setAttribute('name', 'button');
      let newP = document.createElement('p');
      newP.style.display = 'none';
      newP.textContent = word;
      newDiv.appendChild(newP);

      content.appendChild(newDiv);
   }

   function displayElem(e) {
      if (e.target.attributes[0].textContent !== 'button') {
         return;
      }
      
      let childP = e.target.firstElementChild;
      childP.style.display = 'block';
   }
}