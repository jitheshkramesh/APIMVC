using EFMDataAccessModel;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class QuizController : ApiController
    {
        [HttpGet]
        [Route("api/Questions")]
        public HttpResponseMessage GetQuestions()
        {
            using (OVODEntities db = new OVODEntities())
            {
                var qns = db.Questions
                    .Select(x => new { QnId = x.QnID, Qn = x.Qn, ImageName = x.ImageName, x.Option1, x.Option2, x.Option3, x.Option4 })
                    .OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();

                var updated = qns.AsEnumerable()
                    .Select(x => new
                    {
                        QnID = x.QnId,
                        Qn = x.Qn,
                        ImageName = x.ImageName,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                    }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);
            }
        }

        [HttpPost]
        [Route("api/Answers")]
        public HttpResponseMessage GetAnswers(int[] qIDs)
        {
            using (OVODEntities db = new OVODEntities())
            {
                var result = db.Questions
                    .AsEnumerable()
                    .Where(z => qIDs.Contains(z.QnID))
                    .OrderBy(x => { return Array.IndexOf(qIDs, x.QnID); })
                    .Select(z => z.Answer)
                    .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result);

            }
        }
    }
}
