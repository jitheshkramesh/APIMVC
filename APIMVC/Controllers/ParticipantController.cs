using EFMDataAccessModel;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public class ParticipantController : ApiController
    {
        [HttpPost]
        [Route("api/InsertParticipant")]
        public Participant Insert(Participant model)
        {
            using (OVODEntities db = new OVODEntities())
            {
                db.Participants.Add(model);
                db.SaveChanges();
                return model;
            }

        }

        [HttpPost]
        [Route("api/UpdateOutput")]
        public Participant UpdateOutput(Participant model)
        {
            using (OVODEntities db = new OVODEntities())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return model;
            }

        }
    }
}
