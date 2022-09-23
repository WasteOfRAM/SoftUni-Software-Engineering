function search() {
   let list = document.querySelectorAll("#towns li");
   let input = document.getElementById("searchText").value;
   let result = document.getElementById("result");
   let matches = 0;
   for (const item of list) {
      if (item.textContent.includes(input)) {
         item.style.textDecoration = "underline";
         item.style.fontWeight = "bold";

         matches++;
      } else {
         item.style.textDecoration = "none";
         item.style.fontWeight = "normal";
      }
   }

   result.textContent = `${matches} matches found`
}