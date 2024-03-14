$(document).ready(function () {
    $('#loginBtn').click(function (event) {
        // Manually trigger form validation
        var form = $('form')[0];
        if (form.checkValidity() === false) {
            return;
        }
        // Prevent default form submission
        event.preventDefault();

        var email = $('#loginEmail').val();
        var password = $('#loginPassword').val();

        $.ajax({
            type: "POST",
            url: "/Home/Login",
            data: JSON.stringify({ email: email, password: password }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (doctorID) {
                if (doctorID == 0) {
                    alert('Wrong password.');
                }
                else if (doctorID == -1) {
                    alert('User does not exist.');
                } else {
                    window.location.href = "/Doctor/UpcomingAppointments/" + doctorID;
                }
            },
            error: function (xhr, status, error) {
                alert('Something went wrong.');
            }
        });
    });
});
