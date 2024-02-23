using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Model
{
    public class NotesModel
    {
        public int NoteID { get; set; }
        public Nullable<int> ObjectID { get; set; }
        public int ObjectType { get; set; }
        public string NoteText { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }

        public virtual UserDetailsModel UserDetail { get; set; }
    }
}
