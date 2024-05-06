var images = [
    '../assets/Background/1.JPG',
    '../assets/Background/2.JPG',
    '../assets/Background/3.JPG',
    '../assets/Background/4.JPG',
    '../assets/Background/5.JPG',
    '../assets/Background/6.JPEG',
    '../assets/Background/7.png'
];

var imgCount = images.length;
var randomIndex = Math.floor(Math.random() * imgCount);
var randomchange = document.getElementById("container");

function loadImage(index) {
    var img = new Image();
    img.onload = function() {
        randomchange.style.backgroundImage = 'linear-gradient(rgba(0,0,0,0.6),rgba(0,0,0,0.98)),url('+images[index]+')';
    };
    img.src = images[index];
}

window.onload = function() {
    loadImage(randomIndex);
    
    document.getElementById('loginForm').addEventListener('submit', function(e) {
        e.preventDefault();
        var username = document.getElementById('loginUsername').value;
        var password = document.getElementById('loginPassword').value;
        loginUser(username, password);
    });
};

var x = document.getElementById("register");
var y = document.getElementById("login");
var z = document.getElementById("btn");

function login() {
    x.style.left = "-400px";
    y.style.left = "50px";
    z.style.left = "-10px";
}

function register() {
    x.style.left = "50px";
    y.style.left = "-400px";
    z.style.left = "120px";
}

function loginUser(username, password) {
    var loginUrl = 'https://localhost:7033/api/Auth/login'; 

    fetch(loginUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Autentisering mislyktes');
        }
        return response.json();
    })
    .then(data => {
        if(data.token) {
            localStorage.setItem('jwtToken', data.token);
            localStorage.setItem('userId', data.userId); 
            localStorage.setItem('username', data.username);
            localStorage.setItem('email', data.email);
            alert('Innlogging vellykket!');
            window.location.href = '../index.html';
        } else {
            alert('Innlogging mislyktes, ingen token mottatt.');
        }
    })
    .catch(error => {
        console.error('Innloggingsfeil:', error);
        alert('Innlogging mislyktes: ' + error.message);
    });
}