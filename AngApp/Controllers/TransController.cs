using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngApp.Models;
using AngApp.ViewModel;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Dynamic;
using System.IO;
using System.Web.Optimization;
using System.Net.Http;
using System.Data.Entity;

namespace AngApp.Controllers
{
    public class TransController : Controller
    {
        OVODEntities5 db = new OVODEntities5();
        string result = "Error! Document Creation Is Not Completed.";

        // GET: Trans
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult De_PayrollAdj(string DocNo="")
        {
            List<HD_HRPAYADJ> payadjh = new List<HD_HRPAYADJ>();
            if (DocNo != "")
            {
                OVODEntities5 oe = new OVODEntities5();
                var v = (from a in oe.VW_HD_HRPAYADJ
                         where a.PA_DOCNO.Equals(DocNo)
                         orderby a.PA_DOCNO
                         select a
                         ).FirstOrDefault();
                ViewBag.DocNo = v.PA_DOCNO;
                ViewBag.DocDate =Convert.ToDateTime( v.PA_DOCDATE).ToShortDateString();
                ViewBag.Month = v.PA_MONTH;
                ViewBag.Year = v.PA_YEAR;
                ViewBag.Header = v.PA_HEADER;
                ViewBag.PATitle = v.PA_TITLE;
            }
            return View(payadjh);
        }

