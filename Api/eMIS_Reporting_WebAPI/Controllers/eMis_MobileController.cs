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
    builder.EntitySet<eMis_Mobile>("eMis_Mobile");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class eMis_MobileController : ODataController
    {
        private eMIS_MobileEntities db = new eMIS_MobileEntities();

        // GET: odata/eMis_Mobile
        [Authorize]
        [EnableQuery(PageSize = 20)]
        public IQueryable<eMis_Mobile> GeteMis_Mobile()
        {
            try
            {
                return db.eMis_Mobile;
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // GET: odata/eMis_Mobile(5)
        [Authorize]
        [EnableQuery]
        public SingleResult<eMis_Mobile> GeteMis_Mobile([FromODataUri] int key)
        {
            try
            {
                return SingleResult.Create(db.eMis_Mobile.Where(eMis_Mobile => eMis_Mobile.Cntrl_num == key));
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // PUT: odata/eMis_Mobile(5)
        //public IHttpActionResult Put([FromODataUri] int key, Delta<eMis_Mobile> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    eMis_Mobile eMis_Mobile = db.eMis_Mobile.Find(key);
        //    if (eMis_Mobile == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(eMis_Mobile);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!eMis_MobileExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(eMis_Mobile);
        //}

        //// POST: odata/eMis_Mobile
        //public IHttpActionResult Post(eMis_Mobile eMis_Mobile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.eMis_Mobile.Add(eMis_Mobile);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (eMis_MobileExists(eMis_Mobile.Cntrl_num))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Created(eMis_Mobile);
        //}

        //// PATCH: odata/eMis_Mobile(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public IHttpActionResult Patch([FromODataUri] int key, Delta<eMis_Mobile> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    eMis_Mobile eMis_Mobile = db.eMis_Mobile.Find(key);
        //    if (eMis_Mobile == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(eMis_Mobile);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!eMis_MobileExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(eMis_Mobile);
        //}

        //// DELETE: odata/eMis_Mobile(5)
        //public IHttpActionResult Delete([FromODataUri] int key)
        //{
        //    eMis_Mobile eMis_Mobile = db.eMis_Mobile.Find(key);
        //    if (eMis_Mobile == null)
        //    {
        //        return NotFound();
        //    }

        //    db.eMis_Mobile.Remove(eMis_Mobile);
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

        private bool eMis_MobileExists(int key)
        {
            return db.eMis_Mobile.Count(e => e.Cntrl_num == key) > 0;
        }
    }
}
