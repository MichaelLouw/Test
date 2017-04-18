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

namespace eMIS_Reporting_WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using eMIS_Reporting_WebAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<spJobControl_Result1>("spJobControl_Result1");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class spJobControl_ResultController : ODataController
    {
        private eMIS_MobileEntities db = new eMIS_MobileEntities();

        // GET: odata/spJobControl_Result1
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IEnumerable<spJobControl_Result1> GetspJobControl_Result1([FromODataUri] string Department, [FromODataUri] string Municipality)
        {
            try
            {
                return db.spJobControl(Department, Municipality).AsEnumerable().ToList();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return null;
        }

        // GET: odata/spJobControl_Result1(5)
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IEnumerable<spJobControl_Result1> GetspJobControl_Result1([FromODataUri] string key)
        {
            try
            {
                string k = key.ToString();
                var ReferenceNumber = db.spJobControl(key, "a");
                return ReferenceNumber.AsEnumerable().ToList();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 2);
            }
            return null;
        }

        // PUT: odata/spJobControl_Result1(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<spJobControl_Result1> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    spJobControl_Result1 spJobControl_Result1 = await db.spJobControl_Result1.FindAsync(key);
        //    if (spJobControl_Result1 == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(spJobControl_Result1);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!spJobControl_Result1Exists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(spJobControl_Result1);
        //}

        //// POST: odata/spJobControl_Result1
        //public async Task<IHttpActionResult> Post(spJobControl_Result1 spJobControl_Result1)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.spJobControl_Result1.Add(spJobControl_Result1);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (spJobControl_Result1Exists(spJobControl_Result1.Assigned_unit_Mac))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Created(spJobControl_Result1);
        //}

        //// PATCH: odata/spJobControl_Result1(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<spJobControl_Result1> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    spJobControl_Result1 spJobControl_Result1 = await db.spJobControl_Result1.FindAsync(key);
        //    if (spJobControl_Result1 == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(spJobControl_Result1);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!spJobControl_Result1Exists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(spJobControl_Result1);
        //}

        //// DELETE: odata/spJobControl_Result1(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        //{
        //    spJobControl_Result1 spJobControl_Result1 = await db.spJobControl_Result1.FindAsync(key);
        //    if (spJobControl_Result1 == null)
        //    {
        //        return NotFound();
        //    }

        //    db.spJobControl_Result1.Remove(spJobControl_Result1);
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

        //private bool spJobControl_Result1Exists(string key)
        //{
        //    return db.spJobControl_Result1.Count(e => e.Assigned_unit_Mac == key) > 0;
        //}
    }
}
