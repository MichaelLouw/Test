﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class eMIS_ReportingEntities : DbContext
    {
        public eMIS_ReportingEntities()
            : base("name=eMIS_ReportingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<UserStatu> UserStatus { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<Users> Users1 { get; set; }
    
        public virtual ObjectResult<spGetReports_Result> spGetReports()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetReports_Result>("spGetReports");
        }
    
        public virtual ObjectResult<spGetUserByEmailPassword_Result> spGetUserByEmailPassword(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetUserByEmailPassword_Result>("spGetUserByEmailPassword", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<spSearchReports_Result> spSearchReports(string search)
        {
            var searchParameter = search != null ?
                new ObjectParameter("search", search) :
                new ObjectParameter("search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spSearchReports_Result>("spSearchReports", searchParameter);
        }
    
        public virtual ObjectResult<spGetDepartments_Result> spGetDepartments(string municipalityID)
        {
            var municipalityIDParameter = municipalityID != null ?
                new ObjectParameter("municipalityID", municipalityID) :
                new ObjectParameter("municipalityID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetDepartments_Result>("spGetDepartments", municipalityIDParameter);
        }
    
        public virtual ObjectResult<string> spGetPassword(Nullable<System.Guid> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spGetPassword", idParameter);
        }
    
        public virtual int spInsertNewUser(string name, string surname, string password, string email, string number, string department, string municipality, string userName)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var surnameParameter = surname != null ?
                new ObjectParameter("Surname", surname) :
                new ObjectParameter("Surname", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var numberParameter = number != null ?
                new ObjectParameter("Number", number) :
                new ObjectParameter("Number", typeof(string));
    
            var departmentParameter = department != null ?
                new ObjectParameter("Department", department) :
                new ObjectParameter("Department", typeof(string));
    
            var municipalityParameter = municipality != null ?
                new ObjectParameter("Municipality", municipality) :
                new ObjectParameter("Municipality", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertNewUser", nameParameter, surnameParameter, passwordParameter, emailParameter, numberParameter, departmentParameter, municipalityParameter, userNameParameter);
        }
    
        public virtual int spSaveNewPassword(string newPass, Nullable<System.Guid> id)
        {
            var newPassParameter = newPass != null ?
                new ObjectParameter("NewPass", newPass) :
                new ObjectParameter("NewPass", typeof(string));
    
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spSaveNewPassword", newPassParameter, idParameter);
        }
    
        public virtual int spActivateEmail(Nullable<System.Guid> guid)
        {
            var guidParameter = guid.HasValue ?
                new ObjectParameter("Guid", guid) :
                new ObjectParameter("Guid", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spActivateEmail", guidParameter);
        }
    
        public virtual ObjectResult<spActivateUser_Result> spActivateUser(Nullable<System.Guid> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spActivateUser_Result>("spActivateUser", userIDParameter);
        }
    
        public virtual ObjectResult<spDenyUser_Result> spDenyUser(Nullable<System.Guid> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spDenyUser_Result>("spDenyUser", userIDParameter);
        }
    
        public virtual int spInsertNewUser1(string name, string surname, string password, string email, string number, string department, string municipality, Nullable<int> statusID, Nullable<int> security_Level)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var surnameParameter = surname != null ?
                new ObjectParameter("Surname", surname) :
                new ObjectParameter("Surname", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var numberParameter = number != null ?
                new ObjectParameter("Number", number) :
                new ObjectParameter("Number", typeof(string));
    
            var departmentParameter = department != null ?
                new ObjectParameter("Department", department) :
                new ObjectParameter("Department", typeof(string));
    
            var municipalityParameter = municipality != null ?
                new ObjectParameter("Municipality", municipality) :
                new ObjectParameter("Municipality", typeof(string));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("StatusID", statusID) :
                new ObjectParameter("StatusID", typeof(int));
    
            var security_LevelParameter = security_Level.HasValue ?
                new ObjectParameter("Security_Level", security_Level) :
                new ObjectParameter("Security_Level", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertNewUser1", nameParameter, surnameParameter, passwordParameter, emailParameter, numberParameter, departmentParameter, municipalityParameter, statusIDParameter, security_LevelParameter);
        }
    
        public virtual int spUpdateStatusID(Nullable<System.Guid> userID, Nullable<int> statusID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(System.Guid));
    
            var statusIDParameter = statusID.HasValue ?
                new ObjectParameter("statusID", statusID) :
                new ObjectParameter("statusID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateStatusID", userIDParameter, statusIDParameter);
        }
    
        public virtual ObjectResult<Report> SearchReports(string search)
        {
            var searchParameter = search != null ?
                new ObjectParameter("search", search) :
                new ObjectParameter("search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Report>("SearchReports", searchParameter);
        }
    
        public virtual ObjectResult<Report> SearchReports(string search, MergeOption mergeOption)
        {
            var searchParameter = search != null ?
                new ObjectParameter("search", search) :
                new ObjectParameter("search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Report>("SearchReports", mergeOption, searchParameter);
        }
    
        public virtual ObjectResult<spSearchReports_Result> spSearchReports1(string search)
        {
            var searchParameter = search != null ?
                new ObjectParameter("search", search) :
                new ObjectParameter("search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spSearchReports_Result>("spSearchReports1", searchParameter);
        }
    }
}
