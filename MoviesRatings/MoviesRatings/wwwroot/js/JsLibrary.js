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
    if (dialog.open) {
        dialog.close();
    }
}