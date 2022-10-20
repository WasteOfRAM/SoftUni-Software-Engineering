class ArtGallery {
    constructor(creator) {
        this.creator = creator;
        this.possibleArticles = { "picture": 200, "photo": 50, "item": 250 };
        this.listOfArticles = [];
        this.guests = [];
    }

    addArticle(articleModel, articleName, quantity) {
        articleModel = articleModel.toLowerCase();

        if (!this.possibleArticles[articleModel]) {
            throw new Error("This article model is not included in this gallery!");
        }

        let article = this.listOfArticles.find(ar => ar.articleName === articleName && ar.articleModel === articleModel);
        if (article !== undefined) {
            article.quantity += quantity;
        } else {
            this.listOfArticles.push({
                articleModel,
                articleName,
                quantity,

                toString: function () {
                    return `${this.articleModel} - ${this.articleName} - ${this.quantity}`;
                }
            });
        }

        return `Successfully added article ${articleName} with a new quantity- ${quantity}.`;
    }

    inviteGuest(guestName, personality) {
        if (this.guests.some(g => g.guestName === guestName)) {
            throw new Error(`${guestName} has already been invited.`);
        }

        let points = personality === "Vip" ? 500 : personality === "Middle" ? 250 : 50;

        this.guests.push({
            guestName,
            points,
            purchaseArticle: 0,

            toString: function () {
                return `${this.guestName} - ${this.purchaseArticle}`;
            }
        })

        return `You have successfully invited ${guestName}!`;
    }

    buyArticle(articleModel, articleName, guestName) {
        let article = this.listOfArticles.find(ar => ar.articleName === articleName && ar.articleModel === articleModel);
        if (article === undefined) {
            throw new Error("This article is not found.");
        }

        if (article.quantity === 0) {
            return `The ${articleName} is not available.`;
        }

        let guest = this.guests.find(g => g.guestName === guestName);
        if (guest === undefined) {
            return "This guest is not invited.";
        }

        let articlePoints = this.possibleArticles[article.articleModel];
        if (guest.points < articlePoints) {
            return "You need to more points to purchase the article.";
        }

        guest.points -= articlePoints;
        guest.purchaseArticle++;
        article.quantity--;

        return `${guestName} successfully purchased the article worth ${articlePoints} points.`;
    }

    showGalleryInfo(criteria) {
        let str;
        if(criteria === "article"){
            str = "Articles information:\n";

            str += this.listOfArticles.join("\n");
        }

        if(criteria === "guest"){
            str = "Guests information:\n";

            str += this.guests.join("\n");
        }

        return str;
    }
}

const artGallery = new ArtGallery('Curtis Mayfield'); 
artGallery.addArticle('picture', 'Mona Liza', 3);
artGallery.addArticle('Item', 'Ancient vase', 2);
artGallery.addArticle('picture', 'Mona Liza', 1);
artGallery.inviteGuest('John', 'Vip');
artGallery.inviteGuest('Peter', 'Middle');
artGallery.buyArticle('picture', 'Mona Liza', 'John');
artGallery.buyArticle('item', 'Ancient vase', 'Peter');
console.log(artGallery.showGalleryInfo('article'));
console.log(artGallery.showGalleryInfo('guest'));

