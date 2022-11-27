import { get } from "../api/api.js";
import { html, repeat } from "../lib.js";

const profileItemCard = (item) => html`
    <div class="user-meme">
        <p class="user-meme-title">${item.title}</p>
        <img class="userProfileImage" alt="meme-img" src=${item.imageUrl}>
        <a class="button" href="/details/${item._id}">Details</a>
    </div>
`;

const profileTemplate = (data, user) => html`
    <section id="user-profile-page" class="user-profile">
        <article class="user-info">
            <img id="user-avatar-url" alt="user-profile" src=${user.gender === "female" ? "/images/female.png" : "/images/male.png"}>
            <div class="user-content">
                <p>Username: ${user.username}</p>
                <p>Email: ${user.email}</p>
                <p>My memes count: ${data.length}</p>
            </div>
        </article>
        <h1 id="user-listings-title">User Memes</h1>
        <div class="user-meme-listings">
            ${ data.length === 0 ? html`<p class="no-memes">No memes in database.</p>`
            : html`${repeat(data, (item) => item._id, profileItemCard)}`
            }
        </div>
    </section>
`;

export async function profileView(ctx) {
    ctx.updateNavigation();

    const data = await get(`/data/memes?where=_ownerId%3D%22${ctx.user._id}%22&sortBy=_createdOn%20desc`);
    ctx.render(profileTemplate(data, ctx.user));
}