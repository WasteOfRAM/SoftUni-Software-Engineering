function geometry() {
    class Figure {
        constructor(units = "cm"){
            this.units = units;
        }

        get area() {
            return undefined;
        }

        changeUnits(newUnit) {
            this.units = newUnit;
        }

        toString() {
            return `Figures units: ${this.units}`;
        }
    }

    class Circle extends Figure {
        constructor(radius) {
            super();
            this._radius = radius;
        }

        get area() {
            return (this.radius * this.radius) * Math.PI;
        }

        get radius(){
            if(this.units === "m"){
                return this._radius / 10;
            }

            if(this.units === "mm"){
                return this._radius * 10;
            }

            return this._radius;
        }

        set radius(value){
            this._radius = value;
        }

        toString() {
            return super.toString() + " " + `Area: ${this.area} - radius: ${this.radius}`;
        }
    }

    class Rectangle extends Figure {
        constructor(width, height, units){
            super();
            this._width = width;
            this._height = height;
            this.units = units;
        }

        get area() {
            return this.width * this.height;
        }

        get width(){
            if(this.units === "m"){
                return this._width / 10;
            }

            if(this.units === "mm"){
                return this._width * 10;
            }

            return this._width;
        }

        set width(value){
            this._width = value;
        }

        get height(){
            if(this.units === "m"){
                return this._height / 10;
            }

            if(this.units === "mm"){
                return this._height * 10;
            }

            return this._height;
        }

        set height(value){
            this._height = value;
        }

        toString() {
            return super.toString() + " " + `Area: ${this.area} - width: ${this.width}, height: ${this.height}`;
        }
    }

    return {
        Figure,
        Circle,
        Rectangle
    }
}

let c = new Circle(5);
console.log(c.area); // 78.53981633974483
console.log(c.toString()); // Figures units: cm Area: 78.53981633974483 - radius: 5

let r = new Rectangle(3, 4, 'mm');
console.log(r.area); // 1200 
console.log(r.toString()); //Figures units: mm Area: 1200 - width: 30, height: 40

r.changeUnits('cm');
console.log(r.area); // 12
console.log(r.toString()); // Figures units: cm Area: 12 - width: 3, height: 4

c.changeUnits('mm');
console.log(c.area); // 7853.981633974483
console.log(c.toString()) // Figures units: mm Area: 7853.981633974483 - radius: 50
