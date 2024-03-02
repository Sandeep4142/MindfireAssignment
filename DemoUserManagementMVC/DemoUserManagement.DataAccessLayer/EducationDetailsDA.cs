using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public static class EducationDetailsDA
    {
        public static void SaveEducationDetails(EducationDetailsModel edu)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var existingEducationDetail = context.EducationDetails.FirstOrDefault(e => e.UserId == edu.UserId && e.EducationType == edu.EducationType);

                    if (existingEducationDetail == null)
                    {
                        // If no education detail exists for the given UserID, create a new education detail entity
                        int userId= UserDetailsDA.GetLastUserId();
                        var newEducationDetail = new EducationDetail
                        {
                            UserId = userId, //(int)edu.UserId,
                            Institution = edu.Institution,
                            University = edu.University,
                            Marks = (decimal)edu.Marks,
                            EducationType = (int)edu.EducationType
                        };

                        // Add the new education detail entity to the context
                        context.EducationDetails.Add(newEducationDetail);
                    }
                    else
                    {
                        // If an education detail exists for the given UserID, update its properties
                        existingEducationDetail.Institution = edu.Institution;
                        existingEducationDetail.University = edu.University;
                        existingEducationDetail.Marks = (int)edu.Marks;
                    }
                    // Save the changes to the database
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
            }
        }

        public static (List<EducationDetailsModel> Tenth, List<EducationDetailsModel> Twelve, List<EducationDetailsModel> Graduation) GetEducationDetails(int id)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var tenth = context.EducationDetails
                        .Where(e => e.UserId == id && e.EducationType == (int)ObjectTypes.ObjectType.Tenth)
                        .Select(education => new EducationDetailsModel
                        {
                            UserId = education.UserId,
                            Institution = education.Institution,
                            University = education.University,
                            Marks = education.Marks,
                            EducationType = education.EducationType
                        }).ToList();

                    var twelve = context.EducationDetails
                        .Where(e => e.UserId == id && e.EducationType == (int)ObjectTypes.ObjectType.Twelve)
                        .Select(education => new EducationDetailsModel
                        {
                            UserId = education.UserId,
                            Institution = education.Institution,
                            University = education.University,
                            Marks = education.Marks,
                            EducationType = education.EducationType
                        }).ToList();

                    var graduation = context.EducationDetails
                        .Where(e => e.UserId == id && e.EducationType == (int)ObjectTypes.ObjectType.Graduation)
                        .Select(education => new EducationDetailsModel
                        {
                            UserId = education.UserId,
                            Institution = education.Institution,
                            University = education.University,
                            Marks = education.Marks,
                            EducationType = education.EducationType
                        }).ToList();

                    return (tenth, twelve, graduation);
                }
            }catch(Exception ex)
            {
                Logger.WriteLog(ex);
                throw;
            }
        }

    }
}
