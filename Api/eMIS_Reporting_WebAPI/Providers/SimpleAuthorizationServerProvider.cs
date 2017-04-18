using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using static eMIS_WebAPI.Models.AuthContext;
using eMIS_WebAPI.Models;
using System.Data.Entity;
using eMIS_Reporting_WebAPI.Models;
using System.Collections;
using Microsoft.Owin.Security;
using Utilities;

namespace eMIS_WebAPI.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                using (eMIS_Reporting_WebAPI.Models.eMIS_ReportingEntities _repo = new eMIS_Reporting_WebAPI.Models.eMIS_ReportingEntities())
                {
                    List<Users> user = _repo.Users1.Where(x => x.Email == context.UserName).ToList();

                    if (user[0].Password != context.Password || user[0].Email != context.UserName || user[0].StatusID != 3)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }
                }

                eMIS_Reporting_WebAPI.Models.eMIS_ReportingEntities AddClaimDB = new eMIS_Reporting_WebAPI.Models.eMIS_ReportingEntities();

                List<Users> UserFROMDB = AddClaimDB.Users1.Where(x => x.Email == context.UserName).ToList();

                //string[] ReturnUser = ((IEnumerable)UserFROMDB).Cast<Users>().Select(x => x.ToString()).ToArray();

                //context.OwinContext.Response.Headers.Add("User_Data", new[] { UserFROMDB[0].Security_Level.ToString(), UserFROMDB[0].Name, UserFROMDB[0].Surname});

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "Security_Level", UserFROMDB[0].Security_Level.ToString()
                    },
                    {
                        "Name", UserFROMDB[0].Name.ToString()
                    },
                    {
                        "Surname", UserFROMDB[0].Surname.ToString()
                    },
                    {
                        "User_ID", UserFROMDB[0].User_ID.ToString()
                    }
                });

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                var ticket = new AuthenticationTicket(identity, props);

                context.Validated(ticket);
            }
            catch(Exception ex)
            {
                Logger.error("No User Found", 0);
                throw ex;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}