$(document).ready(function () {
    var doctorID = $('#doctorID').val();
    var selectedMonth = $('#appointmentMonth').val();
    generateReport(doctorID, selectedMonth);
    $('#appointmentMonth').change(function () {      
        selectedMonth = $('#appointmentMonth').val();
        generateReport(doctorID, selectedMonth);
    });
    $("#exportPDFButton").click(exportToPDF);

});

function generateReport(doctorID, selectedMonth) {
    $.ajax({
        url: '/Doctor/GetAppointmentDetailedReport',
        type: 'POST',
        data: { doctorID: doctorID, month: selectedMonth},
        success: function (response) {
            var appointmentList = $('#appointmentList');
            appointmentList.empty();
            if (response == null || response.length === 0) {
                appointmentList.append('<tr><td colspan="4"><h4 class="text-center">No appointments</h4></td></tr>');
                return;
            }

            var previousDate = null;
            $.each(response, function (index, appointment) {
                var row = $('<tr>');
                var timestamp = parseInt(appointment.AppointmentDate.match(/\d+/)[0]);
                var appointmentDate = new Date(timestamp);

                // Extract day, month, and year components
                var day = appointmentDate.getDate().toString().padStart(2, '0');
                var month = (appointmentDate.getMonth() + 1).toString().padStart(2, '0');
                var year = appointmentDate.getFullYear();
                appointmentDate = day + '-' + month + '-' + year;
                var appointmentTime = formatTimeSpan(appointment.AppointmentTime);

                // Append to row
                if (appointmentDate !== previousDate) {
                    row.append(`<td rowspan="${response.filter(item => item.AppointmentDate === appointment.AppointmentDate).length}">${appointmentDate}</td>`);
                    row.append('<td>' + appointmentTime + '</td>');
                    row.append('<td>' + appointment.PatientName + '</td>');
                    row.append(`<td id="status_${appointment.AppointmentID}">${appointment.AppointmentStatus}</td>`);
                    appointmentList.append(row);
                    previousDate = appointmentDate;
                } else {
                    row.append('<td>' + appointmentTime + '</td>');
                    row.append('<td>' + appointment.PatientName + '</td>');
                    row.append(`<td id="status_${appointment.AppointmentID}">${appointment.AppointmentStatus}</td>`);
                    appointmentList.append(row);
                }
            });
            console.log("response--")
            console.log(response);        
        },
        error: function (error,xhr) {
            console.log("error--")
            console.log(error);
            console.log("xhr" - xhr)
        }
    });
}

function formatTimeSpan(timeSpan) {
    var hours = timeSpan.Hours.toString().padStart(2, '0');
    var minutes = timeSpan.Minutes.toString().padStart(2, '0');
    var seconds = timeSpan.Seconds.toString().padStart(2, '0');
    return hours + ':' + minutes;
}

function exportToPDF() {
    var doc = new jsPDF();
    var table = document.getElementById("appointmentDetailedReport");
    doc.autoTable({ html: table });
    doc.save("appointment_detailed_report.pdf");
}

    


