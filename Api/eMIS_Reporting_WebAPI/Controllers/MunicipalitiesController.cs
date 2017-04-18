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
    builder.EntitySet<Municipality>("Municipalities");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MunicipalitiesController : ODataController
    {
        private eMIS_ReportingEntities db = new eMIS_ReportingEntities();

        // GET: odata/Municipalities
        [EnableQuery]
        public IEnumerable<Municipality> GetMunicipalities()
        {
            try
            {
                return db.Municipalities.AsEnumerable();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // GET: odata/Municipalities(5)
        [EnableQuery]
        public SingleResult<Municipality> GetMunicipality([FromODataUri] int key)
        {
            try
            {
                return SingleResult.Create(db.Municipalities.Where(municipality => municipality.ID == key));
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return null;
        }

        // PUT: odata/Municipalities(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Municipality> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Municipality municipality = await db.Municipalities.FindAsync(key);
        //    if (municipality == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(municipality);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MunicipalityExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(municipality);
        //}

        //// POST: odata/Municipalities
        //public async Task<IHttpActionResult> Post(Municipality municipality)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Municipalities.Add(municipality);
        //    await db.SaveChangesAsync();

        //    return Created(municipality);
        //}

        //// PATCH: odata/Municipalities(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Municipality> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Municipality municipality = await db.Municipalities.FindAsync(key);
        //    if (municipality == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(municipality);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MunicipalityExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(municipality);
        //}

        //// DELETE: odata/Municipalities(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    Municipality municipality = await db.Municipalities.FindAsync(key);
        //    if (municipality == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Municipalities.Remove(municipality);
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

        private bool MunicipalityExists(int key)
        {
            return db.Municipalities.Count(e => e.ID == key) > 0;
        }
    }
}
