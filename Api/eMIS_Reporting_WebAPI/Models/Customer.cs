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
    
    public partial class Customer
    {
        public int RECNUM { get; set; }
        public int Customer_Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone_Number { get; set; }
        public string Fax_Number { get; set; }
        public string EMail_Address { get; set; }
        public decimal Credit_Limit { get; set; }
        public decimal Purchases { get; set; }
        public decimal Balance { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}