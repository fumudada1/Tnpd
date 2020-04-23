using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Controllers;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdAdmin.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class CaseController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //

        //派案作業
        public ActionResult Assign(int? page, FormCollection fc, int pclass)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var cases = _db.Cases.Include(c => c.WebSite).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            switch (pclass)
            {
                case 1:
                    if (member.MyUnit.Id == 29)
                    {
                        cases = cases.Where(w => (w.CaseType == CaseType.參觀本局暨所屬機關 && w.WebSiteId == webSite.Id) || w.CaseType == CaseType.首長信箱);
                    }
                    else
                    {
                        cases = cases.Where(w => (w.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.WebSiteId == webSite.Id && (w.IsUnitAssign == BooleanType.否 || w.IsUnitAssign == null));
                    }

                    break;
                case 2:
                    cases = cases.Where(w => w.CaseType == CaseType.檢舉貪瀆信箱);

                    break;
                case 3:
                    cases = cases.Where(w => w.CaseType == CaseType.網路報案);
                    break;
                case 4:
                    cases = cases.Where(w => w.CaseType == CaseType.婦幼安全警示地點);
                    break;

            }


            cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);
            cases = cases.Where(w => w.ParentId == null);


            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(cases.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }
        //派案作業
        public ActionResult AssignEdit(int pclass, int id = 0)
        {
            Case case1 = _db.Cases.Find(id);
            if (case1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", case1.WebSiteId);
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.pclass = pclass;
            return View(case1);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AssignEdit(int Id, int pclass, string processtype, string PoprocsContent, string process, int? PoprocsType, int? PoprocsSubType, string[] MemberListSelect, HttpPostedFileBase Upfile)
        {
            Case mycase = _db.Cases.Find(Id);
            if (mycase == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (processtype == "Assign") //指派
            {
                string aa = Request["MemberListSelect"];
                if (!string.IsNullOrEmpty(aa))
                {
                    var members = _db.Members.ToList().Where(c => MemberListSelect.Contains(c.Id.ToString()));
                    foreach (Member selectmember in members)
                    {
                        CasePoproc poproc = new CasePoproc();
                        poproc.CaseId = Id;
                        poproc.MemberId = selectmember.Id;
                        poproc.CaseType = mycase.CaseType;
                        poproc.CaseTime = mycase.InitDate.Value;
                        poproc.UnitId = selectmember.UnitId;
                        poproc.Status = CaseProcessStatus.待辦;
                        poproc.AssignDateTime = DateTime.Now;
                        //poproc.AssignMemo = PoprocsContent;
                        //poproc.process = process;
                        //poproc.PoprocsType = PoprocsType;
                        //poproc.PoprocsSubType = PoprocsSubType;

                        _db.CasePoprocs.Add(poproc);
                        CasePoprocLog log = new CasePoprocLog();
                        log.CaseId = mycase.Id;
                        log.MemberId = member.Id;
                        log.InitDate = DateTime.Now;
                        log.Action = "派案";
                        log.UnitId = member.UnitId;
                        log.ActionMemo = "案件指派";
                        _db.CasePoprocLogs.Add(log);

                    }
                    ViewBag.message = "案件已指派!";


                }
            }
            else if (processtype == "AssignOrg")
            {
                string aa = Request["MemberListSelect"];
                if (!string.IsNullOrEmpty(aa))
                {
                    var members = _db.Members.ToList().Where(c => MemberListSelect.Contains(c.Id.ToString()));
                    foreach (Member selectmember in members)
                    {
                        CasePoproc poproc = new CasePoproc();
                        poproc.CaseId = Id;
                        poproc.MemberId = selectmember.Id;
                        poproc.CaseType = mycase.CaseType;
                        poproc.CaseTime = mycase.InitDate.Value;
                        poproc.UnitId = selectmember.UnitId;
                        poproc.Status = CaseProcessStatus.待辦;
                        poproc.AssignDateTime = DateTime.Now;
                        //poproc.AssignMemo = PoprocsContent;
                        //poproc.process = process;
                        //poproc.PoprocsType = PoprocsType;
                        //poproc.PoprocsSubType = PoprocsSubType;

                        _db.CasePoprocs.Add(poproc);
                        CasePoprocLog log = new CasePoprocLog();
                        log.CaseId = mycase.Id;
                        log.MemberId = member.Id;
                        log.InitDate = DateTime.Now;
                        log.Action = "派案";
                        log.UnitId = member.UnitId;
                        log.ActionMemo = "案件指派";
                        _db.CasePoprocLogs.Add(log);

                    }
                    ViewBag.message = "案件已指派!";


                }
            }
            else if (processtype == "Poprocs") //
            {
                CasePoproc myPoproc = mycase.Poprocs
                    .Where(x => x.Status == CaseProcessStatus.未分派 && x.MemberId == member.Id).FirstOrDefault();
                if (myPoproc == null)
                {
                    CasePoproc poproc = new CasePoproc();
                    poproc.CaseId = Id;
                    poproc.MemberId = member.Id;
                    poproc.CaseType = mycase.CaseType;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = member.UnitId;
                    poproc.Status = CaseProcessStatus.已辦;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    poproc.PoprocsSubType = PoprocsSubType;
                    if (Upfile != null)
                    {
                        //取得副檔名
                        string extension = Upfile.FileName.Split('.')[Upfile.FileName.Split('.').Length - 1];
                        string fileName = Upfile.FileName.Substring(0, Upfile.FileName.Length - extension.Length - 1);
                        string fileNameTemp = fileName;
                        int i = 1;
                        while (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Casefiles"), string.Format("{0}.{1}", fileNameTemp, extension))))
                        {
                            fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                            i++;
                        }

                        //新檔案名稱
                        fileName = string.Format("{0}.{1}", fileNameTemp, extension);
                        string savedName = Path.Combine(Server.MapPath("~/Casefiles"), fileName);
                        Upfile.SaveAs(savedName);
                        poproc.Upfile1 = fileName;
                    }
                    _db.CasePoprocs.Add(poproc);

                }
                else
                {
                    CasePoproc poproc = myPoproc;
                    poproc.Status = CaseProcessStatus.已辦;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    poproc.PoprocsSubType = PoprocsSubType;
                    if (Upfile != null)
                    {
                        //取得副檔名
                        string extension = Upfile.FileName.Split('.')[Upfile.FileName.Split('.').Length - 1];
                        string fileName = Upfile.FileName.Substring(0, Upfile.FileName.Length - extension.Length - 1);
                        string fileNameTemp = fileName;
                        int i = 1;
                        while (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Casefiles"),
                            string.Format("{0}.{1}", fileNameTemp, extension))))
                        {
                            fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                            i++;
                        }

                        //新檔案名稱
                        fileName = string.Format("{0}.{1}", fileNameTemp, extension);
                        string savedName = Path.Combine(Server.MapPath("~/Casefiles"), fileName);
                        Upfile.SaveAs(savedName);
                        poproc.Upfile1 = fileName;
                    }
                }
                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = mycase.Id;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "核判送出";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CasePoprocLogs.Add(log);
                ViewBag.message = "核判送出!";


            }
            else if (processtype == "Save")
            {
                CasePoproc myPoproc = mycase.Poprocs
                    .Where(x => x.Status == CaseProcessStatus.未分派 && x.MemberId == member.Id).FirstOrDefault();
                if (myPoproc == null)
                {
                    CasePoproc poproc = new CasePoproc();
                    poproc.CaseId = Id;
                    poproc.MemberId = member.Id;
                    poproc.CaseType = mycase.CaseType;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = member.MyUnit.ParentId;
                    poproc.Status = CaseProcessStatus.未分派;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    poproc.PoprocsSubType = PoprocsSubType;
                    if (Upfile != null)
                    {
                        //取得副檔名
                        string extension = Upfile.FileName.Split('.')[Upfile.FileName.Split('.').Length - 1];
                        string fileName = Upfile.FileName.Substring(0, Upfile.FileName.Length - extension.Length - 1);
                        string fileNameTemp = fileName;
                        int i = 1;
                        while (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Casefiles"),
                            string.Format("{0}.{1}", fileNameTemp, extension))))
                        {
                            fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                            i++;
                        }

                        //新檔案名稱

                        fileName = string.Format("{0}.{1}", fileNameTemp, extension);
                        string savedName = Path.Combine(Server.MapPath("~/Casefiles"), fileName);
                        Upfile.SaveAs(savedName);
                        poproc.Upfile1 = fileName;
                    }
                    _db.CasePoprocs.Add(poproc);
                }
                else
                {
                    CasePoproc poproc = myPoproc;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    poproc.PoprocsSubType = PoprocsSubType;
                    if (Upfile != null)
                    {
                        //取得副檔名
                        string extension = Upfile.FileName.Split('.')[Upfile.FileName.Split('.').Length - 1];
                        string fileName = Upfile.FileName.Substring(0, Upfile.FileName.Length - extension.Length - 1);
                        string fileNameTemp = fileName;
                        int i = 1;
                        while (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Casefiles"),
                            string.Format("{0}.{1}", fileNameTemp, extension))))
                        {
                            fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                            i++;
                        }

                        //新檔案名稱
                        fileName = string.Format("{0}.{1}", fileNameTemp, extension);
                        string savedName = Path.Combine(Server.MapPath("~/Casefiles"), fileName);
                        Upfile.SaveAs(savedName);
                        poproc.Upfile1 = fileName;
                    }

                }

                ViewBag.message = "案件已儲存!";
            }
            else if (processtype == "UnitAssign")
            {
                mycase.IsUnitAssign = BooleanType.是;
                ViewBag.message = "案件申請單位改派!";
            }
            _db.SaveChanges();
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", mycase.WebSiteId);

            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.pclass = pclass;
            return View(mycase);
        }

        //單位改派
        public ActionResult UnitAssign(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var cases = _db.Cases.Include(c => c.WebSite).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            cases = cases.Where(w => w.CaseType == CaseType.分局長與大隊隊長信箱 && w.IsUnitAssign == BooleanType.是);

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(cases.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult UnitAssignEdit(int pclass, int id = 0)
        {
            Case case1 = _db.Cases.Find(id);
            if (case1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.Where(x => x.Id > 1 && x.Id < 22).OrderBy(p => p.ListNum), "Id", "Subject", case1.WebSiteId);

            ViewBag.pclass = pclass;
            return View(case1);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UnitAssignEdit(int Id, int pclass, int WebSiteId)
        {
            Case mycase = _db.Cases.Find(Id);
            if (mycase == null)
            {
                return HttpNotFound();
            }

            WebSiteName webSite = mycase.WebSite;
            WebSiteName newWebSiteName = _db.WebSiteNames.Find(WebSiteId);

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            mycase.IsUnitAssign = BooleanType.否;
            mycase.WebSiteId = WebSiteId;

            CasePoprocLog log = new CasePoprocLog();
            log.CaseId = mycase.Id;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = "單位改派";
            log.UnitId = member.UnitId;
            log.ActionMemo = "單位改派由" + webSite.Subject + "改派" + newWebSiteName.Subject;
            _db.CasePoprocLogs.Add(log);



            _db.SaveChanges();

            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.Where(x => x.Language == LanguageType.中文版 && x.Id != 1).OrderBy(p => p.ListNum), "Id", "Subject", mycase.WebSiteId);
            ViewBag.message = "案件已單位改派!";
            ViewBag.pclass = pclass;
            return View(mycase);
        }

        //承辦人作業
        public ActionResult Process(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            casePoprocs = casePoprocs.Where(x => (x.Status == CaseProcessStatus.待辦 || x.Status == CaseProcessStatus.辦案) && x.MemberId == member.Id);



            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderByDescending(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult ProcessEdit(int id = 0)
        {
            CasePoproc casePoproc = _db.CasePoprocs.Find(id);
            if (casePoproc == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", casePoproc.Case.WebSiteId);
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            Holiday holiday = new Holiday();
            ViewBag.ExtensionDay = holiday.GetWorkDay(casePoproc.Case.Predate, 7);

            if (member.Roles.Any(x => x.Subject.Contains("分局長及隊長信箱管理者")))
            {
                ViewBag.isAdmin = true;
            }

            return View(casePoproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ProcessEdit(int Id, int? MemberId, string processtype, string PoprocsContent, string process, int? PoprocsType, int? PoprocsSubType, DateTime? DelyDateTime, string DelyMemo, string noplyreason, HttpPostedFileBase Upfile)
        {

            CasePoproc myPoproc = _db.CasePoprocs.Find(Id);
            if (myPoproc == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (processtype == "Poprocs") //
            {

                CasePoproc poproc = myPoproc;
                poproc.Status = CaseProcessStatus.已辦;
                poproc.AssignMemo = PoprocsContent;
                poproc.process = process;
                poproc.PoprocsType = PoprocsType;
                poproc.PoprocsSubType = PoprocsSubType;
                if (Upfile != null)
                {
                    //取得副檔名
                    string extension = Upfile.FileName.Split('.')[Upfile.FileName.Split('.').Length - 1];
                    string fileName = Upfile.FileName.Substring(0, Upfile.FileName.Length - extension.Length - 1);
                    string fileNameTemp = fileName;
                    int i = 1;
                    while (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Casefiles"),
                        string.Format("{0}.{1}", fileNameTemp, extension))))
                    {
                        fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                        i++;

                    }

                    //新檔案名稱
                    fileName = string.Format("{0}.{1}", fileNameTemp, extension);
                    string savedName = Path.Combine(Server.MapPath("~/Casefiles"), fileName);
                    Upfile.SaveAs(savedName);
                    poproc.Upfile1 = fileName;
                }
                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = poproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "核判送出";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CasePoprocLogs.Add(log);
                ViewBag.message = "案件已核判送出!";


            }
            else if (processtype == "ModifyPeople")
            {
                CasePoproc poproc = myPoproc;
                poproc.MemberId = MemberId;
                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = poproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "案件更換承辦人";
                log.UnitId = member.UnitId;
                log.ActionMemo = "案件更換承辦人";
                _db.CasePoprocLogs.Add(log);
                ViewBag.message = "案件已更換承辦人!";
            }
            else if (processtype == "Save")
            {

                CasePoproc poproc = myPoproc;
                poproc.AssignMemo = PoprocsContent;
                poproc.process = process;
                poproc.Status = CaseProcessStatus.辦案;
                poproc.PoprocsType = PoprocsType;
                poproc.PoprocsSubType = PoprocsSubType;
                if (Upfile != null)
                {
                    //取得副檔名
                    string extension = Upfile.FileName.Split('.')[Upfile.FileName.Split('.').Length - 1];
                    string fileName = Upfile.FileName.Substring(0, Upfile.FileName.Length - extension.Length - 1);
                    string fileNameTemp = fileName;
                    int i = 1;
                    while (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Casefiles"),
                        string.Format("{0}.{1}", fileNameTemp, extension))))
                    {
                        fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                        i++;
                    }

                    //新檔案名稱
                    fileName = string.Format("{0}.{1}", fileNameTemp, extension);
                    string savedName = Path.Combine(Server.MapPath("~/Casefiles"), fileName);
                    Upfile.SaveAs(savedName);
                    poproc.Upfile1 = fileName;
                }

                ViewBag.message = "案件已儲存!";
            }
            else if (processtype == "Extension")
            {
                CasePoproc poproc = myPoproc;
                poproc.Status = CaseProcessStatus.展延;
                poproc.DelyDateTime = DelyDateTime;
                poproc.DelyMemo = DelyMemo;

                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = poproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "案件申請展延";
                log.UnitId = member.UnitId;
                log.ActionMemo = DelyMemo;
                _db.CasePoprocLogs.Add(log);
                ViewBag.message = "案件已申請展延!";


            }
            else if (processtype == "Goback")
            {
                CasePoproc poproc = myPoproc;
                poproc.Status = CaseProcessStatus.退文;
                poproc.noplyreason = noplyreason;
                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = poproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "案件申請退文";
                log.UnitId = member.UnitId;
                log.ActionMemo = noplyreason;
                _db.CasePoprocLogs.Add(log);
                ViewBag.message = "案件已申請退文!";
            }
            _db.SaveChanges();
            ViewBag.WebSiteId = new SelectList(_db.WebSiteNames.OrderBy(p => p.ListNum), "Id", "Subject", myPoproc.Case.WebSiteId);

            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            if (DelyDateTime.HasValue)
            {
                ViewBag.ExtensionDay = DelyDateTime;
            }
            else
            {
                Holiday holiday = new Holiday();
                ViewBag.ExtensionDay = holiday.GetWorkDay(myPoproc.Case.Predate, 7);
            }

            if (member.Roles.Any(x => x.Subject.Contains("分局長及隊長信箱管理者")))
            {
                ViewBag.isAdmin = true;
            }

            return View(myPoproc);
        }

        //展期案件審核
        public ActionResult Extension(int? page, FormCollection fc, int pclass)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            switch (pclass)
            {
                case 1:
                    if (member.MyUnit.Id == 29)
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                    }
                    else
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                    }

                    break;
                case 2:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);

                    break;
                case 3:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                    break;
                case 4:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                    break;

            }

            casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延);



            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderByDescending(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult ExtensionEdit(int pclass, int id = 0)
        {
            CasePoproc casePoproc = _db.CasePoprocs.Find(id);
            if (casePoproc == null)
            {
                return HttpNotFound();
            }
            ViewBag.pclass = pclass;
            return View(casePoproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ExtensionEdit(int pclass, int Id, int isPass)
        {
            CasePoproc myPoproc = _db.CasePoprocs.Find(Id);
            if (myPoproc == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            CasePoproc poproc = myPoproc;
            poproc.Status = CaseProcessStatus.待辦;
            ViewBag.message = "案件不通過展延!";
            if (isPass == 1)
            {
                poproc.Case.Predate = poproc.DelyDateTime.Value;
                ViewBag.message = "案件已展延!";
            }
            CasePoprocLog log = new CasePoprocLog();
            log.CaseId = poproc.CaseId;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = ViewBag.message;
            log.UnitId = member.UnitId;
            log.ActionMemo = ViewBag.message;
            _db.CasePoprocLogs.Add(log);
            _db.SaveChanges();



            ViewBag.pclass = pclass;
            return View(myPoproc);
        }

        //退文派案
        public ActionResult Goback(int? page, FormCollection fc, int pclass)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            switch (pclass)
            {
                case 1:
                    if (member.MyUnit.Id == 29)
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                    }
                    else
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                    }

                    break;
                case 2:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);

                    break;
                case 3:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                    break;
                case 4:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                    break;

            }

            casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);



            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderByDescending(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult GobackEdit(int pclass, int id = 0)
        {
            CasePoproc poproc = _db.CasePoprocs.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.pclass = pclass;
            return View(poproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult GobackEdit(int Id, int pclass, string processtype, string[] MemberListSelect)
        {
            CasePoproc myPoproc = _db.CasePoprocs.Find(Id);
            if (myPoproc == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (processtype == "Assign") //指派
            {
                string aa = Request["MemberListSelect"];
                if (!string.IsNullOrEmpty(aa))
                {
                    var members = _db.Members.ToList().Where(c => MemberListSelect.Contains(c.Id.ToString()));
                    foreach (Member selectmember in members)
                    {
                        CasePoproc poproc = new CasePoproc();
                        poproc.CaseId = myPoproc.CaseId;
                        poproc.MemberId = selectmember.Id;
                        poproc.CaseType = myPoproc.CaseType;
                        poproc.CaseTime = myPoproc.Case.InitDate.Value;
                        poproc.UnitId = selectmember.UnitId;
                        poproc.Status = CaseProcessStatus.待辦;
                        poproc.AssignDateTime = DateTime.Now;
                        //poproc.AssignMemo = PoprocsContent;

                        _db.CasePoprocs.Add(poproc);
                        CasePoprocLog log = new CasePoprocLog();
                        log.CaseId = myPoproc.CaseId;
                        log.MemberId = selectmember.Id;
                        log.InitDate = DateTime.Now;
                        log.Action = "案件改派";
                        log.UnitId = selectmember.UnitId;
                        log.ActionMemo = "案件改派";
                        _db.CasePoprocLogs.Add(log);


                    }
                    ViewBag.message = "案件已改派!";
                    myPoproc.Case.Poprocs.Remove(myPoproc);
                    _db.CasePoprocs.Remove(myPoproc);


                }
            }
            else if (processtype == "Delete")
            {
                CasePoprocLog log = new CasePoprocLog();
                var mycase = myPoproc.Case;
                var owner = myPoproc.assignMember;
                log.CaseId = mycase.Id;
                log.MemberId = owner.Id;
                log.InitDate = DateTime.Now;
                log.Action = "刪除分文";
                log.UnitId = owner.UnitId;
                log.ActionMemo = "刪除給分文";
                _db.CasePoprocLogs.Add(log);
                myPoproc.Case.Poprocs.Remove(myPoproc);
                _db.CasePoprocs.Remove(myPoproc);


                ViewBag.message = "刪除分文!";


            }
            _db.SaveChanges();



            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.pclass = pclass;
            return RedirectToAction("Goback", "Case", new { pclass = pclass });
        }
        //結案作業
        public ActionResult Close(int? page, FormCollection fc, int pclass)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            switch (pclass)
            {
                case 1:
                    if (member.MyUnit.Id == 29)
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                    }
                    else
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                    }

                    break;
                case 2:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);

                    break;
                case 3:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                    break;
                case 4:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                    break;

            }
 
            casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && (x.Case.IsAutoClose == null || x.Case.IsAutoClose == BooleanType.否));



            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderByDescending(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult CloseEdit(int pclass, int id = 0)
        {
            CasePoproc poproc = _db.CasePoprocs.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            ViewBag.pclass = pclass;
            return View(poproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseEdit(int Id, int pclass, string processtype, string type1, string type2, string PoprocsContent, int Sendfile)
        {
            CasePoproc myPoproc = _db.CasePoprocs.Find(Id);
            if (myPoproc == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (processtype == "Close") //結案
            {
                myPoproc.Type1 = type1;
                myPoproc.type2 = type2;
                myPoproc.AssignMemo = PoprocsContent;
                myPoproc.Status = CaseProcessStatus.結案;
                myPoproc.EndDateTime = DateTime.Now;

                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = myPoproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "結案";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CasePoprocLogs.Add(log);
                ViewBag.message = "案件已結案!";

                if (myPoproc.PoprocsType != 3)
                {
                    //發信
                    string mailbodyTemp = System.IO.File.ReadAllText(Server.MapPath("/EmailTemp/CasePoproc.html"));
                    string mailbody = mailbodyTemp;
                    mailbody = mailbody.Replace("{CaseID}", myPoproc.Case.CaseID);
                    mailbody = mailbody.Replace("{InitDate}", myPoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
                    mailbody = mailbody.Replace("{CaseType}", myPoproc.Case.CaseType.ToString());
                    mailbody = mailbody.Replace("{EndDateTime}", myPoproc.EndDateTime.Value.ToString("yyyy/MM/dd"));
                    string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];
                    if (Sendfile == 1)
                    {
                        myPoproc.AssignMemo = myPoproc.AssignMemo + "<br><a href='/Casefiles/" + myPoproc.Upfile1 + "' target='_black>回覆附件</a>";
                    }
                    mailbody = mailbody.Replace("{AssignMemo}", Txt2Html(myPoproc.AssignMemo));



                    mailbody = mailbody.Replace("{URL}", InternetURL + "casewq/index/" + myPoproc.Case.CaseGuid);
                    //Utility.SendGmailMail("topidea.justin@gmail.com", myPoproc.Case.Email, "臺南市政府警察局-結案通知", mailbody, "xuqoqvdvvsbwyrbl");
                    Utility.SystemSendMail(myPoproc.Case.Email, "臺南市政府警察局-結案通知", mailbody);

                    //併案發信
                    if (myPoproc.Case.Cases.Count() > 0)
                    {
                        foreach (Case ChildCase in myPoproc.Case.Cases)
                        {
                            mailbody = mailbodyTemp;
                            mailbody = mailbody.Replace("{CaseID}", ChildCase.CaseID);
                            mailbody = mailbody.Replace("{InitDate}", ChildCase.InitDate.Value.ToString("yyyy/MM/dd"));
                            mailbody = mailbody.Replace("{CaseType}", ChildCase.CaseType.ToString());
                            mailbody = mailbody.Replace("{EndDateTime}", myPoproc.EndDateTime.Value.ToString("yyyy/MM/dd"));

                            if (Sendfile == 1)
                            {
                                myPoproc.AssignMemo = myPoproc.AssignMemo + "<br><a href='/Casefiles/" + myPoproc.Upfile1 + "' target='_black>回覆附件</a>";
                            }
                            mailbody = mailbody.Replace("{AssignMemo}", Txt2Html(myPoproc.AssignMemo));



                            mailbody = mailbody.Replace("{URL}", InternetURL + "casewq/index/" + ChildCase.CaseGuid);
                            //Utility.SendGmailMail("topidea.justin@gmail.com", myPoproc.Case.Email, "臺南市政府警察局-結案通知", mailbody, "xuqoqvdvvsbwyrbl");
                            Utility.SystemSendMail(myPoproc.Case.Email, "臺南市政府警察局-結案通知", mailbody);
                        }
                    }

                }


            }
            else if (processtype == "GoProcess") //退回承辦
            {
                myPoproc.Status = CaseProcessStatus.待辦;
                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = myPoproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "退回承辦";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CasePoprocLogs.Add(log);


                ViewBag.message = "案件已退回承辦!";


            }
            _db.SaveChanges();
            ViewBag.pclass = pclass;
            return View(myPoproc);
        }


        /// <summary>
        /// 輸入文字方塊的文字轉成HTML
        /// </summary>
        /// <param name="article">要轉換的文字</param>
        /// <returns>回傳HTML用的文字</returns>
        /// <remarks></remarks>

        string Txt2Html(object article)
        {
            if (article != null)
            {
                string fstr = (string)article;
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace("<", "&lt;");
                fstr = fstr.Replace("\"", "&quot;");
                fstr = fstr.Replace("\'", "&quot;");
                fstr = fstr.Replace("\n", "<br>");
                fstr = fstr.Replace("|", "&brvbar;");
                fstr = fstr.Replace(" ", "&nbsp;");
                fstr = fstr.Replace("[", "<");
                fstr = fstr.Replace("]", ">");
                fstr = fstr.Replace("url=", "a href=");
                fstr = fstr.Replace("/url", "/a");
                return fstr;
            }
            return "";
        }

        //案件查詢
        public ActionResult Index(int? page, FormCollection fc, int pclass)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var myCases = _db.Cases.Include(c => c.Poprocs).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);

            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.isAdmin = true;

            }
            else if (member.Roles.Any(x => x.Subject.Contains("交通檢舉信箱管理者")))
            {
                ViewBag.isAdmin = true;
            }
            else if (member.Roles.Any(x => x.Subject.Contains("所有案件查詢")))
            {
                ViewBag.isAdmin = true;
            }
            else
            {
                ViewBag.isAdmin = false;
            }

            if (ViewBag.isAdmin == false)
            {
                switch (pclass)
                {
                    case 1:
                        if (member.MyUnit.Id == 29)
                        {
                            myCases = myCases.Where(
                                w => (w.CaseType == CaseType.參觀本局暨所屬機關 && w.WebSiteId == webSite.Id) ||
                                     w.CaseType == CaseType.首長信箱);
                        }
                        else
                        {
                            myCases = myCases.Where(
                                w =>
                                     (w.WebSiteId == webSite.Id || w.Poprocs.Count(x => x.UnitId == member.UnitId) > 0));
                        }

                        break;
                    case 2:
                        myCases = myCases.Where(w => w.CaseType == CaseType.檢舉貪瀆信箱);

                        break;
                    case 3:
                        myCases = myCases.Where(w => w.CaseType == CaseType.網路報案);
                        break;
                    case 4:
                        myCases = myCases.Where(w => w.CaseType == CaseType.婦幼安全警示地點);
                        break;

                }
            }




            if (hasViewData("SearchByCategories1"))
            {
                int UnitId = getViewDateInt("SearchByCategories1");
                //TnpdModels.CaseType searchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);
                WebSiteName swebsite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == UnitId);
                myCases = myCases.Where(w => w.WebSiteId == swebsite.Id);
            }

            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                myCases = myCases.Where(w => w.Subject.Contains(searchBySubject) || w.Content.Contains(searchBySubject) || w.Name.Contains(searchBySubject) || w.Email.Contains(searchBySubject) || w.TEL.Contains(searchBySubject) );
            }

            if (hasViewData("SearchByCaseId"))
            {
                string searchByCaseId = getViewDateStr("SearchByCaseId");
                myCases = myCases.Where(w => w.CaseID.Contains(searchByCaseId));
            }

            if (hasViewData("SearchByStatus"))
            {
                string Status = getViewDateStr("SearchByStatus");
                TnpdModels.CaseProcessStatus searchByStatus = (TnpdModels.CaseProcessStatus)Enum.Parse(typeof(TnpdModels.CaseProcessStatus), Status, false);

                myCases = myCases.Where(w => w.Poprocs.Count(x => x.Status == searchByStatus) > 0);
            }

            if (hasViewData("SearchByCaseType"))
            {
                string CaseType = getViewDateStr("SearchByCaseType");
                TnpdModels.CaseType SearchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);

                myCases = myCases.Where(w => w.Poprocs.Count(x => x.CaseType == SearchByCaseType) > 0);
            }


            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                myCases = myCases.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            myCases = myCases.Where(w => w.ParentId == null && (w.IsAutoClose == null || w.IsAutoClose==BooleanType.否));
            //總件數
            ViewBag.Total = myCases.Count();

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(myCases.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        //案件查詢
        public ActionResult Filter(int? page, FormCollection fc, int pclass)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var myCases = _db.Cases.Include(c => c.Poprocs).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);

            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.isAdmin = true;

            }
            else if (member.Roles.Any(x => x.Subject.Contains("交通檢舉信箱管理者")))
            {
                ViewBag.isAdmin = true;
            }
            else if (member.Roles.Any(x => x.Subject.Contains("所有案件查詢")))
            {
                ViewBag.isAdmin = true;
            }
            else
            {
                ViewBag.isAdmin = false;
            }

            if (ViewBag.isAdmin == false)
            {
                switch (pclass)
                {
                    case 1:
                        if (member.MyUnit.Id == 29)
                        {
                            myCases = myCases.Where(
                                w => (w.CaseType == CaseType.參觀本局暨所屬機關 && w.WebSiteId == webSite.Id) ||
                                     w.CaseType == CaseType.首長信箱);
                        }
                        else
                        {
                            myCases = myCases.Where(
                                w =>
                                     (w.WebSiteId == webSite.Id || w.Poprocs.Count(x => x.UnitId == member.UnitId) > 0));
                        }

                        break;
                    case 2:
                        myCases = myCases.Where(w => w.CaseType == CaseType.檢舉貪瀆信箱);

                        break;
                    case 3:
                        myCases = myCases.Where(w => w.CaseType == CaseType.網路報案);
                        break;
                    case 4:
                        myCases = myCases.Where(w => w.CaseType == CaseType.婦幼安全警示地點);
                        break;

                }
            }




            if (hasViewData("SearchByCategories1"))
            {
                int UnitId = getViewDateInt("SearchByCategories1");
                //TnpdModels.CaseType searchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);
                WebSiteName swebsite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == UnitId);
                myCases = myCases.Where(w => w.WebSiteId == swebsite.Id);
            }

            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                myCases = myCases.Where(w => w.Subject.Contains(searchBySubject) || w.Content.Contains(searchBySubject) || w.Name.Contains(searchBySubject) || w.Email.Contains(searchBySubject) || w.TEL.Contains(searchBySubject));
            }

            if (hasViewData("SearchByCaseId"))
            {
                string searchByCaseId = getViewDateStr("SearchByCaseId");
                myCases = myCases.Where(w => w.CaseID.Contains(searchByCaseId));
            }

            if (hasViewData("SearchByStatus"))
            {
                string Status = getViewDateStr("SearchByStatus");
                TnpdModels.CaseProcessStatus searchByStatus = (TnpdModels.CaseProcessStatus)Enum.Parse(typeof(TnpdModels.CaseProcessStatus), Status, false);

                myCases = myCases.Where(w => w.Poprocs.Count(x => x.Status == searchByStatus) > 0);
            }

            if (hasViewData("SearchByCaseType"))
            {
                string CaseType = getViewDateStr("SearchByCaseType");
                TnpdModels.CaseType SearchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);

                myCases = myCases.Where(w => w.Poprocs.Count(x => x.CaseType == SearchByCaseType) > 0);
            }


            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                myCases = myCases.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            myCases = myCases.Where(w => w.ParentId == null && w.IsAutoClose == BooleanType.是);
            //總件數
            ViewBag.Total = myCases.Count();

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(myCases.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult FilterEdit(int pclass, int id = 0)
        {
            Case poproc = _db.Cases.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            ViewBag.pclass = pclass;
            return View(poproc);
        }
        public ActionResult Survey(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面



            var casewqs = _db.Casewqs.Include(c => c.Case).AsQueryable();

            if (hasViewData("SearchByCategories2"))
            {
                int UnitId = getViewDateInt("SearchByCategories2");
                //TnpdModels.CaseType searchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);

                casewqs = casewqs.Where(w => w.Case.Poprocs.FirstOrDefault().UnitId == UnitId);
            }



            if (hasViewData("SearchByCaseType"))
            {
                string CaseType = getViewDateStr("SearchByCaseType");
                TnpdModels.CaseType searchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);

                casewqs = casewqs.Where(w => w.Case.CaseType == searchByCaseType);
            }

            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                casewqs = casewqs.Where(w => w.Case.InitDate >= startDate && w.Case.InitDate <= endDate);
            }


            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casewqs.OrderByDescending(x => x.Case.InitDate).ToList());

        }


        //
        // GET: /Case/Details/5

        public ActionResult Details(int id = 0)
        {
            Case case1 = _db.Cases.Find(id);
            if (case1 == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(case1);
        }

        //
        // GET: /Case/Create



        //
        // GET: /Case/Edit/5

        public ActionResult Edit(int pclass, int id = 0)
        {
            Case poproc = _db.Cases.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            ViewBag.pclass = pclass;
            return View(poproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, int pclass, string processtype)
        {
            Case mycase = _db.Cases.Find(Id);
            if (mycase == null)
            {
                return HttpNotFound();
            }


            //var owner = myPoproc.assignMember;

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (processtype == "Restone") //回復
            {
                for (int i = mycase.Poprocs.Count - 1; i >= 0; i--)
                {
                    CasePoproc poproc = mycase.Poprocs.ElementAt(i);
                    mycase.Poprocs.Remove(poproc);
                    _db.CasePoprocs.Remove(poproc);
                }

                CasePoprocLog log = new CasePoprocLog();
                log.CaseId = mycase.Id;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "案件回復";
                log.UnitId = member.UnitId;
                log.ActionMemo = "案件回復";
                _db.CasePoprocLogs.Add(log);




                _db.SaveChanges();


                return RedirectToAction("Assign", "Case", new { pclass = pclass });
            }
            if (processtype == "SendMail") //指派
            {
                //發信
                string mailbody = System.IO.File.ReadAllText(Server.MapPath("/EmailTemp/CasePoproc.html"));
                mailbody = mailbody.Replace("{CaseID}", mycase.CaseID);
                mailbody = mailbody.Replace("{InitDate}", mycase.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{CaseType}", mycase.CaseType.ToString());
                if (mycase.Poprocs.Count > 0)
                {
                    if (mycase.Poprocs.FirstOrDefault().Status == CaseProcessStatus.結案)
                    {
                        mailbody = mailbody.Replace("{EndDateTime}",
                            mycase.Poprocs.FirstOrDefault().EndDateTime.Value.ToString("yyyy/MM/dd"));
                        mailbody = mailbody.Replace("{AssignMemo}",
                            Txt2Html(mycase.Poprocs.FirstOrDefault().AssignMemo));
                    }
                    else
                    {
                        mailbody = mailbody.Replace("{EndDateTime}",
                            "");
                        mailbody = mailbody.Replace("{AssignMemo}",
                            "");
                    }
                }
                else
                {
                    mailbody = mailbody.Replace("{EndDateTime}",
                        "");
                    mailbody = mailbody.Replace("{AssignMemo}",
                        "");
                }


                string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];
                mailbody = mailbody.Replace("{URL}", InternetURL + "casewq/index/" + mycase.CaseGuid);
                //Utility.SendGmailMail("topidea.justin@gmail.com", myPoproc.Case.Email, "臺南市政府警察局-結案通知", mailbody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(mycase.Email, "臺南市政府警察局-結案通知", mailbody);
                ViewBag.message = "信件已寄出!";


            }
            ViewBag.pclass = pclass;
            return View(mycase);
        }

        //案件狀態回復
        public ActionResult Restone(int? page, int pclass, FormCollection fc)
        {
            ViewBag.pclass = pclass;
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var myCases = _db.Cases.Include(c => c.Poprocs).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);

            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.isAdmin = true;

            }
            else if (member.Roles.Any(x => x.Subject.Contains("交通檢舉信箱管理者")))
            {
                ViewBag.isAdmin = true;
            }
            else if (member.Roles.Any(x => x.Subject.Contains("所有案件查詢")))
            {
                ViewBag.isAdmin = true;
            }
            else
            {
                ViewBag.isAdmin = false;
            }

            if (ViewBag.isAdmin == false)
            {
                switch (pclass)
                {
                    case 1:
                        if (member.MyUnit.Id == 29)
                        {
                            myCases = myCases.Where(
                                w => (w.CaseType == CaseType.參觀本局暨所屬機關 && w.WebSiteId == webSite.Id) ||
                                     w.CaseType == CaseType.首長信箱);
                        }
                        else
                        {
                            myCases = myCases.Where(
                                w =>
                                    (w.WebSiteId == webSite.Id || w.Poprocs.Count(x => x.UnitId == member.UnitId) > 0));
                        }

                        break;
                    case 2:
                        myCases = myCases.Where(w => w.CaseType == CaseType.檢舉貪瀆信箱);

                        break;
                    case 3:
                        myCases = myCases.Where(w => w.CaseType == CaseType.網路報案);
                        break;
                    case 4:
                        myCases = myCases.Where(w => w.CaseType == CaseType.婦幼安全警示地點);
                        break;

                }
            }


            if (hasViewData("SearchByCaseId"))
            {
                string searchByCaseId = getViewDateStr("SearchByCaseId");
                myCases = myCases.Where(w => w.CaseID.Contains(searchByCaseId));
            }
            else
            {
                myCases = myCases.Where(w => w.CaseID == "");
            }




            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(myCases.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));


        }

        public ActionResult RestoneEdit(int pclass, int id = 0)
        {
            Case mycCase = _db.Cases.Find(id);
            if (mycCase == null)
            {
                return HttpNotFound();
            }

            ViewBag.pclass = pclass;
            return View(mycCase);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestoneEdit(int Id, int pclass, string processtype)
        {
            Case mycase = _db.Cases.Find(Id);
            if (mycase == null)
            {
                return HttpNotFound();
            }


            //var owner = myPoproc.assignMember;
            mycase.IsUnitAssign = BooleanType.否;

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            for (int i = mycase.Poprocs.Count - 1; i >= 0; i--)
            {
                CasePoproc poproc = mycase.Poprocs.ElementAt(i);
                mycase.Poprocs.Remove(poproc);
                _db.CasePoprocs.Remove(poproc);
            }

            CasePoprocLog log = new CasePoprocLog();
            log.CaseId = mycase.Id;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = "案件回復";
            log.UnitId = member.UnitId;
            log.ActionMemo = "案件回復";
            _db.CasePoprocLogs.Add(log);




            _db.SaveChanges();



            ViewBag.message = "案件已回復!";
            ViewBag.pclass = pclass;
            return View(mycase);
        }

        //
        // POST: /Case/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Restone(string CaseID, int pclass)
        //{
        //    Case myCase = _db.Cases.FirstOrDefault(x => x.CaseID == CaseID);
        //    if (myCase == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    for (int i = myCase.Poprocs.Count - 1; i >= 0; i--)
        //    {
        //        CasePoproc poproc = myCase.Poprocs.ElementAt(i);
        //        myCase.Poprocs.Remove(poproc);
        //        _db.CasePoprocs.Remove(poproc);
        //    }

        //    Member member =
        //        _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
        //    myCase.IsUnitAssign = BooleanType.否;
        //    CasePoprocLog log = new CasePoprocLog();
        //    log.CaseId = myCase.Id;
        //    log.MemberId = member.Id;
        //    log.InitDate = DateTime.Now;
        //    log.Action = "案件回復";
        //    log.UnitId = member.UnitId;
        //    log.ActionMemo = "案件回復";
        //    _db.CasePoprocLogs.Add(log);




        //    _db.SaveChanges();

        //    ViewBag.pclass = pclass;
        //    ViewBag.message = "案件已回復!";
        //    return View();
        //}

        public ActionResult Print1(int id = 0)
        {
            Case case1 = _db.Cases.Find(id);

            if (case1 == null)
            {
                return HttpNotFound();

            }
            int i = 1;
            string CaseType = "";
            switch (case1.CaseType)
            {
                case TnpdModels.CaseType.首長信箱:
                    i = 1;
                    CaseType = "首長信箱";
                    break;
                case TnpdModels.CaseType.分局長與大隊隊長信箱:
                    i = 2;
                    CaseType = "分局長與大隊隊長信箱";
                    break;
                case TnpdModels.CaseType.檢舉貪瀆信箱:
                    i = 3;
                    CaseType = "檢舉貪瀆信箱";
                    break;
                case TnpdModels.CaseType.網路報案:
                    i = 4;
                    CaseType = "網路報案";
                    break;
                case TnpdModels.CaseType.參觀本局暨所屬機關:
                    i = 5;
                    CaseType = "參觀本局暨所屬機關";
                    break;
                case TnpdModels.CaseType.婦幼安全警示地點:
                    i = 6;
                    CaseType = "婦幼安全警示地點";
                    break;

            }

            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print" + i.ToString() + ".doc"), System.Text.Encoding.Default);
            temp = temp.Replace("{CaseID}", case1.CaseID);
            temp = temp.Replace("{InitDate}", case1.InitDate.Value.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Tel}", case1.TEL);
            temp = temp.Replace("{Subject}", case1.Subject);
            temp = temp.Replace("{Content}", Txt2Html(case1.Content));
            if (case1.Poprocs.Count > 0)
            {
                temp = temp.Replace("{AssignMemo}", case1.Poprocs.FirstOrDefault().AssignMemo);
                temp = temp.Replace("{assignUnit}", case1.Poprocs.FirstOrDefault().assignUnit.Subject);
            }
            else
            {
                temp = temp.Replace("{AssignMemo}", "");
                temp = temp.Replace("{assignUnit}", "");
            }
            temp = temp.Replace("{Predate}", case1.Predate.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Unit}", case1.Unit);
            temp = temp.Replace("{pcnt}", case1.pcnt);
            if (case1.ODate.HasValue)
            {
                temp = temp.Replace("{ODate}", case1.ODate.Value.ToString("yyyy/MM/dd"));
            }
            temp = temp.Replace("{STime}", case1.STime + "~" + case1.ETime);
            temp = temp.Replace("{Job}", case1.Job);
            temp = temp.Replace("{Gender}", case1.Gender.ToString());

            temp = temp.Replace("{HomeTEL}", case1.HomeTEL);
            temp = temp.Replace("{OfficeTEL}", case1.OfficeTEL);
            if (case1.CaseType == TnpdModels.CaseType.網路報案)
            {
                temp = temp.Replace("{STime}", case1.STime);
            }

            temp = temp.Replace("{Oplace}", case1.Oplace);
            temp = temp.Replace("{PostalCode}", case1.PostalCode);
            temp = temp.Replace("{PPostalCode}", case1.PPostalCode);
            temp = temp.Replace("{PAddress}", case1.PAddress);
            temp = temp.Replace("{CaseReportType}", case1.CaseReportType.ToString());
            temp = temp.Replace("{PAddress}", case1.PAddress);
            temp = temp.Replace("{OrgName}", case1.OrgName);

            temp = temp.Replace("{OArea}", case1.OArea);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + case1.CaseID + ".doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + case1.CaseID + ".doc"), "application/msword", "臺南市政府警察局" + CaseType + "案件表(案件編號：" + case1.CaseID + ").doc");

        }

        public ActionResult Print1all(int id = 0)
        {
            Case case1 = _db.Cases.Find(id);


            if (case1 == null)
            {
                return HttpNotFound();

            }
            int i = 1;
            string CaseType = "";
            switch (case1.CaseType)
            {
                case TnpdModels.CaseType.首長信箱:
                    i = 1;
                    CaseType = "首長信箱";
                    break;
                case TnpdModels.CaseType.分局長與大隊隊長信箱:
                    i = 2;
                    CaseType = "分局長與大隊隊長信箱";
                    break;
                case TnpdModels.CaseType.檢舉貪瀆信箱:
                    i = 3;
                    CaseType = "檢舉貪瀆信箱";
                    break;
                case TnpdModels.CaseType.網路報案:
                    i = 4;
                    CaseType = "網路報案";
                    break;
                case TnpdModels.CaseType.參觀本局暨所屬機關:
                    i = 5;
                    CaseType = "參觀本局暨所屬機關";
                    break;
                case TnpdModels.CaseType.婦幼安全警示地點:
                    i = 6;
                    CaseType = "婦幼安全警示地點";
                    break;

            }
            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print" + i.ToString() + "(all).doc"), System.Text.Encoding.Default);
            temp = temp.Replace("{CaseID}", case1.CaseID);
            temp = temp.Replace("{Name}", case1.Name);
            temp = temp.Replace("{Tel}", case1.TEL);
            temp = temp.Replace("{Address}", case1.Address);
            temp = temp.Replace("{InitDate}", case1.InitDate.Value.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Tel}", case1.TEL);
            temp = temp.Replace("{Email}", case1.Email);
            temp = temp.Replace("{Subject}", case1.Subject);
            temp = temp.Replace("{Content}", Txt2Html(case1.Content));
            if (case1.Poprocs.Count > 0)
            {
                temp = temp.Replace("{AssignMemo}", case1.Poprocs.FirstOrDefault().AssignMemo);
                temp = temp.Replace("{assignUnit}", case1.Poprocs.FirstOrDefault().assignUnit.Subject);
            }
            else
            {
                temp = temp.Replace("{AssignMemo}", "");
                temp = temp.Replace("{assignUnit}", "");
            }
            temp = temp.Replace("{Predate}", case1.Predate.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Unit}", case1.Unit);
            temp = temp.Replace("{pcnt}", case1.pcnt);
            if (case1.ODate.HasValue)
            {
                temp = temp.Replace("{ODate}", case1.ODate.Value.ToString("yyyy/MM/dd"));
            }

            temp = temp.Replace("{STime}", case1.STime + "~" + case1.ETime);
            temp = temp.Replace("{Job}", case1.Job);
            temp = temp.Replace("{Gender}", case1.Gender.ToString());
            temp = temp.Replace("{HomeTEL}", case1.HomeTEL);
            temp = temp.Replace("{OfficeTEL}", case1.OfficeTEL);
            if (case1.CaseType == TnpdModels.CaseType.網路報案)
            {
                temp = temp.Replace("{STime}", case1.STime);
            }

            temp = temp.Replace("{Oplace}", case1.Oplace);
            temp = temp.Replace("{PostalCode}", case1.PostalCode);
            temp = temp.Replace("{PPostalCode}", case1.PPostalCode);
            temp = temp.Replace("{PAddress}", case1.PAddress);
            temp = temp.Replace("{CaseReportType}", case1.CaseReportType.ToString());
            temp = temp.Replace("{PAddress}", case1.PAddress);
            temp = temp.Replace("{OArea}", case1.OArea);
            temp = temp.Replace("{OrgName}", case1.OrgName);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + case1.CaseID + "all.doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + case1.CaseID + "all.doc"), "application/msword", "臺南市政府警察局" + CaseType + "案件表(案件編號：" + case1.CaseID + ").doc");
        }

        public ActionResult Print2(int id = 0)
        {
            CasePoproc myPoproc = _db.CasePoprocs.Find(id);

            if (myPoproc == null)
            {
                return HttpNotFound();

            }
            int i = 1;
            string CaseType = "";
            switch (myPoproc.Case.CaseType)
            {
                case TnpdModels.CaseType.首長信箱:
                    i = 1;
                    CaseType = "首長信箱";
                    break;
                case TnpdModels.CaseType.分局長與大隊隊長信箱:
                    i = 2;
                    CaseType = "分局長與大隊隊長信箱";
                    break;
                case TnpdModels.CaseType.檢舉貪瀆信箱:
                    i = 3;
                    CaseType = "檢舉貪瀆信箱";
                    break;
                case TnpdModels.CaseType.網路報案:
                    i = 4;
                    CaseType = "網路報案";
                    break;
                case TnpdModels.CaseType.參觀本局暨所屬機關:
                    i = 5;
                    CaseType = "參觀本局暨所屬機關";
                    break;
                case TnpdModels.CaseType.婦幼安全警示地點:
                    i = 6;
                    CaseType = "婦幼安全警示地點";
                    break;

            }

            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print" + i.ToString() + ".doc"), System.Text.Encoding.Default);
            temp = temp.Replace("{CaseID}", myPoproc.Case.CaseID);
            temp = temp.Replace("{InitDate}", myPoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Tel}", myPoproc.Case.TEL);
            temp = temp.Replace("{Subject}", myPoproc.Case.Subject);
            temp = temp.Replace("{Content}", Txt2Html(myPoproc.Case.Content));
            temp = temp.Replace("{AssignMemo}", myPoproc.AssignMemo);
            if (myPoproc.EndDateTime.HasValue)
            {
                temp = temp.Replace("{assignUnit}", myPoproc.assignUnit.ParentUnit.Subject + "-" + myPoproc.assignUnit.Subject + "(結案日：" + myPoproc.EndDateTime.Value.ToString("yyyy/MM/dd") + ")");
            }
            else
            {
                temp = temp.Replace("{assignUnit}", myPoproc.assignUnit.ParentUnit.Subject + "-" + myPoproc.assignUnit.Subject);
            }

            temp = temp.Replace("{Predate}", myPoproc.Case.Predate.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Unit}", myPoproc.Case.Unit);
            temp = temp.Replace("{pcnt}", myPoproc.Case.pcnt);
            if (myPoproc.Case.ODate.HasValue)
            {
                temp = temp.Replace("{ODate}", myPoproc.Case.ODate.Value.ToString("yyyy/MM/dd"));
            }

            temp = temp.Replace("{STime}", myPoproc.Case.STime + "~" + myPoproc.Case.ETime);
            temp = temp.Replace("{Job}", myPoproc.Case.Job);
            temp = temp.Replace("{Gender}", myPoproc.Case.Gender.ToString());

            temp = temp.Replace("{HomeTEL}", myPoproc.Case.HomeTEL);
            temp = temp.Replace("{OfficeTEL}", myPoproc.Case.OfficeTEL);
            if (myPoproc.Case.CaseType == TnpdModels.CaseType.網路報案)
            {
                temp = temp.Replace("{STime}", myPoproc.Case.STime);
            }

            temp = temp.Replace("{Oplace}", myPoproc.Case.Oplace);
            temp = temp.Replace("{PostalCode}", myPoproc.Case.PostalCode);
            temp = temp.Replace("{PPostalCode}", myPoproc.Case.PPostalCode);
            temp = temp.Replace("{PAddress}", myPoproc.Case.PAddress);
            temp = temp.Replace("{CaseReportType}", myPoproc.Case.CaseReportType.ToString());
            temp = temp.Replace("{PAddress}", myPoproc.Case.PAddress);

            temp = temp.Replace("{OArea}", myPoproc.Case.OArea);
            temp = temp.Replace("{OrgName}", myPoproc.Case.OrgName);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + myPoproc.Case.CaseID + ".doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + myPoproc.Case.CaseID + ".doc"), "application/msword", "臺南市政府警察局" + CaseType + "案件表(案件編號：" + myPoproc.Case.CaseID + ").doc");

        }

        public ActionResult Print2all(int id = 0)
        {
            CasePoproc myPoproc = _db.CasePoprocs.Find(id);


            if (myPoproc == null)
            {
                return HttpNotFound();

            }
            int i = 1;
            string CaseType = "";
            switch (myPoproc.Case.CaseType)
            {
                case TnpdModels.CaseType.首長信箱:
                    i = 1;
                    CaseType = "首長信箱";
                    break;
                case TnpdModels.CaseType.分局長與大隊隊長信箱:
                    i = 2;
                    CaseType = "分局長與大隊隊長信箱";
                    break;
                case TnpdModels.CaseType.檢舉貪瀆信箱:
                    i = 3;
                    CaseType = "檢舉貪瀆信箱";
                    break;
                case TnpdModels.CaseType.網路報案:
                    i = 4;
                    CaseType = "網路報案";
                    break;
                case TnpdModels.CaseType.參觀本局暨所屬機關:
                    i = 5;
                    CaseType = "參觀本局暨所屬機關";
                    break;
                case TnpdModels.CaseType.婦幼安全警示地點:
                    i = 6;
                    CaseType = "婦幼安全警示地點";
                    break;

            }
            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print" + i.ToString() + "(all).doc"), System.Text.Encoding.Default);
            temp = temp.Replace("{CaseID}", myPoproc.Case.CaseID);
            temp = temp.Replace("{Name}", myPoproc.Case.Name);
            temp = temp.Replace("{Tel}", myPoproc.Case.TEL);
            temp = temp.Replace("{Address}", myPoproc.Case.Address);
            temp = temp.Replace("{InitDate}", myPoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Tel}", myPoproc.Case.TEL);
            temp = temp.Replace("{Email}", myPoproc.Case.Email);
            temp = temp.Replace("{Subject}", myPoproc.Case.Subject);
            temp = temp.Replace("{Content}", Txt2Html(myPoproc.Case.Content));
            temp = temp.Replace("{AssignMemo}", myPoproc.AssignMemo);
            if (myPoproc.EndDateTime.HasValue)
            {
                temp = temp.Replace("{assignUnit}", myPoproc.assignUnit.ParentUnit.Subject + "-" + myPoproc.assignUnit.Subject + "(結案日：" + myPoproc.EndDateTime.Value.ToString("yyyy/MM/dd") + ")");
            }
            else
            {
                temp = temp.Replace("{assignUnit}", myPoproc.assignUnit.ParentUnit.Subject + "-" + myPoproc.assignUnit.Subject);
            }
            temp = temp.Replace("{Predate}", myPoproc.Case.Predate.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Unit}", myPoproc.Case.Unit);
            temp = temp.Replace("{pcnt}", myPoproc.Case.pcnt);
            if (myPoproc.Case.ODate.HasValue)
            {
                temp = temp.Replace("{ODate}", myPoproc.Case.ODate.Value.ToString("yyyy/MM/dd"));
            }
            temp = temp.Replace("{STime}", myPoproc.Case.STime + "~" + myPoproc.Case.ETime);
            temp = temp.Replace("{Job}", myPoproc.Case.Job);
            temp = temp.Replace("{Gender}", myPoproc.Case.Gender.ToString());

            temp = temp.Replace("{HomeTEL}", myPoproc.Case.HomeTEL);
            temp = temp.Replace("{OfficeTEL}", myPoproc.Case.OfficeTEL);
            if (myPoproc.Case.CaseType == TnpdModels.CaseType.網路報案)
            {
                temp = temp.Replace("{STime}", myPoproc.Case.STime);
            }

            temp = temp.Replace("{Oplace}", myPoproc.Case.Oplace);
            temp = temp.Replace("{PostalCode}", myPoproc.Case.PostalCode);
            temp = temp.Replace("{PPostalCode}", myPoproc.Case.PPostalCode);
            temp = temp.Replace("{PAddress}", myPoproc.Case.PAddress);
            temp = temp.Replace("{CaseReportType}", myPoproc.Case.CaseReportType.ToString());
            temp = temp.Replace("{PAddress}", myPoproc.Case.PAddress);

            temp = temp.Replace("{OArea}", myPoproc.Case.OArea);
            temp = temp.Replace("{OrgName}", myPoproc.Case.OrgName);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + myPoproc.Case.CaseID + "all.doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + myPoproc.Case.CaseID + "all.doc"), "application/msword", "臺南市政府警察局" + CaseType + "案件表(案件編號：" + myPoproc.Case.CaseID + ").doc");
        }

        public ActionResult Report1(int pclass)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report1(string SearchByStartDate, string SearchByEndDate, int pclass)
        {


            var casePoprocs = _db.CasePoprocs.Include(c => c.Case).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);

            switch (pclass)
            {
                case 1:
                    if (member.MyUnit.Id == 29)
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                    }
                    else
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                    }
                    break;
                case 2:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);

                    break;
                case 3:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                    break;
                case 4:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                    break;

            }

            if (!string.IsNullOrEmpty(SearchByStartDate) && !string.IsNullOrEmpty(SearchByEndDate))
            {
                DateTime startDate = Convert.ToDateTime(SearchByStartDate);
                DateTime endDate = Convert.ToDateTime(SearchByEndDate).AddDays(1);
                casePoprocs = casePoprocs.Where(w => w.Case.InitDate >= startDate && w.Case.InitDate <= endDate);
            }
            else
            {
                return View();
            }


            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/casedayview.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/casedayview.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);

            var casePoprocslist = casePoprocs.OrderByDescending(x => x.Case.InitDate).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var casePoproc in casePoprocslist)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{CaseID}", casePoproc.Case.CaseID);
                strTr = strTr.Replace("{CaseType}", casePoproc.Case.CaseType.ToString());
                strTr = strTr.Replace("{initDate}", casePoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{Name}", casePoproc.Case.Name + " ");
                strTr = strTr.Replace("{Subject}", casePoproc.Case.Subject + " ");
                strTr = strTr.Replace("{assignUnit}", casePoproc.assignUnit.ParentUnit.Subject + "-" + casePoproc.assignUnit.Subject);
                strTr = strTr.Replace("{Predate}", casePoproc.Case.Predate.ToString("yyyy/MM/dd"));

                Double timespan;
                if (casePoproc.EndDateTime.HasValue)
                {
                    strTr = strTr.Replace("{EndDateTime}", casePoproc.EndDateTime.Value.ToString("yyyy/MM/dd"));
                    timespan = new TimeSpan(casePoproc.EndDateTime.Value.Ticks -
                                            casePoproc.Case.InitDate.Value.Ticks).TotalDays;
                    strTr = strTr.Replace("{due}", Math.Round(timespan, 2).ToString());
                }
                else
                {
                    strTr = strTr.Replace("{EndDateTime}", "");
                    timespan = new TimeSpan(DateTime.Now.Ticks - casePoproc.Case.InitDate.Value.Ticks).TotalDays;
                    strTr = strTr.Replace("{due}", Math.Round(timespan, 2).ToString());
                }


                sb.AppendLine(strTr);
            }
            tempBody = tempBody.Replace("{bodytr}", sb.ToString());

            string fileName = member.Account + ".xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "案件處理天數一覽表.xls");

        }


        public ActionResult Report2(int pclass)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report2(string SearchByStartDate, string SearchByEndDate, int pclass)
        {


            var casePoprocs = _db.CasePoprocs.Include(c => c.Case).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);

            switch (pclass)
            {
                case 1:
                    if (member.MyUnit.Id == 29)
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                    }
                    else
                    {
                        casePoprocs = casePoprocs.Where(w => (w.Case.WebSiteId == webSite.Id ||  w.UnitId == member.UnitId));
                    }

                   

                    break;
                case 2:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);

                    break;
                case 3:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                    break;
                case 4:
                    casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                    break;

            }

            if (!string.IsNullOrEmpty(SearchByStartDate) && !string.IsNullOrEmpty(SearchByEndDate))
            {
                DateTime startDate = Convert.ToDateTime(SearchByStartDate);
                DateTime endDate = Convert.ToDateTime(SearchByEndDate).AddDays(1);
                casePoprocs = casePoprocs.Where(w => w.Case.InitDate >= startDate && w.Case.InitDate <= endDate);
            }
            else
            {
                return View();
            }


            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/CaseView.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/CaseView.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);

            var casePoprocslist = casePoprocs.OrderByDescending(x => x.Case.InitDate).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var casePoproc in casePoprocslist)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{CaseID}", casePoproc.Case.CaseID);
                strTr = strTr.Replace("{CaseType}", casePoproc.Case.CaseType.ToString());
                strTr = strTr.Replace("{initDate}", casePoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{Name}", casePoproc.Case.Name);
                strTr = strTr.Replace("{Subject}", casePoproc.Case.Subject);
                if (casePoproc.assignUnit == null)
                {
                    strTr = strTr.Replace("{assignUnit}",
                        "");
                }
                else if (casePoproc.assignUnit.ParentUnit == null)
                {
                    strTr = strTr.Replace("{assignUnit}",
                         casePoproc.assignUnit.Subject);
                }
                else
                {
                    strTr = strTr.Replace("{assignUnit}",
                        casePoproc.assignUnit.ParentUnit.Subject + "-" + casePoproc.assignUnit.Subject);
                }

                strTr = strTr.Replace("{Predate}", casePoproc.Case.Predate.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{Status}", casePoproc.Status.ToString());
                if (casePoproc.EndDateTime.HasValue)
                {
                    strTr = strTr.Replace("{EndDateTime}", casePoproc.EndDateTime.Value.ToString("yyyy/MM/dd"));
                }
                else
                {
                    strTr = strTr.Replace("{EndDateTime}", "");
                }

                strTr = strTr.Replace("{Type1}", casePoproc.Type1);
                strTr = strTr.Replace("{type2}", casePoproc.type2);
                sb.AppendLine(strTr);
            }
            tempBody = tempBody.Replace("{bodytr}", sb.ToString());

            string fileName = member.Account + ".xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "案件一覽表.xls");

        }
        public ActionResult AddMerge(int id)
        {
            List<CaseMerge> caseMerges;
            if (Session["CaseMerge"] == null)
            {
                caseMerges = new List<CaseMerge>();

            }
            else
            {
                caseMerges = (List<CaseMerge>)Session["CaseMerge"];
            }

            if (caseMerges.Count(x => x.Id == id) > 0)
            {
                return Content("Error");
            }


            Case mCase = _db.Cases.Find(id);
            if (mCase == null)
            {
                return Content("Error");
            }

            CaseMerge caseMerge = new CaseMerge();
            caseMerge.CaseID = mCase.CaseID;
            caseMerge.CaseType = mCase.CaseType;
            caseMerge.Id = mCase.Id;
            caseMerge.Subject = mCase.Subject;
            caseMerge.Name = mCase.Name;
            caseMerge.InitDate = mCase.InitDate;
            caseMerges.Add(caseMerge);
            Session["CaseMerge"] = caseMerges;
            return Content(caseMerges.Count().ToString());
        }

        public ActionResult AddMerges(string id)
        {
            string[] ids = id.Trim(',').Split(',');

            List<CaseMerge> caseMerges;
            if (Session["CaseMerge"] == null)
            {
                caseMerges = new List<CaseMerge>();

            }
            else
            {
                caseMerges = (List<CaseMerge>)Session["CaseMerge"];
            }

            foreach (var item in ids)
            {
                int itemid = Convert.ToInt32(item);
                if (caseMerges.Count(x => x.Id == itemid) > 0)
                {
                    continue;
                }
                Case mCase = _db.Cases.Find(itemid);
                if (mCase == null)
                {
                    continue;
                }
                CaseMerge caseMerge = new CaseMerge();
                caseMerge.CaseID = mCase.CaseID;
                caseMerge.CaseType = mCase.CaseType;
                caseMerge.Id = mCase.Id;
                caseMerge.Subject = mCase.Subject;
                caseMerge.Name = mCase.Name;
                caseMerge.InitDate = mCase.InitDate;
                caseMerges.Add(caseMerge);
                Session["CaseMerge"] = caseMerges;
            }
           
            return Content(caseMerges.Count().ToString());
        }

        [HttpPost]
        public ActionResult RemoveMerge(int id)
        {
            List<CaseMerge> caseMerges;
            if (Session["CaseMerge"] == null)
            {
                caseMerges = new List<CaseMerge>();

            }
            else
            {
                caseMerges = (List<CaseMerge>)Session["CaseMerge"];
            }

            CaseMerge mCase = caseMerges.FirstOrDefault(x => x.Id == id);
            if (mCase == null)
            {
                return Content("Error");
            }
            caseMerges.Remove(mCase);
            Session["CaseMerge"] = caseMerges;
            return RedirectToAction("MergeList");
        }

        public ActionResult MergeList()
        {
            List<CaseMerge> caseMerges;
            if (Session["CaseMerge"] == null)
            {
                caseMerges = new List<CaseMerge>();

            }
            else
            {
                caseMerges = (List<CaseMerge>)Session["CaseMerge"];
            }

            return View(caseMerges);
        }

        [HttpPost]
        public ActionResult MergeList(string CaseID, int pclass)
        {
            List<CaseMerge> caseMerges;
            if (Session["CaseMerge"] == null)
            {
                return RedirectToAction("MergeList", new { pclass = pclass });

            }
            else
            {
                caseMerges = (List<CaseMerge>)Session["CaseMerge"];
                Case ParentCase = _db.Cases.FirstOrDefault(x => x.CaseID == CaseID);
                if (ParentCase == null)
                {
                    ViewBag.Message = "找不到案件編號";
                    return View(caseMerges);
                }


                foreach (CaseMerge merge in caseMerges)
                {
                    Case mCase = _db.Cases.Find(merge.Id);
                    mCase.ParentId = ParentCase.Id;
                }

                _db.SaveChanges();
                caseMerges = null;
                Session["CaseMerge"] = caseMerges;
                return RedirectToAction("Assign", new { pclass = pclass });
            }


        }

        public ActionResult CaseMergeList(int id)
        {
            Case mCase = _db.Cases.Find(id);
            if (mCase == null)
            {
                return Content("Error");
            }

            return View(mCase);
        }
        [HttpPost]
        public ActionResult CaseMergeList(int id, int isClose)
        {
            Case mCase = _db.Cases.Find(id);
            if (mCase == null)
            {
                return Content("Error");
            }

            foreach (var item in mCase.Cases)
            {
                item.ParentId = null;
            }

            _db.SaveChanges();
            ViewBag.isClose = true;
            return View(mCase);
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
