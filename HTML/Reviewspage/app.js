const userId = localStorage.getItem('userId');
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const movieId = urlParams.get('movieId');


//Functionen henter alle reviews lagt til filmen
function GetReviews() {
    let movieInfo = "";
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
            `;
            document.getElementById("movie-info").innerHTML = movieInfo;

            return fetch(`https://localhost:7033/api/Review/MovieId=${movieId}`);
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(reviewdata => {
            let reviewPromises = reviewdata.map(review => {
                return Promise.all([
                    // henter reviewbilde
                    fetch(`https://localhost:7033/api/ReviewPicture/Id=${review.id}`)
                        .then(response => {
                            if (!response.ok) {
                                // Hvis den feiler vil den bruke standar bildet i asset folderen
                                const ImageUrl = '../assets/Logo/no-image-icon.png'
                                return ImageUrl;
                            } else {
                                return response.json().then(reviewPictureData => {
                                    const imageData = reviewPictureData.reviewPicture;
                                    const ImageUrl = `data:image/jpeg;base64,${imageData}`;
                                    return ImageUrl;
                                });
                            }
                        }),
                    //Henter info om brukeren som la inn reviewen 
                    fetch(`https://localhost:7033/api/User/public/${review.userId}`)
                        
                        .then(response => {
                            if (!response.ok) {
                                return 'No Author';
                            } else {
                                return response.json();
                            }
                        })
                        .then(userData => {
                            if (userData === 'No Author') {
                                return userData; 
                            }
                            return userData.username;
                        })
                ]).then(([ImageUrl, author]) => {
                    // retunerer en objekt med review dataen og bruker dataen
                    return { review, ImageUrl, author };
                });
            });
        
            //venter på at alle promises er løst 
            return Promise.all(reviewPromises);
        })
        .then(reviews => {
            let review_container = "";
            //Sjekker om en buker er logget inn. nektes å legge til Review om bruker ikke innlogget
            if(userId ==null)
            {
                let addmovie = `<h5> You must be logged in to add a review</h5>`
                document.getElementById('addreview').innerHTML = addmovie;

            }
            else{
                let addmovie = ` <a href="">Add Review</a>`
                document.getElementById('addreview').innerHTML = addmovie;
                // henter knappen
                const addReviewButton = document.querySelector('.addreview a');
                // Legger en eventlistner til knappen
                addReviewButton.addEventListener('click', function(event) {
                event.preventDefault();
                const baseUrl = window.location.href.split('?')[0];
                //sender brukeren til reviewpage med filmId som en queryparameter
                 window.location.href = `..//AddReviewPage/addreview.html?movieId=${movieId}`;
    });
            }

            reviews.forEach(({ review, ImageUrl, author }) => {
                //loper igjennom array med review og legger de til
                const title = review.title;
                const reviewtext = review.text;
                const rating = review.rating;
                const reviewID = review.id;
                //formaterer datoen til mer leselig variant
                const reviewdate = new Date(review.dateCreated);
                const formated = reviewdate.toLocaleDateString();
                let starImages = '';
                for (let i = 0; i < rating; i++) {
                    starImages += `<img src="../assets/Logo/star.png" alt="Star">`;
                }
                review_container += `
                    <div class="review-box">
                        <div class="review-img">
                            <img src="${ImageUrl}" alt="review-picture">
                        </div>
                        <div class="review-text">
                            <div class="headerinfo">
                                <span>${author}</span>
                                <span>│</span>
                                <span>${formated}</span>
                            </div>
                            <a href="../ReviewPage/review.html?reviewID=${reviewID}" class="review-title">${title}</a>
                            <div class="Container-Star">${starImages}</div>
                            <p>${reviewtext}</p>
                            <a href="../ReviewPage/review.html?reviewID=${reviewID}"> Read More</a>
                        </div>
                    </div>`;
            });
            document.getElementById('review-container').innerHTML = review_container;

        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function updateAuthenticationUI() {
    const userToken = localStorage.getItem('jwtToken');
    const profileIcon = document.querySelector('.profile-icon');

    if (userToken) {
        // viser profil-ikonet
        signInButton.style.display = 'none';
        profileIcon.style.display = 'block';
        logOutButton.style.display = 'block';

        // navigerer til profil-siden
        profileIcon.addEventListener('click', () => {
            window.location.href = '../ProfilePage/profile.html';
        });
    } else {
        // skjuler profil-ikonet 
        profileIcon.style.display = 'none';
    }
}

window.onload = function() {
    // Call GetReviews function
    GetReviews();
    updateAuthenticationUI()
};
