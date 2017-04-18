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
    builder.EntitySet<Department>("Departments");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DepartmentsController : ODataController
    {
        private eMIS_ReportingEntities db = new eMIS_ReportingEntities();

        // GET: odata/Departments
        [EnableQuery]
        public IEnumerable<Department> GetDepartments()
        {
            try
            {
                return db.Departments.AsEnumerable();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
            return null;
        }

        // GET: odata/Departments(5)
        [EnableQuery]
        public IEnumerable<Department> GetDepartment([FromODataUri] int key)
        {
            try
            {
                return db.Departments.Where(department => department.Municipality_ID == key).AsEnumerable();
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 1);
            }
            return null;
        }

        // PUT: odata/Departments(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Department> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Department department = await db.Departments.FindAsync(key);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(department);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartmentExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(department);
        //}

        //// POST: odata/Departments
        //public async Task<IHttpActionResult> Post(Department department)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Departments.Add(department);
        //    await db.SaveChangesAsync();

        //    return Created(department);
        //}

        //// PATCH: odata/Departments(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Department> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Department department = await db.Departments.FindAsync(key);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(department);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartmentExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(department);
        //}

        //// DELETE: odata/Departments(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    Department department = await db.Departments.FindAsync(key);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Departments.Remove(department);
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

        private bool DepartmentExists(int key)
        {
            return db.Departments.Count(e => e.Department_ID == key) > 0;
        }
    }
}
