using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml.Linq;
using System.Web.Security;


namespace DemoUserManagement.BussinessLogicLayer
{
    public static class UserDetailsServiceMVC
    {
        //for UserDetails1
        public static void SaveAllDetails(AllDetailsModel userData)
        {
            UserDetailsModel userDetails = userData.user;
            userDetails.Addresses = userData.addresses;
            userDetails.EducationDetails = userData.educations;
            UserDetailsDA.SaveUserDetails(userDetails);
        }

        //for UserDetails2
        public static string SaveAllDetails2(UserDetailsModel user, HttpPostedFileBase profilePic, HttpPostedFileBase aadharCard)
        {
            if (profilePic != null && profilePic.ContentLength > 0)
            {
                user.ProfilePhoto = profilePic.FileName;
                user.GuidProfilePhoto = GetGuidFileName(profilePic);
            }

            if (aadharCard != null && aadharCard.ContentLength > 0)
            {
                user.AadhaarFile = aadharCard.FileName;
                user.GuidAadhaarFile = GetGuidFileName(aadharCard);
            }
            UserDetailsService.SaveUserDetails(user);
            return "UserSaved";
        }

        public static string GetGuidFileName(HttpPostedFileBase file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string guidFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = ConfigurationManager.AppSettings["documents"] + guidFileName; file.SaveAs(filePath);
            return guidFileName;
        }

        public static void SaveNote(string objectId, string objectType, string noteText)
        {
            var newNote = new NotesModel()
            {
                ObjectID = int.Parse(objectId),
                ObjectType = int.Parse(objectType),
                NoteText = noteText,
                TimeStamp = DateTime.Now
            };
            NotesDA.SaveNotes(newNote);
        }

        public static void SaveDocument(string objectId, string objectType, HttpPostedFileBase file, string fileType)
        {
            var newDocument = new DocumentModel()
            {
                ObjectId = int.Parse(objectId),
                ObjectType = int.Parse(objectType),
                DocumentName = file.FileName,
                DocumentType = int.Parse(fileType),
                GuidDocumentName = GetGuidFileName(file),
                TimeStamp = DateTime.Now,
            };

            DocumentDA.SaveDocuments(newDocument);
        }

        public static void DownloadFile(string fileName)
        {
            string filePath = ConfigurationManager.AppSettings["documents"] + fileName;
            if (File.Exists(filePath))
            {
                string contentType = MimeMapping.GetMimeMapping(fileName);

                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
                HttpContext.Current.Response.TransmitFile(filePath);
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.StatusCode = 404; // Not Found
                HttpContext.Current.Response.Write("File not found.");
                HttpContext.Current.Response.End();
            }
        }

        public static List<UserDetailsModel> GetUserList(string sortExp, string sortDirection, int currentPage, int pageSize)
        {
            sortExp = sortExp != null ? sortExp : "UserId";
            sortDirection = sortDirection != null ? sortDirection : "ASC";
            currentPage = currentPage !=0 ? currentPage : 1;

            string storedSortExpression = HttpContext.Current.Session["SortExpression"] as string;
            string storedSortDirection = HttpContext.Current.Session["SortDirection"] as string;

            bool isNewSortExp = !string.IsNullOrEmpty(sortExp) && sortExp != storedSortExpression;

            if (isNewSortExp)
            {
                sortDirection = (storedSortDirection == "ASC") ? "DSC" : "ASC";
                HttpContext.Current.Session["SortDirection"] = sortDirection;
            }

            HttpContext.Current.Session["SortExpression"] = sortExp;

            sortExp = isNewSortExp ? sortExp : storedSortExpression;

            var users = UserDetailsDA.Allusers(sortExp, sortDirection, (currentPage - 1) * pageSize, pageSize);
            return users;
        }

    }
}
