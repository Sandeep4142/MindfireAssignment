using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Model
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }
        public int ObjectType { get; set; }
        public int ObjectId { get; set; }
        public int DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string GuidDocumentName { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
