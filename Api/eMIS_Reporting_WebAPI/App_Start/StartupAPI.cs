using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using eMIS_WebAPI.Providers;
using System.Web;
using eMIS_Reporting_WebAPI;
using System.Web.Http.Cors;
using eMIS_Reporting.Interceptor;

[assembly: OwinStartup(typeof(eMIS_WebAPI.StartupAPI))]

namespace eMIS_WebAPI
{
    public class StartupAPI
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            HttpConfiguration config = new HttpConfiguration();
            System.Data.Entity.Infrastructure.Interception.DbInterception.Add(new EntityLogging());
            //WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
