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
    document.getElementById('login').style.display = 'none'; // Start med å skjule login-skjemaet
};

var x = document.getElementById("register");
var y = document.getElementById("login");
var z = document.getElementById("btn");

function login(){
    x.style.display = "none";
    y.style.display = "block";
    z.style.left = "120px";
}

function register(){
    x.style.display = "block";
    y.style.display = "none";
    z.style.left = "0px";
}

// Event listener for registreringsskjemaet
document.getElementById('register').addEventListener('submit', function(event){
    event.preventDefault();

    var userData = {
        firstname: document.querySelector('#register input[name="firstname"]').value,
        lastname: document.querySelector('#register input[name="lastname"]').value,
        email: document.querySelector('#register input[name="email"]').value,
        username: document.querySelector('#register input[name="username"]').value,
        password: document.querySelector('#register input[name="password"]').value
    };

    var requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(userData)
    };

    var url = 'https://localhost:7033/api/Auth/register';

    fetch(url, requestOptions)
    .then(response => response.json().then(data => {
        if (!response.ok) {
            // Her henter vi ut 'Detail' fra 'ProblemDetails' objektet
            throw new Error(data.detail || 'Det oppstod en feil ved registreringen.');
        }
        return data; // data inneholder nå JSON responsen for vellykkede forespørsler
    }))
    .then(data => {
        window.location.href = '../SignInnPage/index.html';
    })
    .catch(error => {
        const errorElement = document.getElementById('error-container');
        errorElement.textContent = error.message;
        errorElement.style.display = 'block';
    });
});
