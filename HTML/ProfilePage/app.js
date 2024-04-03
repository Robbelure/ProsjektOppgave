document.addEventListener('DOMContentLoaded', () => {
    const username = localStorage.getItem('username'); // Henter lagret brukernavn
    if (username) {
        document.querySelector('.profile-section h1').textContent = `Velkommen, ${username}`;
    }
});