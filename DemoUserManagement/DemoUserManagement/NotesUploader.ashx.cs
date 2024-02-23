using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static DemoUserManagement.Utils.ObjectTypes;
using System.Web.Script.Serialization;

namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for NotesUploader
    /// </summary>
    public class NotesUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                    int objectId = int.Parse(context.Request["ObjectId"]);
                    int objectType = (int)Enum.Parse(typeof(ObjectType), context.Request["ObjectType"]);
                    string noteText = context.Request["NoteText"].ToString();
                    NotesModel notes = new NotesModel()
                    {
                        ObjectID=objectId,
                        ObjectType=objectType,
                        NoteText=noteText,
                        TimeStamp = DateTime.Now,
                    };

                    UserDetailsService.SaveNotes(notes);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}