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
    document.getElementById('login').style.display = 'none'; 
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

document.getElementById('register').addEventListener('submit', function(event){
    event.preventDefault();

    var formData = {
        firstname: document.querySelector('#register input[name="firstname"]').value,
        lastname: document.querySelector('#register input[name="lastname"]').value,
        email: document.querySelector('#register input[name="email"]').value,
        username: document.querySelector('#register input[name="username"]').value,
        password: document.querySelector('#register input[name="password"]').value
    };

    var errors = validateFormData(formData);
    if (errors.length > 0) {
        displayErrors(errors);
        return;
    }

    var requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData)
    };

    var url = 'https://localhost:7033/api/Auth/register';

    fetch(url, requestOptions)
    .then(response => {
        if (!response.ok) {
            throw new Error('Failed to register user.');
        }
        return response.json();
    })
    .then(data => {
        const errorContainer = document.getElementById('error-container');
        errorContainer.innerHTML = '';

        const successMessage = document.createElement('div');
        successMessage.textContent = 'Registration successful!';
        successMessage.style.color = 'green';
        document.getElementById('error-container').appendChild(successMessage);

        setTimeout(() => {
            window.location.href = '../SignInnPage/index.html';
        }, 1250); 
    })
    .catch(error => {
        const errorElement = document.getElementById('error-container');
        errorElement.textContent = error.message;
        errorElement.style.display = 'block';
    });
});

function isValidEmail(email) {
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function validateFormData(formData) {
    var errors = [];

    // Sjekk om brukernavn er gyldig
    if (!formData.username.trim()) {
        errors.push("Username is required");
    } else if (formData.username.length > 50) {
        errors.push("Username cannot be longer than 50 characters");
    }

    // Sjekk om e-post er gyldig
    if (!formData.email.trim()) {
        errors.push("Email is required");
    } else if (!isValidEmail(formData.email)) {
        errors.push("A valid email is required");
    } else if (formData.email.length > 50) {
        errors.push("Email cannot be longer than 50 characters");
    }

    // Sjekk om passord er gyldig
    if (!formData.password.trim()) {
        errors.push("Password is required");
    } else if (formData.password.length < 8) {
        errors.push("Password must contain at least 8 characters");
    } else if (formData.password.length > 100) {
        errors.push("Password cannot be longer than 100 characters");
    } else if (!/[\d]+/.test(formData.password)) {
        errors.push("Password must contain at least 1 number");
    } else if (!/[A-Z]+/.test(formData.password)) {
        errors.push("Password must contain at least 1 uppercase letter");
    } else if (!/[a-z]+/.test(formData.password)) {
        errors.push("Password must contain at least 1 lowercase letter");
    } else if (!/[^a-zA-Z0-9]+/.test(formData.password)) {
        errors.push("Password must contain at least one special character");
    }

    return errors;
}

function displayErrors(errors) {
    const errorElement = document.getElementById('error-container');
    errorElement.innerHTML = ''; // Fjern tidligere feilmeldinger
    errors.forEach(error => {
        const errorMessage = document.createElement('div');
        errorMessage.textContent = error;
        errorElement.appendChild(errorMessage);
    });
    errorElement.style.display = 'block';
}
