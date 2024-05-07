let optionsButtons = document.querySelectorAll(".option-button");
let advancedOptionButton = document.querySelectorAll(".adv-option-button");
let fontName = document.getElementById("fontName");
let fontSizeRef = document.getElementById("fontSize");
let writingArea = document.getElementById("text-input");
let linkButton = document.getElementById("createLink");
let alignButtons = document.querySelectorAll(".align");
let spacingButtons = document.querySelectorAll(".spacing");
let formatButtons = document.querySelectorAll(".format");
let scriptButtons = document.querySelectorAll(".script");


//Liste med fonts
let fontList = [
  "Arial",
  "Verdana",
  "Times New Roman",
  "Garamond",
  "Georgia",
  "Courier New",
  "cursive",
];


const initializer = () => {
  //funksjonskall for å markere knapper
  //Ingen markeringer for link, fjern link, lister, angre, gjenta, siden de er en gang operasjoner"
  highlighter(alignButtons, true);
  highlighter(spacingButtons, true);
  highlighter(formatButtons, false);
  highlighter(scriptButtons, true);

  // Opprett alternativer for skrifttyper
  fontList.map((value) => {
    let option = document.createElement("option");
    option.value = value;
    option.innerHTML = value;
    fontName.appendChild(option);
  });

  //skriftype tillat til størrelse 7
  for (let i = 1; i <= 7; i++) {
    let option = document.createElement("option");
    option.value = i;
    option.innerHTML = i;
    fontSizeRef.appendChild(option);
  }

  //standar størrelse 
  fontSizeRef.value = 3;
};

//hoved logikk
const modifyText = (command, defaultUi, value) => {
//execCommand utfører kommando på valgt tekst
  document.execCommand(command, defaultUi, value);
};

//For grunnleggende operasjoner som ikke trenger verdi-parameter
optionsButtons.forEach((button) => {
  button.addEventListener("click", () => {
    modifyText(button.id, false, null);
  });
});


//alternativer som krever en verdi-parameter (for eksempel farger, skrifter)
advancedOptionButton.forEach((button) => {
  button.addEventListener("change", () => {
    modifyText(button.id, false, button.value);
  });
});



//markerer klikket knapp
const highlighter = (className, needsRemoval) => {
  className.forEach((button) => {
    button.addEventListener("click", () => {

      if (needsRemoval) {
        let alreadyActive = false;
        //Hvis knappen er allerede aktiv 
        if (button.classList.contains("active")) {
          alreadyActive = true;
        }

        //fjerne merke fra andre kanpper
        highlighterRemover(className);
        if (!alreadyActive) {
          button.classList.add("active");
        }
      } else {
        button.classList.toggle("active");
      }
    });
  });
};

const highlighterRemover = (className) => {
  className.forEach((button) => {
    button.classList.remove("active");
  });


};


function upload() {
  const reviewUrl = 'https://localhost:7033/api/Review';
  const reviewPictureUrl = 'https://localhost:7033/api/ReviewPicture/Id=';

  const userId = localStorage.getItem('userId');
  const queryString = window.location.search;
  const urlParams = new URLSearchParams(queryString);
  const movieId = urlParams.get('movieId');
  const rating = document.getElementById('rating').value;
  const title = document.querySelector('.title input').value;
  const text = document.getElementById('text-input').textContent;

  const reviewData = {
    movieId: movieId,
    userId: userId,
    title: title,
    rating: rating,
    text: text
  };

  // Fetch for å legge til review data
  fetch(reviewUrl, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(reviewData)
  })
  .then(response => response.json())
  .then(data => {
    const reviewId = data.id; // Henter id fra responsen

    // Velger fil 
    const fileInput = document.getElementById('file').files[0];

// Oppretter et FormData-objekt for å lagre anmeldelses-ID-en og filen
    const formData = new FormData();
    formData.append('ReviewId', reviewId);
    formData.append('file', fileInput);

    fetch(reviewPictureUrl + reviewId, {
      method: 'POST',
      body: formData
    })
    .then(response => response)
    .then(data => {
      alert('review and poster uploaded successfully!');
      rating.reset(); 
      title.reset(); 
      text.reset(); 
    })
    .catch(error => {
      console.error('Error uploading picture:', error);
    });
  })
  .catch(error => {
    console.error('Error posting review data:', error);
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
  updateAuthenticationUI()
}

window.onload = initializer();


