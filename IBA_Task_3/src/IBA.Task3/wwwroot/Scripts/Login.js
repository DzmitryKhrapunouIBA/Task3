var tokenKey = "accessToken";
var loginKey = "userName";

async function getTokenAsync() {

    const formData = new FormData();
    formData.append("grant_type", "password");
    formData.append("login", document.getElementById("login").value);
    formData.append("password", document.getElementById("password").value);

    const response = await fetch("token", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: formData
    });

    const data = await response.json();

    if (response.ok === true) {

        localStorage.removeItem(tokenKey);
        localStorage.removeItem(loginKey);

        localStorage.setItem(tokenKey, data.access_token);
        localStorage.setItem(loginKey, data.username);
        console.log(data.access_token);
        getHomePage();
    }
    else {
        console.log("Error: ", response.status, data.errorText);
        alert(data.messages[0]);
    }
};

async function getHomePage() {

    const token = localStorage.getItem(tokenKey);

    //$.ajax({
    //    type: 'GET',
    //    url: 'Home',
    //    beforeSend: function (xhr) {

    //        xhr.setRequestHeader("Authorization", "Bearer " + token);
    //    },
    //    success: function (data) {
    //        alert(data);
    //    },
    //    fail: function (data) {
    //        alert(data);
    //    }



    /*location.href = 'Home';*/
    location.href = await fetch("Home", {
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });

    
    /*location = response;*/

    

//    //alert(dat);

//    //if (response.ok === true) {

//    //    location.href = dat;
//    //}
//    //else {
//    //    console.log("Error: ", response.status, data.errorText);
//    //    alert(data.errorText);
//    //}


//    /* alert(href.url);*/

};

document.getElementById("submitLogin").addEventListener("click", e => {

    e.preventDefault();
    getTokenAsync();
});