$('#Email').on('blur', CheckEmailExist);
function CheckEmailExist() {
    var email = $('#Email').val();
    console.log(email);
    $.ajax({
        type: "POST",
        url: "/Home/CheckEmailExist",
        data: JSON.stringify({ email: email }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response) {
                $("#Email").val("");
                alert("User with same email already exists");
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", xhr, status, error);
            alert("An error occurred while checking the email. Please try again later.");
        }
    });
}