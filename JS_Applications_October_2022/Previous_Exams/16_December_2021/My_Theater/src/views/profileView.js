import { get } from "../api/api.js";
import { html, repeat } from "../lib.js";

const profileTemplate = (userData, eventsData) => html`
    <section id="profilePage">
        <div class="userInfo">
            <div class="avatar">
                <img src="./images/profilePic.png">
            </div>
            <h2>${userData.email}</h2>
        </div>
        <div class="board">
            ${eventsData.length !== 0 ? html` ${repeat(eventsData, item => item._id, eventCard)}`
            : 
            html` <div class="no-events">
                <p>This user has no events yet!</p>
            </div>`  }
        </div>
    </section>
`

const eventCard = (item) => html`
    <div class="eventBoard">
        <div class="event-info">
            <img src=${item.imageUrl}>
            <h2>${item.title}</h2>
            <h6>${item.date}</h6>
            <a href="/details/${item._id}" class="details-button">Details</a>
        </div>
    </div>
`

export async function profileView(ctx) {
    const user = ctx.user;
    
    const data = await get(`/data/theaters?where=_ownerId%3D%22${user._id}%22&sortBy=_createdOn%20desc`);
    console.log(data.length);
    ctx.render(profileTemplate(user, data));
}