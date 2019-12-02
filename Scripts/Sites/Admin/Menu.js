function openNav() {
    closeAll();
    document.getElementById("mySidebar").style.width = "250px";
    openBtn();
    document.getElementById("menu-btn").style.backgroundColor = "#b83333"
    $("#menu-btn-icon").removeClass('fa-bars');
    $("#menu-btn-icon").addClass('fa-times');
    $("#menu-btn").attr("onclick", "closeNav()")
}

/* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
function closeNav() {
    closeAll();
    
} 
function openNotifications() {
    closeAll();
    document.getElementById("mySidebar1").style.width = "250px";
    openBtn();
    document.getElementById("notification-btn").style.backgroundColor = "#b83333"
    $("#notification-btn-icon").removeClass('fa-bell');
    $("#notification-btn-icon").addClass('fa-times');
    $("#notification-btn").attr("onclick", "closeNotifications()")
}
function closeNotifications() {
    closeAll();  
}
function openMessageBox() {
    closeAll();
    document.getElementById("mySidebar2").style.width = "250px";
    openBtn();
    document.getElementById("message-btn").style.backgroundColor = "#b83333"
    $("#message-btn-icon").removeClass('fa-comments');
    $("#message-btn-icon").addClass('fa-times');
    $("#message-btn").attr("onclick", "closeMessageBox()")
}
function closeMessageBox() {
    closeAll();
}

function closeAll() {
    document.getElementById("mySidebar").style.width = "0";
    document.getElementById("mySidebar1").style.width = "0";
    document.getElementById("mySidebar2").style.width = "0";
    document.getElementById("menu-btn").style.left = "0";
    document.getElementById("notification-btn").style.left = "0";
    document.getElementById("message-btn").style.left = "0";
    document.getElementById("logout-btn").style.left = "0";

    document.getElementById("notification-btn").style.backgroundColor = "#007bff"
    $("#notification-btn-icon").removeClass('fa-times');
    $("#notification-btn-icon").addClass('fa-bell');
    $("#notification-btn").attr("onclick", "openNotifications()")

    document.getElementById("menu-btn").style.backgroundColor = "#007bff"
    $("#menu-btn-icon").removeClass('fa-times');
    $("#menu-btn-icon").addClass('fa-bars');
    $("#menu-btn").attr("onclick", "openNav()")

    document.getElementById("message-btn").style.backgroundColor = "#007bff"
    $("#message-btn-icon").removeClass('fa-times');
    $("#message-btn-icon").addClass('fa-comments');
    $("#message-btn").attr("onclick", "openMessageBox()")
}
function openBtn() {
    document.getElementById("menu-btn").style.left = "250px";
    document.getElementById("notification-btn").style.left = "250px";
    document.getElementById("message-btn").style.left = "250px";
    document.getElementById("logout-btn").style.left = "250px";
}