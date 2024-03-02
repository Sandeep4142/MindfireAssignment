using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public class CountryDA
    {
        public static List<CountryModel> GetCountry()
        {
            List<CountryModel> countryList = new List<CountryModel>();
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    return context.Countries.Select(s => new CountryModel
                    {
                        CountryId = s.CountryId,
                        CountryName = s.CountryName,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
            }
            return countryList;
        }
    }
}
