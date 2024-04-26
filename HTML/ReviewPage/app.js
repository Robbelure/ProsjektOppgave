const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const reviewId = urlParams.get('reviewID');

// Fetch review data
fetch(`https://localhost:7033/api/Review/Id=${reviewId}`)
    .then(response => response.json())
    .then(review => {
        const { title, rating, text } = review;

        // Create star images
        let starImages = "";
        for (let i = 0; i < rating; i++) {
            starImages += `<img src="../assets/Logo/star.png" alt="Star">`;
        }
//TODO!! Samme som add reviews 
        // Fetch review image
        const reviewImageURL = `https://localhost:7033/api/ReviewPicture/Id=${reviewId}`;
        fetch(reviewImageURL)
            .then(imageResponse => {
                if (!imageResponse.ok) {
                    // If the request fails, return a default image URL
                    return '../assets/Logo/no-image-icon.png';
                }
                return imageResponse.blob();
            })
            .then(blob => {
                let imageURL;
                if (typeof blob === 'string') {
                    // If the response is already a string (default image URL), use it directly
                    imageURL = blob;
                } else {
                    // If the response is a blob, create an object URL
                    imageURL = URL.createObjectURL(blob);
                }

                const reviewHTML = `
                    <div class="reviewimage">
                        <img src="${imageURL}" alt="Review Image">
                    </div>
                    <section class="reviewbody">
                        <h1 class="title">${title}</h1>
                        <div class="starscontainer">${starImages}</div>
                        <div class="textsection">${text}</div>
                    </section>
                `;
                document.getElementById('Review').innerHTML = reviewHTML;
            })
            .catch(error => console.error('Fetch Error:', error));
    })
    .catch(error => console.error('Fetch Error:', error));
