using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Utilities;
using ContextGenerator;
using System.Xml.Serialization;

namespace eMIS_Reporting_WebAPI.Models
{
    //public class JobControl : DbContext
    //{
    //    public DbSet<JobControlModel.JobControlObject[]> O { get; set; }
    //    public JobControl() : base("name=JobControlModel.JobControlObject[]") { }

    //    ContextGenerator.MemoryDbSet<JobControlModel.JobControlObject[]> o = new MemoryDbSet<JobControlModel.JobControlObject[]>(); 
    //}


    public class JobControlModel
    {
        public string Device_Desc { get; set; }
        public int JobsAlocated { get; set; }
        public int JobsReceived { get; set; }
        public string Assigned_unit_Mac { get; set; }
        public string Area { get; set; }
        [Key]
        public int Cntrl_Num { get; set; }
        //[XmlArray("JobControlList")]
        //[XmlArrayItem("Jobs")]
        //public static List<JobControlModel.JobControlObject> JobControlList = new List<JobControlObject>();

        //[XmlArray("obj")]
        //public static JobControlModel.JobControlObject[] obj;

        //public static JobcontrolList GetJobControl(string Department, string Municipality)
        //{
        //    JobcontrolList jobs = new JobcontrolList();
        //    jobs.Listname = "JobControl";
        //    //o.SaveChanges();

        //    int i = 0;

        //    SqlHelper sql = new SqlHelper();
        //    sql.AddVarCharInputParam("@Department", Department, 250);
        //    sql.AddVarCharInputParam("@Municipality", Municipality, 250);
        //    DataSet ds = sql.GetDataSet("spJobControl", WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
        //    obj = new JobControlObject[ds.Tables[0].Rows.Count];
        //    //DataTable JobControlTable = new DataTable();
        //    //JobControlTable.Clear();
        //    //JobControlTable.Columns.Add("Device_Desc", typeof(string));
        //    //JobControlTable.Columns.Add("JobsAlocated", typeof(Int32));
        //    //JobControlTable.Columns.Add("JobsReceived", typeof(Int32));
        //    //JobControlTable.Columns.Add("Assigned_unit_Mac", typeof(string));
        //    //JobControlTable.Columns.Add("Area", typeof(string));
        //    //JobControlTable.Columns.Add("Cntrl_Num", typeof(Int32));
        //    try
        //    {
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                int jobsalocated, jobsreceived, cntrl_num;
        //                jobsalocated = 0;
        //                jobsreceived = 0;
        //                cntrl_num = 0;
        //                int.TryParse(dr["JobsAlocated"].ToString(), out jobsalocated);
        //                int.TryParse(dr["JobsReceived"].ToString(), out jobsreceived);
        //                int.TryParse(dr["Cntrl_Num"].ToString(), out cntrl_num);
        //                //JobControlObject JobControl = new JobControlObject(dr["Device_Desc"].ToString(), jobsalocated, jobsreceived, dr["Assigned_unit_Mac"].ToString(), dr["Area"].ToString(), cntrl_num);
        //                //JobControlList.Add(JobControl);
        //                //obj[i] = new JobControlObject(dr["Device_Desc"].ToString(), jobsalocated, jobsreceived, dr["Assigned_unit_Mac"].ToString(), dr["Area"].ToString(), cntrl_num);

        //                jobs.AddJob(new JobControlObject(dr["Device_Desc"].ToString(), jobsalocated, jobsreceived, dr["Assigned_unit_Mac"].ToString(), dr["Area"].ToString(), cntrl_num));
        //                //i++;


        //                //DataRow datarow = JobControlTable.NewRow();
        //                //datarow["Device_Desc"] = dr["Device_Desc"].ToString();
        //                //datarow["JobsAlocated"] = jobsalocated;
        //                //datarow["JobsReceived"] = jobsreceived;
        //                //datarow["Assigned_unit_Mac"] = dr["Assigned_unit_Mac"].ToString();
        //                //datarow["Area"] = dr["Area"].ToString();
        //                //datarow["Cntrl_Num"] = cntrl_num;
        //                //JobControlTable.Rows.Add(datarow);
        //            }
        //        }
        //        //o.Add(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.error(ex.Message, 0);
        //    }

        //    return jobs;
        //}

        //[XmlRoot("JobControlList")]
        //[XmlInclude(typeof(JobControlObject))]
        //public class JobcontrolList
        //{
        //    [XmlArray("JobControlArray")]
        //    [XmlArrayItem("JobControlObject")]
        //    public List<JobControlObject> JobObject = new List<JobControlObject>();

        //    [XmlElement("Listname")]
        //    public string Listname { get; set; }

        //    public JobcontrolList() { }

        //    public JobcontrolList(string name)
        //    {
        //        this.Listname = name;
        //    }

        //    public void AddJob(JobControlObject jobcontrolobject)
        //    {
        //        JobObject.Add(jobcontrolobject);
        //    }
        //}

        //[XmlType("JobControlObject")]
        //public class JobControlObject
        //{
        //    [XmlElement("Device_Desc")]
        //    public string Device_Desc { get; set; }
        //    [XmlElement("JobsAlocated")]
        //    public int JobsAlocated { get; set; }
        //    [XmlElement("JobsReceived")]
        //    public int JobsReceived { get; set; }
        //    [XmlElement("Assigned_unit_Mac")]
        //    public string Assigned_unit_Mac { get; set; }
        //    [XmlElement("Area")]
        //    public string Area { get; set; }
        //    [XmlElement("Cntrl_Num")]
        //    public int Cntrl_Num { get; set; }

        //    public JobControlObject(string device_desc, int jobsalocated, int jobsreceived, string assigned_unit_mac, string area, int cntrl_num)
        //    {
        //        Device_Desc = device_desc;
        //        JobsAlocated = jobsalocated;
        //        JobsReceived = jobsreceived;
        //        Assigned_unit_Mac = assigned_unit_mac;
        //        Area = area;
        //        Cntrl_Num = cntrl_num;
        //    }
        //}
    }
}