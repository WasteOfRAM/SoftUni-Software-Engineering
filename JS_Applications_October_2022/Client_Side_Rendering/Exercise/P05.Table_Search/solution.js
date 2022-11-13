import { html, render } from "./node_modules/lit-html/lit-html.js";
import { renderer } from "./renderer.js";

document.querySelector('#searchBtn').addEventListener('click', onClick);

const root = document.querySelector("tbody");

const response = await fetch("http://localhost:3030/jsonstore/advanced/table");
const data = await response.json();

renderer(root, render, html, data);

function onClick(e) {
   const input = document.getElementById("searchField").value;
   const tbody = document.querySelector("table.container tbody");

   [...tbody.children].map(tr => 
         tr.classList.remove("select")
   );

   if (!input) {
      return;
   }

   document.getElementById("searchField").value = "";

   [...tbody.children].map(tr => {
      if(tr.textContent.toLowerCase().includes(input.toLowerCase())){
         tr.classList.add("select");
      }
   });
}