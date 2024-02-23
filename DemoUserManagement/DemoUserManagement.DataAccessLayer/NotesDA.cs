using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public class NotesDA
    {
        public static void SaveNotes(NotesModel notes)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var note = new Note
                    {
                        ObjectID = (int)notes.ObjectID,
                        ObjectType = notes.ObjectType,
                        NoteText = notes.NoteText,
                        TimeStamp = (DateTime)notes.TimeStamp,
                    };
                    context.Notes.Add(note);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }


        public static List<NotesModel> GetNotes(int userId, int objectType)
        {
            List<NotesModel> notesList = new List<NotesModel>();
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var notes = context.Notes.Where(n => n.ObjectID == userId && n.ObjectType == objectType).ToList();
                    foreach (var note in notes)
                    {
                        NotesModel noteModel = new NotesModel
                        {
                            NoteID = note.NoteID,
                            ObjectID = note.ObjectID,
                            ObjectType = note.ObjectType,
                            NoteText = note.NoteText,
                            TimeStamp = note.TimeStamp,
                        };
                        notesList.Add(noteModel);
                    }
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
            }
            return notesList;
        }



    }
}
