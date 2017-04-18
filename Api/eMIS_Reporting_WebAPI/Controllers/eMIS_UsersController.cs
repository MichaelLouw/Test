using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using eMIS_Reporting_WebAPI.Models;
using eMIS_Reporting_WebAPI.HelperMethods;
using Utilities;
using System.Configuration;
using System.Net.Mail;

namespace eMIS_Reporting_WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using eMIS_Reporting_WebAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<eMIS_Users>("eMIS_Users");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class eMIS_UsersController : ODataController
    {
        private eMIS_ReportingEntities db = new eMIS_ReportingEntities();

        // GET: odata/eMIS_Users
        [EnableQuery]
        public IQueryable<eMIS_Users> GeteMIS_Users()
        {
            try
            {
                return db.eMIS_Users;
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // GET: odata/eMIS_Users(5)
        [EnableQuery]
        public IQueryable<eMIS_Users> GeteMIS_Users([FromODataUri] int key)
        {
            return db.eMIS_Users.Where(eMIS_Users => eMIS_Users.StatusID == key).AsQueryable();
        }

        // PUT: odata/eMIS_Users(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<eMIS_Users> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            eMIS_Users eMIS_Users = await db.eMIS_Users.FindAsync(key);
            if (eMIS_Users == null)
            {
                return NotFound();
            }

            patch.Put(eMIS_Users);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eMIS_UsersExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eMIS_Users);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ForgotPassword([FromUri]string userEmail)
        {
            IHttpActionResult response;
            //we want a 303 with the ability to set location
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.Accepted);
            responseMsg.Headers.Add("Location", ConfigurationManager.AppSettings["LoginLink"].ToString());// = new Uri(ConfigurationManager.AppSettings["LoginLink"].ToString());
            response = ResponseMessage(responseMsg);
            //return response;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var user = db.eMIS_Users.FirstOrDefault(e => e.Email == userEmail);

                    try
                    {
                        // send email
                        MailMessage emailMessage = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("mail.software-solutions.co.za");
                        emailMessage.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                        emailMessage.To.Add(user.Email);
                        emailMessage.Subject = "eMIS Password Reset";
                        SqlHelper sql = new SqlHelper();
                        sql.AddVarCharInputParam("@username", user.Email, 100);
                        sql.AddVarCharInputParam("@password", user.Password, 100);
                        DataSet ds = sql.GetDataSet("spGetUserByEmailPassword", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());

                        // Body
                        //emailMessage.Body = "Hi " + user.Name + "," + Environment.NewLine
                        //    + Environment.NewLine
                        //    + "Click on the link below to reset your password: " + Environment.NewLine +
                        //    Environment.NewLine
                        //    + ConfigurationManager.AppSettings["ResetPasswordPage"].ToString() + "?userId=" + user.User_ID.ToString();

                        var newPass = System.Web.Security.Membership.GeneratePassword(10, 4);

                        //TODO: change password in db
                        user.Password = newPass;

                        try
                        {
                            var e = db.spSaveNewPassword(newPass, user.User_ID);

                            if (e == -1)
                            {
                                emailMessage.Body = "Hi " + user.Name + "," + Environment.NewLine
                               + Environment.NewLine
                               + "Your new password is: " + Environment.NewLine +
                               Environment.NewLine
                               + newPass;
                            }
                            else
                            {
                                emailMessage.Body = "Hi " + user.Name + "," + Environment.NewLine
                               + Environment.NewLine
                               + "Your password was not changed, please contact your administrator or try again.";
                                return InternalServerError();
                            }

                        }
                        catch
                        {
                            emailMessage.Body = "Hi " + user.Name + "," + Environment.NewLine
                                + Environment.NewLine
                                + "Your password was not changed, please contact your administrator or try again.";
                            return InternalServerError();
                        }

                        //System.Net.Mail.Attachment attachment;
                        //attachment = new Attachment(path + Convert.ToString(id) + quoteorinvoice + time + ".pdf");
                        //email.Attachments.Add(attachment);

                        SmtpServer.Port = 25;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                        SmtpServer.EnableSsl = false;

                        SmtpServer.Send(emailMessage);
                    }
                    catch (Exception ex)
                    {
                        Logger.error(ex.Message, 0);
                        return InternalServerError();
                    }
                }
                catch
                {
                    return InternalServerError();
                }
                return response;
            }
        }

        // POST: odata/eMIS_Users
        [HttpPost]
        public async Task<IHttpActionResult> Post(eMIS_Users eMIS_Users)
        {
            IHttpActionResult response;
            //we want a 303 with the ability to set location
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.Accepted);
            responseMsg.Headers.Add("Location", ConfigurationManager.AppSettings["LoginLink"].ToString());// = new Uri(ConfigurationManager.AppSettings["LoginLink"].ToString());
            response = ResponseMessage(responseMsg);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (eMIS_Users.User_ID == Guid.Empty)
                {
                    try
                    {
                        db.eMIS_Users.Add(eMIS_Users);
                        await db.SaveChangesAsync();
                        MailMessage email = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("mail.software-solutions.co.za");
                        email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                        email.To.Add(eMIS_Users.Email);
                        email.Subject = "eMIS email Activation";
                        SqlHelper sql = new SqlHelper();
                        sql.AddVarCharInputParam("@username", eMIS_Users.Email, 100);
                        sql.AddVarCharInputParam("@password", eMIS_Users.Password, 100);
                        DataSet ds = sql.GetDataSet("spGetUserByEmailPassword", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                        email.Body = "Thank you for registering for eMIS Reporting" + Environment.NewLine + Environment.NewLine + "Kindly verify your email address by following this link: " + ConfigurationManager.AppSettings["EmailConfirmLink"].ToString() + "?email=" + ds.Tables[0].Rows[0][0] + "&" + "link=" + ConfigurationManager.AppSettings["EmailConfirm"].ToString();

                        //System.Net.Mail.Attachment attachment;
                        //attachment = new Attachment(path + Convert.ToString(id) + quoteorinvoice + time + ".pdf");
                        //email.Attachments.Add(attachment);

                        SmtpServer.Port = 25;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                        SmtpServer.EnableSsl = false;

                        SmtpServer.Send(email);
                    }
                    catch (Exception ex)
                    {
                        Logger.error(ex.Message, 0);
                    }
                }
                else
                {
                    try
                    {
                        if (eMIS_Users.StatusID == 1)
                        {
                            MailMessage email = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("mail.software-solutions.co.za");
                            email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                            email.Subject = "eMIS user Confirmation";
                            SqlHelper sql = new SqlHelper();
                            sql.AddGuidInputParam("@userID", eMIS_Users.User_ID);
                            DataSet ds = sql.GetDataSet("spActivateUser", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                            email.To.Add(Convert.ToString(ds.Tables[0].Rows[0][6]));
                            email.Body = "Dear " + ds.Tables[0].Rows[0][1] + Environment.NewLine + Environment.NewLine + "Yoour eMIS_Reporting account is now active and ready for use." + Environment.NewLine + Environment.NewLine + "Please login with your email address and the password you enetered when requesting the registration." + Environment.NewLine + Environment.NewLine + "Link: " + ConfigurationManager.AppSettings["LoginLink"].ToString() + Environment.NewLine + Environment.NewLine + "Kind Regards" + Environment.NewLine + "The EMIS Reporting Team";

                            //System.Net.Mail.Attachment attachment;
                            //attachment = new Attachment(path + Convert.ToString(id) + quoteorinvoice + time + ".pdf");
                            //email.Attachments.Add(attachment);

                            SmtpServer.Port = 25;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                            SmtpServer.EnableSsl = false;

                            SmtpServer.Send(email);
                        }
                        else
                        {
                            SqlHelper sql = new SqlHelper();
                            sql.AddGuidInputParam("@userID", eMIS_Users.User_ID);
                            DataSet ds = sql.GetDataSet("spDenyUser", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.error(ex.Message, 0);
                    }
                }
                //var username = bO_User.Name + bO_User.Surname[0];
                //bO_User.User_Name = username;

                //db.BO_User.Add(bO_User);
                //await db.SaveChangesAsync();
                //MailMessage email = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("mail.software-solutions.co.za");
                //email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                //email.To.Add(bO_User.Email);
                //email.Subject = "eMIS Activation";
                //SqlHelper sql = new SqlHelper();
                //sql.AddVarCharInputParam("@username", username, 100);
                //sql.AddVarCharInputParam("@password", bO_User.Password, 100);
                //DataSet ds = sql.GetDataSet("spGetUserByEmailPassword", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                //email.Body = "Welcome to emis follow the link to activate your email" + Environment.NewLine + Environment.NewLine + "http://localhost:63416/EmailConfirmationPage.html?email=" + ds.Tables[0].Rows[0][0] + "&" + "link=" + ConfigurationManager.AppSettings["EmailConfirm"].ToString();

                ////System.Net.Mail.Attachment attachment;
                ////attachment = new Attachment(path + Convert.ToString(id) + quoteorinvoice + time + ".pdf");
                ////email.Attachments.Add(attachment);

                //SmtpServer.Port = 25;
                //SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                //SmtpServer.EnableSsl = false;

                //SmtpServer.Send(email);
            }
            return Created(eMIS_Users);
        }

        // PATCH: odata/eMIS_Users(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<eMIS_Users> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            eMIS_Users eMIS_Users = await db.eMIS_Users.FindAsync(key);
            if (eMIS_Users == null)
            {
                return NotFound();
            }

            patch.Patch(eMIS_Users);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eMIS_UsersExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eMIS_Users);
        }

        // DELETE: odata/eMIS_Users(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            eMIS_Users eMIS_Users = await db.eMIS_Users.FindAsync(key);
            if (eMIS_Users == null)
            {
                return NotFound();
            }

            db.eMIS_Users.Remove(eMIS_Users);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool eMIS_UsersExists(Guid key)
        {
            return db.eMIS_Users.Count(e => e.User_ID == key) > 0;
        }
    }
}
