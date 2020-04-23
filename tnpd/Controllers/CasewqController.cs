using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace tnpd.Controllers
{
    public class CasewqController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /Casewq/

        public ActionResult Index(string id)
        {
            ViewBag.guid = id;
            Case myCase = _db.Cases.FirstOrDefault(x => x.CaseGuid == id);
            if (myCase == null)
            {
                CaseTraffic myTraffic = _db.CaseTraffics.FirstOrDefault(x => x.CaseGuid == id);
                if (myTraffic == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (myTraffic.Casewqs.Any())
                {
                    ViewBag.Message = "您已經填寫過此案件滿意度調查問卷!";
                }

            }
            else
            {
                if (myCase.Casewqs.Any())
                {
                    ViewBag.Message = "您已經填寫過此案件滿意度調查問卷!";
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string id, int q1, int q2, int q3, string checkCode)
        {
            ViewBag.UnId = id.ToString();
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : "000";
            if (checkCode.ToLower() != sCheckCode)
            {
                ViewBag.guid = id;
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                return View();
            }
            Case myCase = _db.Cases.FirstOrDefault(x => x.CaseGuid == id);
            if (myCase == null)
            {
                CaseTraffic myTraffic = _db.CaseTraffics.FirstOrDefault(x => x.CaseGuid == id);
                if (myTraffic == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (myTraffic.Casewqs.Count == 0)
                {
                    CaseTrafficqw casewq = new CaseTrafficqw();
                    casewq.CaseId = myTraffic.Id;
                    casewq.Q1 = q1;
                    casewq.Q2 = q2;
                    casewq.Q3 = q3;
                    casewq.InitDate = DateTime.Now;
                    casewq.IP = Request.UserHostAddress;
                    _db.CaseTrafficqws.Add(casewq);
                    _db.SaveChanges();
                }

            }
            else
            {
                if (myCase.Casewqs.Count == 0)
                {
                    Casewq casewq = new Casewq();
                    casewq.CaseId = myCase.Id;
                    casewq.Q1 = q1;
                    casewq.Q2 = q2;
                    casewq.Q3 = q3;
                    casewq.InitDate = DateTime.Now;
                    casewq.IP = Request.UserHostAddress;
                    _db.Casewqs.Add(casewq);
                    _db.SaveChanges();
                }

            }


            ViewBag.guid = id;
            ViewBag.Message = "案件滿意度已送出";
            return View();
        }

    }
}
