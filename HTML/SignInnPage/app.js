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
        // Set the background image once the image has loaded
        randomchange.style.backgroundImage = 'linear-gradient(rgba(0,0,0,0.6),rgba(0,0,0,0.98)),url('+images[index]+')'

    };
    img.src = images[index];
}

window.onload = function() {
    // Load the random image initially
    loadImage(randomIndex);
};


var x =  document.getElementById("register");
var y =  document.getElementById("login");
var z =   document.getElementById("btn");  

function login(){
    x.style.left = "-400px";
    y.style.left = "50px";
    z.style.left = "-10px"
}

function register(){
    x.style.left = "50px";
    y.style.left = "-400px";
    z.style.left = "120px"
}
