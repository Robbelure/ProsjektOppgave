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


//List of fontlist
let fontList = [
  "Arial",
  "Verdana",
  "Times New Roman",
  "Garamond",
  "Georgia",
  "Courier New",
  "cursive",
];

//Initial Settings
const initializer = () => {
  //function calls for highlighting buttons
  //No highlights for link, unlink,lists, undo,redo since they are one time operations
  highlighter(alignButtons, true);
  highlighter(spacingButtons, true);
  highlighter(formatButtons, false);
  highlighter(scriptButtons, true);

  //create options for font names
  fontList.map((value) => {
    let option = document.createElement("option");
    option.value = value;
    option.innerHTML = value;
    fontName.appendChild(option);
  });

  //fontSize allows only till 7
  for (let i = 1; i <= 7; i++) {
    let option = document.createElement("option");
    option.value = i;
    option.innerHTML = i;
    fontSizeRef.appendChild(option);
  }

  //default size
  fontSizeRef.value = 3;
};

//main logic
const modifyText = (command, defaultUi, value) => {
  //execCommand executes command on selected text
  document.execCommand(command, defaultUi, value);
};

//For basic operations which don't need value parameter
optionsButtons.forEach((button) => {
  button.addEventListener("click", () => {
    modifyText(button.id, false, null);
  });
});

//options that require value parameter (e.g colors, fonts)
advancedOptionButton.forEach((button) => {
  button.addEventListener("change", () => {
    modifyText(button.id, false, button.value);
  });
});

//link
linkButton.addEventListener("click", () => {
  let userLink = prompt("Enter a URL");
  //if link has http then pass directly else add https
  if (/http/i.test(userLink)) {
    modifyText(linkButton.id, false, userLink);
  } else {
    userLink = "http://" + userLink;
    modifyText(linkButton.id, false, userLink);
  }
});

//Highlight clicked button
const highlighter = (className, needsRemoval) => {
  className.forEach((button) => {
    button.addEventListener("click", () => {
      //needsRemoval = true means only one button should be highlight and other would be normal
      if (needsRemoval) {
        let alreadyActive = false;

        //If currently clicked button is already active
        if (button.classList.contains("active")) {
          alreadyActive = true;
        }

        //Remove highlight from other buttons
        highlighterRemover(className);
        if (!alreadyActive) {
          //highlight clicked button
          button.classList.add("active");
        }
      } else {
        //if other buttons can be highlighted
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
  const rating = 4;
  const title = document.querySelector('.title input').value;
  const text = document.getElementById('text-input').textContent;
  console.log(userId , movieId);

  const reviewData = {
    movieId: movieId,
    userId: userId,
    title: title,
    rating: rating,
    text: text
  };

  // Fetch request to post the reviewData to the server
  fetch(reviewUrl, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(reviewData)
  })
  .then(response => response.json())
  .then(data => {
    const reviewId = data.id; // Extracting the review ID from the response

    // Selecting the file input
    const fileInput = document.getElementById('file').files[0];

    // Creating a FormData object to store the review ID and the file
    const formData = new FormData();
    formData.append('ReviewId', reviewId);
    formData.append('file', fileInput);

    // Fetch request to post the picture with the review ID
    fetch(reviewPictureUrl + reviewId, {
      method: 'POST',
      body: formData
    })
    .then(response => response.json())
    .then(data => {
      console.log('Picture upload successful:', data);
    })
    .catch(error => {
      console.error('Error uploading picture:', error);
    });
  })
  .catch(error => {
    console.error('Error posting review data:', error);
  });
}






window.onload = initializer();


