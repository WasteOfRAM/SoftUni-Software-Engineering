function solution() {
    class Post {
        constructor(title, content) {
            this.title = title;
            this.content = content;
        }

        toString() {
            let result = `Post: ${this.title}\n` +
                `Content: ${this.content}`;

            return result;
        }
    }

    class SocialMediaPost extends Post {
        constructor(title, content, likes, dislikes){
            super(title, content);
            this.likes = Number(likes);
            this.dislikes = Number(dislikes);
            this.comments = [];
        }

        addComment(comment) {
            this.comments.push(" * " + comment);
        }

        toString() {
            let result = super.toString() + `\n` +
            `Rating: ${this.likes - this.dislikes}`;

            if(this.comments.length !== 0){
                result += `\nComments:\n` +
                `${this.comments.join("\n")}`
            }
            
            return result;
        }
    }

    class BlogPost extends Post {
        constructor(title, content, views){
            super(title, content);
            this.views = Number(views);
        }

        view() {
            this.views++;
            return this;
        }

        toString() {
            let result = super.toString() + `\n` +
            `Views: ${this.views}`;

            return result;
        }
    }

    return {
        Post,
        SocialMediaPost,
        BlogPost
    }
}


const classes = solution();
let post = new classes.Post("Post", "Content");

console.log(post.toString());

// Post: Post
// Content: Content

let scm = new classes.SocialMediaPost("TestTitle", "TestContent", 25, 30);

scm.addComment("Good post");
scm.addComment("Very good post");
scm.addComment("Wow!");

console.log(scm.toString());

// Post: TestTitle
// Content: TestContent
// Rating: -5
// Comments:
//  * Good post
//  * Very good post
//  * Wow!

let blog = new classes.BlogPost("Test", "No", 20);

blog.view();

console.log(blog.views);
let returnedBlog = blog.view();
console.log(returnedBlog);

console.log(blog.toString());