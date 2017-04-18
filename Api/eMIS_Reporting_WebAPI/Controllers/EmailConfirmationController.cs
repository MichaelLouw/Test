using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eMIS_Reporting_WebAPI.Models;
using Utilities;
using System.Configuration;

namespace eMIS_Reporting_WebAPI.Controllers
{
    public class EmailConfirmationController : ApiController
    {
        private eMIS_ReportingEntities db = new eMIS_ReportingEntities();
        // GET: api/EmailConfirmation
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EmailConfirmation/5
        [HttpGet]
        public bool Get([FromUri] string id)
        {
            try
            {
                bool res = BO_UserExists(id);
                return BO_UserExists(id);
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return false;
        }

        // POST: api/EmailConfirmation
        [HttpPost]
        public string Post([FromUri]string value)
        {
            try
            {
                var currentStatusID = db.Users1.Where(a => a.User_ID == new Guid(value)).ToList();
                if (currentStatusID[0].StatusID == 3)
                {
                    //do nothing.
                }
                else
                {
                    Logger.info("Confirming email: " + value, 2);
                    SqlHelper sql = new SqlHelper();
                    sql.AddGuidInputParam("@Guid", new Guid(value));
                    sql.ExecuteNonQuery("spActivateEmail", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                    Logger.info("Email Confirmed: " + value, 3);
                }
                return "Success";
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 4);
            }
            return "Error";
        }

        // PUT: api/EmailConfirmation/5
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EmailConfirmation/5
        [Authorize]
        public void Delete(int id)
        {

        }

        [Authorize]
        private bool BO_UserExists(string key)
        {
            var user = db.Users1.FirstOrDefault(a => a.Email == key);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
