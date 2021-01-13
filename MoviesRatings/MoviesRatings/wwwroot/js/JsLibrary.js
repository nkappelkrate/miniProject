//Author: Hieu Nguyen

function showModal(dialog) {
    console.log("showModal");
    if (!dialog.open) {
        dialog.showModal();
    }
}
function initializeModal(dialog, reference) {
    dialog.addEventListener("close", async e => {
        //calling C# method
        await reference.invokeMethodAsync("OnClose", dialog.returnValue);
    });
}

function closeModal(dialog) {
    console.log("close Modal");
    if (dialog.open) {
        dialog.close();
    }
}

function displaySuccessAddMsg() {
    var msg = "You have successfully added new Actor/Actress";
    window.alert(msg);
}
function displaySuccessEditMsg() {
    var msg = "You have successfully updated Actor/Actress";
    window.alert(msg);
}

