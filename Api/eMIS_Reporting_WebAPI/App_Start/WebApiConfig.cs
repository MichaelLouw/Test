using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using eMIS_Reporting_WebAPI.Models;
using System.Web.Http.Cors;
using ContextGenerator;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace eMIS_Reporting_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "eMIS_Reporting",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<eMis_Mobile>("eMIS_Mobile");
            builder.EntitySet<Report>("Reports");
            builder.EntitySet<SQL_DASH>("SQL_DASH");
            //builder.EntitySet<JobControlModel.JobControlObject>("JobControl");
            //builder.EntitySet<List<JobControlModel.JobControlObject>>("JobController");
            builder.EntitySet<spJobControl_Result1>("spJobControl_Result");
            builder.EntitySet<Municipality>("Municipalities");
            builder.EntitySet<spGetDepartments_Result>("spGetDepartments_Result");
            builder.EntitySet<Department>("Departments");
            builder.EntitySet<Users>("Users");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
