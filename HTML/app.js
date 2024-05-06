var images = [
    'assets/Background/1.JPG',
    'assets/Background/2.JPG',
    'assets/Background/3.JPG',
    'assets/Background/4.JPG',
    'assets/Background/5.JPG',
    'assets/Background/6.JPEG',
    'assets/Background/7.png'
];

const endpointURL = "https://localhost:7033/api/MoviePoster?PageSize=30&PageNumber=1";


let posterArray = [];

function authenticatedFetch(url, options = {}, requireAuth = false) {
    const token = localStorage.getItem('jwtToken');
    
    options.headers = new Headers(options.headers || {});
    if (token) {
        options.headers.append('Authorization', `Bearer ${token}`);
    }

    return fetch(url, options)
        .then(response => {
            if (response.status === 401) {
                if (requireAuth) {
                    redirectToLogin("Sesjonen din har utløpt, vennligst logg inn igjen.");
                }
                throw new Error('Sesjonen har utløpt eller token mangler');
            }
            return response;
        });
}

function redirectToLogin(message) {
    alert(message);
    logOut();
}

function logOut() {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('email');
    window.location.href = 'index.html';
}

function getposters() {
    return authenticatedFetch(endpointURL)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            posterArray = data.map(item => `data:image/jpeg;base64,${item.moviePoster}`);
            return posterArray;
        })
        .catch(error => console.error('Error during fetch operation:', error));
}

function loadImage(index) {
    var img = new Image();
    img.onload = function() {
        var randomchange = document.getElementById("background");
        randomchange.style.backgroundImage = `linear-gradient(rgba(0,0,0,0.5),rgba(0,0,0,0.8)),url('${posterArray[index]}')`;
    };
    img.src = posterArray[index];
}

function initialize() {
    if (!localStorage.getItem('jwtToken') || !localStorage.getItem('userId')) {
        localStorage.removeItem('jwtToken');
        localStorage.removeItem('email'); 
        localStorage.removeItem('userId');
        localStorage.removeItem('username');        
    }
}

function fetchUserData() {
    const userId = localStorage.getItem('userId');
    const apiUrl = `https://localhost:7033/api/User/${userId}`;

    authenticatedFetch(apiUrl, {}, true)
        .then(response => {
            if (!response.ok) {
                throw new Error('Problem med å hente brukerdata');
            }
            return response.json();
        })
        .then(userData => {
            console.log('Brukerdata hentet suksessfullt:', userData);
        })
        .catch(error => {
            console.error('Feil under henting av brukerdata:', error);
        });
}

window.onload = function() {
    initialize();

    getposters(false)
        .then(() => {
            var imgCount = posterArray.length;
            var randomIndex = Math.floor(Math.random() * imgCount);
            loadImage(randomIndex);
        })
        .catch(error => {
            console.error('Error during initialization:', error);
        });

    const userToken = localStorage.getItem('jwtToken');
    const signInButton = document.querySelector('.signinn');
    const signUpButton = document.querySelector('.signup');
    const profileIcon = document.querySelector('.profile-icon');
    const logOutButton = document.getElementById('logOutButton');

    if (userToken) {
        signInButton.style.display = 'none';
        signUpButton.style.display = 'none';
        profileIcon.style.display = 'block';
        logOutButton.style.display = 'block';

        profileIcon.addEventListener('click', () => {
            window.location.href = 'ProfilePage/profile.html';
        });

        fetchUserData();
    } else {
        signInButton.style.display = 'block';
        signUpButton.style.display = 'block';
        profileIcon.style.display = 'none';
        logOutButton.style.display = 'none';
    }
    
    document.getElementById('logOutButton').addEventListener('click', logOut);
};