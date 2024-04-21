const userId = localStorage.getItem('userId');
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const movieId = urlParams.get('movieId');

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

            // Fetch reviews for the movie
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
                    // Fetch review picture
                    fetch(`https://localhost:7033/api/ReviewPicture/Id=${review.id}`)
                        .then(response => {
                            if (!response.ok) {
                                // If the request fails, return a default image
                                return '../assets/Logo/no-image-icon.png';
                            } else {
                                return response.blob();
                            }
                        })
                        .then(blob => {
                            if (typeof blob === 'string') {
                                return blob; // Default image URL
                            }
                            // Convert blob to URL
                            return URL.createObjectURL(blob);
                        }),
                    // Fetch user data
                    //TODO! Vi trenger ny endepunkt her slike at vi vi kan hente opp brukernavnet 
                    fetch(`https://localhost:7033/api/User/${review.userId}`)
                        .then(response => {
                            if (!response.ok) {
                                return 'No Author'; // If the request fails, return 'No Author'
                            } else {
                                return response.json();
                            }
                        })
                        .then(userData => {
                            if (userData === 'No Author') {
                                return userData; // Return 'No Author' directly
                            }
                            return userData.username;
                        })
                ]).then(([reviewImageUrl, author]) => {
                    // Return an object containing the review data
                    return { review, reviewImageUrl, author };
                });
            });
        
            // Wait for all promises to resolve
            return Promise.all(reviewPromises);
        })
        .then(reviews => {
            // Now we have an array of review objects with image URLs and authors
            let review_container = "";
            reviews.forEach(({ review, reviewImageUrl, author }) => {
                const title = review.title;
                const reviewtext = review.text;
                const rating = review.rating;
                const reviewID = review.id;
                let starImages = '';
                for (let i = 0; i < rating; i++) {
                    starImages += `<img src="../assets/Logo/star.png" alt="Star">`;
                }
                review_container += `
                    <div class="review-box">
                        <div class="review-img">
                            <img src="${reviewImageUrl}" alt="Review Image">
                        </div>
                        <div class="review-text">
                            <div class="headerinfo">
                                <span>${author}</span>
                                <span>│</span>
                                <span>18.03.2023</span>
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

window.onload = function() {
    // Call GetReviews function
    GetReviews();
    // Get the button element
    const addReviewButton = document.querySelector('.addreview a');
    // Add an event listener to the button
    addReviewButton.addEventListener('click', function(event) {
        // Prevent the default behavior of the anchor tag
        event.preventDefault();
        // Get the current URL without parameters
        const baseUrl = window.location.href.split('?')[0];
        // Get the movieId
        // Redirect to the AddReviewPage with the movieId as a query parameter
        window.location.href = `..//AddReviewPage/addreview.html?movieId=${movieId}`;
    });
};
