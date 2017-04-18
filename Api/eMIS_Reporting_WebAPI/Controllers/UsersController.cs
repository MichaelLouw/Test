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
using Utilities;
using System.Configuration;
using System.Net.Mail;
using Microsoft.Owin.Security.OAuth;
using Utilities;

namespace eMIS_Reporting_WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using eMIS_Reporting_WebAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Users>("Users");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UsersController : ODataController
    {
        private eMIS_ReportingEntities db = new eMIS_ReportingEntities();

        // GET: odata/Users
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IQueryable<Users> GetUsers()
        {
            try
            {
                return db.Users1;
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // GET: odata/Users(5)
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IQueryable<Users> GetUsers([FromODataUri] int key)
        {
            try
            {
                return db.Users1.Where(Users1 => Users1.StatusID == key).AsQueryable();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return null;
        }

        [Authorize]
        public IQueryable<Users> GetUsers([FromODataUri] string id)
        {
            try
            {
                return db.Users1.Where(Users1 => Users1.User_ID.ToString() == id);
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return null;
        }

        // PUT: odata/Users(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Users> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Users users = await db.Users1.FindAsync(key);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(users);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(users);
        //}
        
        public IHttpActionResult getUsers([FromODataUri]string userEmail)
        {
            IHttpActionResult response;
            //we want a 303 with the ability to set location
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.Accepted);
            responseMsg.Headers.Add("Location", ConfigurationManager.AppSettings["WebSiteURL"].ToString() + "Login.aspx");// = new Uri(ConfigurationManager.AppSettings["LoginLink"].ToString());
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
                    var user = db.Users1.FirstOrDefault(e => e.Email == userEmail);

                    try
                    {
                        // send email
                        Logger.info("Forgot password send email: " + user.Email, 3);
                        MailMessage emailMessage = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"].ToString());
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
                                SmtpServer.Port = 25;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                                SmtpServer.EnableSsl = false;

                                SmtpServer.Send(emailMessage);
                                Logger.info("Forgot password email sent: " + user.Email, 4);
                            }
                            else
                            {
                                // emailMessage.Body = "Hi " + user.Name + "," + Environment.NewLine
                                //+ Environment.NewLine
                                //+ "Your password was not changed, please contact your administrator or try again.";
                                Logger.error("Error saving new password", 3.1);
                                return InternalServerError();
                            }

                        }
                        catch
                        {
                            //emailMessage.Body = "Hi " + user.Name + "," + Environment.NewLine
                            //    + Environment.NewLine
                            //    + "Your password was not changed, please contact your administrator or try again.";
                            Logger.error("Error saving new password", 3.2);
                            return InternalServerError();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.error(ex.Message, 5);
                        return InternalServerError();
                    }
                }
                catch
                {
                    Logger.info("User does not exist: " + userEmail, 5.1);
                    return InternalServerError();
                }
                return response;
            }
        }

        // POST: odata/Users
        [AllowAnonymous]
        public async Task<IHttpActionResult> Post(Users users)
        {
            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.Accepted);
            responseMsg.Headers.Add("Location", ConfigurationManager.AppSettings["WebSiteURL"].ToString() + "Login.aspx");// = new Uri(ConfigurationManager.AppSettings["LoginLink"].ToString());
            response = ResponseMessage(responseMsg);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // MANUAL ADDING USER
            if (users.Logged_In_User_ID != Guid.Empty)
            {
                // EDIT USER
                if (users.User_ID != Guid.Empty)
                {
                    var dbUser = new Users();
                    var theuserMakingChanges = new Users();

                    dbUser = db.Users1.First(u => u.User_ID == users.User_ID);
                    theuserMakingChanges = db.Users1.First(u => u.User_ID == users.Logged_In_User_ID);
                    
                    Logger.info("EDIT USER " + users.Email + "  the user making changes is: " + theuserMakingChanges.Email, 99999);

                    dbUser.Name = string.IsNullOrWhiteSpace(users.Name) ? dbUser.Name : users.Name;
                    dbUser.Surname = string.IsNullOrWhiteSpace(users.Surname) ? dbUser.Surname : users.Surname;
                    dbUser.Password = string.IsNullOrWhiteSpace(users.Password) ? dbUser.Password : users.Password;
                    dbUser.Security_Level = (users.Security_Level == null) ? dbUser.Security_Level : users.Security_Level;
                    dbUser.Contact_Number = string.IsNullOrWhiteSpace(users.Contact_Number) ? dbUser.Contact_Number : users.Contact_Number;
                    dbUser.Email = string.IsNullOrWhiteSpace(users.Email) ? dbUser.Email : users.Email;
                    dbUser.Department = string.IsNullOrWhiteSpace(users.Department) ? dbUser.Department : users.Department;
                    dbUser.Municipality = string.IsNullOrWhiteSpace(users.Municipality) ? dbUser.Municipality : users.Municipality;
                    dbUser.StatusID = users.StatusID == null ? dbUser.StatusID : users.StatusID;
                    db.SaveChanges();

                    var newUser = db.Users1.FirstOrDefault(u => u.Email == users.Email);
                    return Created(newUser);

                }
                // NEW USER
                else if (users.User_ID == Guid.Empty)
                {
                    try
                    {
                        var theuserMakingChanges = db.Users1.First(u => u.User_ID == users.Logged_In_User_ID);
                        Logger.info("ADDING USER " + users.Email + ": the user making changes had User_ID" + theuserMakingChanges.Email, 99999);
                    }
                    catch
                    {
                        return NotFound();
                    }

                    users.Security_Level = 1;
                    users.StatusID = 3;

                    db.Users1.Add(users);
                    db.SaveChanges();

                    var newUser = db.Users1.FirstOrDefault(u => u.Email == users.Email);
                    return Created(newUser);
                }
            }

            if (users.User_ID == Guid.Empty)
            {
                try
                {
                    Logger.info("Adding User: " + users.Email, 6);
                    users.StatusID = 1;
                    db.Users1.Add(users);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger.error(ex.Message, 6.1);
                    }
                    MailMessage email = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"].ToString());
                    email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                    email.To.Add(users.Email);
                    email.Subject = "eMIS email Activation";
                    SqlHelper sql = new SqlHelper();
                    sql.AddVarCharInputParam("@username", users.Email, 100);
                    sql.AddVarCharInputParam("@password", users.Password, 100);
                    DataSet ds = sql.GetDataSet("spGetUserByEmailPassword", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());

                    try
                    {
                        email.Body = "Thank you for registering for eMIS Reporting" + Environment.NewLine + Environment.NewLine + "Kindly verify your email address by following this link: " +  ConfigurationManager.AppSettings["WebSiteURL"].ToString() + "EmailConfirmationPage.aspx?guid=" + ds.Tables[0].Rows[0][0];
                        SmtpServer.Port = 25;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                        SmtpServer.EnableSsl = false;
                        Logger.info("SendingEmail: " + users.Email, 7);
                        SmtpServer.Send(email);
                        Logger.info("Email sent: " + users.Email, 8);

                        return Created(users);
                    }
                    catch (Exception ex)
                    {
                        Logger.error(ex.Message, 8.1);
                        Logger.info("Email was not sent: " + users.Email, 8.2);
                        responseMsg = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                        response = ResponseMessage(responseMsg);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    Logger.error(ex.Message, 10);
                    Logger.error("Failed to register user", 9);
                    responseMsg = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                    response = ResponseMessage(responseMsg);
                    return response;
                }
            }
            else
            {
                try
                {
                    switch (users.StatusID)
                    {
                        case 0:
                            try
                            {
                                Logger.info("Denying User: " + users.Email, 14);
                                SqlHelper sql = new SqlHelper();
                                sql.AddGuidInputParam("@userID", users.User_ID);
                                DataSet ds = sql.GetDataSet("spDenyUser", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                                Logger.info("User Denied: " + users.Email, 15);
                            }
                            catch (Exception ex)
                            {
                                Logger.error(ex.Message, 15.1);
                                responseMsg = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                                response = ResponseMessage(responseMsg);
                                return response;
                            }
                            break;
                        case 1:
                            try
                            {
                                Logger.info("Activating User: " + users.User_ID, 11);
                                MailMessage email = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"].ToString());
                                email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                                email.Subject = "eMIS user Confirmation";
                                SqlHelper sql = new SqlHelper();
                                sql.AddGuidInputParam("@userID", users.User_ID);
                                DataSet ds = sql.GetDataSet("spActivateUser", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                                email.To.Add(Convert.ToString(ds.Tables[0].Rows[0][6]));
                                email.Body = "Dear " + ds.Tables[0].Rows[0][1] + Environment.NewLine + Environment.NewLine + "Your eMIS_Reporting account is now active and ready for use." + Environment.NewLine + Environment.NewLine + "Please login with your email address and the password you entered on registration." + Environment.NewLine + Environment.NewLine + "Link: " + ConfigurationManager.AppSettings["WebSiteURL"].ToString() + "Login.aspx" + Environment.NewLine + Environment.NewLine + "Kind Regards" + Environment.NewLine + "The EMIS Reporting Team";

                                //System.Net.Mail.Attachment attachment;
                                //attachment = new Attachment(path + Convert.ToString(id) + quoteorinvoice + time + ".pdf");
                                //email.Attachments.Add(attachment);

                                SmtpServer.Port = 25;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                                SmtpServer.EnableSsl = false;
                                Logger.info("Sending email: " + users.Email, 12);
                                SmtpServer.Send(email);
                                Logger.info("Email sent: " + users.Email, 13);
                            }
                            catch (Exception ex)
                            {
                                Logger.error(ex.Message, 13.1);
                                responseMsg = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                                response = ResponseMessage(responseMsg);
                                return response;
                            }
                            break;
                        case 2:
                            try
                            {
                                Logger.info("Confirm email user: " + users.Email, 17.0);
                                var user = db.Users1.FirstOrDefault(u => u.User_ID == users.User_ID);
                                user.StatusID = 2;
                                db.Users1.Attach(user);
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                                Logger.info("Confirming user succeeded: " + users.Email, 18.0);
                            }
                            catch (Exception ex)
                            {
                                Logger.error(ex.Message, 16.99);
                            }
                            break;
                        case 3:
                            try
                            {
                                Logger.info("Activate user: " + users.Email, 17.0);
                                var user = db.Users1.FirstOrDefault(u => u.User_ID == users.User_ID);
                                user.StatusID = 3;
                                db.Users1.Attach(user);
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                                Logger.info("Activating user succeeded: " + users.Email, 18.0);
                                Logger.info("Activating User: " + users.User_ID, 11);
                                MailMessage email = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"].ToString());
                                email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
                                email.Subject = "eMIS user Confirmation";
                                SqlHelper sql = new SqlHelper();
                                sql.AddGuidInputParam("@userID", users.User_ID);
                                DataSet ds = sql.GetDataSet("spActivateUser", ConfigurationManager.ConnectionStrings["eMIS_Reporting"].ToString());
                                email.To.Add(Convert.ToString(ds.Tables[0].Rows[0][6]));
                                email.Body = "Dear " + ds.Tables[0].Rows[0][1] + Environment.NewLine + Environment.NewLine + "Your eMIS_Reporting account is now active and ready for use." + Environment.NewLine + Environment.NewLine + "Please login with your email address and the password you entered on registration." + Environment.NewLine + Environment.NewLine + "Link: " + ConfigurationManager.AppSettings["WebSiteURL"].ToString() + "Login.aspx" + Environment.NewLine + Environment.NewLine + "Kind Regards" + Environment.NewLine + "The EMIS Reporting Team";

                                //System.Net.Mail.Attachment attachment;
                                //attachment = new Attachment(path + Convert.ToString(id) + quoteorinvoice + time + ".pdf");
                                //email.Attachments.Add(attachment);

                                SmtpServer.Port = 25;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmailAddress"].ToString(), ConfigurationManager.AppSettings["FromEmailAddressPassword"].ToString());
                                SmtpServer.EnableSsl = false;
                                Logger.info("Sending email: " + users.Email, 12);
                                SmtpServer.Send(email);
                                Logger.info("Email sent: " + users.Email, 13);
                            }
                            catch (Exception ex)
                            {
                                Logger.error(ex.Message, 16.99);
                            }
                            break;
                        case 4:
                            try
                            {
                                Logger.info("Denying user: " + users.Email, 17.0);
                                var user = db.Users1.FirstOrDefault(u => u.User_ID == users.User_ID);
                                user.StatusID = 4;
                                db.Users1.Attach(user);
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                                Logger.info("Denying user succeeded: " + users.Email, 18.0);
                            }
                            catch(Exception ex)
                            {
                                Logger.error(ex.Message, 16.99);
                            }                            
                            break;
                        case 5:
                            try
                            {
                                Logger.info("Archiving user: " + users.Email, 17.0);
                                var user = db.Users1.FirstOrDefault(u => u.User_ID == users.User_ID);
                                user.StatusID = 5;
                                db.Users1.Attach(user);
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                                Logger.info("Archiving user succeeded: " + users.Email, 18.0);
                            }
                            catch (Exception ex)
                            {
                                Logger.error("Archiving user may have failed:" + ex.Message, 17.1);

                                responseMsg = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                                response = ResponseMessage(responseMsg);
                                return response;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.error(ex.Message, 16);
                }
             }

        return Created(users);
    }

     
        // PATCH: odata/Users(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Users> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Users users = await db.Users1.FindAsync(key);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(users);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(users);
        //}

        //// DELETE: odata/Users(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        //{
        //    Users users = await db.Users1.FindAsync(key);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users1.Remove(users);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool UsersExists(Guid key)
        {
            return db.Users1.Count(e => e.User_ID == key) > 0;
        }

        //[Route("Login")]
        //public async Task<IHttpActionResult> Login([FromUri] string username,[FromUri] string password)
        //{
        //    try
        //    {
        //        using (var context = new App_Start.UsersDBContext())
        //        {
        //            return Ok(context.Users.Where(x => x.Email == username && x.Password == password));
        //        } 
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.error(ex.Message, 17);
        //    }
        //    return null;
        //}
    }
}
