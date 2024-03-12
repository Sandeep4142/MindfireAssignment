Project Name - BOOK AN APPOINTMENT

1. Doctor Interface -
-Login/SignUp
-Upcoming Appointments :
doctor have option to close (seen patient) or cancel (patient no-show)  appointment, i.e. status will change to 'Closed' or 'Cancelled'
Patient details with option to filter by date

-Appointment Summary Report : 
filter by month, export to pdf feature
```
Date         # of Appointments   # of Appointment Closed   # of Appointment Cancelled
2020-02-20   5                   4                         1
2020-02-21   5                   5                         0
2020-02-22   5                   2                         0
...

-Appointment Detailed Report :
filter by month, export to pdf feature
```
Date         Patient Name   Status
2020-02-20   Patient 01     Closed
             Patient 02     Closed
             Patient 05     Canceled
2020-02-21   Patient 06     Closed
             Patient 07     Closed
```

2. Patient interface -
List of all doctor will be visible with doctor name, appointment time and bookAppointment link.
When bookAppointment is clicked it will redirect to BookAppointment page.
BookAppointment page contains appointment date with available slots.
Patient can select date and available slots, and then enter their details to book the appointment with doctor
