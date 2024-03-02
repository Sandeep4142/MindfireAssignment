using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public class StateDA
    {
        public static List<StateModel> GetState()
        {
            List<StateModel> stateList = new List<StateModel>();
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    return context.States.Select(s => new StateModel
                    {
                        StateId=s.StateId,
                        StateName = s.StateName,
                        CountryId = s.CountryId,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
            }
            return stateList;
        }
    }
}
