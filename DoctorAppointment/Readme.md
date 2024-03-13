# Project Name - BOOK AN APPOINTMENT

- In this web application patient can book appointment with doctor based on available time slots .
- Doctor can register in this application . They can see the upcoming appointments, summary report and detailed report and download its pdf.

## Doctor Interface -
 ### Login/SignUp
- Doctor can register in this application. 
- After login they can see upcoming appointments, summary and detailed report .
- They can dowmload the report ( filter by month )

#### Upcoming Appointments : 
( filter by day - todays day as default )
Doctor have option to close (seen patient) or cancel (patient no-show)  appointment, i.e. status will change to 'Closed' or 'Cancelled'

#### Appointment Summary Report : 
filter by month( current month by default )
```
Date         # of Appointments   # of Appointment Closed   # of Appointment Cancelled
2020-02-20   5                   4                         1
2020-02-21   5                   5                         0
2020-02-22   5                   2                         0
```
#### Appointment Detailed Report :
filter by month( current month by default )
```
Date         Patient Name   Status
2020-02-20   Patient 01     Closed
             Patient 02     Closed
             Patient 05     Canceled
2020-02-21   Patient 06     Closed
             Patient 07     Closed
```

## Patient interface -
- List of all doctor will be visible with doctor name, appointment time and bookAppointment link.
- When bookAppointment is clicked it will redirect to BookAppointment page.
- BookAppointment page contains appointment date with available slots.
- Patient can select date and available slots, and then enter their details to book the appointment with doctor



