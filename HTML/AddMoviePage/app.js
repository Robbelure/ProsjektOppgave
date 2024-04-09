document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('movieform');
    form.addEventListener('submit', function (event) {
        event.preventDefault(); // prevent the default form submission
        const movieurl = 'https://localhost:7033/api/Movie';
        const movieposterurl = 'https://localhost:7033/api/MoviePoster';
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
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to upload movie data');
            }
            return response.json();
        })
        .then(data => {
            // Extract movie ID from the response
            const movieId = data.id;

            // Prepare movie poster data
            const posterFile = formData.get('poster');
            const posterData = new FormData();
            posterData.append('MovieID', movieId);
            posterData.append('file', posterFile);

            // Upload poster data
            return fetch(movieposterurl, {
                method: 'POST',
                body: posterData,
            });
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to upload poster image');
            }
            alert('Movie and poster uploaded successfully!');
            form.reset(); // Reset the form after successful submission
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to submit form');
        });
    });
});
