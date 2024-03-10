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
        url: '/Doctor/GetAppointmentSummaryReport',
        type: 'POST',
        data: { doctorID: doctorID, month: selectedMonth },
        success: function (response) {
            var appointmentList = $('#appointmentList');
            appointmentList.empty();
            if (response == null || response.length === 0) {
                appointmentList.append('<tr><td colspan="4"><h4 class="text-center">No appointments</h4></td></tr>');
                return;
            }

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
    var table = document.getElementById("appointmentDetailedReport");
    doc.autoTable({ html: table });
    doc.save("appointment_summary_report.pdf");
}







