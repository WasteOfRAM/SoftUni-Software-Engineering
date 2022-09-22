function solve() {
  let input = document.getElementById("text").value.toLowerCase().split(' ');
  let caseType = document.getElementById("naming-convention").value;
  let result = document.getElementById("result");

  if (caseType === 'Camel Case') {
    for (let i = 1; i < input.length; i++) {
      input[i] = input[i][0].toUpperCase() + input[i].slice(1);
    }

    result.textContent = input.join('');
  } else if (caseType === 'Pascal Case') {
    for (let i = 0; i < input.length; i++) {
      input[i] = input[i][0].toUpperCase() + input[i].slice(1);
    }

    result.textContent = input.join('');
  } else {
    result.textContent = 'Error!';
  }
}