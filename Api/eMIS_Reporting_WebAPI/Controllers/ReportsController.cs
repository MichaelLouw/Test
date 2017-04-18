using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using eMIS_Reporting_WebAPI.Models;
using Utilities;
using System.Web.Http.Cors;
using System.Data.SqlClient;

namespace eMIS_Reporting_WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using eMIS_Reporting_WebAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Report>("Reports");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ReportsController : ODataController
    {
        private eMIS_ReportingEntities db = new eMIS_ReportingEntities();

        // GET: odata/Reports
        [Authorize]
        [EnableQuery]
        public IQueryable<Report> GetReports()
        {
            try
            {
                return db.Reports.AsQueryable();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        [Authorize]
        [EnableQuery]
        public IEnumerable<Report> GetReport([FromODataUri] string key)
        {
            try
            {
                var result = db.SearchReports(key.ToString()).AsEnumerable().ToList();
                return result;
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return null;
        }

        // GET: odata/Reports(5)
        //[EnableQuery]
        //public SingleResult<Report> GetReport([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Reports.Where(report => report.reportId == key));
        //}

        // PUT: odata/Reports(5)
        //public IHttpActionResult Put([FromODataUri] int key, Delta<Report> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Report report = db.Reports.Find(key);
        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(report);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReportExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(report);
        //}

        //// POST: odata/Reports
        //public IHttpActionResult Post(Report report)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Reports.Add(report);
        //    db.SaveChanges();

        //    return Created(report);
        //}

        //// PATCH: odata/Reports(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public IHttpActionResult Patch([FromODataUri] int key, Delta<Report> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Report report = db.Reports.Find(key);
        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(report);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReportExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(report);
        //}

        //// DELETE: odata/Reports(5)
        //public IHttpActionResult Delete([FromODataUri] int key)
        //{
        //    Report report = db.Reports.Find(key);
        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Reports.Remove(report);
        //    db.SaveChanges();

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

        private bool ReportExists(int key)
        {
            return db.Reports.Count(e => e.reportId == key) > 0;
        }
    }
}
