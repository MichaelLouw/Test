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

    public partial class Tabi_Data
    {
        public Nullable<int> Agent_ID { get; set; }
        public string Ani { get; set; }
        public Nullable<System.DateTime> Call_Date { get; set; }
        public string Call_ID { get; set; }
        public string Campaign { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> dnis { get; set; }
        public Nullable<int> Duration { get; set; }
        public string End_Reason { get; set; }
        public string Memo { get; set; }
        public Nullable<int> Queue { get; set; }
        public Nullable<int> Status_Code { get; set; }
        public string Status_Detail { get; set; }
        public string Status_Group { get; set; }
        public string Status_Text { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Key]
        public int Row_ID { get; set; }
    }
}