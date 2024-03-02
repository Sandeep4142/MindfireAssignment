using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace DemoUserManagementMVC
{
    public class CustomAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var userData = (SessionData)filterContext.HttpContext.Session["User"];


            if (controllerName == "login")
            {
                if (userData != null)
                {
                    if (userData.IsAdmin)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "UserList", action = "Index" }));
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "UserDetails", action = "Index", id = userData.UserId }));
                    }
                }
            }

            else if (controllerName == "userdetails" || controllerName == "userdetails2")
            {
                var id = filterContext.RouteData.Values["id"];
                if (userData == null)
                {
                    if (id != null)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Login", action = "Index" }));
                        return;
                    }
                }

                else if (!userData.IsAdmin && userData.UserId != int.Parse(id.ToString()))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "UserDetails", action = "Index", id = userData.UserId }));
                }
            }
            else if (controllerName == "userlist2" || controllerName == "userlist" || controllerName == "userlist3")
            {
                if (userData == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                    return;
                }

                if (!userData.IsAdmin)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "UserDetails", action = "Index", id = userData.UserId }));
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
                    { "controller", "Login" },
                    { "action", "Index" }
                    });
            }
        }
    }

}