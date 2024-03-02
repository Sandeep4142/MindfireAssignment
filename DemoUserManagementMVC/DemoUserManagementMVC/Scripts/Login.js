$(document).ready(function () {
    function CheckUser() {
        console.log("login clicked");
        var email = $('#loginEmail').val();
        var password = $('#loginPassword').val();

        $.ajax({
            type: "POST",
            url: "/Login2/CheckUser",
            data: JSON.stringify({ email: email, password: password }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (userId) {
                if (userId == 0) {
                    alert('Wrong password.');
                }
                else if (userId == -1) {
                    alert('User does not exist.');
                } 
            },
            error: function (xhr, status, error) {
                console.error("An error occurred while checking user:", error);
                alert('Something went wrong.');
            }
        });
    }
   
});
