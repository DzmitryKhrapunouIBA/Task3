async function postTestDataAsync() {

    const formData = new FormData();
   
    formData.append("id", document.getElementById("TestId").value);
    

    const token = localStorage.getItem(tokenKey);

    const response1 = await fetch("Delete", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        },
        body: formData
    });

    const data = await response1.json();

    if (response1.ok === true) {
        goToNewUrl();
    }
    else {

        console.log("Error: ", response1.status, data.errorText);
        alert(data.errorText);
    }
};

document.getElementById("submitDelete").addEventListener("click", e => {

    e.preventDefault();
    postTestDataAsync();
});