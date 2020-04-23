using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Tnpd.Controllers;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class CaseTrafficsController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //


        public ActionResult Assign(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var cases = _db.CaseTraffics.Include(c => c.assignUnit).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            cases = cases.Where(w => w.UnitId == member.MyUnit.ParentId && (w.IsUnitAssign == BooleanType.否 || w.IsUnitAssign == null));


            cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);

            ViewBag.Total = cases.Count();

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(cases.OrderBy(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult AssignEdit(int id = 0)
        {
            CaseTraffic case1 = _db.CaseTraffics.Find(id);
            if (case1 == null)
            {
                return HttpNotFound();
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;

            var trafficDepartmentdetails = _db.TrafficDepartmentdetails.Where(x => x.assignMember.MyUnit.ParentId == member.MyUnit.ParentId)
                .OrderBy(p => p.InitDate).ToList();


            ViewBag.AssignMemberCount = trafficDepartmentdetails.Count;
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.MemberListSelect = new SelectList(trafficDepartmentdetails, "assignMember.Id", "assignMember.Name");



            return View(case1);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AssignEdit(int Id, string processtype, string PoprocsContent, string process, int? PoprocsType, int? PoprocsSubType, int MemberListSelect, HttpPostedFileBase Upfile)
        {
            CaseTraffic mycase = _db.CaseTraffics.Find(Id);
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
                    var AssignMember = _db.Members.Find(MemberListSelect);
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = mycase.Id;
                    poproc.MemberId = AssignMember.Id;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = AssignMember.UnitId;
                    poproc.Status = CaseProcessStatus.待辦;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;

                    _db.CaseTrafficPoprocs.Add(poproc);
                    CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                    log.CaseId = mycase.Id;
                    log.MemberId = member.Id;
                    log.InitDate = DateTime.Now;
                    log.Action = "派案";
                    log.UnitId = member.UnitId;
                    log.ActionMemo = "案件指派";
                    _db.CaseTrafficPoprocLogs.Add(log);
                    ViewBag.message = "案件已指派!";


                }
            }
            else if (processtype == "Poprocs") //
            {
                CaseTrafficPoproc myPoproc = mycase.Poprocs
                    .Where(x => x.Status == CaseProcessStatus.未分派 && x.MemberId == member.Id).FirstOrDefault();
                if (myPoproc == null)
                {
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = Id;
                    poproc.MemberId = member.Id;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = member.UnitId;
                    poproc.Status = CaseProcessStatus.已辦;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;
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
                    _db.CaseTrafficPoprocs.Add(poproc);

                }
                else
                {
                    CaseTrafficPoproc poproc = myPoproc;
                    poproc.Status = CaseProcessStatus.已辦;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;
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
                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                log.CaseId = mycase.Id;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "核判送出";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CaseTrafficPoprocLogs.Add(log);
                ViewBag.message = "核判送出!";


            }
            else if (processtype == "Save")
            {
                CaseTrafficPoproc myPoproc = mycase.Poprocs
                    .Where(x => x.Status == CaseProcessStatus.未分派 && x.MemberId == member.Id).FirstOrDefault();
                if (myPoproc == null)
                {
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = Id;
                    poproc.MemberId = member.Id;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = member.MyUnit.ParentId;
                    poproc.Status = CaseProcessStatus.未分派;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;
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
                    _db.CaseTrafficPoprocs.Add(poproc);
                }
                else
                {
                    CaseTrafficPoproc poproc = myPoproc;
                    poproc.AssignMemo = PoprocsContent;
                    poproc.process = process;
                    poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;
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
            var trafficDepartmentdetails = _db.TrafficDepartmentdetails.Where(x => x.assignMember.MyUnit.ParentId == member.MyUnit.ParentId)
                .OrderBy(p => p.InitDate).ToList();


            ViewBag.AssignMemberCount = trafficDepartmentdetails.Count;
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.MemberListSelect = new SelectList(trafficDepartmentdetails, "assignMember.Id", "assignMember.Name");

            return View(mycase);
        }

        public ActionResult UnitAssign(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var cases = _db.CaseTraffics.Include(c => c.assignUnit).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            cases = cases.Where(w => w.IsUnitAssign == BooleanType.是);

            ViewBag.Total = cases.Count();

            ViewBag.WebSiteId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1 && x.Id != 194 && x.Id != 333 && x.Id != 270 && x.Id != 364).OrderBy(p => p.ListNum), "Id", "Subject");


            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(cases.OrderBy(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        [HttpPost]
        public ActionResult UnitAssign(string CaseID, int WebSiteId)
        {
            CaseTraffic mycase = _db.CaseTraffics.FirstOrDefault(x => x.CaseID == CaseID);
            Trafficdisdata trafficdisdata = _db.Trafficdisdatas
                .FirstOrDefault(x => x.assignMember.MyUnit.ParentId == WebSiteId);
            if (mycase == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            mycase.IsUnitAssign = BooleanType.否;
            mycase.UnitId = WebSiteId;
            for (int i = mycase.Poprocs.Count - 1; i == 0; i--)
            {
                var item = mycase.Poprocs.ElementAt(i);
                _db.CaseTrafficPoprocs.Remove(item);
            }




            _db.SaveChanges();
            if (trafficdisdata != null)
            {
                if (trafficdisdata.isAutoAssign == BooleanType.是)
                {
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = mycase.Id;
                    poproc.MemberId = trafficdisdata.MemberId;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = trafficdisdata.assignMember.UnitId;
                    poproc.Status = CaseProcessStatus.待辦;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = "";
                    //poproc.process = process;
                    //poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;

                    _db.CaseTrafficPoprocs.Add(poproc);
                    _db.SaveChanges();
                }

            }

            CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
            log.CaseId = mycase.Id;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = "單位改派";
            log.UnitId = member.UnitId;
            log.ActionMemo = "單位改派";
            _db.CaseTrafficPoprocLogs.Add(log);



            _db.SaveChanges();

            ViewBag.WebSiteId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1).OrderBy(p => p.ListNum), "Id", "Subject", mycase.UnitId);
            ViewBag.message = "案件已單位改派!";

            //取得正確的頁面
            int currentPageIndex = 1;

            var cases = _db.CaseTraffics.Include(c => c.assignUnit).OrderByDescending(p => p.InitDate).AsQueryable();



            cases = cases.Where(w => w.IsUnitAssign == BooleanType.是);

            ViewBag.Total = cases.Count();

            ViewBag.WebSiteId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1 && x.Id != 194 && x.Id != 333 && x.Id != 270 && x.Id != 364).OrderBy(p => p.ListNum), "Id", "Subject");


            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(cases.OrderBy(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        public ActionResult UnitAssignEdit(int id = 0)
        {
            CaseTraffic case1 = _db.CaseTraffics.Find(id);
            if (case1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebSiteId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1 && x.Id != 194 && x.Id != 333 && x.Id != 270 && x.Id != 364).OrderBy(p => p.ListNum), "Id", "Subject", case1.UnitId);

            return View(case1);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UnitAssignEdit(int Id, int WebSiteId)
        {
            CaseTraffic mycase = _db.CaseTraffics.Find(Id);
            Trafficdisdata trafficdisdata = _db.Trafficdisdatas
                .FirstOrDefault(x => x.assignMember.MyUnit.ParentId == WebSiteId);
            if (mycase == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            mycase.IsUnitAssign = BooleanType.否;
            mycase.UnitId = WebSiteId;
            _db.SaveChanges();
            if (trafficdisdata != null)
            {
                if (trafficdisdata.isAutoAssign == BooleanType.是)
                {
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = mycase.Id;
                    poproc.MemberId = trafficdisdata.MemberId;
                    poproc.CaseTime = mycase.InitDate.Value;
                    poproc.UnitId = trafficdisdata.assignMember.UnitId;
                    poproc.Status = CaseProcessStatus.待辦;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = "";
                    //poproc.process = process;
                    //poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;

                    _db.CaseTrafficPoprocs.Add(poproc);
                    _db.SaveChanges();
                }

            }

            CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
            log.CaseId = mycase.Id;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = "單位改派";
            log.UnitId = member.UnitId;
            log.ActionMemo = "單位改派";
            _db.CaseTrafficPoprocLogs.Add(log);



            _db.SaveChanges();

            ViewBag.WebSiteId = new SelectList(_db.Units.Where(x => x.ParentId == null && x.Id != 1).OrderBy(p => p.ListNum), "Id", "Subject", mycase.UnitId);
            ViewBag.message = "案件已單位改派!";

            return View(mycase);
        }

        public ActionResult Process(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                casePoprocs = casePoprocs.Where(w => w.Case.InitDate >= startDate && w.Case.InitDate <= endDate);
            }


            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);


            casePoprocs = casePoprocs.Where(x => (x.Status == CaseProcessStatus.待辦 || x.Status == CaseProcessStatus.辦案) && x.MemberId == member.Id);

            ViewBag.Total = casePoprocs.Count();

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderBy(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult ProcessEdit(int id = 0)
        {
            CaseTrafficPoproc casePoproc = _db.CaseTrafficPoprocs.Find(id);
            if (casePoproc == null)
            {
                return HttpNotFound();
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            Holiday holiday = new Holiday();
            ViewBag.ExtensionDay = holiday.GetWorkDay(casePoproc.Case.Predate, 7);

            ViewBag.ViolationsClasses = new SelectList(_db.TrafficViolationsClasses.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.ViolationsRejectclasses = new SelectList(_db.TrafficViolationsRejectclasses.OrderBy(p => p.ListNum), "Id", "Subject");


            return View(casePoproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ProcessEdit(int Id, string processtype, string PoprocsContent, string process, int? PoprocsType, int? ViolationsRejectclasses, int? ViolationsClasses, DateTime? DelyDateTime, string DelyMemo, string noplyreason, HttpPostedFileBase Upfile)
        {
            CaseTrafficPoproc myPoproc = _db.CaseTrafficPoprocs.Find(Id);
            if (myPoproc == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            if (processtype == "Poprocs") //
            {

                CaseTrafficPoproc poproc = myPoproc;
                poproc.Status = CaseProcessStatus.已辦;
                poproc.AssignMemo = PoprocsContent;
                poproc.process = process;
                poproc.PoprocsType = PoprocsType;
                poproc.ViolationsRejectclassId = ViolationsRejectclasses;
                poproc.TrafficViolationsClassId = ViolationsClasses;
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
                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                log.CaseId = poproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "核判送出";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CaseTrafficPoprocLogs.Add(log);
                ViewBag.message = "案件已核判送出!";


            }
            else if (processtype == "Save")
            {

                CaseTrafficPoproc poproc = myPoproc;
                poproc.AssignMemo = PoprocsContent;
                poproc.process = process;
                poproc.Status = CaseProcessStatus.辦案;
                poproc.PoprocsType = PoprocsType;
                poproc.ViolationsRejectclassId = ViolationsRejectclasses;
                poproc.TrafficViolationsClassId = ViolationsClasses;

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
                CaseTrafficPoproc poproc = myPoproc;
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
                CaseTrafficPoproc poproc = myPoproc;
                poproc.Status = CaseProcessStatus.退文;
                poproc.noplyreason = noplyreason;
                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                log.CaseId = poproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "案件申請退文";
                log.UnitId = member.UnitId;
                log.ActionMemo = DelyMemo;
                _db.CaseTrafficPoprocLogs.Add(log);
                ViewBag.message = "案件已申請退文!";
            }
            _db.SaveChanges();


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
            ViewBag.ViolationsClasses = new SelectList(_db.TrafficViolationsClasses.OrderBy(p => p.ListNum), "Id", "Subject");
            ViewBag.ViolationsRejectclasses = new SelectList(_db.TrafficViolationsRejectclasses.OrderBy(p => p.ListNum), "Id", "Subject");

            return View(myPoproc);
        }

        public ActionResult Extension(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延 && x.Case.UnitId == member.MyUnit.ParentId);
            ViewBag.Total = casePoprocs.Count();


            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderBy(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult ExtensionEdit(int id = 0)
        {
            CaseTrafficPoproc casePoproc = _db.CaseTrafficPoprocs.Find(id);
            if (casePoproc == null)
            {
                return HttpNotFound();
            }

            return View(casePoproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ExtensionEdit(int Id, int isPass)
        {
            CaseTrafficPoproc myPoproc = _db.CaseTrafficPoprocs.Find(Id);
            if (myPoproc == null)
            {
                return HttpNotFound();
            }
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            CaseTrafficPoproc poproc = myPoproc;
            poproc.Status = CaseProcessStatus.待辦;
            ViewBag.message = "案件不通過展延!";
            if (isPass == 1)
            {
                poproc.Case.Predate = poproc.DelyDateTime.Value;
                ViewBag.message = "案件已展延!";
            }
            CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
            log.CaseId = poproc.CaseId;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = ViewBag.message;
            log.UnitId = member.UnitId;
            log.ActionMemo = ViewBag.message;
            _db.CaseTrafficPoprocLogs.Add(log);
            _db.SaveChanges();

            return View(myPoproc);
        }

        public ActionResult Goback(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }

            else if (member.Roles.Any(x => x.Subject.Contains("交通檢舉信箱管理者")))
            {
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);
            }
            else
            {
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文 && x.Case.UnitId == member.MyUnit.ParentId);
            }

            ViewBag.Total = casePoprocs.Count();



            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderBy(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult GobackEdit(int id = 0)
        {
            CaseTrafficPoproc poproc = _db.CaseTrafficPoprocs.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;


            var trafficDepartmentdetails = _db.TrafficDepartmentdetails.Where(x => x.assignMember.MyUnit.ParentId == member.MyUnit.ParentId)
                .OrderBy(p => p.InitDate).ToList();


            ViewBag.AssignMemberCount = trafficDepartmentdetails.Count;
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.MemberListSelect = new SelectList(trafficDepartmentdetails, "assignMember.Id", "assignMember.Name");

            return View(poproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult GobackEdit(int Id, string processtype, int MemberListSelect)
        {
            CaseTrafficPoproc myPoproc = _db.CaseTrafficPoprocs.Find(Id);
            CaseTraffic mycase = myPoproc.Case;
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
                    var AssignMembers = _db.Members.Find(MemberListSelect);
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = myPoproc.CaseId;
                    poproc.MemberId = AssignMembers.Id;
                    poproc.CaseTime = myPoproc.Case.InitDate.Value;
                    poproc.UnitId = AssignMembers.UnitId;
                    poproc.Status = CaseProcessStatus.待辦;
                    poproc.AssignDateTime = DateTime.Now;
                    //poproc.AssignMemo = PoprocsContent;

                    mycase.UnitId = AssignMembers.MyUnit.ParentId;


                    _db.CaseTrafficPoprocs.Add(poproc);
                    CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                    log.CaseId = myPoproc.CaseId;
                    log.MemberId = member.Id;
                    log.InitDate = DateTime.Now;
                    log.Action = "案件改派";
                    log.UnitId = member.UnitId;
                    log.ActionMemo = "案件改派";
                    _db.CaseTrafficPoprocLogs.Add(log);
                    ViewBag.message = "案件已改派!";
                    myPoproc.Case.Poprocs.Remove(myPoproc);
                    _db.CaseTrafficPoprocs.Remove(myPoproc);


                }
            }
            else if (processtype == "Delete")
            {
                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();

                var owner = myPoproc.assignMember;
                log.CaseId = mycase.Id;
                log.MemberId = owner.Id;
                log.InitDate = DateTime.Now;
                log.Action = "刪除分文";
                log.UnitId = owner.UnitId;
                log.ActionMemo = "刪除給分文";
                _db.CaseTrafficPoprocLogs.Add(log);
                myPoproc.Case.Poprocs.Remove(myPoproc);
                _db.CaseTrafficPoprocs.Remove(myPoproc);


                ViewBag.message = "刪除分文!";


            }
            _db.SaveChanges();



            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;

            var trafficDepartmentdetails = _db.TrafficDepartmentdetails.Where(x => x.assignMember.MyUnit.ParentId == member.MyUnit.ParentId)
                .OrderBy(p => p.InitDate).ToList();


            ViewBag.AssignMemberCount = trafficDepartmentdetails.Count;
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.MemberListSelect = new SelectList(trafficDepartmentdetails, "assignMember.Id", "assignMember.Name");


            return RedirectToAction("Goback", "CaseTraffics");
        }

        public ActionResult Close(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && x.Case.UnitId == member.MyUnit.ParentId);

            ViewBag.Total = casePoprocs.Count();

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderBy(p => p.Case.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }

        public ActionResult CloseEdit(int id = 0)
        {
            CaseTrafficPoproc poproc = _db.CaseTrafficPoprocs.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            return View(poproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseEdit(int Id, string processtype, string type1, string type2, string PoprocsContent, int Sendfile)
        {
            CaseTrafficPoproc myPoproc = _db.CaseTrafficPoprocs.Find(Id);
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

                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                log.CaseId = myPoproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "結案";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CaseTrafficPoprocLogs.Add(log);
                ViewBag.message = "案件已結案!";

                if (myPoproc.PoprocsType != 3)
                {
                    //發信
                    string mailbody = System.IO.File.ReadAllText(Server.MapPath("/EmailTemp/CasePoproc.html"));
                    mailbody = mailbody.Replace("{CaseID}", myPoproc.Case.CaseID);
                    mailbody = mailbody.Replace("{InitDate}", myPoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
                    mailbody = mailbody.Replace("{CaseType}", "交通檢舉信箱");
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
                }


            }
            else if (processtype == "GoProcess") //退回承辦
            {
                myPoproc.Status = CaseProcessStatus.待辦;
                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                log.CaseId = myPoproc.CaseId;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "退回承辦";
                log.UnitId = member.UnitId;
                log.ActionMemo = PoprocsContent;
                _db.CaseTrafficPoprocLogs.Add(log);


                ViewBag.message = "案件已退回承辦!";


            }
            _db.SaveChanges();

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

        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var casePoprocs = _db.CaseTraffics.Include(c => c.Poprocs).OrderByDescending(p => p.InitDate).AsQueryable();

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);


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
                casePoprocs = casePoprocs.Where(x => x.UnitId == member.MyUnit.ParentId);
            }

            if (hasViewData("SearchByCategories1"))
            {
                int UnitId = getViewDateInt("SearchByCategories1");
                //TnpdModels.CaseType searchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);

                casePoprocs = casePoprocs.Where(w => w.UnitId == UnitId);
            }

            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                casePoprocs = casePoprocs.Where(w => w.Subject.Contains(searchBySubject) || w.Content.Contains(searchBySubject) || w.Name.Contains(searchBySubject) || w.Email.Contains(searchBySubject) || w.TEL.Contains(searchBySubject) || w.violation_carno.Contains(searchBySubject) || w.itemno.Contains(searchBySubject));
            }

            if (hasViewData("SearchByCaseId"))
            {
                string searchByCaseId = getViewDateStr("SearchByCaseId");
                casePoprocs = casePoprocs.Where(w => w.CaseID.Contains(searchByCaseId));
            }

            if (hasViewData("SearchByStatus"))
            {
                string Status = getViewDateStr("SearchByStatus");
                TnpdModels.CaseProcessStatus searchByStatus = (TnpdModels.CaseProcessStatus)Enum.Parse(typeof(TnpdModels.CaseProcessStatus), Status, false);

                casePoprocs = casePoprocs.Where(w => w.Poprocs.Count(x => x.Status == searchByStatus) > 0);
            }

            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                casePoprocs = casePoprocs.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            //總件數
            ViewBag.Total = casePoprocs.Count();
            //不舉發
            ViewBag.PoprocsType1Count =
                casePoprocs.Count(x => x.Poprocs.Count(y => y.EndDateTime.HasValue == true && y.PoprocsType == 1) > 0);
            //舉發
            ViewBag.PoprocsType2Count =
                casePoprocs.Count(x => x.Poprocs.Count(y => y.EndDateTime.HasValue == true && y.PoprocsType == 2) > 0);

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casePoprocs.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }


        public ActionResult Survey(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面



            var casewqs = _db.CaseTrafficqws.Include(c => c.Case).AsQueryable();

            if (hasViewData("SearchByCategories2"))
            {
                int UnitId = getViewDateInt("SearchByCategories2");
                //TnpdModels.CaseType searchByCaseType = (TnpdModels.CaseType)Enum.Parse(typeof(TnpdModels.CaseType), CaseType, false);

                casewqs = casewqs.Where(w => w.Case.Poprocs.FirstOrDefault().UnitId == UnitId);
            }





            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                casewqs = casewqs.Where(w => w.Case.InitDate >= startDate && w.Case.InitDate <= endDate);
            }
            ViewBag.Total = casewqs.Count();

            //            ViewBag.Subject = Subject;//            ViewBag.Name = Name;
            return View(casewqs.OrderByDescending(x => x.Case.InitDate).ToList());

        }




        //
        // GET: /Case/Create



        //
        // GET: /Case/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CaseTraffic poproc = _db.CaseTraffics.Find(id);
            if (poproc == null)
            {
                return HttpNotFound();
            }

            return View(poproc);
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string processtype)
        {
            CaseTraffic mycase = _db.CaseTraffics.Find(Id);
            if (mycase == null)
            {
                return HttpNotFound();
            }




            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            if (processtype == "Restone") //回復
            {
                for (int i = mycase.Poprocs.Count - 1; i >= 0; i--)
                {
                    CaseTrafficPoproc poproc = mycase.Poprocs.ElementAt(i);
                    mycase.Poprocs.Remove(poproc);
                    _db.CaseTrafficPoprocs.Remove(poproc);
                }

                CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                log.CaseId = mycase.Id;
                log.MemberId = member.Id;
                log.InitDate = DateTime.Now;
                log.Action = "案件回復";
                log.UnitId = member.UnitId;
                log.ActionMemo = "案件回復";
                _db.CaseTrafficPoprocLogs.Add(log);




                _db.SaveChanges();


                return RedirectToAction("Assign", "Case");
            }
            if (processtype == "SendMail") //指派
            {

                List<CaseTrafficPoproc> caseTrafficPoprocs =
                    _db.CaseTrafficPoprocs.Where(x => x.Status == CaseProcessStatus.結案).ToList();

                foreach (CaseTrafficPoproc poproc in caseTrafficPoprocs)
                {

                }

                //發信
                string mailbody = System.IO.File.ReadAllText(Server.MapPath("/EmailTemp/CasePoproc.html"));
                mailbody = mailbody.Replace("{CaseID}", mycase.CaseID);
                mailbody = mailbody.Replace("{InitDate}", mycase.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{CaseType}", "");
                if (mycase.Poprocs.Count > 0)
                {
                    if (mycase.Poprocs.FirstOrDefault().Status == CaseProcessStatus.結案)
                    {
                        mailbody = mailbody.Replace("{EndDateTime}",
                            mycase.Poprocs.FirstOrDefault().EndDateTime.Value.ToString("yyyy/MM/dd"));
                        mailbody = mailbody.Replace("{AssignMemo}", "【車號：" + mycase.violation_carno + "】<br/>" +
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
                mailbody = mailbody.Replace("{URL}", InternetURL + "Casewq/index/" + mycase.CaseGuid);
                //Utility.SendGmailMail("topidea.justin@gmail.com", myPoproc.Case.Email, "臺南市政府警察局-結案通知", mailbody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(mycase.Email, "臺南市政府警察局-結案通知(修正滿意度調查表連結)", mailbody);
                Utility.SystemSendMail("topidea.justin@gmail.com", "臺南市政府警察局-結案通知(修正滿意度調查表連結)", mailbody);
                ViewBag.message = "信件已寄出!";


            }

            return View(mycase);
        }

        public ActionResult Restone()
        {

            return View();
        }

        //
        // POST: /Case/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Restone(string CaseID)
        {
            CaseTraffic myCase = _db.CaseTraffics.FirstOrDefault(x => x.CaseID == CaseID);
            if (myCase == null)
            {
                return HttpNotFound();
            }

            for (int i = myCase.Poprocs.Count - 1; i >= 0; i--)
            {
                CaseTrafficPoproc poproc = myCase.Poprocs.ElementAt(i);
                myCase.Poprocs.Remove(poproc);
                _db.CaseTrafficPoprocs.Remove(poproc);
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
            log.CaseId = myCase.Id;
            log.MemberId = member.Id;
            log.InitDate = DateTime.Now;
            log.Action = "案件回復";
            log.UnitId = member.UnitId;
            log.ActionMemo = "案件回復";
            _db.CaseTrafficPoprocLogs.Add(log);




            _db.SaveChanges();


            ViewBag.message = "案件已回復!";
            return View();
        }

        public ActionResult Print1(int id = 0)
        {
            CaseTraffic case1 = _db.CaseTraffics.Find(id);

            if (case1 == null)
            {
                return HttpNotFound();

            }



            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print7.doc"), System.Text.Encoding.Default);
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

            temp = temp.Replace("{Job}", case1.Job);
            temp = temp.Replace("{Gender}", case1.Gender.ToString());
            temp = temp.Replace("{violation_place}", case1.violation_place_area + case1.violation_place_road + case1.violation_place);
            temp = temp.Replace("{violation_date}", case1.violation_date.ToString("yyyy/MM/dd") + " " + case1.violation_time);
            temp = temp.Replace("{violation_carno}", case1.violation_carno);
            temp = temp.Replace("{itemno}", case1.itemno);


            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + case1.CaseID + ".doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + case1.CaseID + ".doc"), "application/msword", "臺南市政府警察局交通檢舉信箱案件表(案件編號：" + case1.CaseID + ").doc");

        }

        public ActionResult Print1all(string CaseType, int id = 0)
        {
            CaseTraffic case1 = _db.CaseTraffics.Find(id);


            if (case1 == null)
            {
                return HttpNotFound();

            }

            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print7(all).doc"), System.Text.Encoding.Default);
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

            temp = temp.Replace("{Job}", case1.Job);
            temp = temp.Replace("{Gender}", case1.Gender.ToString());


            temp = temp.Replace("{violation_place}", case1.violation_place_area + case1.violation_place_road + case1.violation_place);
            temp = temp.Replace("{violation_date}", case1.violation_date.ToString("yyyy/MM/dd") + " " + case1.violation_time);
            temp = temp.Replace("{violation_carno}", case1.violation_carno);
            temp = temp.Replace("{itemno}", case1.itemno);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + case1.CaseID + "all.doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + case1.CaseID + "all.doc"), "application/msword", "臺南市政府警察局交通檢舉信箱案件表(案件編號：" + case1.CaseID + ").doc");
        }

        public ActionResult Print2(string CaseType, int id = 0)
        {
            CaseTraffic caseTraffic = _db.CaseTraffics.Find(id);

            if (caseTraffic == null)
            {
                return HttpNotFound();

            }


            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print7.doc"), System.Text.Encoding.Default);
            temp = temp.Replace("{CaseID}", caseTraffic.CaseID);
            temp = temp.Replace("{InitDate}", caseTraffic.InitDate.Value.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Tel}", caseTraffic.TEL);
            temp = temp.Replace("{Subject}", caseTraffic.Subject);
            temp = temp.Replace("{Content}", Txt2Html(caseTraffic.Content));

            if (caseTraffic.Poprocs.Count > 0)
            {
                temp = temp.Replace("{AssignMemo}", caseTraffic.Poprocs.FirstOrDefault().AssignMemo);
                temp = temp.Replace("{assignUnit}", caseTraffic.Poprocs.FirstOrDefault().assignUnit.ParentUnit.Subject + "-" + caseTraffic.Poprocs.FirstOrDefault().assignUnit.Subject);
            }
            else
            {
                temp = temp.Replace("{AssignMemo}", "");
                temp = temp.Replace("{assignUnit}", "");
            }

            temp = temp.Replace("{Predate}", caseTraffic.Predate.ToString("yyyy/MM/dd"));

            temp = temp.Replace("{Job}", caseTraffic.Job);
            temp = temp.Replace("{Gender}", caseTraffic.Gender.ToString());

            temp = temp.Replace("{violation_place}", caseTraffic.violation_place_area + caseTraffic.violation_place_road + caseTraffic.violation_place);
            temp = temp.Replace("{violation_date}", caseTraffic.violation_date.ToString("yyyy/MM/dd") + " " + caseTraffic.violation_time);
            temp = temp.Replace("{violation_carno}", caseTraffic.violation_carno);
            temp = temp.Replace("{itemno}", caseTraffic.itemno);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + caseTraffic.CaseID + ".doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + caseTraffic.CaseID + ".doc"), "application/msword", "臺南市政府警察局交通檢舉信箱案件表(案件編號：" + caseTraffic.CaseID + ").doc");

        }

        public ActionResult Print2all(string CaseType, int id = 0)
        {
            CaseTraffic caseTraffic = _db.CaseTraffics.Find(id);

            if (caseTraffic == null)
            {
                return HttpNotFound();

            }

            string temp = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/print7(all).doc"), System.Text.Encoding.Default);
            temp = temp.Replace("{CaseID}", caseTraffic.CaseID);
            temp = temp.Replace("{Name}", caseTraffic.Name);
            temp = temp.Replace("{Tel}", caseTraffic.TEL);
            temp = temp.Replace("{Address}", caseTraffic.Address);
            temp = temp.Replace("{InitDate}", caseTraffic.InitDate.Value.ToString("yyyy/MM/dd"));
            temp = temp.Replace("{Tel}", caseTraffic.TEL);
            temp = temp.Replace("{Email}", caseTraffic.Email);
            temp = temp.Replace("{Subject}", caseTraffic.Subject);
            temp = temp.Replace("{Content}", Txt2Html(caseTraffic.Content));
            if (caseTraffic.Poprocs.Count > 0)
            {
                temp = temp.Replace("{AssignMemo}", caseTraffic.Poprocs.FirstOrDefault().AssignMemo);
                temp = temp.Replace("{assignUnit}", caseTraffic.Poprocs.FirstOrDefault().assignUnit.ParentUnit.Subject + "-" + caseTraffic.Poprocs.FirstOrDefault().assignUnit.Subject);
            }
            else
            {
                temp = temp.Replace("{AssignMemo}", "");
                temp = temp.Replace("{assignUnit}", "");
            }
            temp = temp.Replace("{Predate}", caseTraffic.Predate.ToString("yyyy/MM/dd"));

            temp = temp.Replace("{Job}", caseTraffic.Job);
            temp = temp.Replace("{Gender}", caseTraffic.Gender.ToString());

            temp = temp.Replace("{violation_place}", caseTraffic.violation_place_area + caseTraffic.violation_place_road + caseTraffic.violation_place);
            temp = temp.Replace("{violation_date}", caseTraffic.violation_date.ToString("yyyy/MM/dd") + " " + caseTraffic.violation_time);
            temp = temp.Replace("{violation_carno}", caseTraffic.violation_carno);
            temp = temp.Replace("{itemno}", caseTraffic.itemno);

            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + caseTraffic.CaseID + "all.doc"), temp, System.Text.Encoding.Default);

            return File(Server.MapPath("/MailTemp/" + caseTraffic.CaseID + "all.doc"), "application/msword", "臺南市政府警察局交通檢舉信箱案件表(案件編號：" + caseTraffic.CaseID + ").doc");
        }

        public ActionResult Report1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report1(string SearchByStartDate, string SearchByEndDate, string SearchByCategories1)
        {
            var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).AsQueryable();

            if (!string.IsNullOrEmpty(SearchByCategories1))
            {
                int Categories1 = Convert.ToInt32(SearchByCategories1);
                casePoprocs = casePoprocs.Where(w => w.assignUnit.ParentId == Categories1);
            }



            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);




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


            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/Trafficcasedayview.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/Trafficcasedayview.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);

            var casePoprocslist = casePoprocs.OrderBy(x => x.Case.Id).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var casePoproc in casePoprocslist)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{CaseID}", casePoproc.Case.CaseID);
                strTr = strTr.Replace("{CaseType}", "交通檢舉信箱");
                strTr = strTr.Replace("{initDate}", casePoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{Name}", casePoproc.Case.Name + " ");
                strTr = strTr.Replace("{Subject}", casePoproc.Case.Subject + " ");
                strTr = strTr.Replace("{assignUnit}", casePoproc.assignUnit.ParentUnit.Subject + "-" + casePoproc.assignUnit.Subject);
                if (casePoproc.EndDateTime.HasValue)
                {
                    if (casePoproc.PoprocsType == 1)
                    {
                        strTr = strTr.Replace("{PoprocsType}", "不舉發");
                    }
                    else if (casePoproc.PoprocsType == 2)
                    {
                        strTr = strTr.Replace("{PoprocsType}", "舉發");
                    }
                    else
                    {
                        strTr = strTr.Replace("{PoprocsType}", "");
                    }
                }
                else
                {
                    strTr = strTr.Replace("{PoprocsType}", "");
                }



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


        public ActionResult Report2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report2(string SearchByStartDate, string SearchByEndDate, string SearchByCategories1)
        {
            var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).AsQueryable();

            if (!string.IsNullOrEmpty(SearchByCategories1))
            {
                int Categories1 = Convert.ToInt32(SearchByCategories1);
                casePoprocs = casePoprocs.Where(w => w.assignUnit.ParentId == Categories1);
            }

            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);


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


            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/TrafficCaseView.xls"), System.Text.Encoding.UTF8);
            string temptr = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/TrafficCaseView.txt"), System.Text.Encoding.UTF8);
            tempBody = tempBody.Replace("{initDate}", SearchByStartDate + "~" + SearchByEndDate);

            var casePoprocslist = casePoprocs.OrderBy(x => x.Case.Id).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var casePoproc in casePoprocslist)
            {
                string strTr = temptr;
                strTr = strTr.Replace("{CaseID}", casePoproc.Case.CaseID);
                strTr = strTr.Replace("{CaseType}", "交通檢舉信箱");
                strTr = strTr.Replace("{initDate}", casePoproc.Case.InitDate.Value.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{Name}", casePoproc.Case.Name);
                strTr = strTr.Replace("{Subject}", casePoproc.Case.Subject);
                strTr = strTr.Replace("{assignUnit}", casePoproc.assignUnit.ParentUnit.Subject + "-" + casePoproc.assignUnit.Subject + "-" + casePoproc.assignMember.Name);
                strTr = strTr.Replace("{Predate}", casePoproc.Case.Predate.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{Status}", casePoproc.Status.ToString());

                strTr = strTr.Replace("{violation_date}", casePoproc.Case.violation_date.ToString("yyyy/MM/dd"));
                strTr = strTr.Replace("{violation_time}", casePoproc.Case.violation_time);
                strTr = strTr.Replace("{violation_place}", casePoproc.Case.violation_place);
                strTr = strTr.Replace("{violation_carno}", casePoproc.Case.violation_carno);
                strTr = strTr.Replace("{Content}", casePoproc.Case.Content);


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
                if (casePoproc.EndDateTime.HasValue)
                {
                    if (casePoproc.PoprocsType == 1)
                    {
                        strTr = strTr.Replace("{PoprocsType}", "不舉發");
                    }
                    else if (casePoproc.PoprocsType == 2)
                    {
                        strTr = strTr.Replace("{PoprocsType}", "舉發");
                    }
                    else
                    {
                        strTr = strTr.Replace("{PoprocsType}", "");
                    }
                }
                else
                {
                    strTr = strTr.Replace("{PoprocsType}", "");
                }

                string assignMember = ""; //分派人
                string closeMember = "";//結案人
                foreach (CaseTrafficPoprocLog log in casePoproc.Case.PoprocslLogs)
                {
                    if (log.Action == "派案")
                    {
                        assignMember = log.assignMember.Name;
                    }
                    if (log.Action == "結案")
                    {
                        closeMember = log.assignMember.Name;
                    }
                }
                strTr = strTr.Replace("{assignMember}", assignMember);
                strTr = strTr.Replace("{closeMember}", closeMember);

                strTr = strTr.Replace("{assign2Member}", casePoproc.assignMember.Name);
                sb.AppendLine(strTr);
            }
            tempBody = tempBody.Replace("{bodytr}", sb.ToString());
            tempBody = tempBody.Replace("2019/04/01", SearchByStartDate);
            tempBody = tempBody.Replace("2019/04/25", SearchByEndDate);


            string fileName = member.Account + ".xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "案件一覽表.xls");

        }

        public ActionResult Report3()
        {
            var cases = _db.CaseTraffics.Include(x=>x.Poprocs).Where(x=>x.Id==0).AsQueryable();
            return View(cases.ToList());
        }

        [HttpPost]
        public ActionResult Report3(string SearchByStartDate, string SearchByEndDate, string SearchByCategories1)
        {
            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/report3.xls"), System.Text.Encoding.UTF8);
            string sql = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/report3.sql"), System.Text.Encoding.Default);

            if (!string.IsNullOrEmpty(SearchByStartDate) && !string.IsNullOrEmpty(SearchByEndDate))
            {
               
                DateTime endDate = Convert.ToDateTime(SearchByEndDate).AddDays(1);
                sql = sql.Replace("2019/1/1", SearchByStartDate);
                sql = sql.Replace("2019/3/1", endDate.ToString("yyyy/MM/dd"));
                tempBody = tempBody.Replace("2019/1/1", SearchByStartDate);
                tempBody = tempBody.Replace("2019/3/1", SearchByEndDate);
            }
            else
            {
                return View();
            }

            

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TnpdConnection"].ToString());
            SqlCommand comm=new SqlCommand(sql,conn);
            SqlDataAdapter da=new SqlDataAdapter(comm);
            DataTable dt=new DataTable();
            da.Fill(dt);
            int vcol = dt.Columns.Count;
            string vcolname = "";
            string body = "";
            tempBody = tempBody.Replace("{vcol}", (vcol-5).ToString());
            for (int i = 5; i <= vcol-1; i++)
            {
                vcolname = vcolname + "<td>" + dt.Columns[i].ColumnName+ "</td>";
            }
            tempBody = tempBody.Replace("{vcolname}", vcolname);
            foreach (DataRow row in dt.Rows)
            {
                body = body + "<tr align=\"center\">";
                for (int i = 0; i <= vcol-1; i++)
                {
                    body = body + "<td>" + row[i].ToString() + "</td>";
                }
                body = body + "</tr>";
                
            }
            tempBody = tempBody.Replace("{body}", body);
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            string fileName = member.Account + "3.xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "檢舉違規行為態樣表.xls");

        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
