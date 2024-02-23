using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{

    public partial class UserDetails2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Notes.Visible = true;
                Document.Visible = true;
            }
            else
            {
                Notes.Visible = false;
                Document.Visible = false;
            }
        }

        //Save user using complex class (method- 1)
        [WebMethod]
        public static string SaveAllDetails(AllDetailsModel userData)
        {
            try
            {
                UserDetailsService.SaveUserDetails(userData.user);

                foreach (var address in userData.addresses)
                {
                    UserDetailsService.SaveAddressDetails(address);
                }

                foreach (var education in userData.educations)
                {
                    UserDetailsService.SaveEducationDetails(education);
                }
                return "Saved User";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return "Failed to save";
            }
        }

        //save user details separately (method - 2)
        [WebMethod]
        public static string SaveUser(UserDetailsModel user)
        {
            UserDetailsService.SaveUserDetails(user);
            return "Save";
        }

        [WebMethod]
        public static void SaveAddressDetails(AddressModel address)
        {
            // AddressModel addressDetail = JsonConvert.DeserializeObject<AddressModel>(address);
            UserDetailsService.SaveAddressDetails(address);
        }

        [WebMethod]
        public static void SaveEducationDetails(EducationDetailsModel education)
        {
            // EducationDetailsModel ed = JsonConvert.DeserializeObject<EducationDetailsModel>(education);
            UserDetailsService.SaveEducationDetails(education);
        }

        //

        [WebMethod]
        public static List<CountryModel> GetCountries()
        {
            List<CountryModel> countryList = UserDetailsService.GetCountries();
            return countryList;
        }

        [WebMethod]
        public static List<StateModel> GetStates(int countryId)
        {
            List<StateModel> States = UserDetailsService.GetStates();
            List<StateModel> states = States.Where(s => s.CountryId == countryId).ToList();
            return states;
        }

        [WebMethod]
        public static UserInfoModel LoadUser(int id)
        {
            return UserDetailsService.GetUser(id);
        }

        [WebMethod]
        public static bool CheckEmailExists(string email)
        {
            if ((HttpContext.Current.Session["User"])!=null && ((SessionData)HttpContext.Current.Session["User"]).Email == email)
            {
                return false;
            }
            return UserDetailsService.CheckEmailExists(email);
        }


        

    }
}