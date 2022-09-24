function solve() {
   document.querySelector('#btnSend').addEventListener('click', onClick);

   function onClick() {
      let input = document.querySelector("#inputs textarea").value;
      let bestResturantParagraph = document.querySelector("#bestRestaurant p");
      let bestResturantWorkersParagraph = document.querySelector("#workers p");

      input = JSON.parse(input);
      let resturants = [];

      for (const resturantData of input) {
         let [resturantName, workers] = resturantData.split(' - ');
         workers = workers.split(', ')
            .map(worker => {
               let [name, salary] = worker.split(' ');
               return { name: name, salary: Number(salary) }
            });

         if (resturants.find(r => r.name === resturantName)) {
            let currentResturant = resturants.find(r => r.name === resturantName);
            currentResturant.workerList = currentResturant.workerList.concat(workers);
            continue;
         }

         resturants.push({
            name: resturantName,
            workerList: workers,
            averageSalary: function () {
               let ava = this.workerList.reduce((a, w) => a + w.salary, 0) / this.workerList.length
               return ava.toFixed(2);
            },
            bestSalary: function () {
               return this.workerList.sort((a, b) => b.salary - a.salary)[0].salary.toFixed(2);
            },
            sortedWorkers: function () {
               return this.workerList.sort((a, b) => b.salary - a.salary);
            }
         });

      }

      let bestResturant = resturants.sort((a, b) => b.averageSalary() - a.averageSalary())[0];
      bestResturantParagraph.textContent = `Name: ${bestResturant.name} Average Salary: ${bestResturant.averageSalary()} Best Salary: ${bestResturant.bestSalary()}`;
      let result = '';
      let workers = bestResturant.sortedWorkers();
      for (const worker of workers) {
         result += `Name: ${worker.name} With Salary: ${worker.salary} `;
      }

      bestResturantWorkersParagraph.textContent = result;
   }
}