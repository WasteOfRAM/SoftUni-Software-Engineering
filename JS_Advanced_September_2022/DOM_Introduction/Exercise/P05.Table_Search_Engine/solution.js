function solve() {
   document.querySelector('#searchBtn').addEventListener('click', onClick);
   

   function onClick() {
      let tableRows = document.querySelectorAll(".container tbody tr");
      let searched = document.getElementById("searchField").value;

      for (const row of tableRows) {
         if(row.innerText.includes(searched)){
            row.classList.add("select");
         } else {
            row.classList.remove("select");
         }
      }
   }
}