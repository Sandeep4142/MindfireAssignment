using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoctorAppointment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AppointmentDetailedReport",
                url: "Doctor/AppointmentDetailedReport/{doctorID}",
                defaults: new { controller = "Doctor", action = "AppointmentDetailedReport", doctorID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AppointmentSummaryReport",
                url: "Doctor/AppointmentSummaryReport/{doctorID}",
                defaults: new { controller = "Doctor", action = "AppointmentSummaryReport", doctorID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UpcomingAppointments",
                url: "Doctor/UpcomingAppointments/{doctorID}",
                defaults: new { controller = "Doctor", action = "UpcomingAppointments", doctorID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
