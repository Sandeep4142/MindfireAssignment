using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services.Description;

namespace DemoUserManagement
{

    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var file = context.Request.Files[0];
                string fileName = context.Request["Name"]; // Retrieve the guid file name

                if (file != null && file.ContentLength > 0)
                {
                    
                    //string uploadFolder = ConfigurationManager.AppSettings["documentImages"]; /
                    string filePath = @"D:\DocumentImages\" + fileName;

                    file.SaveAs(filePath);
                    context.Response.Write(fileName);
                }
                else
                {
                    context.Response.Write("{\"error\": \"No file received.\"}");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
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