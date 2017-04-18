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

namespace eMIS_Reporting_WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using eMIS_Reporting_WebAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<SQL_DASH>("SQL_DASH");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SQL_DASHController : ODataController
    {
        private WMSEntities db = new WMSEntities();

        // GET: odata/SQL_DASH
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IEnumerable<SQL_DASH> GetSQL_DASH()
        {
            try
            {
                return db.SQL_DASH.ToList();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // GET: odata/SQL_DASH(5)
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IEnumerable<SQL_DASH> GetSQL_DASH([FromODataUri] decimal key)
        {
            try
            {
                string k = key.ToString();
                var ReferenceNumber = db.SQL_DASH.Where(Reference => Reference.CTRL.ToString().Contains(k));
                return ReferenceNumber.Cast<SQL_DASH>().AsEnumerable().ToList();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 2);
            }
            return null;
        }

        // PUT: odata/SQL_DASH(5)
        //public IHttpActionResult Put([FromODataUri] decimal key, Delta<SQL_DASH> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    SQL_DASH sQL_DASH = db.SQL_DASH.Find(key);
        //    if (sQL_DASH == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(sQL_DASH);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SQL_DASHExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(sQL_DASH);
        //}

        //// POST: odata/SQL_DASH
        //public IHttpActionResult Post(SQL_DASH sQL_DASH)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.SQL_DASH.Add(sQL_DASH);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SQL_DASHExists(sQL_DASH.CTRL))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Created(sQL_DASH);
        //}

        //// PATCH: odata/SQL_DASH(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public IHttpActionResult Patch([FromODataUri] decimal key, Delta<SQL_DASH> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    SQL_DASH sQL_DASH = db.SQL_DASH.Find(key);
        //    if (sQL_DASH == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(sQL_DASH);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SQL_DASHExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(sQL_DASH);
        //}

        //// DELETE: odata/SQL_DASH(5)
        //public IHttpActionResult Delete([FromODataUri] decimal key)
        //{
        //    SQL_DASH sQL_DASH = db.SQL_DASH.Find(key);
        //    if (sQL_DASH == null)
        //    {
        //        return NotFound();
        //    }

        //    db.SQL_DASH.Remove(sQL_DASH);
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

        private bool SQL_DASHExists(decimal key)
        {
            return db.SQL_DASH.Count(e => e.CTRL == key) > 0;
        }
    }
}
