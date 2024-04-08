document.addEventListener('DOMContentLoaded', function () {
    var movieurl = 'https://localhost:7033/api/Movie';
    var posterurl = 'https://localhost:7033/api/MoviePoster';
    const form = document.getElementById('movieform');
    form.addEventListener('submit', function (event) {
        event.preventDefault(); // prevent the default form submission

        // Get form data
        const formData = new FormData(form);
        const movieData = {
            MovieName: formData.get('MovieName'),
            Summary: formData.get('summary'),
            ReleaseYear: parseInt(formData.get('ReleaseYear')),
            Director: formData.get('Director'),
            Genre: formData.get('Genre'),
        };

        // Upload movie data
        fetch(movieurl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(movieData),
        })
        .then(response => response.json())
        .then(data => {
            // Once movie is uploaded, upload the poster image
            const movieId = data.Id; // assuming the API returns the movie ID
            const posterFile = formData.get('poster');
            const posterData = new FormData();
            posterData.append('MovieId', movieId);
            posterData.append('MoviePoster', posterFile);

            // Upload poster image
            fetch(posterurl, {
                method: 'POST',
                body: posterData,
            })
            .then(response => {
                if (response.ok) {
                    alert('Movie and Poster uploaded successfully!');
                    // Clear the form
                    form.reset();
                } 
            })
            .catch(error => {
                console.error('Failed to upload poster image', error);
                alert('Failed to upload poster image');
            });
        })
        .catch(error => {
            console.error('Failed to upload movie data', error);
            alert('Failed to upload movie data');
        });
    });
});
