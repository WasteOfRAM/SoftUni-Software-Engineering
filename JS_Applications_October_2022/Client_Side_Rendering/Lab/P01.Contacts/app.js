import { render, html } from 'https://unpkg.com/lit-html?module';
import { contacts } from "./contacts.js";

const contactsSection = document.getElementById("contacts");

const contactCard = (contact, onToggle) => html`
<div class="contact card">
    <div>
        <i class="far fa-user-circle gravatar"></i>
    </div>
    <div class="info">
        <h2>Name: ${contact.name}</h2>
        <button class="detailsBtn" @click=${onToggle.bind(null, contact.id)}>Details</button>
        <div class="details" id=${contact.id}>
            <p>Phone number: ${contact.phoneNumber}</p>
            <p>Email: ${contact.email}</p>
        </div>
    </div>
</div>`;

function onToggle(id) {
    const details = document.getElementById(id);
    if (details.style.display === "block") {
        details.style.display = "none";
    } else {
        details.style.display = "block";
    }
}

render(contacts.map(c => contactCard(c, onToggle)), contactsSection);