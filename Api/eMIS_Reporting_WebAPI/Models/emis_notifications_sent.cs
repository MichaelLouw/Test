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

    public partial class emis_notifications_sent
    {
        [Key]
        public int ID { get; set; }
        public string cntrl_num { get; set; }
        public Nullable<System.DateTime> DateTimeSent { get; set; }
    }
}
