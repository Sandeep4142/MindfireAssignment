$(document).ready(function () {
    $(".bookAppointmentBtn").click(function () {
        var doctorID = $(this).data('doctor-id');
        window.location.href = "/Appointment/BookAppointment/" + doctorID;
    });
});