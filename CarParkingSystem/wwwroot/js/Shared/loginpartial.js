const hamburgerElements = document.getElementsByClassName('hamburger-element');
const dropdown = document.getElementById('dropdown-li');
if ($(window).width() < 577) {
    dropdown.style.display = "none";
    for (let i = 0; i < hamburgerElements.length; i++) {
        hamburgerElements[i].style.display = "block";
    }
}
else {
    dropdown.style.display = "block";
    for (let i = 0; i < hamburgerElements.length; i++) {
        hamburgerElements[i].style.display = "none";
    }
}

$(window).resize(function () {
    if ($(window).width() < 577) {
        dropdown.style.display = "none";
        for (let i = 0; i < hamburgerElements.length; i++) {
            hamburgerElements[i].style.display = "block";
        }
    }
    else {
        dropdown.style.display = "block";
        for (let i = 0; i < hamburgerElements.length; i++) {
            hamburgerElements[i].style.display = "none";
        }
    }
});