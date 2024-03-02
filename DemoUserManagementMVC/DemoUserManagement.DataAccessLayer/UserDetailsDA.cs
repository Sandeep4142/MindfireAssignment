using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoUserManagement.DataAccessLayer
{
    public static class UserDetailsDA
    {
        public static void SaveUserDetails(UserDetailsModel user)
        {

            if (user.UserId != 0)
            {
                UpdateUserDetails(user.UserId, user);
            }
            else
            {
                try
                {
                    using (var context = new DemoUserManagementEntities())
                    {
                        var userDetail = new UserDetail
                        {
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            FathersName = user.FathersName,
                            MothersName = user.MothersName,
                            DateOfBirth = user.DateOfBirth,
                            Gender = user.Gender,
                            AadhaarNo = user.AadhaarNo,
                            AadhaarFile = user.AadhaarFile,
                            GuidAadhaarFile = user.GuidAadhaarFile,
                            ProfilePhoto = user.ProfilePhoto,
                            GuidProfilePhoto = user.GuidProfilePhoto,
                            PrimaryPhoneNo = user.PrimaryPhoneNo,
                            AlternatePhoneNo = user.AlternatePhoneNo,
                            PrimaryEmailId = user.PrimaryEmailId,
                            AlternateEmailId = user.AlternateEmailId,
                            Hobbies = user.Hobbies,
                            Password = user.Password,

                        };
                        userDetail.Addresses = new List<Address>();
                        userDetail.EducationDetails = new List<EducationDetail>();
                        // Add addresses to the user
                        foreach (var address in user.Addresses)
                        {
                            var addressEntity = new Address
                            {
                                Locality = address.Locality,
                                City = address.City,
                                CountryId = address.CountryId,
                                StateId = address.StateId,
                                Pincode = address.Pincode,
                                AddressType = address.AddressType
                            };
                            userDetail.Addresses.Add(addressEntity);
                        }
                        // Add education details to the user
                        foreach (var eduDetail in user.EducationDetails)
                        {
                            var eduDetailEntity = new EducationDetail
                            {
                                Institution = eduDetail.Institution,
                                University = eduDetail.University,
                                Marks = eduDetail.Marks,
                                EducationType = eduDetail.EducationType
                            };
                            userDetail.EducationDetails.Add(eduDetailEntity);
                        }

                        context.UserDetails.Add(userDetail);
                        context.SaveChanges();
                        AssignDefaultRole(GetLastUserId());
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                }
            }
        }

        public static void UpdateUserDetails(int userId, UserDetailsModel updatedUser)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var existingUser = context.UserDetails.Include(u => u.Addresses).Include(u => u.EducationDetails).FirstOrDefault(u => u.UserId == userId);
                    if (existingUser != null)
                    {
                        // Update the existing user details
                        existingUser.FirstName = updatedUser.FirstName;
                        existingUser.MiddleName = updatedUser.MiddleName;
                        existingUser.LastName = updatedUser.LastName;
                        existingUser.FathersName = updatedUser.FathersName;
                        existingUser.MothersName = updatedUser.MothersName;
                        existingUser.DateOfBirth = updatedUser.DateOfBirth;
                        existingUser.Gender = updatedUser.Gender;
                        existingUser.AadhaarNo = updatedUser.AadhaarNo;
                        existingUser.GuidAadhaarFile = updatedUser.GuidAadhaarFile;
                        existingUser.AadhaarFile = updatedUser.AadhaarFile;
                        existingUser.ProfilePhoto = updatedUser.ProfilePhoto;
                        existingUser.GuidProfilePhoto = updatedUser.GuidProfilePhoto;
                        existingUser.PrimaryPhoneNo = updatedUser.PrimaryPhoneNo;
                        existingUser.AlternatePhoneNo = updatedUser.AlternatePhoneNo;
                        existingUser.PrimaryEmailId = updatedUser.PrimaryEmailId;
                        existingUser.AlternateEmailId = updatedUser.AlternateEmailId;
                        existingUser.Hobbies = updatedUser.Hobbies;
                        existingUser.Password = updatedUser.Password;



                        // Update addresses

                        foreach (var address in updatedUser.Addresses)
                        {
                            var addId = address.AddressID;
                          //  var existingAddress = existingUser.Addresses.FirstOrDefault(a => a.AddressID == address.AddressID);
                            var existingAddress = existingUser.Addresses.FirstOrDefault(a => (a.AddressID == address.AddressID) || (a.AddressType == address.AddressType && a.UserID == address.UserID));

                            if (existingAddress != null)
                            {
                                existingAddress.Locality = address.Locality;
                                existingAddress.City = address.City;
                                existingAddress.CountryId = 1;
                                existingAddress.StateId = 1;
                                existingAddress.Pincode = address.Pincode;
                            }
                            else
                            {
                                // Handle case where address with given AddressId is not found
                                throw new Exception("Address with AddressId " + address.AddressID + " not found.");
                            }
                        }

                        // Update education details
                        foreach (var eduDetail in updatedUser.EducationDetails)
                        {
                            var eduId = eduDetail.EducationId;
                           // var existingEduDetail = existingUser.EducationDetails.FirstOrDefault(e => e.EducationId == eduDetail.EducationId);
                            var existingEduDetail = existingUser.EducationDetails.FirstOrDefault(e => (e.EducationId == eduDetail.EducationId) || (e.EducationType == eduDetail.EducationType && e.UserId == eduDetail.UserId));

                            if (existingEduDetail != null)
                            {
                                existingEduDetail.Institution = eduDetail.Institution;
                                existingEduDetail.University = eduDetail.University;
                                existingEduDetail.Marks = eduDetail.Marks;
                            }
                            else
                            {
                                // Handle case where education detail with given EducationId is not found
                                throw new Exception("Education detail with EducationId " + eduDetail.EducationId + " not found.");
                            }
                        }

                        context.SaveChanges();
                    }
                    else
                    {
                        // Handle case where user with given UserId is not found
                        throw new Exception("User with UserId " + userId + " not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }


        public static void AssignDefaultRole(int userId)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var defaultRoles = context.Roles.Where(r => r.IsDefault == true).ToList();
                    foreach (var role in defaultRoles)
                    {
                        var userRole = new UserRole
                        {
                            UserId = userId,
                            RoleId = role.RoleId
                        };
                        context.UserRoles.Add(userRole);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

        public static UserDetailsModel GetUserDetails(int id)
        {
            UserDetailsModel users = null;
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    UserDetail user = context.UserDetails.Find(id);
                    users = new UserDetailsModel()
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        FathersName = user.FathersName,
                        MothersName = user.MothersName,
                        DateOfBirth = user.DateOfBirth,
                        Gender = user.Gender,
                        AadhaarNo = user.AadhaarNo,
                        AadhaarFile = user.AadhaarFile,
                        GuidAadhaarFile = user.GuidAadhaarFile,
                        ProfilePhoto = user.ProfilePhoto,
                        GuidProfilePhoto = user.GuidProfilePhoto,
                        PrimaryPhoneNo = user.PrimaryPhoneNo,
                        AlternatePhoneNo = user.AlternatePhoneNo,
                        PrimaryEmailId = user.PrimaryEmailId,
                        AlternateEmailId = user.AlternateEmailId,
                        Hobbies = user.Hobbies,
                        Password = user.Password,
                        Addresses = new List<AddressModel>(),
                        EducationDetails = new List<EducationDetailsModel>()
                    };


                    foreach (var item in user.Addresses)
                    {
                        AddressModel a = new AddressModel()
                        {
                            AddressID = item.AddressID,
                            AddressType = item.AddressType,
                            UserID = item.UserID,
                            Locality = item.Locality,
                            City = item.City,
                            CountryId = item.CountryId,
                            StateId = item.StateId,
                            Pincode = item.Pincode,

                        };

                        users.Addresses.Add(a);
                    }
                    foreach (var item in user.EducationDetails)
                    {
                        users.EducationDetails.Add(new EducationDetailsModel()
                        {
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

        public static int CheckUser(string email, string password)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var user = context.UserDetails.FirstOrDefault(u => u.PrimaryEmailId == email);

                    if (user != null)
                    {
                        if (user.Password == password)
                        {
                            return user.UserId; //user exist
                        }
                        else
                        {
                            return 0; // wrong password
                        }
                    }
                    else
                    {
                        return -1; //  user not found
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return -1;
            }
        }

        public static string GetUserGuidAadhharFile(int id)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var user = context.UserDetails.Find(id);
                    if (user != null)
                    {
                        return user.GuidAadhaarFile;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return null;
        }

        public static string GetUserGuidProfilePic(int id)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var user = context.UserDetails.Find(id);
                    if (user != null)
                    {
                        return user.GuidProfilePhoto;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return null;
        }

        public static List<UserDetailsModel> GetAllUserDetails()
        {
            List<UserDetailsModel> userDetailsList = new List<UserDetailsModel>();
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var users = context.UserDetails.ToList();
                    foreach (var user in users)
                    {
                        UserDetailsModel userDetails = new UserDetailsModel
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            FathersName = user.FathersName,
                            MothersName = user.MothersName,
                            DateOfBirth = user.DateOfBirth,
                            Gender = user.Gender,
                            AadhaarNo = user.AadhaarNo,
                            AadhaarFile = user.AadhaarFile,
                            GuidAadhaarFile = user.GuidAadhaarFile,
                            ProfilePhoto = user.ProfilePhoto,
                            GuidProfilePhoto = user.GuidProfilePhoto,
                            PrimaryPhoneNo = user.PrimaryPhoneNo,
                            AlternatePhoneNo = user.AlternatePhoneNo,
                            PrimaryEmailId = user.PrimaryEmailId,
                            AlternateEmailId = user.AlternateEmailId,
                            Hobbies = user.Hobbies,
                            Password = user.Password,

                        };
                        userDetailsList.Add(userDetails);
                    }
                }
                return userDetailsList;

            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static int GetLastUserId()
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var lastUserId = context.UserDetails
                                            .OrderByDescending(u => u.UserId)
                                            .Select(u => u.UserId)
                                            .FirstOrDefault();
                    return lastUserId;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return 0;
            }
        }

        /// ///////////////////////////////////////////////////////

        public static List<UserDetailsModel> Allusers(string sortExpression, string sortDirection, int startRowIndex, int maximumRows)
        {
            List<UserDetailsModel> users = null;
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    IQueryable<UserDetail> query = context.UserDetails;

                    // Sorting
                    query = ApplySorting(query, sortExpression, sortDirection);

                    // Pagination
                    query = query.Skip(startRowIndex).Take(maximumRows);

                    List<UserDetail> alluser = query.ToList();
                    users = new List<UserDetailsModel>();

                    foreach (var user in alluser)
                    {
                        users.Add(new UserDetailsModel
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            FathersName = user.FathersName,
                            MothersName = user.MothersName,
                            DateOfBirth = user.DateOfBirth,
                            Gender = user.Gender,
                            AadhaarNo = user.AadhaarNo,
                            AadhaarFile = user.AadhaarFile,
                            GuidAadhaarFile = user.GuidAadhaarFile,
                            ProfilePhoto = user.ProfilePhoto,
                            GuidProfilePhoto = user.GuidProfilePhoto,
                            PrimaryPhoneNo = user.PrimaryPhoneNo,
                            AlternatePhoneNo = user.AlternatePhoneNo,
                            PrimaryEmailId = user.PrimaryEmailId,
                            AlternateEmailId = user.AlternateEmailId,
                            Hobbies = user.Hobbies
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

        private static IQueryable<UserDetail> ApplySorting(IQueryable<UserDetail> query, string sortExpression, string sortDirection)
        {
            switch (sortExpression)
            {
                case "UserId":
                    query = sortDirection == "ASC" ? query.OrderBy(u => u.UserId) : query.OrderByDescending(u => u.UserId);
                    break;
                case "FirstName":
                    query = sortDirection == "ASC" ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
                    break;
                case "LastName":
                    query = sortDirection == "ASC" ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName);
                    break;
                case "PrimaryEmailId":
                    query = sortDirection == "ASC" ? query.OrderBy(u => u.PrimaryEmailId) : query.OrderByDescending(u => u.PrimaryEmailId);
                    break;
                case "PrimaryPhoneNo":
                    query = sortDirection == "ASC" ? query.OrderBy(u => u.PrimaryPhoneNo) : query.OrderByDescending(u => u.PrimaryPhoneNo);
                    break;

                    // Add other cases for additional columns
            }
            return query;
        }


        public static int Lenusers()
        {
            int lenuser = 0;
            try
            {
                using (DemoUserManagementEntities context = new DemoUserManagementEntities())
                {
                    List<UserDetail> alluser = context.UserDetails.ToList();
                    lenuser = alluser.Count;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            return lenuser;
        }

        public static bool CheckEmailExists(string email)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    return context.UserDetails.Any(u => u.PrimaryEmailId == email);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
    }
}
