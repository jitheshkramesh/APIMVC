using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserAuthentication.Controllers
{
    [Authorize]
    public class AngEmployeeController : ApiController
    {
        public IEnumerable<ANG_EMPLOYEE> Get() {
            using (OVODEntities entities = new OVODEntities()) {
                return entities.ANG_EMPLOYEE.ToList();
            }
        }
    }
}
