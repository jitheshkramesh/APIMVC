using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIMVC.Models;
using EFMDataAccessModel;
using System.Web.Http.Cors;

namespace APIMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PaymentDetailsController : ApiController
    {
        OVODEntities db = new OVODEntities();
        public PaymentDetailsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET api/<controller>
        public HttpResponseMessage Get_PaymentDetailsList()
        {
            using (OVODEntities entities = new OVODEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.PaymentDetails.ToList());
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]PaymentDetail PaymentDetails)
        {
            try
            {
                using (OVODEntities entities = new OVODEntities())
                {
                    //eMPLOYEE.PhotoPath = imageName;
                    entities.PaymentDetails.Add(PaymentDetails);
                    entities.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        // PUT api/<controller>/5
        public HttpResponseMessage Put_PaymentDetails(int id, PaymentDetail PaymentDetails)
        {
            try
            {
                if (id != PaymentDetails.PMId) {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
                }

                using (OVODEntities entities = new OVODEntities())
                {
                    var entity = entities.PaymentDetails.FirstOrDefault(e => e.PMId == PaymentDetails.PMId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Paymentdetails id: " + PaymentDetails.PMId.ToString() + " not found to update.");
                    }
                    else
                    {
                        entity.CardOwnerName = PaymentDetails.CardOwnerName;
                        entity.CardNumber = PaymentDetails.CardNumber;
                        entity.ExpirationDate = PaymentDetails.ExpirationDate;
                        entity.CVV = PaymentDetails.CVV;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (OVODEntities entities = new OVODEntities())
                {
                    var entity = entities.PaymentDetails.FirstOrDefault(e => e.PMId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Paymentdetails id: " + id.ToString() + " not found to delete.");
                    }
                    else
                    {
                        entities.PaymentDetails.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}