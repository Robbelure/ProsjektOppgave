const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const reviewId = urlParams.get('reviewID');
const userId = localStorage.getItem('userId');



// Fetch review data
fetch(`https://localhost:7033/api/Review/Id=${reviewId}`)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(review => {
        const { title, rating, text} = review;
        // Create star images
        let starImages = "";
        for (let i = 0; i < rating; i++) {
            starImages += `<img src="../assets/Logo/star.png" alt="Star">`;
        }

        // Fetch review image
        const reviewImageURL = `https://localhost:7033/api/ReviewPicture/Id=${reviewId}`;
        fetch(reviewImageURL)
            .then(imageResponse => {
                if (!imageResponse.ok) {
                    // If the request fails, return a default image
                    throw new Error('Network response was not ok');
                }
                return imageResponse.json();
            })
            .then(reviewPictureData => {
                
                // reviewPictureData has the structure { reviewPicture: imageData }
                const imageData = reviewPictureData.reviewPicture;
                const imageURL = `data:image/jpeg;base64,${imageData}`;
                const review_userid = review.userId;
                if(userId == review_userid  )
                {
                    const reviewHTML = `
                    <div class="reviewimage">
                        <img src="${imageURL}" alt="Review Image">
                    </div>
                    <section class="reviewbody">
                        <h1 class="title">${title}</h1>
                        <button class="delete">
                        <a href=""onclick=" deleteReview(${reviewId})">Delete</a>
                        </button>
                        <div class="starscontainer">${starImages}</div>
                        <div class="textsection">${text}</div>
                    </section>
                `;
                    document.getElementById('Review').innerHTML = reviewHTML;

                }
                else{
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
                }
                
            })
            .catch(error => console.error('Fetch Error:', error));
    })
    .catch(error => console.error('Fetch Error:', error));

    function postComment() {
        const userTitle = document.querySelector(".usertitle").value;
        const userComment = document.querySelector(".usercomment").value;
    
        const commentData = {
            userId: userId,
            reviewId: reviewId,
            title: userTitle,
            comment: userComment
        };
    
        fetch('https://localhost:7033/api/Comment', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(commentData),
        })
        .then(response => {
            if (response.ok) {
                console.log('Comment posted successfully');
                getcomments();
            } else {
                console.error('Failed to post comment');
            }
        })
        .catch(error => {
            console.error('Error posting comment:', error);
        });
    }

function AddComment() {
    if(userId == null)
    {let content = 
        `<div class="headNotLoggedInn"><h1>You must be logged in To comment</h1></div>
        <div><span id="comment">0</span> Comments</div>
        <div class="comments" id="comments"></div>
        </div>`
        document.getElementById('container').innerHTML = content;
    }else
    {
        let content = 
        ` <div class="head"><h1>Post a Comment</h1></div>
        <div><span id="comment"></span> Comments</div>
        <div class="text"><p>We are happy to hear from you</p></div>
        <div class="comments" id="comments"></div>
        <div class="commentbox">
            <img src="asset/user.png" id="userImage" alt="">
            <div class="content">
                <h2>Comment as: </h2>
                <input type="text" value="Anonymous" class="user">
                <div class="commentinput">
                    <input type="text" placeholder="Enter Title" class="usertitle">
                    <input type="text" placeholder="Enter comment" class="usercomment">
                    <div class="buttons">
                        <button type="submit" disabled id="publish">Publish</button>
                    </div>
                </div>
            </div>
        </div>
        `
        document.getElementById('container').innerHTML = content;
        const publishBtn = document.querySelector("#publish");
        const userComment = document.querySelector(".usercomment");
         userComment.addEventListener("input", e => {
        if(!userComment.value) {
            publishBtn.setAttribute("disabled", "disabled");
            publishBtn.classList.remove("abled")
        }else {
            publishBtn.removeAttribute("disabled");
            publishBtn.classList.add("abled")
            publishBtn.addEventListener("click", postComment);
        }
    });

    }


}

    function getcomments() {
        let published = "";
        let commentLengt = 0;
        fetch(`https://localhost:7033/api/Comment/ReviewId=${reviewId}`)
        .then(response => response.json())
        .then(data => {
            data.forEach(resp => {
                const { userId, title, comment, created } = resp;
                commentLengt ++;
                fetch(`https://localhost:7033/api/User/public/${resp.userId}`)
                .then(response => response.json())
                .then(userData => {
                    const { username } = userData;
                    const commentdate = new Date(resp.created);
                    const formated = commentdate.toLocaleDateString();
                    published += 
                        `<div class="parents">
                            <img src="asset/user.png">
                            <div>
                                <h1>${username}</h1>
                                <p>${resp.title}</p>
                                <p>${resp.comment}</p>
                                <div class="engagements"><img src="asset/like.png" id="like"><img src="asset/share.png" alt=""></div>
                                <span class="date">${formated}</span>
                            </div>    
                        </div>`;
                    document.getElementById('comments').innerHTML = published; 
                    document.getElementById('comment').innerHTML = commentLengt
                })
                .catch(error => console.error('Error fetching user data:', error));
            });
        })
        .catch(error => console.error('Error fetching comments:', error));
    } 


    function deleteReview(reviewid)
    {
        fetch(`https://localhost:7033/api/Review/Id=${reviewid}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        alert('Review deleted successfully');
        window.location.href = '../MoviePage/movie.html'
    })
    .catch(error => {
        // Handle error
        console.error('There was a problem deleting the review:', error);
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
        AddComment();
        getcomments();
        updateAuthenticationUI()
    }
    
