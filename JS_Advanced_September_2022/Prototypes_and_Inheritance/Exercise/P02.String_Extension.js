(function solve() {
    String.prototype.ensureStart = function (str) {
        if (this.startsWith(str)) {
            return `${this}`;
        }

        return str + this;
    };

    String.prototype.ensureEnd = function (str) {
        if (this.endsWith(str)) {
            return `${this}`;
        }

        return this  + str;
    }

    String.prototype.isEmpty = function () {
        return this.length === 0;
    }

    String.prototype.truncate = function (n) {
        if (this.length <= n) {
            return `${this}`;
        }

        let string = this;
        let index;
        while (string.length > n) {
            index = string.lastIndexOf(" ");

            if (index < 0) {
                break;
            }

            string = string.slice(0, index) + "...";
        }

        if (n < 4) {
            return ".".repeat(n);
        }

        if (index < 0) {
            return string.slice(0, n - 3) + "...";
        }

        return string;
    }

    String.format = function (string, ...params) {
        let formatted = string;
        for (let i = 0; i < params.length; i++) {
            let regexp = new RegExp('\\{' + i + '\\}', 'gi');
            formatted = formatted.replace(regexp, params[i]);
        }
        return formatted;
    }
})();


//let str = 'my string';
let str = 'mysdgggegeawsgffstring';
str = str.ensureStart('my');
console.log(str);

str = str.ensureStart('hello ');
console.log(str);

// str = str.truncate(3);
// console.log(str);
// str = str.truncate(0);
// console.log(str);

// str = str.ensureEnd("string");
// console.log(str);

// str = str.ensureEnd("Test");
// console.log(str);

// str = "";
// console.log(str.isEmpty());
// str = "Test";
// console.log(str.isEmpty());

str = str.truncate(16);
console.log(str);
str = str.truncate(14);
console.log(str);
str = str.truncate(8);
console.log(str);
str = str.truncate(4);
console.log(str);
str = str.truncate(2);
console.log(str);

str = String.format('The {0} {1} fox',
    'quick', 'brown');
console.log(str);

str = String.format('jumps {0} {1}',
    'dog');
console.log(str);