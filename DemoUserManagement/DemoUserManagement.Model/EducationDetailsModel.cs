using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Model
{
    public class EducationDetailsModel
    {
        public int EducationId { get; set; }
        public int UserId { get; set; }
        public string Institution { get; set; }
        public string University { get; set; }
        public decimal Marks { get; set; }
        public int EducationType { get; set; }

        public virtual UserDetailsModel UserDetail { get; set; }
    }
}
