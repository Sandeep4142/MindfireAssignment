$(document).ready(function () {
    var doctorID = $('#doctorID').val();
    var defaultDate = new Date();
    var formattedDefaultDate = defaultDate.toISOString().split('T')[0]; // Format: YYYY-MM-DD
    $('#datepicker').val(formattedDefaultDate);
    GetUpcomingAppointments(doctorID, formattedDefaultDate);

    $('#datepicker').change(function () {
        var selectedDate = $(this).val();
        GetUpcomingAppointments(doctorID, selectedDate);
    });
})

function GetUpcomingAppointments(doctorID, selectedDate) {
    $.ajax({
        url: '/Doctor/GetUpcomingAppointments',
        type: 'POST',
        data: { doctorID: doctorID, selectedDate: selectedDate },
        success: function (data) {
            var appointmentList = $('#appointmentList');
            appointmentList.empty();
            if (data == null || data.length === 0) {
                appointmentList.append('<tr><td colspan="4"><h4 class="text-center">No appointments</h4></td></tr>');
                return;
            }

            $.each(data, function (index, appointment) {

                var row = $('<tr>');
                var appointmentTime = formatTimeSpan(appointment.AppointmentTime);

                row.append('<td>' + appointmentTime + '</td>');
                row.append('<td>' + appointment.PatientName + '</td>');
                row.append(`<td id="status_${appointment.AppointmentID}">${appointment.AppointmentStatus}</td>`);

                if (appointment.AppointmentStatus != "Closed" && appointment.AppointmentStatus != "Cancelled") {
                    row.append(`<td>
                    <button type="button" class="btn btn-success"
                    onclick="closeAppointment(${appointment.AppointmentID})">Close</button>
                    <button type="button" class="btn btn-danger"
                    onclick="cancelAppointment(${appointment.AppointmentID})">Cancel</button>
                    </td>`);
                } else {
                    row.append('<td></td>');
                }
                appointmentList.append(row);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
        }
    });
}

function formatTimeSpan(timeSpan) {
    var hours = timeSpan.Hours.toString().padStart(2, '0');
    var minutes = timeSpan.Minutes.toString().padStart(2, '0');
    return hours + ':' + minutes;
}

function closeAppointment(appointmentID) {
    $.ajax({
        url: '/Doctor/CloseAppointment',
        type: 'POST',
        data: { appointmentID: appointmentID },
        success: function (response) {
            if (response) {
                $('#status_' + appointmentID).text('Closed');
            } else {
                alert('Error occurred while canceling the appointment.');
            }
        },
        error: function () {
            alert('Error occurred while closing the appointment.');
        }
    });
}

function cancelAppointment(appointmentID) {
    $.ajax({
        url: '/Doctor/CancelAppointment',
        type: 'POST',
        data: { appointmentID: appointmentID },
        success: function (response) {
            if (response) {
                $('#status_' + appointmentID).text('Cancelled');
            } else {
                alert('Error occurred while canceling the appointment.');
            }
        },
        error: function (error) {
            console.log(error)
            alert('Error occurred while canceling the appointment.');
        }
    });
}

