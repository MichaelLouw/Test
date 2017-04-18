using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eMIS_Reporting_WebAPI.Models;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using ContextGenerator;

namespace eMIS_Reporting_WebAPI.DataSource
{
    public class JobControlDataSource
    {
        private static JobControlDataSource instance = null;
        public static string Dep = "";
        public static string Mun = "";
        public static JobControlDataSource Instance(string Department, string Municipality)
        {
            
            if (instance == null)
            {
                Dep = Department;
                Mun = Municipality;
                instance = new JobControlDataSource();
            }
            return instance;
        }
        public JobControlModel JobControlList { get; set; }

        //private JobControlDataSource()
        //{
        //    //Reset();
        //    Initialize();
        //}
        //public void Reset()
        //{
        //    JobControlList = new DbSet<JobControlModel.JobControlObject>().AsNoTracking();
        //}
        //public void Initialize()
        //{
        //    JobControlList = JobControlModel;
        //}
    }
}