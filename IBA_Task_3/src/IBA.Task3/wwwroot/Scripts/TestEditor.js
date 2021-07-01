document.getElementById("editForm").style.display = "block";
document.getElementById("finishEditForm").style.display = "none";

//document.getElementById("editForm").style.display = "none";
//document.getElementById("finishEditForm").style.display = "block";

var testNameKey = "testName";
var numberOfQuestionsKey = "numberOfQuestions";

var testName = localStorage.getItem(testNameKey);

document.getElementById("testName").innerText = testName;

/*--------------------------------------*/

async function postTestDataAsync() {

    const formData = new FormData();
    formData.append("name", localStorage.getItem(testNameKey));
    formData.append("question", document.getElementById("question").value);
    formData.append("answer1", document.getElementById("answer1").value);
    formData.append("answer2", document.getElementById("answer2").value);
    formData.append("answer3", document.getElementById("answer3").value);
    formData.append("answer4", document.getElementById("answer4").value);
    formData.append("correctAnswer", document.getElementById("correctAnswer").value);

    const token = localStorage.getItem(tokenKey);

    const response1 = await fetch("Edit", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        },
        body: formData
    });

    const data = await response1.json();

    if (response1.ok === true) {
        if (data.Num > 0) {

            localStorage.setItem(numberOfQuestionsKey, data.Num)
            goToNewUrl();
        }
        else {

            document.getElementById("editForm").style.display = "none";
            document.getElementById("finishEditForm").style.display = "block";
        }
    }
    else {

        console.log("Error: ", response1.status, data.errorText);
        alert(data.errorText);
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

document.getElementById("submitEdit").addEventListener("click", e => {

    e.preventDefault();
    postTestDataAsync();
});