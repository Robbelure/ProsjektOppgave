<<<<<<< HEAD
var images = [
    'assets/Background/1.JPG',
    'assets/Background/2.JPG',
    'assets/Background/3.JPG',
    'assets/Background/4.JPG',
    'assets/Background/5.JPG',
    'assets/Background/6.JPEG',
    'assets/Background/7.png'
];
//Funksjon for å endre backgrunnen til en vilkårlig bilde
=======
//var images = [
//     'assets/Background/1.JPG',
//     'assets/Background/2.JPG',
//     'assets/Background/3.JPG',
//     'assets/Background/4.JPG',
//     'assets/Background/5.JPG',
//     'assets/Background/6.JPEG',
//     'assets/Background/7.png'
//];

const endpointURL = "https://localhost:7033/api/MoviePoster?PageSize=10&PageNummer=1";

let posterArray = [];


function getposters() {
    return fetch(endpointURL)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            posterArray = data.map(item => `data:image/jpeg;base64,${item.moviePoster}`);
            console.log(posterArray);
            return posterArray;
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            throw error; // re-throw the error to be caught in the caller
        });
}
>>>>>>> 9df2737a5ee5531ce04ff266eb6368c7fc7fcbca

function loadImage(index) {
    var img = new Image();
    img.onload = function() {
        var randomchange = document.getElementById("background");
        randomchange.style.backgroundImage = `linear-gradient(rgba(0,0,0,0.5),rgba(0,0,0,0.8)),url('${posterArray[index]}')`;
    };
    img.src = posterArray[index];
}



// Initialiserer applikasjonen
function initialize() {
    if (!localStorage.getItem('jwtToken') || !localStorage.getItem('userId')) {
        localStorage.removeItem('jwtToken');
        localStorage.removeItem('userId');
        localStorage.removeItem('username');
        
    }
}

// Funksjon for å hente brukerdata
function fetchUserData() {
    const userId = localStorage.getItem('userId');
    const apiUrl = `https://localhost:7033/api/User/${userId}`;
    const token = localStorage.getItem('jwtToken');

    if (!token || !userId) {
        console.error('Ingen token eller bruker-ID funnet, vennligst logg inn først.');
        return;
    }

    fetch(apiUrl, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
    .then(response => response.json())
    .then(userData => {
        console.log('Brukerdata hentet suksessfullt:', userData);
        // Oppdaterer brukergrensesnittet med brukerdata her
    })
    .catch(error => {
        console.error('Feil under henting av brukerdata:', error);
    });
}

window.onload = function() {
    initialize();
    getposters()
    .then(() => {
        var imgCount = posterArray.length;
        var randomIndex = Math.floor(Math.random() * imgCount);
        loadImage(randomIndex);
    })
    .catch(error => {
        console.error('Error initializing:', error);
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
};

function logOut() {
    localStorage.removeItem('jwtToken'); // Fjerner token fra lokal lagring
    localStorage.removeItem('userId'); // Fjerner brukerens ID
    localStorage.removeItem('username'); // Fjerner brukerens navn
    window.location.href = 'index.html'; 
}

// Lytter etter klikk på logut-knappen
document.getElementById('logOutButton').addEventListener('click', logOut);