const userId = localStorage.getItem('userId');
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const movieId = urlParams.get('movieId');

function GetReviews(){
    let movieInfo ="";
    fetch(`https://localhost:7033/api/Movie/movieId=${movieId}`)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        movieInfo += `
        <span class="Movietitle">${data.movieName}</span>
            <span class="releaseyear">Released: ${data.releaseYear}</span>
            <span class="Director">Director: ${data.director}</span>
            <span class="AverageRating">Average user rating: ${data.averageRating}</span>
            <span class="Genre">${data.genre}</span>
        `
        document.getElementById("movie-info").innerHTML= movieInfo;
    })
    .then
    {
        fetch(`https://localhost:7033/api/Review/MovieId=${movieId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })


    }

};




window.onload = function() {
    GetReviews();

};