using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoctorAppointment
{
    public class CustomAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = filterContext.ActionDescriptor.ActionName;
            var doctorData = (SessionData)filterContext.HttpContext.Session["Doctor"];

            if (controllerName == "doctor")
            {
                // Check if doctorID is present in the route data and parse it
                int? doctorID = null;
                if (filterContext.RouteData.Values["doctorID"] != null)
                {
                    if (!int.TryParse(filterContext.RouteData.Values["doctorID"].ToString(), out int tempDoctorID))
                    {
                        // Redirect to an appropriate action if doctorID is not valid
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Home", action = "Index" }));
                        return;
                    }
                    doctorID = tempDoctorID;
                }

                // Check doctor authentication
                if (doctorData == null)
                {
                    // Redirect to login if doctor is not authenticated
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "Login" }));
                    return;
                }
                else if (doctorID == null)
                {
                    // Redirect to corresponding controller/action with doctorData.DoctorID
                    string returnUrl = $"~/Doctor/{actionName}/{doctorData.DoctorID}";
                    filterContext.Result = new RedirectResult(returnUrl);
                    return;
                }
                else if (doctorData.DoctorID != doctorID)
                {
                    // Redirect to appropriate action with logged in doctorID
                    string returnUrl = $"~/Doctor/{actionName}/{doctorData.DoctorID}";
                    filterContext.Result = new RedirectResult(returnUrl);
                    return;
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    { "controller", "Home" },
                    { "action", "Index" }
                    });
            }
        }
    }
}