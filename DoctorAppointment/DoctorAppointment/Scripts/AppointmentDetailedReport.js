$(document).ready(function () {
    var doctorID = $('#doctorID').val();
    var selectedMonth = $('#selectedMonth').val();
    var selectedYear = $('#selectedYear').val();
    generateReport(doctorID, selectedMonth, selectedYear);
    $('#selectedMonth, #selectedYear').change(function () {      
        selectedMonth = $('#selectedMonth').val();
        selectedYear = $('#selectedYear').val();
        generateReport(doctorID, selectedMonth, selectedYear);
    });

    $("#exportPDFButton").click(exportToPDF);

});

function generateReport(doctorID, selectedMonth, selectedYear) {
    $.ajax({
        url: '/Doctor/GetAppointmentDetailedReport',
        type: 'POST',
        data: { doctorID: doctorID, month: selectedMonth, year: selectedYear },
        success: function (response) {
            var appointmentList = $('#appointmentList');
            appointmentList.empty();

            var previousDate = null;
            $.each(response, function (index, appointment) {
                var row = $('<tr>');

                var timestamp = parseInt(appointment.AppointmentDate.match(/\d+/)[0]);
                var appointmentDate = new Date(timestamp);

                // Extract day, month, and year components
                var day = appointmentDate.getDate().toString().padStart(2, '0');
                var month = (appointmentDate.getMonth() + 1).toString().padStart(2, '0'); // Month is zero-indexed
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
    var table = $('#appointmentDetailedReport');
    doc.autoTable({ html: table });
    doc.save("appointment_report.pdf");
}

    


