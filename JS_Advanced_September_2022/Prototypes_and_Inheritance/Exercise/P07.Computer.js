function createComputerHierarchy() {
    class Keyboard {
        constructor(manufacturer, responseTime){
            this.manufacturer = manufacturer;
            this.responseTime = responseTime;
        }
    }

    class Monitor {
        constructor(manufacturer, width, height){
            this.manufacturer = manufacturer;
            this.width = Number(width);
            this.height = Number(height);
        }
    }

    class Battery {
        constructor(manufacturer, expectedLife){
            this.manufacturer = manufacturer;
            this.expectedLife = expectedLife;
        }
    }

    class Computer {
        constructor(manufacturer, processorSpeed, ram, hardDisckSpace){
            if(this.constructor == Computer){
                throw new Error("Abstract classes can't be instantiated.");
            }

            this.manufacturer = manufacturer;
            this.processorSpeed = Number(processorSpeed);
            this.ram = Number(ram);
            this.hardDisckSpace = Number(hardDisckSpace);
        }
    }

    class Laptop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDisckSpace, weight, color, battery){
            super(manufacturer, processorSpeed, ram, hardDisckSpace);
            this.weight = Number(weight);
            this.color = color;
            this.battery = battery;
        }

        get battery() {
            return this._battery;
        }

        set battery(value) {
            if(!(value instanceof Battery)){
                throw new TypeError(`Incorect type: expected Battery received ${value}`);
            }
            this._battery = value;
        }
    }

    class Desktop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDisckSpace, keyboard, monitor){
            super(manufacturer, processorSpeed, ram, hardDisckSpace);
            this.keyboard = keyboard;
            this.monitor = monitor;
        }

        get keyboard() {
            return this._keyboard;
        }

        set keyboard(value) {
            if(!(value instanceof Keyboard)){
                throw new TypeError(`Incorect type: expected Keyboard received ${value}`);
            }

            this._keyboard = value;
        }

        get monitor() {
            return this._monitor;
        }

        set monitor(value) {
            if(!(value instanceof Monitor)){
                throw new TypeError(`Incorect type: expected Monitor received ${value}`);
            }

            this._monitor = value;
        }
    }

    return {
        Battery,
        Keyboard,
        Monitor,
        Computer,
        Laptop,
        Desktop
    }
}

let classes = createComputerHierarchy();
let Computer = classes.Computer;
let Laptop = classes.Laptop;
let Desktop = classes.Desktop;
let Monitor = classes.Monitor;
let Battery = classes.Battery;
let Keyboard = classes.Keyboard;

let battery = new Battery('Energy', 3);
console.log(battery);
let laptop = new Laptop("Hewlett Packard", 2.4, 4, 0.5, 3.12, "Silver", battery);
console.log(laptop);
