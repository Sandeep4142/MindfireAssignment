using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using static DemoUserManagement.Utils.ObjectTypes;
using System.Configuration;


namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for DocumentUpload2
    /// </summary>
    public class DocumentUpload2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                // Access the file from the request
                HttpPostedFile fileUpload = context.Request.Files[0];

                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    Guid uniqueGuid = Guid.NewGuid();

                    string fileExtension = Path.GetExtension(fileUpload.FileName);
                    string uniqueFileName = uniqueGuid.ToString() + fileExtension;
                    string filePath = ConfigurationManager.AppSettings["documentImages"] + uniqueFileName;
                    fileUpload.SaveAs(filePath);

                    int objectId = int.Parse(context.Request["ObjectId"]);
                    int documentType = int.Parse(context.Request["FileType"]);
                    int objectType = (int)ObjectTypes.ObjectType.Parse(typeof(ObjectType), context.Request["ObjectType"]);


                    DocumentModel document = new DocumentModel()
                    {
                        ObjectId = objectId,
                        ObjectType = objectType,
                        DocumentType = documentType,
                        DocumentName = fileUpload.FileName,
                        GuidDocumentName = uniqueFileName,
                        TimeStamp = DateTime.Now,
                    };

                    UserDetailsService.SaveDocuments(document);

                    var jsonResponse = new
                    {
                        success = true,
                        message = "File uploaded successfully.",
                        filePath,
                        uniqueFileName,
                        originalFileName = fileUpload.FileName
                    };

                    // Serialize the response to JSON and write it to the response
                    context.Response.Write(new JavaScriptSerializer().Serialize(jsonResponse));
                }
                else
                {
                    context.Response.Write("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
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
