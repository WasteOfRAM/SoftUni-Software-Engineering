function extensibleObject() {
    return {
        extend: function (obj) {
            for (const key in obj) {
                if (typeof obj[key] === 'function') {
                    this.__proto__[key] = obj[key];
                } else {
                    this[key] = obj[key];
                }
            }
        }
    }
}

const myObj = extensibleObject();
const template = {
    extensionMethod: function () { },
    extensionProperty: 'someString'
}

myObj.extend(template);