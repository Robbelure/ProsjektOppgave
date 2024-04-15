document.addEventListener('DOMContentLoaded', () => {
    const username = localStorage.getItem('username');
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('jwtToken');

    if (username) {
        document.getElementById('welcomeMessage').textContent = `Velkommen, ${username}`;
    }

    const profilePictureElement = document.getElementById('profilePicture');

    // Henter og viser profilbilde
    const fetchAndDisplayProfilePicture = () => {
        const apiUrl = `https://localhost:7033/api/UploadProfilePicture/Id=${userId}`;

        fetch(apiUrl, {
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Profilbilde ikke funnet, kan laste opp et nytt.');
            }
            return response.json();
        })
        .then(data => {
            const imageDataUrl = `data:image;base64,${data.profilePicture}`;
            profilePictureElement.src = imageDataUrl;
        })
        .catch(error => {
            console.error('Error fetching profile picture:', error);
            profilePictureElement.src = 'placeholder-profile-image.png';
        });
    };

    // Opplasting av nytt profilbilde
    const uploadProfilePicture = () => {
        const fileInput = document.getElementById('profileImage');
        if (fileInput.files.length === 0) {
            alert('Vennligst velg en fil først.');
            return;
        }

        const formData = new FormData();
        formData.append('file', fileInput.files[0]);

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
            return response.json();
        })
        .then(() => {
            fetchAndDisplayProfilePicture();
            alert('Profilbildet ble lastet opp.');
        })
        .catch(error => {
            console.error('Error uploading profile picture:', error);
            alert(error.message);
        });
    };

    document.getElementById('uploadButton').addEventListener('click', uploadProfilePicture);

    fetchAndDisplayProfilePicture();
});