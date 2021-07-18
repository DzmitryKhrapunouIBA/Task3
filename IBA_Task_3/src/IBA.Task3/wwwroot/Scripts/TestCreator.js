var testNameKey = "testName";

/*--------------------------------------*/

async function postTestDataAsync() {

    const formData = new FormData();
    formData.append("Name", document.getElementById("testName").value);
    formData.append("attempts", document.getElementById("attempts").value);

    const token = localStorage.getItem(tokenKey);

    const response = await fetch("Create", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        },
        body: formData
    });

    const data = await response.json();

    if (response.ok === true) {

        localStorage.setItem(testNameKey, document.getElementById("testName").value);

        response = await fetch(NewQuestionWithAnswers, {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token
            }
        });

        location.href = await response;
    }
    else {

        console.log("Error: ", response.status, data.errorText);
        alert(data.messages[0]);
    }
};

async function goToNewUrl(url) {
    const token = localStorage.getItem(tokenKey);

    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });

    location.href = await response;
};

document.getElementById("submit").addEventListener("click", e => {

    e.preventDefault();
    postTestDataAsync();
});

document.getElementById("submitFinish").addEventListener("click", e => {

    e.preventDefault();
    localStorage.setItem(testNameKey, "")
    goToNewUrl("Home");
    
});