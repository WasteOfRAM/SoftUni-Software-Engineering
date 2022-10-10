function toStringExtension() {
    class Person {
        constructor(name, email) {
            this.name = name;
            this.email = email;
        }

        toString() {
            let props = [];
            for (const [key, value] of Object.entries(this)) {
                props.push(`${key}: ${value}`);
            }
    
            return `${this.constructor.name} (${props.join(", ")})`;
        }
    }

    class Teacher extends Person {
        constructor(name, email, subject) {
            super(name, email)
            this.subject = subject
        }
    }

    class Student extends Person {
        constructor(name, email, course) {
            super(name, email);
            this.course = course;
        }
    }

    return {
        Person,
        Teacher,
        Student
    }
}