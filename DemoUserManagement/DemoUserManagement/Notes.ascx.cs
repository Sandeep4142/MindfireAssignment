using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast;
using DemoUserManagement.Model;
using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Utils;
using static DemoUserManagement.Utils.ObjectTypes;
using System.Xml.Linq;

namespace DemoUserManagement
{
    public partial class Notes : System.Web.UI.UserControl
    {
        public string ObjType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string idParameter = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(idParameter) && int.TryParse(idParameter, out int id))
            {
                ViewState["id"] = id;
                LoadNotesGrid(id);
            }

        }
        protected void SaveNotes(object sender, EventArgs e)
        {
            //if (note.Text != "" && ViewState["id"] != null)
            //{
            //    int objectId = int.Parse(ViewState["id"].ToString());
            //    AddNoteToDatabase(objectId,note.Text);
            //    LoadNotesGrid(objectId);
            //    note.Text = "";
            //}
        }

        private void LoadNotesGrid(int objectId)
        {
           
            DataTable notesData = RetrieveNotesFromDatabase(objectId);

            notesGrid.DataSource = notesData;
            notesGrid.DataBind();
        }

        private void AddNoteToDatabase(int objectId,string noteText)
        {
           
            NotesModel note = new NotesModel()
            {
                ObjectID = objectId,
                NoteText = noteText,
                ObjectType = (int)Enum.Parse(typeof(ObjectType), ObjType),
                TimeStamp = DateTime.Now,
            };

            UserDetailsService.SaveNotes(note);
           
        }

        private DataTable RetrieveNotesFromDatabase(int objectId)
        {
            DataTable notesData = new DataTable();
            int objType = (int)Enum.Parse(typeof(ObjectType), ObjType);
            List<NotesModel> notes = UserDetailsService.GetNotes(objectId, objType);

            notesData.Columns.Add("NoteText", typeof(string));
            notesData.Columns.Add("TimeStamp", typeof(string));

            if (notes != null)
            {
                foreach (var note in notes)
                {
                    string formattedTimeStamp = String.Format("{0:MM/dd/yyyy hh:mm:ss tt}", note.TimeStamp);

                    notesData.Rows.Add(note.NoteText, formattedTimeStamp);
                }
            }

            return notesData;
        }
    }
}