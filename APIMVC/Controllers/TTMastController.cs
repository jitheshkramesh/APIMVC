using EFMDataAccessModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class TTMastController : ApiController
    {
        OVODEntities DB = new OVODEntities();
        public TTMastController()
        {
            DB.Configuration.ProxyCreationEnabled = false;
        }

        [HttpPost]
        public HttpResponseMessage TTList([FromBody]TTMAST tTMAST)
        {
            try
            {
                using (OVODEntities db = new OVODEntities())
                {
                    if (tTMAST.TT_ID == 0)
                    {
                        db.TTMASTs.Add(tTMAST);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(tTMAST).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        public HttpResponseMessage TTList()
        {
            using (OVODEntities entities = new OVODEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.TTMASTs.ToList());
            }
        }

        [HttpGet]
        public IHttpActionResult TTList(int id)
        {
            using (OVODEntities entities = new OVODEntities())
            {
                //return Request.CreateResponse(HttpStatusCode.OK, entities.TTMASTs.Where(x => x.TT_ID == id).ToList());
                var Ttmast = (from a in entities.TTMASTs
                                       where a.TT_ID == id
                                       select new
                                       {
                                           a.TT_ID,
                                           a.TT_CODE,
                                           a.TT_DESC,
                                           a.TT_ED,
                                           a.TT_GRATACCR,
                                           a.TT_GROUP,
                                           a.TT_LVEACCR,
                                           a.TT_LVERESUME,
                                           a.TT_SUSAIR,
                                           a.TT_SUSGRAT,
                                           a.TT_SUSLVE,
                                           a.TT_TYPE
                                       }).FirstOrDefault();

                return Ok(new { Ttmast});
            }
        }
    }
}
