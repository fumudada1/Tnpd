using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.wpb.Controllers
{
    public class WandaformController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /wpb/Wandaform/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            Wandaform wandaform = new Wandaform();
            return View(wandaform);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Guid id, Wandaform wandaform, string checkCode)
        {
            ViewBag.UnId = id.ToString();
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : "000";
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                return View(wandaform);
            }

            if (ModelState.IsValid)
            {
                Case mailCase = new Case();
                mailCase.Subject = wandaform.Oplace; //記得改
                mailCase.Content = wandaform.Content;

                mailCase.CaseGuid = Guid.NewGuid().ToString();
                mailCase.Email = wandaform.Email;

                mailCase.Name = wandaform.Name;
                mailCase.TEL = wandaform.TEL;
                mailCase.WebSiteId = 21;


                mailCase.Address = wandaform.Address;


                mailCase.CaseType = CaseType.婦幼安全警示地點;

                Holiday holiday = new Holiday();
                mailCase.Predate = holiday.GetWorkDay(DateTime.Today, 7);
                mailCase.OArea = wandaform.OArea;
                mailCase.Oplace = wandaform.Oplace;
                mailCase.IP = Request.UserHostAddress;
                mailCase.InitDate = DateTime.Now;

                List<CaseFilters> caseFilterses = _db.CaseFilterses.ToList();
                CaseFilters filterItem = null;
                foreach (var filterse in caseFilterses)
                {
                    filterItem = filterse;
                    if (filterse.FilterType == CaseFilterType.姓名)
                    {
                        if (filterse.Subject.Equals(wandaform.Name.Trim()))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = wandaform.Name.Trim();
                            mailCase.FilterType = filterse.PoprocsSubType.Subject;
                            break;
                        }
                    }
                    if (filterse.FilterType == CaseFilterType.IP)
                    {
                        if (filterse.Subject.Equals(Request.UserHostAddress))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = Request.UserHostAddress;
                            mailCase.FilterType = filterse.PoprocsSubType.Subject;
                            break;
                        }
                    }
                    if (filterse.FilterType == CaseFilterType.電子郵件)
                    {
                        if (filterse.Subject.Equals(wandaform.Email.Trim()))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = wandaform.Email.Trim();
                            mailCase.FilterType = filterse.PoprocsSubType.Subject;
                            break;
                        }
                    }

                }

                _db.Cases.Add(mailCase);
                if (mailCase.IsAutoClose == BooleanType.是)
                {
                    CasePoproc poproc = new CasePoproc();
                    poproc.CaseId = mailCase.Id;

                    poproc.CaseType = mailCase.CaseType;
                    poproc.CaseTime = mailCase.InitDate.Value;

                    poproc.Status =CaseProcessStatus.結案;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = filterItem.PoprocsSubType.Article;
                    poproc.AssignMemo = poproc.AssignMemo.Replace("{Name}", mailCase.Name);
                    poproc.AssignMemo = poproc.AssignMemo.Replace("{InitDate}", Date2CrocFormat(DateTime.Now));
                    poproc.AssignMemo = poproc.AssignMemo.Replace("{subject}", mailCase.Subject);

                    poproc.process = "process";
                    poproc.PoprocsType = 3;
                    poproc.PoprocsSubType = filterItem.PoprocsSubType.Id;
                    _db.CasePoprocs.Add(poproc);
                }
                _db.SaveChanges();



                return RedirectToAction("CreateSuccess", new { unid = id, id = mailCase.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            return View(wandaform);
        }
 public string Date2CrocFormat(DateTime dat)
        {
            return "民國" + (dat.Year - 1911).ToString() + "年" + dat.Month.ToString() + "月" + dat.Day.ToString() + "日";
        }
        public ActionResult CreateSuccess(Guid unid, string id)
        {
            ViewBag.UnId = unid.ToString();
            var MyCase = _db.Cases.FirstOrDefault(x => x.CaseGuid == id);
            return View(MyCase);
        }

    }
}
