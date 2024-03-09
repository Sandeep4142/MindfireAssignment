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
        url: '/Doctor/GetAppointmentSummaryReport',
        type: 'POST',
        data: { doctorID: doctorID, month: selectedMonth, year: selectedYear },
        success: function (response) {
            var appointmentList = $('#appointmentList');
            appointmentList.empty();

            $.each(response, function (index, appointment) {
                var timestamp = parseInt(appointment.date.match(/\d+/)[0]);
                var appointmentDate = new Date(timestamp);

                // Extract day, month, and year components
                var day = appointmentDate.getDate().toString().padStart(2, '0');
                var month = (appointmentDate.getMonth() + 1).toString().padStart(2, '0'); 
                var year = appointmentDate.getFullYear();
                appointmentDate = day + '-' + month + '-' + year;

                //append to row
                var row = $('<tr>');      
                row.append('<td>' + appointmentDate + '</td>');
                row.append('<td>' + appointment.totalAppointments + '</td>');
                row.append('<td>' + appointment.totalClosedAppointments + '</td>');
                row.append('<td>' + appointment.totalCancelledAppointments + '</td>');
                appointmentList.append(row);
                
            });

            console.log("response--")
            console.log(response);
        },
        error: function (error, xhr) {
            console.log("error--")
            console.log(error);
            console.log("xhr" - xhr)
        }
    });
}

function exportToPDF() {
    var doc = new jsPDF();
    var table = $("#appointmentDetailedReport");
    doc.autoTable({ html: table });
    doc.save("appointment_report.pdf");
}







