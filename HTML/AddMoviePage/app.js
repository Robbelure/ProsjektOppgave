
const endpointURL = "https://localhost:7033/api/MoviePoster?PageSize=30&PageNummer=1";


function GetMovies()
{
    fetch(endpointURL)
    .then(response =>{
        if(!response.ok)
        {
            throw new Error('Network response was not ok')
        }
        return response.json();
    })
    .then(data => {
        movies = data;
        console.log('Movies:', movies);
        var moviecount = movies.length;
        var randommovie = Math.floor(Math.random() * moviecount);
        const movie = movies[randommovie]
        var poster = `data:image/jpeg;base64,${movie.moviePoster}`;
        var background = document.getElementById("hero");
        background.style.backgroundImage = `linear-gradient(rgba(0,0,0,0.7),rgba(255,0,0,0.7)),url('${poster}')`;
    })
};


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
                'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` // Assuming you store your token in localStorage
            },
            body: JSON.stringify(movieData),
        })
        .then(response => {
            if (response.status === 401) {
                throw new Error('Authentication failed. Please log in again.');
            }
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
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` // Also include the token here for protected routes
                },
                body: posterData,
            });
        })
        .then(response => {
            if (response.status === 401) {
                throw new Error('Authentication failed. Please log in again.');
            }
            if (!response.ok) {
                throw new Error('Failed to upload poster image');
            }
            alert('Movie and poster uploaded successfully!');
            form.reset(); // Reset the form after successful submission
        })
        .catch(error => {
            console.error('Error:', error);
            if (error.response && error.response.status === 401) {
                // Dette vil håndtere både en utløpt sesjon og en ikke-innlogget bruker
                alert('Your session has expired, please log in again.');
                // Fjern token og annen brukerdata her
                localStorage.removeItem('jwtToken');
                localStorage.removeItem('userId');
                // osv...
                // Omdiriger til innloggingssiden
                window.location.href = "../SignInnPage/index.html";
            } else {
                alert('Failed to submit form');
            }
        });
    });
});





window.onload = function() {
    GetMovies();
};

