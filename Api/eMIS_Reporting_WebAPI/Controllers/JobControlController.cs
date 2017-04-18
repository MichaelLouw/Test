using eMIS_Reporting_WebAPI.DataSource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eMIS_Reporting_WebAPI.Models;
using System.Web.Http.OData;
using ContextGenerator;
using System.Xml.Serialization;
using System.Web.Http.WebHost;
using System.Web.Http.Description;

namespace eMIS_Reporting_WebAPI.Controllers
{
    public class JobControlController : ApiController
    {
        // GET: api/JobControl-
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IQueryable<JobControlModel> GetJobControl()
        {
            //HttpResponseMessage response = new HttpResponseMessage();
            //response.Content = new StringContent(Convert.ToString(JobControlDataSource.Instance("Sewer", "a").JobControlList));

            return null;
        }

        // GET: api/JobControl/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/JobControl
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/JobControl/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/JobControl/5
        //public void Delete(int id)
        //{
        //}
    }
}
