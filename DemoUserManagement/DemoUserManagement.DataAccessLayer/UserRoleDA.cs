using System;
using System.Collections.Generic;
using System.Linq;
using DemoUserManagement.Utils;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public static class UserRoleDA
    {
        public static bool CheckIfUserIsAdmin(int userID)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var userRoles = context.UserRoles.Where(u => u.UserId == userID);
                    foreach (var userRole in userRoles)
                    {
                        var role = context.Roles.FirstOrDefault(r => r.RoleId == userRole.RoleId);
                        if (role != null && role.IsAdmin == true)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
    }
}
