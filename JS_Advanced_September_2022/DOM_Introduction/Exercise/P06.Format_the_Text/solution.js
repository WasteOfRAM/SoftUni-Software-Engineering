function solve() {
  let inputText = document.getElementById("input").value.split('.').filter(Boolean);
  let output = document.getElementById("output");
  output.innerHTML = '';

  for (let i = 0; i < inputText.length; i += 3) {
    let paragraphs = [];
    for (let j = 0; j < 3; j++) {
      if(inputText[i + j]){
        paragraphs.push(inputText[i + j]);
      }
    }

    let result = paragraphs.join('. ') + '.';
    output.innerHTML += `<p>${result}</p>`;
  }
}