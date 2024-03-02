using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public static class UserInfoDA
    {


        public static UserInfoModel GetUser(int id)
        {
            UserInfoModel users = null;
            try
            {
                using (DemoUserManagementEntities context = new DemoUserManagementEntities())
                {
                    UserDetail user = context.UserDetails.Find(id);

                    if (user != null)
                    {
                        users = new UserInfoModel()
                        {
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            FathersName = user.FathersName,
                            MothersName= user.MothersName,
                            DateOfBirth = user.DateOfBirth,
                            Gender = user.Gender,
                            AadhaarNo   = user.AadhaarNo,
                            AadhaarFile = user.AadhaarFile,
                            GuidAadhaarFile= user.GuidAadhaarFile,
                            ProfilePhoto = user.ProfilePhoto,
                            GuidProfilePhoto= user.GuidProfilePhoto,
                            PrimaryPhoneNo = user.PrimaryPhoneNo,
                            AlternatePhoneNo = user.AlternatePhoneNo,
                            PrimaryEmailId  = user.PrimaryEmailId,
                            AlternateEmailId = user.AlternateEmailId,
                            Hobbies=user.Hobbies,
                            Password = user.Password,
                            Address = new List<AddressModel>(), 
                            Educations = new List<EducationDetailsModel>() 
                        };
                    }

                    foreach (var item in user.Addresses)
                    {
                        AddressModel a= new AddressModel()
                        {
                            AddressType = item.AddressType,
                            UserID = item.UserID,
                            Locality = item.Locality,
                            City = item.City,
                            CountryId = item.CountryId,
                            StateId = item.StateId,
                            Pincode = item.Pincode,

                        };

                        users.Address.Add(a);
                    }
                    foreach (var item in user.EducationDetails)
                    {
                        users.Educations.Add(new EducationDetailsModel() { 
                            EducationId = item.EducationId,
                            EducationType = item.EducationType,
                            Institution = item.Institution,
                            University = item.University,
                            Marks = item.Marks,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return users;
        }
    }
}
