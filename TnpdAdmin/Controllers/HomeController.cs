using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdModels;
using Newtonsoft.Json;

namespace Tnpd.Controllers
{
    public class HomeController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /Admin/Home/

        [PermissionFilters]
        [Authorize]
        public ActionResult Index()
        {
            Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            var webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.MyUnit.ParentId && x.Language == LanguageType.中文版);
            if (webSite == null)
            {
                webSite = _db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId && x.Language == LanguageType.中文版);
            }
            if (member.Roles.Any(x => x.Subject.Contains("首長信箱總管理者")))
            {
                var cases = _db.Cases.OrderByDescending(p => p.InitDate).AsQueryable();

                //派案作業
                cases = cases.Where(w => (w.CaseType == CaseType.參觀本局暨所屬機關 && w.WebSiteId == webSite.Id) || w.CaseType == CaseType.首長信箱);
                cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);
                cases = cases.Where(w => w.ParentId == null);
                ViewBag.c11 = cases.Count();
            
                //退文派案
                var casePoprocs = _db.CasePoprocs.OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);
                ViewBag.c12 = casePoprocs.Count();


                //結案作業
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && (x.Case.IsAutoClose == null || x.Case.IsAutoClose == BooleanType.否));
                ViewBag.c13 = casePoprocs.Count();

                //展期案件審核
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.參觀本局暨所屬機關 && w.Case.WebSiteId == webSite.Id) || w.Case.CaseType == CaseType.首長信箱);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延);
                ViewBag.c14 = casePoprocs.Count();

                //單位改派
                 cases = _db.Cases.Include(c => c.WebSite).OrderByDescending(p => p.InitDate).AsQueryable();
                 cases = cases.Where(w => w.CaseType == CaseType.分局長與大隊隊長信箱 && w.IsUnitAssign == BooleanType.是);
                 ViewBag.c15 = cases.Count();

            }
            
            if (member.Roles.Any(x => x.Subject.Contains("分局長及隊長信箱管理者")))
            {
                var cases = _db.Cases.OrderByDescending(p => p.InitDate).AsQueryable();

                //派案作業
                cases = cases.Where(w => (w.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.WebSiteId == webSite.Id && (w.IsUnitAssign == BooleanType.否 || w.IsUnitAssign == null));
                cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);
                cases = cases.Where(w => w.ParentId == null);
                ViewBag.c21 = cases.Count();

                //退文派案
                var casePoprocs = _db.CasePoprocs.OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);
                ViewBag.c22 = casePoprocs.Count();

                //結案作業
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && (x.Case.IsAutoClose == null || x.Case.IsAutoClose == BooleanType.否));
                ViewBag.c23 = casePoprocs.Count();

                //展期案件審核
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => (w.Case.CaseType == CaseType.分局長與大隊隊長信箱 || w.CaseType == CaseType.參觀本局暨所屬機關) && w.Case.WebSiteId == webSite.Id && (w.Case.IsUnitAssign == BooleanType.否 || w.Case.IsUnitAssign == null));
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延);
                ViewBag.c24 = casePoprocs.Count();




            }
           
            if (member.Roles.Any(x => x.Subject.Contains("交通信箱各單位管理者")))
            {
                //派案作業
                var cases = _db.CaseTraffics.Include(c => c.assignUnit).OrderByDescending(p => p.InitDate).AsQueryable();
                cases = cases.Where(w => w.UnitId == member.MyUnit.ParentId && (w.IsUnitAssign == BooleanType.否 || w.IsUnitAssign == null));


                cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);

                ViewBag.c31 = cases.Count();
                //退文派案
                var casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文 && x.Case.UnitId == member.MyUnit.ParentId);
                ViewBag.c32 = casePoprocs.Count();

                //結案作業
                 casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && x.Case.UnitId == member.MyUnit.ParentId);

                ViewBag.c33 = casePoprocs.Count();

                //展期案件審核
                casePoprocs = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();

                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延 && x.Case.UnitId == member.MyUnit.ParentId);
                ViewBag.c34 = casePoprocs.Count();

            }
            if (member.Roles.Any(x => x.Subject.Contains("檢舉貪瀆信箱管理者")))
            {
                var cases = _db.Cases.OrderByDescending(p => p.InitDate).AsQueryable();

                //派案作業
                cases = cases.Where(w => w.CaseType == CaseType.檢舉貪瀆信箱);
                cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);
                cases = cases.Where(w => w.ParentId == null);
                ViewBag.c41 = cases.Count();
                //退文派案
                var casePoprocs = _db.CasePoprocs.OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);
                ViewBag.c42 = casePoprocs.Count();

                //結案作業
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && (x.Case.IsAutoClose == null || x.Case.IsAutoClose == BooleanType.否));
                ViewBag.c43 = casePoprocs.Count();

                //展期案件審核
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.檢舉貪瀆信箱);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延);
                ViewBag.c44 = casePoprocs.Count();

            }
            if (member.Roles.Any(x => x.Subject.Contains("網路報案案件管理者")))
            {
                var cases = _db.Cases.OrderByDescending(p => p.InitDate).AsQueryable();

                //派案作業
                cases = cases.Where(w => w.CaseType == CaseType.網路報案);
                cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);
                cases = cases.Where(w => w.ParentId == null);
                ViewBag.c51 = cases.Count();
                //退文派案
                var casePoprocs = _db.CasePoprocs.OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);
                ViewBag.c52 = cases.Count();

                //結案作業
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && (x.Case.IsAutoClose == null || x.Case.IsAutoClose == BooleanType.否));
                ViewBag.c53 = casePoprocs.Count();


                //展期案件審核
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.網路報案);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延);
                ViewBag.c54 = casePoprocs.Count();

            }
            if (member.Roles.Any(x => x.Subject.Contains("婦幼安全警示地點管理者")))
            {
                var cases = _db.Cases.OrderByDescending(p => p.InitDate).AsQueryable();

                //派案作業
                cases = cases.Where(w => w.CaseType == CaseType.婦幼安全警示地點);
                cases = cases.Where(w => w.Poprocs.Count(x => x.Status != CaseProcessStatus.未分派) == 0);
                cases = cases.Where(w => w.ParentId == null);
                ViewBag.c61 = cases.Count();
                //退文派案
                var casePoprocs = _db.CasePoprocs.OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.退文);
                ViewBag.c62 = cases.Count();

                //結案作業
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.已辦 && (x.Case.IsAutoClose == null || x.Case.IsAutoClose == BooleanType.否));
                ViewBag.c63 = casePoprocs.Count();


                //展期案件審核
                casePoprocs = _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
                casePoprocs = casePoprocs.Where(w => w.Case.CaseType == CaseType.婦幼安全警示地點);
                casePoprocs = casePoprocs.Where(x => x.Status == CaseProcessStatus.展延);
                ViewBag.c64 = casePoprocs.Count();
            }

            //首長信箱處理
            var caseProcess= _db.CasePoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
            caseProcess = caseProcess.Where(x => (x.Status == CaseProcessStatus.待辦 || x.Status == CaseProcessStatus.辦案) && x.MemberId == member.Id);
            ViewBag.c71 = caseProcess.Count();
            //交通信箱處理

            var CaseTrafficProcess = _db.CaseTrafficPoprocs.Include(c => c.Case).OrderByDescending(p => p.InitDate).AsQueryable();
            CaseTrafficProcess = CaseTrafficProcess.Where(x => (x.Status == CaseProcessStatus.待辦 || x.Status == CaseProcessStatus.辦案) && x.MemberId == member.Id);
            ViewBag.c81 = CaseTrafficProcess.Count();


            return View();
        }


        /// <summary>
        /// 由內網管理後台登入的入口
        /// carl
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string code)
        {
            
            if (!string.IsNullOrEmpty(code))
            {

                //carl 改成md5的編碼方式
                string userAccout = Utility.DecodingByMD5(code);

              Member member =
                _db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
                if (member != null)
                {
                    Utility.GetPerssionGlobal(member);
                    string userData = JsonConvert.SerializeObject(member);
                    Utility.SetAuthenTicket(userData, member.Account);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }

        private Member ValidateUser(string userName, string password)
        {
            Member member = _db.Members.SingleOrDefault(o => o.Account == userName);
            if (member == null)
            {
                return null;
            }
            string saltPassword = Utility.GenerateHashWithSalt(password, member.PasswordSalt);
            return saltPassword == member.Password ? member : null;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return Redirect(FormsAuthentication.LoginUrl);


        }


        public ActionResult Error() 
        {
            return View();
        }
       

    }
}
