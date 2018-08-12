using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using APIMVC.Models;

namespace APIMVC.Controllers
{
    //[Authorize]
    public class DESIGNATIONsController : ApiController
    {
        private dbModel db = new dbModel();

        public IEnumerable<ANG_EMPLOYEE> GetANG_EMPLOYEE()
        {
            return db.ANG_EMPLOYEE;
        }

        // GET: api/DESIGNATIONs
        public IEnumerable<DESIGNATION> GetDESIGNATIONs()
        {
            return db.DESIGNATIONs;
        }

        // GET: api/DESIGNATIONs/5
        [ResponseType(typeof(DESIGNATION))]
        public IHttpActionResult GetDESIGNATION(int id)
        {
            DESIGNATION dESIGNATION = db.DESIGNATIONs.Find(id);
            if (dESIGNATION == null)
            {
                return NotFound();
            }

            return Ok(dESIGNATION);
        }

        // PUT: api/DESIGNATIONs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDESIGNATION(int id, DESIGNATION dESIGNATION)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != dESIGNATION.DG_ID)
            {
                return BadRequest();
            }

            db.Entry(dESIGNATION).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DESIGNATIONExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DESIGNATIONs
        [ResponseType(typeof(DESIGNATION))]
        public IHttpActionResult PostDESIGNATION(DESIGNATION dESIGNATION)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.DESIGNATIONs.Add(dESIGNATION);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dESIGNATION.DG_ID }, dESIGNATION);
        }

        // DELETE: api/DESIGNATIONs/5
        [ResponseType(typeof(DESIGNATION))]
        public IHttpActionResult DeleteDESIGNATION(int id)
        {
            DESIGNATION dESIGNATION = db.DESIGNATIONs.Find(id);
            if (dESIGNATION == null)
            {
                return NotFound();
            }

            db.DESIGNATIONs.Remove(dESIGNATION);
            db.SaveChanges();

            return Ok(dESIGNATION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DESIGNATIONExists(int id)
        {
            return db.DESIGNATIONs.Count(e => e.DG_ID == id) > 0;
        }
    }
}