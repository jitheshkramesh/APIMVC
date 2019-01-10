using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using EFMDataAccessModel;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace APIMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PayrollAdjustmentController : ApiController
    {
        OVODEntities DB = new OVODEntities();
        public PayrollAdjustmentController()
        {
            DB.Configuration.ProxyCreationEnabled = false;
        }

        [HttpPost]
        [Route("api/HD_HRPAYADJs")]
        [ResponseType(typeof(HD_HRPAYADJ))]
        public IHttpActionResult PostHD_HRPAYADJ(HD_HRPAYADJ DT_HRPAYADJ) {
            try
            {
                if (DT_HRPAYADJ.PA_ID == 0)
                    DB.HD_HRPAYADJ.Add(DT_HRPAYADJ);
                else
                    //DB.Entry(DT_HRPAYADJ).State = EntityState.Modified;
                    DB.Set<HD_HRPAYADJ>().AddOrUpdate(DT_HRPAYADJ);
                foreach (var item in DT_HRPAYADJ.DT_HRPAYADJ)
                {
                    if (item.PAD_ID == 0)
                        DB.DT_HRPAYADJ.Add(item);
                    else
                        DB.Entry(item).State = EntityState.Modified;
                }
                foreach (var id in DT_HRPAYADJ.DeletedPayAdjdIds.Split(',').Where(x => x != ""))
                {
                    DT_HRPAYADJ x = DB.DT_HRPAYADJ.Find(Convert.ToInt64(id));
                    DB.DT_HRPAYADJ.Remove(x);
                }
                DB.SaveChanges();
                //return CreatedAtRoute("DefaultApi", new { id = DT_HRPAYADJ.PA_ID }, DT_HRPAYADJ);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("api/HD_HRPAYADJs")]
        public System.Object GetHD_HRPAYADJs()
        {
            return DB.HD_HRPAYADJ.ToList();
        }

        [HttpGet]
        [Route("api/HD_HRPAYADJs/{PA_ID:int}")]
        //[Route("{id:int}",Name = "api/PayrollAdjustmentDetails")]
        [ResponseType(typeof(HD_HRPAYADJ))]
        public IHttpActionResult PayrollAdjustmentDetails(long PA_ID) {
            var PayadjustHeader = (from a in DB.HD_HRPAYADJ
                           where a.PA_ID == PA_ID
                           select new
                           {
                               a.PA_ID,
                               a.PA_DOCNO,
                               a.PA_DOCDATE,
                               a.PA_YEAR,
                               a.PA_MONTH,
                               a.PA_HEADER,
                               a.PA_TITLE,
                               DeletedPayAdjdIds=""
                           }).FirstOrDefault();

            var payadjustmentDetails = (from a in DB.DT_HRPAYADJ
                                  join b in DB.TTMASTs on a.TT_ID equals b.TT_ID
                                  join c in DB.ANG_EMPLOYEE on a.EM_ID equals c.id
                                  where a.PA_ID == PA_ID
                                  select new {
                                      a.PAD_ID,
                                      a.PA_ID,
                                      a.EM_ID,
                                      EM_NAME = c.name,
                                      a.TT_ID,
                                      b.TT_DESC,
                                      a.PAD_QTY,
                                      a.PAD_RATE,
                                      a.PAD_AMT,
                                      a.PAD_REMARKS
                                  }).ToList();
            return Ok(new { PayadjustHeader, payadjustmentDetails });

        }

        [HttpDelete]
        [Route("api/HD_HRPAYADJs/{PA_ID:int}")]
        [ResponseType(typeof(HD_HRPAYADJ))]
        public IHttpActionResult DeletePayrollAdjustment(long PA_ID)
        {
            try
            {
                HD_HRPAYADJ hdpay = DB.HD_HRPAYADJ.Include(y => y.DT_HRPAYADJ)
                     .SingleOrDefault(x => x.PA_ID == PA_ID);
                foreach (var item in hdpay.DT_HRPAYADJ.ToList())
                {
                    DB.DT_HRPAYADJ.Remove(item);
                }
                DB.HD_HRPAYADJ.Remove(hdpay);
                DB.SaveChanges();
                return Ok(hdpay);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
