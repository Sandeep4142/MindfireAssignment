using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DemoUserManagement.Model;

namespace DemoUserManagement.Utils
{

    public class SessionData
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
    }
}
