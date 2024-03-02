using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoUserManagement.DataAccessLayer
{
    public static class DocumentDA
    {
        public static void SaveDocuments(DocumentModel doc)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var existingDocument = context.Documents.FirstOrDefault(d =>
                        d.ObjectType == doc.ObjectType &&
                        d.ObjectId == doc.ObjectId &&
                        d.DocumentType == doc.DocumentType);

                    if (existingDocument != null)
                    {
                        existingDocument.DocumentName = doc.DocumentName;
                        existingDocument.GuidDocumentName = doc.GuidDocumentName;
                        existingDocument.TimeStamp = doc.TimeStamp;
                    }
                    else
                    {
                        var document = new Document
                        {
                            ObjectType = doc.ObjectType,
                            ObjectId = doc.ObjectId,
                            DocumentType = doc.DocumentType,
                            DocumentName = doc.DocumentName,
                            GuidDocumentName = doc.GuidDocumentName,
                            TimeStamp = doc.TimeStamp,
                        };
                        context.Documents.Add(document);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }


        public static List<DocumentModel> GetDocuments(int objectId, int objectType)
        {
            List<DocumentModel> documentList = new List<DocumentModel>();
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var documents = context.Documents.Where(n => n.ObjectId == objectId && n.ObjectType == objectType).ToList();
                    foreach (var doc in documents)
                    {
                        DocumentModel documentModel = new DocumentModel
                        {
                            DocumentId=doc.DocumentId,
                            ObjectType = doc.ObjectType,
                            ObjectId = doc.ObjectId,
                            DocumentType = doc.DocumentType,
                            DocumentName = doc.DocumentName,
                            GuidDocumentName = doc.GuidDocumentName,
                            TimeStamp=doc.TimeStamp,
                        };
                        documentList.Add(documentModel);
                    }
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
            }
            return documentList;
        }

    }
}
