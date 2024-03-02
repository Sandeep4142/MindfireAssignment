using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;


namespace DemoUserManagement.Model
{
    public class UserDetailsModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadhaarNo { get; set; }
        public string AadhaarFile { get; set; }
        public string GuidAadhaarFile { get; set; }
        public string ProfilePhoto { get; set; }
        public string GuidProfilePhoto { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string PrimaryEmailId { get; set; }
        public string AlternateEmailId { get; set; }
        public string Hobbies { get; set; }
        public string Password { get; set; }
        public virtual ICollection<AddressModel> Addresses { get; set; }
        public virtual ICollection<EducationDetailsModel> EducationDetails { get; set; }
        public virtual ICollection<NotesModel> Notes { get; set; }
        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
    }
}
