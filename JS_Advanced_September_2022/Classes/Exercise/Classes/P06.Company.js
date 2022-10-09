class Company {
    constructor() {
        this.departments = {};
    }

    addEmployee(name, salary, position, department) {
        if (!name || salary <= 0 || !position || !department) {
            throw new Error("Invalid input!");
        }

        if (!this.departments[department]) {
            this.departments[department] = [];
        }

        this.departments[department].push({ name: name, salary: Number(salary), position: position, toString() { return `${name} ${salary} ${position}` } });

        return `New employee is hired. Name: ${name}. Position: ${position}`;
    }

    bestDepartment() {
        let bestDepartment = "";
        let bestAverage = 0;
        let bestEmployees = [];

        for (const [department, arr] of Object.entries(this.departments)) {
            let currentAvarege = arr.reduce((avarege, em) => avarege += em.salary, 0) / arr.length;

            if (currentAvarege > bestAverage) {
                bestAverage = currentAvarege;
                bestDepartment = department;
                bestEmployees = arr;
            }
        }

        bestEmployees.sort((em1, em2) => { return em2.salary - em1.salary || em1.name.localeCompare(em2.name) });

        return `Best Department is: ${bestDepartment}\n` +
            `Average salary: ${bestAverage.toFixed(2)}\n` +
            `${bestEmployees.join("\n")}`;

    }
}


let c = new Company();
c.addEmployee("Stanimir", 2000, "engineer", "Construction");
c.addEmployee("Pesho", 1500, "electrical engineer", "Construction");
c.addEmployee("Slavi", 500, "dyer", "Construction");
c.addEmployee("Stan", 2000, "architect", "Construction");
c.addEmployee("Stanimir", 1200, "digital marketing manager", "Marketing");
c.addEmployee("Pesho", 1000, "graphical designer", "Marketing");
c.addEmployee("Gosho", 1350, "HR", "Human resources");
console.log(c.bestDepartment());


try {
    c.addEmployee("Gosho", -1, "HR", "Human resources");
} catch (error) {
    console.log(error);
}