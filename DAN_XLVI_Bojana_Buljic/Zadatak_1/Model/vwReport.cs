//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwReport
    {
        public int ReportId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string FullName { get; set; }
        public System.DateTime ReportDate { get; set; }
        public string Project { get; set; }
        public string Position { get; set; }
        public int WorkHours { get; set; }
    }
}