        [HttpPost]
        public ActionResult De_PayrollAdjDetails(string DocNo = "")
        {
            if (DocNo != "")
            {
                using (OVODEntities5 dbContext = new OVODEntities5())
                {
                    var entryPoint = (from ep in dbContext.HD_HRPAYADJ
                                      join e in dbContext.DT_HRPAYADJ on ep.PA_ID equals e.PA_ID
                                      join t in dbContext.TTMASTs on e.TT_ID equals t.TT_ID
                                      join m in dbContext.EMPLOYEEs on e.EM_ID equals m.EM_ID
                                      where ep.PA_DOCNO == DocNo
                                      select new
                                      {
                                          m.EM_ID,
                                          m.EM_NAME,
                                          t.TT_ID,
                                          t.TT_DESC,
                                          e.PAD_QTY,
                                          e.PAD_RATE,
                                          e.PAD_AMT,
                                          e.PAD_REMARKS,
                                          e.PAD_ID
                                      }).ToList();
                    return Json(new { data = entryPoint }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { data ="" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayadjSave(string PA_DOCNO, string PA_DOCDATE, string PA_HEADER, string PA_TITLE, string PA_MONTH, string PA_YEAR, DT_HRPAYADJ[] det)
        {
            int DocId;
            if (PA_DOCNO != null || PA_DOCDATE != null || PA_MONTH != null || PA_YEAR != null)
            {
                using (var context = new OVODEntities5())
                {
                    using (var dbcxtransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var DocData = context.HD_HRPAYADJ.Where(x => x.PA_DOCNO == PA_DOCNO).FirstOrDefault();
                            if (DocData != null)
                            {
                                //HD_HRPAYADJ hd = new HD_HRPAYADJ()
                                //{
                                //    PA_DOCDATE = Convert.ToDateTime(PA_DOCDATE),
                                //    PA_HEADER = PA_HEADER,
                                //    PA_TITLE = PA_TITLE,
                                //    PA_MONTH = PA_MONTH,
                                //    PA_YEAR = PA_YEAR
                                //};
                                DocData.PA_DOCDATE = Convert.ToDateTime(PA_DOCDATE);
                                DocData.PA_HEADER = PA_HEADER;
                                DocData.PA_TITLE = PA_TITLE;
                                DocData.PA_MONTH = PA_MONTH;
                                DocData.PA_YEAR = PA_YEAR; ;
                                context.Entry(DocData).State = EntityState.Modified; 
                                context.SaveChanges();
                                DocId = context.HD_HRPAYADJ.Where(x => x.PA_DOCNO == PA_DOCNO).Select(a=>a.PA_ID).Single() ;

                                //DT_HRPAYADJ details = new DT_HRPAYADJ { PAD_ID = DocId };
                                //context.Entry(details).State = EntityState.Deleted;
                                var items = context.DT_HRPAYADJ.Where(b => b.PA_ID==DocId);
                                foreach (var item in items)
                                {
                                    context.DT_HRPAYADJ.Remove(item);
                                }

                                context.SaveChanges();

                                if (DocId > 0)
                                {
                                    foreach (var item in det)
                                    {
                                        DT_HRPAYADJ s = new DT_HRPAYADJ()
                                        {
                                            PA_ID = DocId,
                                            EM_ID = item.EM_ID,
                                            TT_ID = item.TT_ID,
                                            PAD_QTY = item.PAD_QTY,
                                            PAD_RATE = item.PAD_RATE,
                                            PAD_AMT = (item.PAD_RATE * item.PAD_QTY),
                                            PAD_REMARKS = item.PAD_REMARKS

                                        };
                                        context.DT_HRPAYADJ.Add(s);
                                    }
                                    context.SaveChanges();
                                    dbcxtransaction.Commit();
                                    result = "Success! Document Update Completed.";
                                }
                                else
                                {
                                    dbcxtransaction.Rollback();
                                    result = "Invalid Code!";
                                }

                            }
                            else
                            {
                                HD_HRPAYADJ hd = new HD_HRPAYADJ()
                                {
                                    PA_DOCNO = PA_DOCNO,
                                    PA_DOCDATE = Convert.ToDateTime(PA_DOCDATE),
                                    PA_HEADER = PA_HEADER,
                                    PA_TITLE = PA_TITLE,
                                    PA_MONTH = PA_MONTH,
                                    PA_YEAR = PA_YEAR
                                };
                                context.HD_HRPAYADJ.Add(hd);
                                context.SaveChanges();
                                DocId = hd.PA_ID;

                                if (DocId > 0)
                                {
                                    foreach (var item in det)
                                    {
                                        DT_HRPAYADJ s = new DT_HRPAYADJ()
                                        {
                                            PA_ID = DocId,
                                            EM_ID = item.EM_ID,
                                            TT_ID = item.TT_ID,
                                            PAD_QTY = item.PAD_QTY,
                                            PAD_RATE = item.PAD_RATE,
                                            PAD_AMT = (item.PAD_RATE * item.PAD_QTY),
                                            PAD_REMARKS = item.PAD_REMARKS

                                        };
                                        context.DT_HRPAYADJ.Add(s);
                                    }
                                    context.SaveChanges();
                                    dbcxtransaction.Commit();
                                    result = "Success! Document Creation Completed.";
                                }
                                else
                                {
                                    dbcxtransaction.Rollback();
                                    result = "Invalid Code!";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            result = ex.ToString();
                            dbcxtransaction.Rollback();
                        }
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PayrollAdjustmentChklst() {
            return View();
        }
              

        [HttpPost]
        public ActionResult PayrollAdjdChklst()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumndir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pagesize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;

            OVODEntities5 oe = new OVODEntities5();
            var v = (from a in oe.VW_HD_HRPAYADJ
                     orderby a.PA_DOCNO
                     select a
                     );

            totalRecords = v.Count();
            var data = v.Skip(skip).Take(pagesize).ToList();
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult PayrollAdjdGrdDetails(string DocNo)
        {
            OVODEntities5 oe = new OVODEntities5();
            var v = (from a in oe.VW_DT_HRPAYADJ
                     where a.PA_DOCNO.Equals(DocNo)
                     orderby a.PA_DOCNO
                     select a
                     );

            return Json(v, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumndir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pagesize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;
            OVODEntities5 oe = new OVODEntities5();
            var v = (from a in oe.DEPARTMENTs
                     orderby a.DP_CD
                     select a
                     );


            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumndir)))
            //{
            //    v = v.OrderBy(sortColumn + " " + sortColumndir);
            //}
            totalRecords = v.Count();
            var data = v.Skip(skip).Take(pagesize).ToList();
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Invoice()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Leave()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Leave")]
        public ActionResult Post_Leave(DT_LEAVE model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (OVODEntities5 db = new OVODEntities5())
                    {
                        DT_LEAVE lv = new DT_LEAVE()
                        {
                            LV_DOC_DATE = model.LV_DOC_DATE,
                            LV_DOC_NO = model.LV_DOC_NO,
                            EMP_ID = model.EMP_ID,
                            TT_ID = model.TT_ID,
                            LV_EMAIL = model.LV_EMAIL,
                            LV_DT_FROM = model.LV_DT_FROM,
                            LV_DT_TO = model.LV_DT_TO
                        };
                        db.DT_LEAVE.Add(lv);
                        db.SaveChanges();
                    }
                }
                ViewBag.Message = "Record Saved Successfully.";
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Error." + ex.Message.ToString() ;
            }
            return View();
        }
    }
}