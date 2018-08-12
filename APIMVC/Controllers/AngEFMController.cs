using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EFMDataAccessModel;
using System.Web.Http.Cors;

namespace APIMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AngEFMController : ApiController
    {

        public HttpResponseMessage GetANG_EMPLOYEEs(string gender="All")
        {
            using (OVODEntities entities = new OVODEntities()) {
                switch (gender.ToLower()) {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.ANG_EMPLOYEE.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, 
                            entities.ANG_EMPLOYEE.Where(e=>e.gender.ToLower()=="male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.ANG_EMPLOYEE.Where(e => e.gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK,
                           entities.ANG_EMPLOYEE.Where(e => e.gender.ToLower() == "female").ToList());
                        //return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        //"Value for gender must be All,Male of Female." + gender + "is invalid.");
                }
            }
        }

        public HttpResponseMessage GetANG_EMPLOYEEs(int id)
        {
            using (OVODEntities entities = new OVODEntities())
            {
                var entity =entities.ANG_EMPLOYEE.FirstOrDefault(e => e.id == id);
                if (entities != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee id: " + id.ToString() + " not found");
                }

            }
        }

        //public HttpResponseMessage PostAng_Employees([FromBody]ANG_EMPLOYEE employee) {
        //    try
        //    {
        //        using (OVODEntities entities = new OVODEntities()) {
        //            entities.ANG_EMPLOYEE.Add(employee);
        //            entities.SaveChanges();
        //            var message = Request.CreateResponse(HttpStatusCode.Created, employee);
        //            message.Headers.Location = new Uri(Request.RequestUri + employee.id.ToString());
        //            return message;
        //        }
        //    }
        //    catch (Exception ex){
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        public HttpResponseMessage PutAng_Employees([FromBody]ANG_EMPLOYEE employee)
        {
            try
            {
                using (OVODEntities entities = new OVODEntities())
                {
                    var entity = entities.ANG_EMPLOYEE.FirstOrDefault(e => e.id == employee.id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee id: " + employee.id.ToString() + " not found to update.");
                    }
                    else
                    {
                        entity.name = employee.name;
                        entity.isActive = employee.isActive;
                        entity.password = employee.password;
                        entity.PhoneNumber = employee.PhoneNumber;
                        entity.PhotoPath = employee.PhotoPath;
                        entity.gender = employee.gender;
                        entity.email = employee.email;
                        entity.department = employee.department;
                        entity.dateofBirth = employee.dateofBirth;
                        entity.ContactPreference = employee.ContactPreference;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (OVODEntities entities = new OVODEntities())
                {
                    var entity = entities.ANG_EMPLOYEE.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee id: " + id.ToString() + " not found to delete.");
                    }
                    else
                    {
                        entities.ANG_EMPLOYEE.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody] ANG_EMPLOYEE eMPLOYEE)
        {
            try
            {
                using (OVODEntities entities = new OVODEntities())
                {
                    entities.ANG_EMPLOYEE.Add(eMPLOYEE);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }

    
}
