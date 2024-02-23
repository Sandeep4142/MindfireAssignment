using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pageHeader.InnerText = Page.Title;
            if (((SessionData)HttpContext.Current.Session["User"]) != null && ((SessionData)HttpContext.Current.Session["User"]).IsAdmin)
            {
                navUserList.Visible = true; 
            }
            else
            {
                navUserList.Visible = false; 
            }
        }

    }
}