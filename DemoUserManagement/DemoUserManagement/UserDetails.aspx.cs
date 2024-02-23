using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                string id = Session["UserId"].ToString();
                bool isAdmin = UserDetailsService.CheckIfUserIsAdmin(int.Parse(id)); // Implement this method to check if the user is an admin

                if ((id != Request.QueryString["id"]) && !isAdmin)
                {
                    // Show alert message if they don't have a userId or if they are not an admin
                    ScriptManager.RegisterStartupScript(this, GetType(), "UnauthorizedAlert", "alert('You are not authorized.');", true);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        AddItemsToCountryDropDown(currentCntry);
                        AddItemsToCountryDropDown(permanentCntry);
                        // Initialize scrollPosition to 0 if it's the initial page load
                        ViewState["ScrollPosition"] = 0;

                        Notes.Visible = false;
                        Document.Visible = false;

                        ViewState["id"] = Request.QueryString["id"];
                        if (ViewState["id"] != null)
                        {
                            // Call methods to fill user details, address details, and education details
                            int userId = int.Parse(Request.QueryString["id"]);
                            FillUserDetails(userId);
                            FillAddressDetails(userId);
                            FillEducationDetails(userId);
                        }
                        else
                        {
                            // Notes.Visible = false;
                        }
                    }
                }
            }
            // Restore scroll position after postback
            int scrollPosition = Convert.ToInt32(ViewState["ScrollPosition"]);
            ScriptManager.RegisterStartupScript(this, GetType(), "RestoreScroll", $"window.scrollTo(0, {scrollPosition});", true);

        }

        protected void SaveUser(object sender, EventArgs e)
        {
            if (ViewState["id"] != null)
            {
                UpadateUserDetails(); 
                SaveAddressDetails(); 
                SaveEducationDetails();
            }
            else
            {
                SaveUserDetails();
                SaveAddressDetails();
                SaveEducationDetails();
            }
            Response.Redirect("UserList.aspx");
        }

        protected void SaveUserDetails()
        {
            UserDetailsModel user = new UserDetailsModel()
            {
                FirstName = firstName.Text,
                MiddleName = middleName.Text,
                LastName = lastName.Text,
                FathersName = fatherName.Text,
                MothersName = motherName.Text,
                DateOfBirth = Convert.ToDateTime(dateOfBirth.Text),
                Gender = male.Checked ? "Male" : (female.Checked ? "Female" : "Other"),
                AadhaarNo = aadharCardNo.Text,
                AadhaarFile = Path.GetFileName(aadhaarCardFile.FileName),
                GuidAadhaarFile = GetGuidFileName(aadhaarCardFile),
                ProfilePhoto = Path.GetFileName(profilePic.FileName),
                GuidProfilePhoto = GetGuidFileName(profilePic),
                PrimaryPhoneNo = primaryMobileno.Text,
                AlternatePhoneNo = alternateMobileno.Text,
                PrimaryEmailId = primaryEmailId.Text,
                AlternateEmailId = alternateEmailId.Text,
                Hobbies = string.Join(",", GetSelectedHobbies()),
                Password = password.Text,
               
            };
            UserDetailsService.SaveUserDetails(user);
        }

        protected string GetGuidFileName(FileUpload file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string guidFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Server.MapPath("Images/") + guidFileName;
            file.SaveAs(filePath);
            return guidFileName;
        }

        protected void UpadateUserDetails()
        {
            int id = int.Parse(ViewState["id"].ToString());
            DateTime dateOfBirthValue;
            if (DateTime.TryParseExact(dateOfBirth.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirthValue)) ;

            UserDetailsModel user = new UserDetailsModel()
            {
                FirstName = firstName.Text,
                MiddleName = middleName.Text,
                LastName = lastName.Text,
                FathersName = fatherName.Text,
                MothersName = motherName.Text,
                DateOfBirth = dateOfBirthValue.Date,
                Gender = male.Checked ? "Male" : (female.Checked ? "Female" : "Other"),
                AadhaarNo = aadharCardNo.Text,
                AadhaarFile = Path.GetFileName(aadhaarCardFile.FileName),
                GuidAadhaarFile = GetGuidFileName(aadhaarCardFile),
                ProfilePhoto = Path.GetFileName(profilePic.FileName),
                GuidProfilePhoto = GetGuidFileName(profilePic),
                PrimaryPhoneNo = primaryMobileno.Text,
                AlternatePhoneNo = alternateMobileno.Text,
                PrimaryEmailId = primaryEmailId.Text,
                AlternateEmailId = alternateEmailId.Text,
                Hobbies = string.Join(",", GetSelectedHobbies())
            };
            UserDetailsService.UpadetUserDetails(id, user);
        }

        protected void SaveAddressDetails()
        {
            int id;
            if (ViewState["id"] != null)
            {
                id = int.Parse(ViewState["id"].ToString());
            }
            else
            {
                id = UserDetailsService.GetLastUserId();
            }
            //save current address
            AddressModel currentAddress = new AddressModel()
            {
                UserID = id,
                Locality = currentLocality.Text,
                City = currentCity.Text,
                CountryId = int.Parse(currentCntry.SelectedValue),
                StateId = int.Parse(currentState.SelectedValue),
                Pincode = currentPincode.Text,
                AddressType = (int)ObjectTypes.ObjectType.CurrentAddress
            };
            UserDetailsService.SaveAddressDetails(currentAddress);

            //save permanent address
            AddressModel permanentAddress = new AddressModel()
            {
                UserID = id,
                Locality = permanentLocality.Text,
                City = permanentCity.Text,
                CountryId = int.Parse(currentCntry.SelectedValue),
                StateId = int.Parse(currentState.SelectedValue),
                Pincode = permanentPincode.Text,
                AddressType = (int)ObjectTypes.ObjectType.PermanentAddress
            };
            UserDetailsService.SaveAddressDetails(permanentAddress);
        }
        
        protected void SaveEducationDetails()
        {
            int id;
            if (ViewState["id"] != null)
            {
                id = int.Parse(ViewState["id"].ToString());
            }
            else
            {
                id = UserDetailsService.GetLastUserId();
            }
            //10th details
            EducationDetailsModel tenth = new EducationDetailsModel()
            {
                UserId = id,
                Institution = TenthInst.Text,
                University = TenthBoard.Text,
                Marks = Decimal.Parse(TenthMarks.Text),
                EducationType = (int)ObjectTypes.ObjectType.Tenth
            };

            UserDetailsService.SaveEducationDetails(tenth);

            //12th details
            EducationDetailsModel twelve = new EducationDetailsModel()
            {
                UserId = id,
                Institution = TwelveInst.Text,
                University = TwelveBoard.Text,
                Marks = Decimal.Parse(TwelveMarks.Text),
                EducationType = (int)ObjectTypes.ObjectType.Twelve
            };

            UserDetailsService.SaveEducationDetails(twelve);

            //Graduation details
            EducationDetailsModel graduation = new EducationDetailsModel()
            {
                UserId = id,
                Institution = gradInst.Text,
                University = gradUni.Text,
                Marks = Decimal.Parse(gradMarks.Text),
                EducationType=(int)ObjectTypes.ObjectType.Graduation
            };
            UserDetailsService.SaveEducationDetails(graduation);
        }

        protected List<string> GetSelectedHobbies()
        {
            List<string> selectedHobbies = new List<string>();

            foreach (ListItem item in hobbiesCheckBoxList.Items)
            {
                if (item.Selected)
                {
                    selectedHobbies.Add(item.Text);
                }
            }
            return selectedHobbies;
        }

        private void AddItemsToCountryDropDown(DropDownList dropdown)
        {
            List<CountryModel> countryList = UserDetailsService.GetCountries();
            dropdown.Items.Clear();
            dropdown.Items.Add(new ListItem("Select Country --", ""));
            foreach (CountryModel country in countryList)
            {
                dropdown.Items.Add(new ListItem(country.CountryName, country.CountryId.ToString()));
            }
        }

        protected void ShowSates(object sender, EventArgs e)
        {
            DropDownList senderDropdown = (DropDownList)sender;

            // Store scroll position before postback
            int currentScrollPosition = 0;
            if (!string.IsNullOrEmpty(Request.Form["scrollPosition"]))
            {
                currentScrollPosition = Convert.ToInt32(Request.Form["scrollPosition"]);
            }
            scrollPosition.Value = currentScrollPosition.ToString();

            if (senderDropdown == currentCntry)
            {
                // If the current country dropdown triggered the event, populate states for current address
                PopulateStates(currentCntry.SelectedValue, currentState);
            }
            else if (senderDropdown == permanentCntry)
            {
                // If the permanent country dropdown triggered the event, populate states for permanent address
                PopulateStates(permanentCntry.SelectedValue, permanentState);
            }
        }

        private void PopulateStates(string countryId, DropDownList stateDropdown)
        {
            List<StateModel> States = UserDetailsService.GetStates();
            stateDropdown.Items.Clear();
            List<StateModel> states = States.Where(s => s.CountryId == int.Parse(countryId)).ToList();
            stateDropdown.Items.Clear();
            // Add default "Select State" option
            stateDropdown.Items.Add(new ListItem("Select State --", ""));

            // Add states to the dropdown
            foreach (StateModel state in states)
            {
                stateDropdown.Items.Add(new ListItem(state.StateName, state.StateId.ToString()));
            }
        }

        protected void sameAsCurrentAddressCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (sameAsCurrentAddressCheck.Checked)
            {
                permanentLocality.Text = currentLocality.Text;
                permanentCity.Text = currentCity.Text;
                permanentCntry.SelectedValue = currentCntry.SelectedValue; 

                PopulateStates(permanentCntry.SelectedValue, permanentState); 

                permanentState.SelectedValue = currentState.SelectedValue; 
                permanentPincode.Text = currentPincode.Text;
            }
            else
            {
                // If the checkbox is unchecked, clear the permanent address details
                permanentLocality.Text = string.Empty;
                permanentCity.Text = string.Empty;
                permanentCntry.SelectedIndex = -1; // Set default selected index
                permanentState.Items.Clear(); 
                permanentPincode.Text = string.Empty;
            }
        }

        private void FillUserDetails(int id)
        {
            UserDetailsModel user = UserDetailsService.GetUserDetails(id);
            if (user != null)
            {
                Notes.Visible = true;
                Document.Visible = true;
                firstName.Text = user.FirstName;
                middleName.Text = user.MiddleName;
                lastName.Text = user.LastName;
                fatherName.Text = user.FathersName;
                motherName.Text = user.MothersName;
                var formattedDate = user.DateOfBirth.ToString("yyyy-MM-dd");
                dateOfBirth.Attributes["value"] = formattedDate;
                if (user.Gender == "Male")
                {
                    male.Checked = true;
                }
                else if (user.Gender == "Female")
                {
                    female.Checked = true;
                }
                else
                {
                    other.Checked = true;
                }
                aadharCardNo.Text = user.AadhaarNo;
                AadhaarFileName.Text = user.AadhaarFile;
                ProfilePicFileName.Text = user.ProfilePhoto;
                primaryMobileno.Text = user.PrimaryPhoneNo;
                alternateMobileno.Text = user.AlternatePhoneNo;
                primaryEmailId.Text = user.PrimaryEmailId;
                Session["Email"] = user.PrimaryEmailId;
                password.Text = user.Password;
                alternateEmailId.Text = user.AlternateEmailId;
                if (!string.IsNullOrEmpty(user.Hobbies))
                {
                    var hobbiesArray = user.Hobbies.Split(',');
                    foreach (var hobby in hobbiesArray)
                    {
                        if (!string.IsNullOrWhiteSpace(hobby))
                        {
                            if (hobbiesCheckBoxList.Items.FindByValue(hobby) != null)
                            {
                                hobbiesCheckBoxList.Items.FindByValue(hobby).Selected = true;
                            }
                        }
                    }
                }
            }
        }

        private void FillAddressDetails(int id)
        {
            // Get the address details for the given user ID
            var addressDetails = UserDetailsService.GetAddressDetails(id);

            // Populate current address details if available
            var currentAddress = addressDetails.CurrentAddress.FirstOrDefault();
            if (currentAddress != null)
            {
                currentLocality.Text = currentAddress.Locality;
                currentCity.Text = currentAddress.City;
                currentCntry.SelectedValue = currentAddress.CountryId.ToString();
                PopulateStates(currentCntry.SelectedValue, currentState);
                currentState.SelectedValue = currentAddress.StateId.ToString();
                currentPincode.Text = currentAddress.Pincode;
            }

            // Populate permanent address details if available
            var permanentAddress = addressDetails.PermanentAddress.FirstOrDefault();
            if (permanentAddress != null)
            {
                permanentLocality.Text = permanentAddress.Locality;
                permanentCity.Text = permanentAddress.City;
                permanentCntry.SelectedValue = permanentAddress.CountryId.ToString();
                PopulateStates(permanentCntry.SelectedValue, permanentState);
                permanentState.SelectedValue = permanentAddress.StateId.ToString();
                permanentPincode.Text = permanentAddress.Pincode;
            }
        }

        private void FillEducationDetails(int id)
        {
            var educationDetails = UserDetailsService.GetEducationDetails(id);

            var type1EducationDetails = educationDetails.Tenth.FirstOrDefault();
            if (type1EducationDetails != null)
            {
                TenthInst.Text = type1EducationDetails.Institution;
                TenthBoard.Text = type1EducationDetails.University;
                TenthMarks.Text = type1EducationDetails.Marks.ToString(); 
            }

            var type2EducationDetails = educationDetails.Twelve.FirstOrDefault();
            if (type2EducationDetails != null)
            {
                TwelveInst.Text = type2EducationDetails.Institution;
                TwelveBoard.Text = type2EducationDetails.University;
                TwelveMarks.Text = type2EducationDetails.Marks.ToString(); 
            }

            var type3EducationDetails = educationDetails.Graduation.FirstOrDefault();
            if (type3EducationDetails != null)
            {
                gradInst.Text = type3EducationDetails.Institution;
                gradUni.Text = type3EducationDetails.University;
                gradMarks.Text = type3EducationDetails.Marks.ToString(); 
            }
        }


        /////////////////////

    [WebMethod]
    public static bool CheckEmailExists(string email)
    {
        // Check if the provided email matches the one stored in the session
        if (HttpContext.Current.Session["Email"] != null && HttpContext.Current.Session["Email"].ToString() == email)
        {
            return false;
        }

        // Otherwise, check if the email exists in the data service
        return UserDetailsService.CheckEmailExists(email);
    }


}
}