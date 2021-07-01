
async function registrationAsync() {

    const formData = new FormData();
    formData.append("grant_type", "password");
    formData.append("lastname", document.getElementById("lastname").value);
    formData.append("firstname", document.getElementById("firstname").value);
    formData.append("surname", document.getElementById("surname").value);
    formData.append("login", document.getElementById("login").value);
    formData.append("password", document.getElementById("password").value);
    formData.append("passwordConfirm", document.getElementById("passwordConfirm").value);

    const response = await fetch("registration", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: formData
    });

    const result = await response.json();

    if (response.ok) {
        if (result.success) {

            document.getElementById("return").style.display = "block";
            document.getElementById("loginForm").style.display = "none";
        }
    }
    else {

        console.log("Error: ", response.status, result);
        alert(result.messages[0]);
    }
};

document.getElementById("submitRegist").addEventListener("click", e => {

    e.preventDefault();
    registrationAsync();
});

document.getElementById("regOut").addEventListener("click", e => {

    e.preventDefault();
    location.href = "/";
});