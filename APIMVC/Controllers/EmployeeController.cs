using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMVC.Models;

namespace APIMVC.Controllers
{
    public class EmployeeController : Controller
    {
        //private dbModel db = new dbModel();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //public IEnumerable<ANG_EMPLOYEE> GetANG_EMPLOYEEs() {
        //    return db.ANG_EMPLOYEE;
        //}

        public JsonResult GetANG_EMPLOYEEsList()
        {
            using (var dbm = new dbModel()) {
                var emp = dbm.ANG_EMPLOYEE.ToList();
                return Json(dbm.ANG_EMPLOYEE, JsonRequestBehavior.AllowGet);
            }
        }
    }
}