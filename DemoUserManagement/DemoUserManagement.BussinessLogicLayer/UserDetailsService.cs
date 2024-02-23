using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.BussinessLogicLayer
{
    public static class UserDetailsService
    {
        public static void SaveUserDetails(UserDetailsModel user)
        {
            UserDetailsDA.SaveUserDetails(user);
        }

        public static void UpadetUserDetails(int id, UserDetailsModel user)
        {
            UserDetailsDA.UpdateUserDetails(id, user);
        }

        public static void SaveAddressDetails(AddressModel address)
        {
            AddressDetailsDA.SaveAddressDetails(address);
        }

        public static void SaveEducationDetails(EducationDetailsModel eduacation)
        {
            EducationDetailsDA.SaveEducationDetails(eduacation);
        }

        public static int GetLastUserId()
        {
            return UserDetailsDA.GetLastUserId();
        }

        public static UserDetailsModel GetUserDetails(int userId)
        {
            return UserDetailsDA.GetUserDetails(userId);
        }

        public static (List<AddressModel> CurrentAddress, List<AddressModel> PermanentAddress) GetAddressDetails(int userId)
        {
            return AddressDetailsDA.GetAddressDetails(userId);
        }

        public static (List<EducationDetailsModel> Tenth, List<EducationDetailsModel> Twelve, List<EducationDetailsModel> Graduation) GetEducationDetails(int userId)
        {
            return EducationDetailsDA.GetEducationDetails(userId);
        }
        
        public static List<CountryModel> GetCountries()
        {
            return CountryDA.GetCountry();
        }

        public static List<StateModel> GetStates()
        {
            return StateDA.GetState();
        }

        public static List<UserDetailsModel> GetAllUserDetails()
        {
            return UserDetailsDA.GetAllUserDetails();
        }

        public static string GetGuidAadhaarFile(int id)
        {
            return UserDetailsDA.GetUserGuidAadhharFile(id);
        }
        public static string GetGuidProfilePic(int id)
        {
            return UserDetailsDA.GetUserGuidProfilePic(id);
        }

        public static void SaveNotes(NotesModel notes)
        {
            NotesDA.SaveNotes(notes);
        }

        public static List<NotesModel> GetNotes(int objectId, int objectType)
        {
            return NotesDA.GetNotes(objectId, objectType);
        }

        public static void SaveDocuments(DocumentModel document)
        {
            DocumentDA.SaveDocuments(document);
        }

        public static List<DocumentModel> GetDocuments(int objectId, int objectType)
        {
            return DocumentDA.GetDocuments(objectId, objectType);
        }

        public static List<UserDetailsModel> Allusers(string sortExpression, string sortDirection, int startRowIndex, int maximumRows)
        {
            return UserDetailsDA.Allusers(sortExpression, sortDirection, startRowIndex, maximumRows);
        }

        public static int Lenusers()
        {
            return UserDetailsDA.Lenusers();
        }

        public static int CheckUser(string email, string password)
        {
            return UserDetailsDA.CheckUser(email, password);
        }

        public static bool CheckIfUserIsAdmin(int userId)
        {
            return UserRoleDA.CheckIfUserIsAdmin(userId);
        }

        public static bool CheckEmailExists(string email)
        {
            return UserDetailsDA.CheckEmailExists(email);
        }

        public static UserInfoModel GetUser(int id)
        {
            return UserInfoDA.GetUser(id);
        }

    }
}
