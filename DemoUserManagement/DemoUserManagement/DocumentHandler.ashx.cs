using System;
using System.IO;
using System.Web;
using System.Configuration;

namespace DemoUserManagement
{
    /// <summary>
    /// Summary description for DocumentHandler
    /// </summary>
    public class DocumentHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // Get the GuidDocumentName from the query string
                string guidDocumentName = context.Request.QueryString["guidDocumentName"];

                // Get the physical path to the file
                string filePath = ConfigurationManager.AppSettings["documentImages"] + guidDocumentName;

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Set the content type based on the file extension
                    string contentType = MimeMapping.GetMimeMapping(filePath);
                    context.Response.ContentType = contentType;

                    // Set the response headers for content disposition and file name
                    context.Response.AppendHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(filePath));

                    // Write the file content to the response stream
                    context.Response.WriteFile(filePath);
                }
                else
                {
                    // If the file does not exist, return a 404 error
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "File not found";
                }
            }
            catch (Exception ex)
            {
                // Handle any errors and log them
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "Internal Server Error";
                context.Response.Write("An error occurred: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }


    }
}
