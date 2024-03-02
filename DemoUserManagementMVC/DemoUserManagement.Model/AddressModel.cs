using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace DemoUserManagement.Model
{
    public class AddressModel
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Pincode { get; set; }
        public int AddressType { get; set; }

        public virtual UserDetailsModel UserDetail { get; set; }
        public virtual StateModel State { get; set; }
        public virtual CountryModel Country { get; set; }
    }
}
