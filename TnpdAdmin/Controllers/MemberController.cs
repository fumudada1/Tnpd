using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcPaging;
using Newtonsoft.Json;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdAdmin.Models;
using TnpdModels;

namespace Tnpd.Controllers
{

    [Authorize]
    public class MemberController :_BaseController
    {
        private BackendContext db = new BackendContext();
        private const int DefaultPageSize = 100;
        //
        // GET: /Member/
         [PermissionFilters]
        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.admin = false;
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.admin = true;
            }

            return View(db.Members.Where(x=>x.UnitId==member.UnitId).OrderBy(x=>x.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

         [PermissionFilters]
        [HttpPost]
        public ActionResult Index(int? page, FormCollection fc,int Categories1, int Categories2)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);

            ViewBag.ca1 = Categories1;
            ViewBag.ca2 = Categories2;
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.admin = false;
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.admin = true;
            }

            return View(db.Members.Where(x => x.UnitId == Categories2).OrderBy(x => x.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }
        //
        // GET: /Member/Details/5


         [PermissionFilters]
        public ActionResult Details(int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // GET: /Member/Create
         [PermissionFilters]
        public ActionResult Create()
        {
            ViewBag.Units = db.Units.ToList();

            return View();
        }

        //
        // POST: /Member/Create
         [PermissionFilters]
        [HttpPost]
        public ActionResult Create(Member member, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                //上傳檔案
                if (upfile != null)
                {
                    member.MyPic = Utility.SaveUpFile(upfile);
                }
                member.PasswordSalt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(member.Password, member.PasswordSalt);

                member.Permission = member.Permission ?? "";
                member.JobTitle = member.JobTitle ?? "";
                if (!member.AddMember())
                {
                    ViewBag.Units = db.Units.ToList();
                    ViewBag.Message = "帳號重複!";
                    member.Password = "";
                    ViewBag.Units = db.Units.ToList();
                    return View(member);
                }
                return RedirectToAction("Index");
            }
            //db.Members.Add(member);
            //db.SaveChanges();

            ViewBag.Units = db.Units.ToList();
            return View(member);
        }






        //
        // GET: /Member/Edit/5
         [PermissionFilters]
        public ActionResult Edit(string actionName, int id = 0)
        {
            ViewBag.Units = db.Units.ToList();
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            string strMenu = Utility.GetMenu(member.Permission);

            if (actionName == null)
            {
                actionName = "index";
            }
            ViewBag.actionName = actionName;

            ViewBag.TreeScript = strMenu;

            return View(member);
        }

        //
        // POST: /Member/Edit/5
         [PermissionFilters]
        [HttpPost]
        public ActionResult Edit(Member member, string actionName)
        {
            //移除驗證
            ModelState.Remove("Account");
            ModelState.Remove("Password");
           // member.Password = Request["NewPassword"] != "" ? Utility.GenerateHashWithSalt(Request["NewPassword"], member.PasswordSalt) : Request["hash"];
            member.Permission = member.Permission ?? "";
            if (ModelState.IsValid)
            {
                member.Update(db, db.Members);
                if (actionName == null)
                {
                    actionName = "index";
                }
                return RedirectToActionPermanent(actionName, null,
                    new { page = Request["page"] });
            }
            ViewBag.Units = db.Units.ToList();
            string strMenu = Utility.GetMenu(member.Permission);
            ViewBag.TreeScript = strMenu.Trim();
            return View(member);
        }

        //
        // GET: /Member/Delete/5
         [PermissionFilters]
        public ActionResult Delete(string actionName, int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            if (actionName == null)
            {
                actionName = "index";
            }
            ViewBag.actionName = actionName;
            return View(member);
        }

        //
        // POST: /Member/Delete/5
         [PermissionFilters]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string actionName)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            if (actionName == null)
            {
                actionName = "index";
            }
            return RedirectToAction(actionName);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                if (!IsADUserValid(userName, password, "tncpb.gov"))
                {
                    ViewBag.message = "登入失敗!";
                    return View();
                }
                Member member = ValidateUser(userName);
                if (member != null)
                {
                    member.Permission = member.Permission + ",M4,M5,M6";
                    Utility.GetPerssion(member);
                    string userData = JsonConvert.SerializeObject(member);
                    //宣告一個驗證票
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(5), false, userData);
                    //加密驗證票
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //建立Cookie
                    HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    
                    //將Cookie寫入回應
                    Session["user"] = member;
                    System.Web.HttpContext.Current.Response.Cookies.Add(authenticationcookie);

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }

        [AllowAnonymous]
        public ActionResult Loginadmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Loginadmin(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                //if (!IsADUserValid(userName, password, "tncpb.gov"))
                //{
                //    ViewBag.message = "登入失敗!";
                //    return View();
                //}
                Member member = ValidateUser(userName);
                if (member != null)
                {
                    member.Permission = member.Permission + ",M4,M5,M6";
                    Utility.GetPerssion(member);
                    string userData = JsonConvert.SerializeObject(member);
                    //宣告一個驗證票
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(5), false, userData);
                    //加密驗證票
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //建立Cookie
                    HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //將Cookie寫入回應

                    System.Web.HttpContext.Current.Response.Cookies.Add(authenticationcookie);

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }

        [AllowAnonymous]
        public ActionResult LoginChiefMailbox()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginChiefMailbox(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                //if (!IsADUserValid(userName, password, "tncpb.gov"))
                //{
                //    ViewBag.message = "登入失敗!";
                //    return View();
                //}
                Member member = ValidateUser(userName);
                if (member != null)
                {
                    if (!member.Roles.Any(x => x.Subject.Contains("分局長及隊長信箱管理者")))
                    {
                        ViewBag.message = "登入失敗!";
                        return View();
                    }

                    member.Permission = member.Permission + ",M4,M5,M6";
                    Utility.GetPerssion(member);
                    string userData = JsonConvert.SerializeObject(member);
                    //宣告一個驗證票
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(5), false, userData);
                    //加密驗證票
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //建立Cookie
                    HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //將Cookie寫入回應

                    System.Web.HttpContext.Current.Response.Cookies.Add(authenticationcookie);

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }

        [AllowAnonymous]
        public ActionResult LoginTrafficMailbox()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginTrafficMailbox(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                //if (!IsADUserValid(userName, password, "tncpb.gov"))
                //{
                //    ViewBag.message = "登入失敗!";
                //    return View();
                //}
                Member member = ValidateUser(userName);
                if (member != null)
                {
                    if (!member.Roles.Any(x => x.Subject.Contains("交通信箱各單位管理者")))
                    {
                        ViewBag.message = "登入失敗!";
                        return View();
                    }

                    member.Permission = member.Permission + ",M4,M5,M6";
                    Utility.GetPerssion(member);
                    string userData = JsonConvert.SerializeObject(member);
                    //宣告一個驗證票
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(5), false, userData);
                    //加密驗證票
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //建立Cookie
                    HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //將Cookie寫入回應

                    System.Web.HttpContext.Current.Response.Cookies.Add(authenticationcookie);

                    return RedirectToAction("Index", "Home");
                }
                ViewBag.message = "登入失敗!";
                return View();
            }
            ViewBag.message = "登入失敗!";
            return View();

        }

        private bool IsADUserValid(string UserName, string Password, string ADschema)
        {
            if (UserName == "admin")
            {
                return true;
            }

            

            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + ADschema, UserName, Password);
                string guid = de.Guid.ToString();

                System.DirectoryServices.PropertyCollection pc = de.Properties;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

           

        }



        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string newPassword)
        {
            Member member = db.Members.SingleOrDefault(o => o.Account == User.Identity.Name);
            if (member != null)
            {

                member.Password = Utility.GenerateHashWithSalt(newPassword, member.PasswordSalt);
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.message = "修改成功";
                return View();
            }
            ViewBag.message = "修改失敗!請重新登入試試!!";
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }

       

        private Member ValidateUser(string userName)
        {
            Member member = db.Members.SingleOrDefault(o => o.Account == userName);
            if (member == null)
            {
                return null;
            }
            return member;
        }

        
        [AllowAnonymous]
       
        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("LoginAdmin");
        }

        //輸出treeView javascript Code 
        public JavaScriptResult TreeScript(int id = 0)
        {
            Member member = db.Members.Find(id);
            Member me =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            string strPermission = "";

            if (member != null)
            {
                strPermission = member.Permission;
               
            }
            string strMenu = "";
            if (me.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                strMenu=string.Format("var treeData =[{0}]", Utility.GetMenu(strPermission));
            }
            else
            {
                var web = db.WebSiteNames.FirstOrDefault(x => x.UnitId == me.MyUnit.ParentId);
                strMenu = string.Format("var treeData =[{0}]", Utility.GetMenu(strPermission, "~/Config/" +web.SiteCode　+　".xml"));
            }

            string treeScript = System.IO.File.ReadAllText(Server.MapPath("~/Config/PermissionTree.js"));

            return JavaScript(strMenu + treeScript);

        }
        public ActionResult CheckAccount(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return Content("參數錯誤");
            }
            Member member = db.Members.SingleOrDefault(o => o.Account == userName);
            if (member == null)
            {
                return Content("這個帳號尚未使用!");
            }
            return Content("這個帳號已使用!");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}