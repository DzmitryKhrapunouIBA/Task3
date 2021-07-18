
async function postTestDataAsync() {

    const token = localStorage.getItem(tokenKey);

    const response1 = await fetch("MyTests", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token
        }
    });

    const data = await response1.json();

};
document.getElementById("submitEdit").addEventListener("click", e => {

    e.preventDefault();
    postTestDataAsync();
});
