document.addEventListener('DOMContentLoaded', () => {
    const username = localStorage.getItem('username');
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('jwtToken');

    const welcomeMessageElement = document.getElementById('welcomeMessage');
    const profilePictureElement = document.getElementById('profilePicture');
    const feedbackElement = document.getElementById('imageUploadFeedback');
    const firstNameElement = document.getElementById('firstName'); 
    const lastNameElement = document.getElementById('lastName'); 
    const emailElement = document.getElementById('email');
    const formElement = document.querySelector('.profile-section');

    // Vis brukernavn i velkomstmelding
    if (username) {
        welcomeMessageElement.textContent = `Welcome, ${username}`;
        document.getElementById('username').value = username;
    }

    // Hent og vis profilbilde
    const fetchAndDisplayProfilePicture = () => {
        const apiUrl = `https://localhost:7033/api/UploadProfilePicture/Id=${userId}`;
        fetch(apiUrl, {
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch profile picture.');
            }
            return response.json();
        })
        .then(data => {
            console.log("Received data: ", data);  
            if (data && data.profilePicture) {
                const imageDataUrl = `data:image/jpeg;base64,${data.profilePicture}`;
                profilePictureElement.src = imageDataUrl;
            } else {
                console.log("No profile picture found, using default.");
                profilePictureElement.src = 'https://localhost:7033/images/profile-icon.jpg';
            }
        })
        .catch(error => {
            console.error('Error fetching profile picture:', error);
            profilePictureElement.src = 'https://localhost:7033/images/profile-icon.jpg';
        });
    };

    // Hent og vis brukerdata
    const fetchAndDisplayUserData = () => {
        const userDataApiUrl = `https://localhost:7033/api/User/${userId}`;
        fetch(userDataApiUrl, {
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch user data.');
            }
            return response.json();
        })
        .then(userData => {
            // vis fornavn og etternavn hvis de eksisterer
            if (userData.firstname) {
                firstNameElement.value = userData.firstname;
            }
            if (userData.lastname) {
                lastNameElement.value = userData.lastname;
            }
            emailElement.value = localStorage.getItem('email');
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
        });
    };

    const uploadProfilePicture = () => {
        const fileInput = document.getElementById('profileImage');
        if (fileInput.files.length === 0) {
            feedbackElement.textContent = 'Ingen fil valgt.';
            feedbackElement.style.color = '#dc3545';
            return;
        }
        const file = fileInput.files[0];
        const reader = new FileReader();
        reader.onload = function(e) {
            profilePictureElement.src = e.target.result;
        };
        reader.readAsDataURL(file);
        feedbackElement.textContent = 'Uploading...';
        feedbackElement.style.color = '#ffc107';
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
                throw new Error('Failed to upload profile picture.');
            }
            return response.text();
        })
        .then(() => {
            feedbackElement.textContent = 'Profile picture uploaded successfully!';
            feedbackElement.style.color = '#28a745';
            setTimeout(() => {
                feedbackElement.textContent = '';
            }, 3000);
        })
        .catch(error => {
            console.error('Error uploading profile picture:', error);
            feedbackElement.textContent = 'Failed to upload profile picture.';
            feedbackElement.style.color = '#dc3545';
        });
    };

    const updateUserData = (event) => {
        event.preventDefault(); 

        const updatedUserData = {
            firstname: firstNameElement.value,
            lastname: lastNameElement.value,
        };

        const updateApiUrl = `https://localhost:7033/api/User/${userId}`;
        fetch(updateApiUrl, {
            method: 'PUT', 
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedUserData)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to update user data.');
            }
            feedbackElement.textContent = 'User data updated successfully!';
            feedbackElement.style.color = '#28a745';
            setTimeout(() => {
                feedbackElement.textContent = '';
            }, 3000);
        })
        .catch(error => {
            console.error('Error updating user data:', error);
            feedbackElement.textContent = 'Failed to update user data.';
            feedbackElement.style.color = '#dc3545';
        });
    };

    document.getElementById('profileImage').addEventListener('change', uploadProfilePicture);
    formElement.addEventListener('submit', updateUserData);

    fetchAndDisplayProfilePicture();
    fetchAndDisplayUserData();
});
