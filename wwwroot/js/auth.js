const signUpBtn = document.getElementById("signUp");
const signInBtn = document.getElementById("signIn");
const container = document.getElementById("container");

document.getElementById("signUp").addEventListener("click", function () {
    container.classList.add("active");
});

document.getElementById("signIn").addEventListener("click", function () {
    container.classList.remove("active");
});