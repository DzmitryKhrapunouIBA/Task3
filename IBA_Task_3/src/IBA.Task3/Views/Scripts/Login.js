    var tokenKey = "accessToken";
    var loginKey = "userName";

    async function getTokenAsync() {

        const formData = new FormData();
        formData.append("grant_type", "password");
        formData.append("login", document.getElementById("login").value);
        formData.append("password", document.getElementById("password").value);

        const response = await fetch("/token", {
            method: "POST",
            headers: {"Accept": "application/json" },
            body: formData
        });

        const data = await response.json();

        if (response.ok === true)
        {

            localStorage.setItem(tokenKey, data.access_token);
            localStorage.setItem(loginKey, data.username);
            console.log(data.access_token);
            location.href = "/HomePage.html";
        }
        else
        {
            console.log("Error: ", response.status, data.errorText);
            alert(data.errorText);
        }
    };

    async function getData(url) {
        const token = localStorage.getItem(tokenKey);

        const response = await fetch(url, {
            method: "GET",
            headers: {"Accept": "application/json",
                "Authorization": "Bearer " + token
            }
        });
        if (response.ok === true)
        {

            const data = await response.json();
            alert(data)
        }
        else
            console.log("Status: ", response.status);
    };

    document.getElementById("submitLogin").addEventListener("click", e => {

        e.preventDefault();
        getTokenAsync();
    });

    document.getElementById("fromName").addEventListener("submit", e => {

    // оставновка default action для FORM
        e.preventDefault();
        e.stopPropagation();
    });