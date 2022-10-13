$(function () {
    debugger
    //document.cookie = "Token" + "=" + $("#Token").val();
    if ($("#Token").val() != "")
        setCookie("Token", $("#Token").val(), 1)
})