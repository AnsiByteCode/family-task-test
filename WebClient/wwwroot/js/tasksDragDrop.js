function SetCheckBoxSelected(elementId) {
    debugger;
    var element = document.getElementsByName(elementId);
    element[0].checked = true;
} 
function RemoveClass(elementId, className) {

    var element = document.getElementById(elementId);
    element.classList.remove(className);
}
function AddClass(elementId, className) {

    var element = document.getElementById(elementId);
    element.classList.add(className);
}
function HandleDragDropAndLeave() {
    var menuItems = document.querySelectorAll(".menu-item");

    menuItems.forEach(c => {
        c.classList.remove("disableDrop");
        c.classList.remove("enableDrop");
    });

} 