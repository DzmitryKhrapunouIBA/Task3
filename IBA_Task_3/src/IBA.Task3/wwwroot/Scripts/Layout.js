var tokenKey = "accessToken";
var loginKey = "userName";

var data = localStorage.getItem(loginKey);

document.getElementById("userName").innerText = data;

document.getElementById("RegOut").addEventListener("click", e => {

    e.preventDefault();
    localStorage.removeItem(tokenKey);
    localStorage.removeItem(loginKey);
    location.href = "/";
});