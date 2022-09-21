function createSortedList() {
    return {
        sortedList: [],
        size: 0,

        add: function (number) {
            this.sortedList.push(number);
            this.size++;
            this.sortedList.sort((a, b) => a -b);
        },
        
        remove: function (index) {
            if (index >= 0 && index < this.sortedList.length) {
                this.sortedList.splice(index, 1);
                this.size--;
            }
        },

        get: function (index) {
            return this.sortedList[index];
        },
    }
}

let list = createSortedList();
console.log(list.size);
list.add(5);
list.add(6);
list.add(7);
console.log(list.get(1)); 
list.remove(1);
console.log(list.get(1));
console.log(list.size);