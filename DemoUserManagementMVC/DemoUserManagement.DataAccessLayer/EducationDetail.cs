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
    
    public partial class EducationDetail
    {
        public int EducationId { get; set; }
        public int UserId { get; set; }
        public string Institution { get; set; }
        public string University { get; set; }
        public decimal Marks { get; set; }
        public int EducationType { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
    }
}
