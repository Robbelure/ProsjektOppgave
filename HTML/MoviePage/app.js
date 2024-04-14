const endpointURL = "https://localhost:7033/api/Movie?pagesize=30&pagenummer=1";

function GetMovies() {
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
            console.log('Movies:', movies);
            var moviecount = movies.length;
            var randommovie = Math.floor(Math.random() * moviecount);
            const movie = movies[randommovie]
            const movieid = movie.id;
            herocontent += `
            <section class="hero-content">
            <h1>${movie.movieName}</h1>
            <p>${movie.summary}</p>
            <a class="AddMovie" href="../AddMoviePage/addmove.html">Add A Movie</a>
            </section>`;
            document.getElementById("hero-content").innerHTML = herocontent;
            //sort the movies by date : latest added
            movies.sort((b, a) => new Date(a.dateCreated) - new Date(b.dateCreated));
            const latestaddedmovie = movies.slice(0, 5);
            console.log("Oldest 5 movies:", latestaddedmovie);
            latestaddedmovie.forEach(async movie => {
                try {
                    const response = await fetch(`https://localhost:7033/api/MoviePoster/movieId=${movie.id}`);
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const data = await response.json();
                    // Use the fetched data here
                    const posterURL = `data:image/jpeg;base64,${data.moviePoster}`;
                    const movieName = movie.movieName;
                    const averageRating = movie.averageRating;
                    // Generate star images based on averageRating
                    let starImages = '';
                    for (let i = 0; i < averageRating; i++) {
                        starImages += `<img src="asset/star.png" alt="Star">`;
                    }
                    latestaddedmoviesection += 
                    `
                    <div  class="Container-poster">
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

            // Fetch movie poster data
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


window.onload = function() {
    GetMovies();

};
