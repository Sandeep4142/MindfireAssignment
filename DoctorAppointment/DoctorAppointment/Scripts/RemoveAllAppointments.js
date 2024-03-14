$(document).ready(function () {
    var doctorID = $('#doctorID').val();
    var removeAppointmentBtn = $('#removeAllAppointmentButton');
    removeAppointmentBtn.click(function () {
        RemoveAllAppointments(doctorID);
    });
});

function RemoveAllAppointments(doctorID) {
    $.ajax({
        url: '/Doctor/RemoveAllAppointments',
        type: 'POST',
        data: { doctorID: doctorID },
        success: function (response) {
            if (response == true) {
                alert("All appointments removed ");
                location.reload(); 
            } else {
                alert("Failed to remove appointments ")
            }           
        },
        error: function (xhr, status, error) {
            alert("Failed to remove appointments ")
        }
    });
}
