using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using static DemoUserManagement.Utils.ObjectTypes;

namespace DemoUserManagement
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

            if (pageName == "loginpage.aspx")
            {
                if (Session["User"] != null )
                {
                    if (((SessionData)Session["User"]).IsAdmin)
                    {
                        Response.Redirect("UserList.aspx");
                    }
                    else
                    {
                        Response.Redirect("UserDetails2.aspx?id="+ ((SessionData)Session["User"]).UserId);
                    }
                }
            }
            else if (pageName == "userdetails2.aspx")
            {
                if (Session["User"] == null)
                {
                    
                }
                else
                {
                    if (((SessionData)Session["User"]).IsAdmin)
                    {

                    }
                    else if (((SessionData)Session["User"]).UserId == int.Parse(Request.QueryString["id"].ToString()))
                    {

                    }
                    else
                    {
                        Response.Redirect("UserDetails2.aspx?id=" + ((SessionData)Session["User"]).UserId);
                    }
                }
            }
            else if (pageName == "userlist.aspx")
            {
                if (Session["User"] != null)
                {
                    if (((SessionData)Session["User"]).IsAdmin)
                    {

                    }
                    else
                    {
                        Response.Redirect("UserDetails2.aspx?id=" + ((SessionData)Session["User"]).UserId);
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        //for notes user control
        [WebMethod]
        private void AddNoteToDatabase(string notesData)
        {
            dynamic notes = JsonConvert.DeserializeObject(notesData);
            NotesModel note = new NotesModel()
            {
                ObjectID = notes.ObjectId,
                NoteText = notes.NoteText,
                ObjectType = (int)Enum.Parse(typeof(ObjectType), notes.ObjectType),
                TimeStamp = DateTime.Now,
            };
            UserDetailsService.SaveNotes(note);
        }

    }
}