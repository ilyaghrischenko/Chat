'use strict'

const searchBar = document.querySelector('.search-bar input');
const contactList = document.querySelector('.contact-list');

searchBar.addEventListener('input', function () {
    const searchText = searchBar.value.toLowerCase();
    const contacts = contactList.querySelectorAll('a');

    for (let i = 0; i < contacts.length; i++) {
        const contact = contacts[i];
        const contactText = contact.textContent.toLowerCase();

        if (contactText.includes(searchText)) {
            contact.style.display = 'block';
        } else {
            contact.style.display = 'none';
        }
    }
});