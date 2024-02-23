using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DemoUserManagement.Utils.ObjectTypes;

namespace DemoUserManagement
{
    public partial class Document : System.Web.UI.UserControl
    {
        public string ObjType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ViewState["id"] = Request.QueryString["id"];
                int id;
                if (int.TryParse(ViewState["id"].ToString(), out id))
                {
                    int objectType = (int)Enum.Parse(typeof(ObjectType), ObjType); 
                    LoadNotesGrid(id, objectType);
                }
                else
                {
                }
            }
        }

        private void LoadNotesGrid(int objectId, int objType)
        {
            documentGrid.DataSource = UserDetailsService.GetDocuments(objectId,objType);
            documentGrid.DataBind();
        }

        protected string GetGuidFileName(FileUpload file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string guidFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath  = "D:\\DocumentImages\\" + guidFileName;

            file.SaveAs(filePath);
            return guidFileName;
        }

        protected string GetDocumentTypeName(int documentTypeValue)
        {
            // Convert the integer value to the corresponding enum value
            return ((ObjectTypes.DocumentType)documentTypeValue).ToString();
        }
    }
}