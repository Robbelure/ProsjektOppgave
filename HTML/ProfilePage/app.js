document.addEventListener('DOMContentLoaded', () => {
    const username = localStorage.getItem('username');
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('jwtToken');

    if (username) {
        document.getElementById('welcomeMessage').textContent = `Welcome, ${username}`;
        document.getElementById('username').value = username; // Viser brukernavnet
    }

    const profilePictureElement = document.getElementById('profilePicture');
    const feedbackElement = document.getElementById('imageUploadFeedback');

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
        })
        .catch(error => {
            console.error('Error uploading profile picture:', error);
            feedbackElement.textContent = 'Failed to upload profile picture.';
            feedbackElement.style.color = '#dc3545';
        });
    };

    document.getElementById('profileImage').addEventListener('change', uploadProfilePicture);

    fetchAndDisplayProfilePicture();

    function fetchAndFillUserInfo() {
        fetch(`https://localhost:7033/api/User/${userId}`, {
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(userInfo => {
            document.getElementById('firstName').value = userInfo.firstname || '';
            document.getElementById('lastName').value = userInfo.lastname || '';
            document.getElementById('email').value = userInfo.email || '';
        })
        .catch(error => {
            console.error('Failed to fetch user information:', error);
        });
    }

    document.querySelector('.profile-section').addEventListener('submit', function(e) {
        e.preventDefault(); 
        const updatedFirstName = document.getElementById('firstName').value;
        const updatedLastName = document.getElementById('lastName').value;
        const updatedEmail = document.getElementById('email').value;
        const userData = {
            firstName: updatedFirstName,
            lastName: updatedLastName,
            email: updatedEmail
        };
        fetch(`https://localhost:7033/api/User/${userId}`, {
            method: 'PUT', 
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
            },
            body: JSON.stringify(userData)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to update user profile');
            }
            return response.json();
        })
        .then(updatedUser => {
            console.log('Profile updated successfully:', updatedUser);
            alert('Profile updated successfully!');
        })
        .catch(error => {
            console.error('Error updating profile:', error);
            alert('Failed to update profile.');
        });
    });

    fetchAndFillUserInfo(); 
});