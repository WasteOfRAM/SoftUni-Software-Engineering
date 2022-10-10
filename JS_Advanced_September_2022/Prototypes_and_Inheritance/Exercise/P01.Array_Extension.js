(function solve() {
    Array.prototype.last = function () {
        return this[this.length - 1];
    };

    Array.prototype.skip = function (n) {
        // let result = [];

        // for (let i = n; i < this.length; i++) {
        //     result.push(this[i]);
        // }

        // return result;

        return this.slice(Number(n));
    };

    Array.prototype.take = function (n) {
        // let result = [];

        // for (let i = 0; i < Number(n); i++) {
        //     result.push(this[i]);
        // }
        // return result;
        return this.slice(0, Number(n));
    };

    Array.prototype.sum = function () {
        return this.reduce((total, elem) => total += elem, 0);
    };

    Array.prototype.average = function () {
        return this.sum() / this.length;
    }
})();

let arr = [1, 2, 3, 4, 5];

let lastElemrnt = arr.last();
console.log(lastElemrnt);

let skipNth = arr.skip(3);
console.log(skipNth);

let takeSome = arr.take(4);
console.log(takeSome);

console.log(arr.sum());

console.log(arr.average());