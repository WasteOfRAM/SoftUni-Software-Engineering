function solution(command) {
    if (command === 'upvote') {
        this.upvotes++;
    } else if (command === 'downvote') {
        this.downvotes++;
    } else if (command === 'score') {
        let upvotes;
        let downvotes;

        if (this.upvotes + this.downvotes > 50) {
            let numberToAdd = this.upvotes >= this.downvotes ? Math.ceil(this.upvotes * 0.25) : Math.ceil(this.downvotes * 0.25);
            upvotes = this.upvotes + numberToAdd;
            downvotes = this.downvotes + numberToAdd;
        } else {
            upvotes = this.upvotes;
            downvotes = this.downvotes;
        }

        let rating = 'new';
        let balance = this.upvotes - this.downvotes;

        if (this.upvotes + this.downvotes >= 10) {
            if (this.upvotes >= this.downvotes) {
                let upvoatsPercentage = this.upvotes / (this.upvotes + this.downvotes) * 100;

                if (upvoatsPercentage > 66) {
                    rating = 'hot';
                } else if (this.upvotes + this.downvotes > 100) {
                    rating = 'controversial';
                }
            } else {
                rating = 'unpopular';
            }
        }

        return [upvotes, downvotes, balance, rating];
    }
}


let post = {
    id: '3',
    author: 'emil',
    content: 'wazaaaaa',
    upvotes: 100,
    downvotes: 100
};
solution.call(post, 'upvote');
solution.call(post, 'downvote');
let score = solution.call(post, 'score'); // [127, 127, 0, 'controversial']
console.log(score);
for (let index = 0; index < 50; index++) {
    solution.call(post, 'downvote');
}         // (executed 50 times)
score = solution.call(post, 'score'); // [139, 189, -50, 'unpopular']
console.log(score);

// var forumPost = {
//     id: '1',
//     author: 'pesho',
//     content: 'hi guys',
//     upvotes: 0,
//     downvotes: 0
// };

// solution.call(forumPost, 'upvote');

// var score = score = solution.call(forumPost, 'score');
// console.log(score);