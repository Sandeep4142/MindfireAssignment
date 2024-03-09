$(document).ready(function () {
    var defaultDate = new Date();
    var formattedDefaultDate = defaultDate.toISOString().split('T')[0]; // Format: YYYY-MM-DD
    $('#datepicker').val(formattedDefaultDate);
    fetchAvailableTimeSlots(formattedDefaultDate);

    $('#datepicker').change(function () {
        var selectedDate = $(this).val();
        fetchAvailableTimeSlots(selectedDate);
    });

    var selectedSlot = null;

    function fetchAvailableTimeSlots(selectedDate) {
        var doctorId = $("#doctorID").val();
        $.ajax({
            url: '/Appointment/GetAvailableTimeSlots',
            type: 'POST',
            data: { selectedDate: selectedDate, doctorId: doctorId },
            success: function (data) {
                var availableSlots = data.availableTimeSlots;
                var doctorSlot = data.doctorSlotTime;
                var timeSlotsHtml = '';
                for (var i = 0; i < availableSlots.length; i++) {
                    var startTime = new Date(); // Current date/time
                    startTime.setHours(availableSlots[i].Hours);
                    startTime.setMinutes(availableSlots[i].Minutes);

                    var endTime = new Date(startTime.getTime() + doctorSlot.TotalMilliseconds);

                    var startHours = startTime.getHours();
                    var startMinutes = startTime.getMinutes();
                    var endHours = endTime.getHours();
                    var endMinutes = endTime.getMinutes();

                    var startTimeString = (startHours < 10 ? '0' : '') + startHours + ':' + (startMinutes < 10 ? '0' : '') + startMinutes;
                    var endTimeString = (endHours < 10 ? '0' : '') + endHours + ':' + (endMinutes < 10 ? '0' : '') + endMinutes;
                    var timeSlotString = startTimeString + '-' + endTimeString;
                    timeSlotsHtml += '<div class="time-slot" data-start="' + startTimeString + '" data-end="' + endTimeString + '">' + timeSlotString + '</div>';
                }

                $('#availableSlots').html(timeSlotsHtml);

                $('.time-slot').click(function () {
                    var $this = $(this);
                    if (selectedSlot) {
                        selectedSlot.removeClass('selected');
                    }
                    $this.addClass('selected');
                    selectedSlot = $this;
                });
            }
        });
    }

    $('form').submit(function (event) {
        event.preventDefault();
        if (selectedSlot == null) {
            alert("Select appointment slot");
            return;
        }
        var startTime = selectedSlot.data('start');     
        var selectedDate = $('#datepicker').val();
        var startTime = selectedSlot.data('start');
        var endTime = selectedSlot.data('end');
        var patientName = $('#PatientName').val();
        var patientEmail = $('#PatientEmail').val();
        var patientPhone = $('#PatientPhone').val();
        var doctorId = $('#doctorID').val();

        var appointmentData = {
            appointmentDate: selectedDate,
            appointmentTime: startTime,
            endTime: endTime,
            patientName: patientName,
            patientEmail: patientEmail,
            patientPhone: patientPhone,
            doctorId: doctorId
        };

        $.ajax({
            url: '/Appointment/BookAppointment',
            type: 'POST',
            data: { appointmentData: appointmentData },
            success: function (response) {
                if (response == true) {
                    alert("Appointment Booked");
                    window.location.href = "/Home/Index";
                } else {
                    alert("Appointment Booking Failed")
                }
            },
            error: function (xhr, status, error) {
                console.error('Error booking appointment:', error);
                 alert('An error occurred while booking the appointment. Please try again later.');
            }
        });
    });



});
