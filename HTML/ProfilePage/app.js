document.addEventListener('DOMContentLoaded', () => {
    const username = localStorage.getItem('username');
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('jwtToken');

    if (username) {
        document.getElementById('welcomeMessage').textContent = `Welcome, ${username}`;
    }

    const profilePictureElement = document.getElementById('profilePicture');
    const feedbackElement = document.getElementById('imageUploadFeedback');

    // Henter og viser profilbilde
    const fetchAndDisplayProfilePicture = () => {
        const apiUrl = `https://localhost:7033/api/UploadProfilePicture/Id=${userId}`;

        fetch(apiUrl, {
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.profilePicture) {
                const imageDataUrl = `data:image/jpeg;base64,${data.profilePicture}`;
                profilePictureElement.src = imageDataUrl;
            } else {
                throw new Error('Profilbilde ikke funnet, kan laste opp et nytt.');
            }
        })
        .catch(error => {
            console.error('Error fetching profile picture:', error);
            profilePictureElement.src = 'placeholder-profile-image.png';
        });
    };

    // Opplasting av nytt profilbilde
    document.getElementById('profileImage').addEventListener('change', uploadProfilePicture);

    // Funksjon for å laste opp profilbilde
    function uploadProfilePicture() {
        const fileInput = document.getElementById('profileImage');

        if (fileInput.files.length === 0) {
            feedbackElement.textContent = 'Ingen fil valgt.';
            feedbackElement.style.color = '#dc3545'; // Feilfarge
            return;
        }

        const file = fileInput.files[0];
        const reader = new FileReader();

        reader.onload = function(e) {
            profilePictureElement.src = e.target.result;
        };

        reader.readAsDataURL(file); // Leser filen som Data URL for å vise et forhåndsvisningsbilde

        // Tilbakemelding før opplastingen er fullført
        feedbackElement.textContent = 'Laster opp...';
        feedbackElement.style.color = '#ffc107'; // Advarselsfarge (gul)

        const formData = new FormData();
        formData.append('file', file);

        fetch(`https://localhost:7033/api/UploadProfilePicture/Id=${userId}`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
            },
            body: formData
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Opplasting av profilbilde feilet.');
            }
            return response.text();
        })
        .then(() => {
            feedbackElement.textContent = 'Opplastning av profilbilde vellykket!';
            feedbackElement.style.color = '#28a745'; // Suksessfarge
        })
        .catch(error => {
            console.error('Error uploading profile picture:', error);
            feedbackElement.textContent = 'Feil ved opplasting av bildet.';
            feedbackElement.style.color = '#dc3545'; // Feilfarge
        });
    }

    fetchAndDisplayProfilePicture();
});