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
    using System.ComponentModel.DataAnnotations;

    public partial class Calls_v2
    {
        [Key]
        public int ID { get; set; }
        public string CTRL { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string FAULT_TYPE { get; set; }
        public int RESPONSE_TIME { get; set; }
        public System.DateTime CRDATETIME { get; set; }
        public string TelephoneNumber { get; set; }
        public string Area { get; set; }
        public string Township { get; set; }
        public string Processed { get; set; }
    }
}