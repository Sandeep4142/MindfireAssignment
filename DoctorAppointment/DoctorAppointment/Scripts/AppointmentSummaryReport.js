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
                var timestamp = parseInt(appointment.Date.match(/\d+/)[0]);
                var appointmentDate = new Date(timestamp);

                // Extract day, month, and year components
                var day = appointmentDate.getDate().toString().padStart(2, '0');
                var month = (appointmentDate.getMonth() + 1).toString().padStart(2, '0'); 
                var year = appointmentDate.getFullYear();
                appointmentDate = day + '-' + month + '-' + year;

                //append to row
                var row = $('<tr>');      
                row.append('<td>' + appointmentDate + '</td>');
                row.append('<td>' + appointment.TotalAppointments + '</td>');
                row.append('<td>' + appointment.TotalClosedAppointments + '</td>');
                row.append('<td>' + appointment.TotalCancelledAppointments + '</td>');
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

//function exportToPDF() {
//    var doc = new jsPDF();
//    var table = document.getElementById("appointmentDetailedReport");
//    doc.autoTable({ html: table });
//    doc.save("appointment_summary_report.pdf");
//}

function exportToPDF() {
    var doc = new jsPDF();
    var table = document.getElementById("appointmentSummaryReport");

    // Add doctor name and date as heading
    var doctorName = document.getElementById("doctorName").innerHTML;
    var selectedMonth = document.getElementById("appointmentMonth").value;

    var heading = "<div style='font-size: 18px; font-weight: bold;'>Appointment Detailed Report</div><br>" +
        "<div>Doctor : " + doctorName + "</div>" +
        "<div>Month  : " + selectedMonth + "</div><br>";
    doc.fromHTML(heading, 15, 10);

    // Add table to PDF
    doc.autoTable({
        html: table,
        startY: 30
    });
    doc.save("appointment_Summary_report.pdf");
}








