const endpointURL = "https://localhost:7033/api/Movie?pageSize=30&pageNumber=1";
const ReviewsURL = "https://localhost:7033/api/Review?pagesize=1&pagenummer=30";
const userId = localStorage.getItem('userId');

function GetLatestAddedMovies() {
    let movies = [];
    let herocontent = "";
    let latestaddedmoviesection = "";
    fetch(endpointURL)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            movies = data;
            var moviecount = movies.length;
            var randommovie = Math.floor(Math.random() * moviecount);
            //Gir en tilfeldig movie som brukes som hero Content
            const movie = movies[randommovie]
            const movieid = movie.id;
            //sjekker om brukeren er logget inn: hvis ikke nektes de å legge inn en film
            if(userId == null)
            {
                herocontent += `
                <section class="hero-content">
                <h1>${movie.movieName}</h1>
                <p>${movie.summary}</p>
                <h5>You must be logged in to add movies</h5>
                </section>`;
            }
            else{
                herocontent += `
                <section class="hero-content">
                <h1>${movie.movieName}</h1>
                <p>${movie.summary}</p>
                <a class="AddMovie" href="../AddMoviePage/addmove.html">Add A Movie</a>
                </section>`;
            }
            document.getElementById("hero-content").innerHTML = herocontent;
            //sorter filmene etter sist lagt til
            movies.sort((b, a) => new Date(a.dateCreated) - new Date(b.dateCreated));
            //henter de siste 5 lagt til filmene 
            const latestaddedmovie = movies.slice(0, 5);
            latestaddedmovie.forEach(async movie => {
                try {
                    const response = await fetch(`https://localhost:7033/api/MoviePoster/movieId=${movie.id}`);
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const data = await response.json();
                    const posterURL = `data:image/jpeg;base64,${data.moviePoster}`;
                    const movieName = movie.movieName;
                    const averageRating = movie.averageRating;
                    let starImages = '';
                    for (let i = 0; i < averageRating; i++) {
                        starImages += `<img src="asset/star.png" alt="Star">`;
                    }
                    latestaddedmoviesection += 
                    `
                    <div  class="Container-poster" onclick="redirectToReviewspage(${movie.id})">
                        <img src="${posterURL}" alt="${movieName} Poster">
                        <h3>${movieName}</h3>
                        <div class="Container-Star">
                            ${starImages}
                        </div>
                    </div>
                    `;
                    document.getElementById("latesmovieContainer").innerHTML = latestaddedmoviesection;
                    
                } catch (error) {
                    console.error('Error fetching movie poster:', error);
                }
            })

            // Henter movie poster
            return fetch(`https://localhost:7033/api/MoviePoster/movieId=${movieid}`);
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            var poster = `data:image/jpeg;base64,${data.moviePoster}`;
            var background = document.getElementById("background");
            background.style.backgroundImage = `linear-gradient(rgba(0,0,0,0.5),rgba(0,0,0,0.8)),url('${poster}')`;
        })
        .catch(error => {
            console.error('Error fetching movies:', error);
        });
}

async function GetLatestReviewed() {
    try {
        let reviews = await fetch(ReviewsURL);
        if (!reviews.ok) {
            throw new Error('Network response was not ok');
        }
        let reviewData = await reviews.json();
        //sorterer reviewene etter sist lagt til 
        reviewData.sort((a, b) => new Date(b.dateCreated) - new Date(a.dateCreated));
        //Gir siste 5 lagt til reviewene
        const latestAddedReviews = reviewData.slice(0, 5);

        let latestAddedReviewsSection = "";

        for (let review of latestAddedReviews) {
            let movieResponse = await fetch(`https://localhost:7033/api/Movie/movieId=${review.movieId}`);
            if (!movieResponse.ok) {
                throw new Error('Network response was not ok');
            }
            let movieData = await movieResponse.json();
            const rating = review.rating;
            let starImages = '';
            for (let i = 0; i < rating; i++) {
                starImages += `<img src="asset/star.png" alt="Star">`;
            }

            let posterResponse = await fetch(`https://localhost:7033/api/MoviePoster/movieId=${review.movieId}`);
            if (!posterResponse.ok) {
                throw new Error('Network response was not ok');
            }
            let posterData = await posterResponse.json();
            const posterURL = `data:image/jpeg;base64,${posterData.moviePoster}`;

            latestAddedReviewsSection +=
                `
                <div  class="Container-poster" id="reviewContainer" onclick="redirectReview(${review.id})">
                    <img src="${posterURL}" alt="${movieData.movieName} Poster">
                    <h3>${movieData.movieName}</h3>
                    <div class="Container-Star">
                        ${starImages}
                    </div>
                </div>
                `;
        }

        document.getElementById("Latestreviews").innerHTML = latestAddedReviewsSection;
    } catch (error) {
        console.error(error);
    }
}

//Henter alle filmer
function getallmovies() {
    let allmovies = "";
    fetch(endpointURL)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            data.forEach(resp => {
                const { id, movieName, averageRating } = resp;
                fetch(`https://localhost:7033/api/MoviePoster/movieId=${resp.id}`)
                    .then(posterResponse => posterResponse.json())
                    .then(posterData => {
                        const { moviePoster } = posterData;
                        const poster = `data:image/jpeg;base64,${moviePoster}`;
                        let starImages = '';
                         for (let i = 0; i < averageRating; i++) {
                                starImages += `<img src="asset/star.png" alt="Star">`;
                                }
                        const movieHTML = `
                            <div class="Container-poster" id="reviewContainer" onclick="redirectToReviewspage(${id})">
                                <img src="${poster}" alt="${movieName} Poster">
                                <h3>${movieName}</h3>
                                <div class="Container-Star">
                                    ${starImages} 
                                </div>
                            </div>
                        `;
  
                        allmovies += movieHTML;
                        document.getElementById('Allmovies').innerHTML = allmovies;
                    })
                    .catch(error => console.error('Error fetching movie poster:', error));
            });
        })
        .catch(error => console.error('Error fetching movies:', error));
}

window.onload = function() {
    GetLatestAddedMovies();
    GetLatestReviewed();
    getallmovies();
    updateAuthenticationUI();
};

function updateAuthenticationUI() {
    const userToken = localStorage.getItem('jwtToken');
    const signInButton = document.querySelector('.signinn');
    const profileIcon = document.querySelector('.profile-icon');
    const logOutButton = document.getElementById('logOutButton');

    if (userToken) {
        signInButton.style.display = 'none';
        profileIcon.style.display = 'block'; 
        logOutButton.style.display = 'block';

        profileIcon.addEventListener('click', () => {
            window.location.href = '../ProfilePage/profile.html';
        });
    } else {
        signInButton.style.display = 'block';
        profileIcon.style.display = 'none';
    }
}

function redirectToReviewspage(movieId) {
    window.location.href = `../Reviewspage/Reviews.html?movieId=${movieId}`;
}

function redirectReview(Reviewid) {
    window.location.href = `../ReviewPage/review.html?reviewID=${Reviewid}`;
}