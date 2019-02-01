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
using System.Data.Entity;

namespace APIMVC.Controllers
{
    
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AngEFMController : ApiController
    {
        OVODEntities DB=new OVODEntities();
        public AngEFMController() {
            DB.Configuration.ProxyCreationEnabled = false;
        }
        public HttpResponseMessage GetANG_EMPLOYEEs(string gender = "All")
        {
            using (OVODEntities entities = new OVODEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.ANG_EMPLOYEE.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.ANG_EMPLOYEE.Where(e => e.gender.ToLower() == "male").ToList());
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
                var entity = entities.ANG_EMPLOYEE.FirstOrDefault(e => e.id == id);
                if (entities != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
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
                string imageName = null;
                var httpRequest = System.Web.HttpContext.Current.Request;
                var postedFile = httpRequest.Files["Image"];
                imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
                postedFile.SaveAs(imageName);

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
                        //entity.PhotoPath = employee.PhotoPath;
                        entity.PhotoPath = imageName;
                        entity.gender = employee.gender;
                        entity.email = employee.email;
                        entity.department = employee.department;
                        entity.dateofBirth = employee.dateofBirth;
                        entity.ContactPreference = employee.ContactPreference;
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
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
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
                //string imageName = null;
                //var httpRequest = System.Web.HttpContext.Current.Request;
                //var postedFile = httpRequest.Files["Image"];
                //imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "_");
                //imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                //postedFile.SaveAs(imageName);

                //if (eMPLOYEE.Image != null)
                //{


                //    //eMPLOYEE.Image.SaveAs(Path.Combine(filePath));

                //}
                using (OVODEntities entities = new OVODEntities())
                {
                    //eMPLOYEE.PhotoPath = imageName;
                    entities.ANG_EMPLOYEE.Add(eMPLOYEE);
                    entities.SaveChanges();
                }
                //if (eMPLOYEE.Image != null)
                //{
                //    imageName = Path.GetFileNameWithoutExtension(eMPLOYEE.Image.FileName);
                //    string imageExtn = Path.GetExtension(eMPLOYEE.Image.FileName);
                //    imageName = imageName + DateTime.Now.ToString("yymmssfff") + imageExtn;
                //    var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
                //    eMPLOYEE.Image.SaveAs(Path.Combine(filePath));
                //}
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/UploadImage")]
        public HttpResponseMessage UploadImage()
        {
            string ImageName = null;
            int EmpId;
            var httpRequest = HttpContext.Current.Request;
            var PostedFile = httpRequest.Files["Image"];
            ImageName = new string(Path.GetFileNameWithoutExtension(PostedFile.FileName).Take(10).ToArray()).Replace(" ", "_");
            ImageName = ImageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(PostedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + ImageName);
            PostedFile.SaveAs(filePath);
            EmpId = Convert.ToInt32(httpRequest["EmployeeId"]);

            using (OVODEntities entities = new OVODEntities())
            {
                var entity = entities.ANG_EMPLOYEE.FirstOrDefault(e => e.id == EmpId);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee id: " + EmpId.ToString() + " not found to update.");
                }
                else
                {
                    entity.PhotoPath = ImageName;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
        }

        

       


    }

}

    

