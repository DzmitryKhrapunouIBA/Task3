var testNameKey = "testName";
var numberOfQuestionsKey = "numberOfQuestions";

/*--------------------------------------*/

async function postTestDataAsync() {

    const formData = new FormData();
    formData.append("testName", document.getElementById("testName").value);
    formData.append("numberOfQuestions", document.getElementById("numberOfQuestions").value);
    formData.append("attempts", document.getElementById("attempts").value);
    formData.append("login", localStorage.getItem(loginKey));

    const token = localStorage.getItem(tokenKey);

    const response1 = await fetch("Create", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        },
        body: formData
    });

    const data2 = await response1.json();

    if (response1.ok === true) {

        localStorage.setItem(testNameKey, document.getElementById("name").value);
        localStorage.setItem(numberOfQuestionsKey, document.getElementById("numberOfQuestions").value);
        goToNewUrl();
    }
    else {

        console.log("Error: ", response1.status, data2.errorText);
        alert(data2.errorText);
    }
};

async function goToNewUrl() {
    const token = localStorage.getItem(tokenKey);

    const response2 = await fetch("/getTestEditorLink", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });

    const url = await response2.json();
    location.href = url;
};

document.getElementById("submit").addEventListener("click", e => {

    e.preventDefault();
    postTestDataAsync();
});