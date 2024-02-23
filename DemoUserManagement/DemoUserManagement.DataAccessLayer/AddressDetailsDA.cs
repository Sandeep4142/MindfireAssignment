using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public class AddressDetailsDA
    {
        public static void SaveAddressDetails(AddressModel address)
        {
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    // Check if an address exists for the given UserID
                    var existingAddress = context.Addresses.FirstOrDefault(a => a.UserID == address.UserID && a.AddressType == address.AddressType);

                    if (existingAddress == null)
                    {
                        // If no address exists for the given UserID, create a new address entity
                        int userId = UserDetailsDA.GetLastUserId();
                        var newAddress = new Address
                        {
                            UserID = userId, //(int)address.UserID,
                            Locality = address.Locality,
                            City = address.City,
                            CountryId = address.CountryId,
                            StateId = address.StateId,
                            Pincode = address.Pincode,
                            AddressType = (int)address.AddressType
                        };

                        // Add the new address entity to the context
                        context.Addresses.Add(newAddress);
                    }
                    else
                    {
                        // If an address exists for the given UserID, update its properties
                        existingAddress.Locality = address.Locality;
                        existingAddress.City = address.City;
                        existingAddress.CountryId = address.CountryId;
                        existingAddress.StateId = address.StateId;
                        existingAddress.Pincode = address.Pincode;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
            }
        }


        public static (List<AddressModel> currentAddress, List<AddressModel> permanentAddresses) GetAddressDetails(int id)
        {
            
            try
            {
                using (var context = new DemoUserManagementEntities())
                {
                    var currentAddresses = context.Addresses
                        .Where(a => a.UserID == id && a.AddressType == (int)ObjectTypes.ObjectType.CurrentAddress)
                        .Select(address => new AddressModel
                        {
                            UserID = address.UserID,
                            Locality = address.Locality,
                            City = address.City,
                            CountryId = address.CountryId,
                            StateId = address.StateId,
                            Pincode = address.Pincode,
                            AddressType = address.AddressType
                        }).ToList();

                    var permanentAddresses = context.Addresses
                        .Where(a => a.UserID == id && a.AddressType == (int)ObjectTypes.ObjectType.PermanentAddress)
                        .Select(address => new AddressModel
                        {
                            UserID = address.UserID,
                            Locality = address.Locality,
                            City = address.City,
                            CountryId = address.CountryId,
                            StateId = address.StateId,
                            Pincode = address.Pincode,
                            AddressType = address.AddressType
                        }).ToList();

                    return (currentAddresses, permanentAddresses);
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return (null,null);
            }
        }
        /////////////////
        /////////////////

        
    }
}
