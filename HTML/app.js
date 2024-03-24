var images = [
    '/assets/Background/1.JPG',
    '/assets/Background/2.JPG',
    '/assets/Background/3.JPG',
    '/assets/Background/4.JPG',
    '/assets/Background/5.JPG',
    '/assets/Background/6.JPEG',
    '/assets/Background/7.png'
];

var imgCount = images.length;
var randomIndex = Math.floor(Math.random() * imgCount);
var randomchange = document.getElementById("background");

function loadImage(index) {
    var img = new Image();
    img.onload = function() {
        // Set the background image once the image has loaded
        randomchange.style.backgroundImage = 'url('+images[index]+')';
    };
    img.src = images[index];
}

window.onload = function() {
    // Load the random image initially
    loadImage(randomIndex);
};
