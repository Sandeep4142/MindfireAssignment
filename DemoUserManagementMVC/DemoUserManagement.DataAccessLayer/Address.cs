//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoUserManagement.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Pincode { get; set; }
        public int AddressType { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
    }
}