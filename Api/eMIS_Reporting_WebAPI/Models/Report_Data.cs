//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eMIS_Reporting_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report_Data
    {
        public int Control_Number { get; set; }
        public Nullable<System.DateTime> Receive_Date { get; set; }
        public Nullable<System.DateTime> Finish_Date { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Status_Date { get; set; }
        public string Department { get; set; }
        public string Fault { get; set; }
        public string Area { get; set; }
        public Nullable<int> GIS_ID { get; set; }
        public string Township { get; set; }
    }
}
