using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tnpd.Filters;
using Tnpd.Models;
using TnpdModels;

namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class RoleController : Controller
    {
        private BackendContext db = new BackendContext();

        //
        // GET: /Role/

        public ActionResult Index()
        {

            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            int unitID = member.MyUnit.ParentUnit.Id;

            return View(db.Roles.Where(x=>x.UnitId==unitID).ToList());
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id = 0)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {

            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.admin = false;
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.admin = true;
            }
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role, string[] MemberListSelect)
        {
            if (ModelState.IsValid)
            {
                string aa = Request["MemberListSelect"];
                if (!string.IsNullOrEmpty(aa))
                {


                    var members = db.Members.ToList().Where(c => MemberListSelect.Contains(c.Id.ToString()));

                    foreach (var member in members)
                    {
                        role.Members.Add(member);
                    }
                }
                role.Create(db, db.Roles);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Role role = db.Roles.Find(id);
            //ViewBag.Members = db.Members.Where(x=>x.Id==0).ToList().Where(p => !(role.Members.Contains(p)));
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
            ViewBag.ca1 = member.MyUnit.ParentUnit.Id;
            ViewBag.ca2 = member.UnitId;
            ViewBag.admin = false;
            if (member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                ViewBag.admin = true;
            }

            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role, string MemberListSelect)
        {
            if (ModelState.IsValid)
            {
                //取得資料庫裏面原來的值
                var roleItem = db.Roles.Single(r => r.Id == role.Id);

                //套用新的值
                db.Entry(roleItem).CurrentValues.SetValues(role);


                //放入新的值
                roleItem.Members.Clear();
                if (!string.IsNullOrEmpty(MemberListSelect))
                {
                    string[] strArray = MemberListSelect.Split(',');
                    var memberLists = db.Members.ToList().Where(c => strArray.Contains(c.Id.ToString()));
                    foreach (var m in memberLists)
                    {
                        roleItem.Members.Add(m);
                    }
                }
                db.Entry(roleItem).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        
        public ActionResult Report()
        {
            List<Role> roles = db.Roles.Include(x=>x.Members).ToList();
            string tempBody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/Permission1.xls"), System.Text.Encoding.UTF8);
            string rowtr1 = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/Permission1.txt"), System.Text.Encoding.UTF8);
            string rowtr2 = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/Permission2.txt"), System.Text.Encoding.UTF8);
            StringBuilder sb = new StringBuilder();
            int j = 1;
            foreach (Role role in roles)
            {
                int i = 1;
               
                
                string Permissions = Utility.GetPermissionString(role.Permission);
              
                foreach (Member member in role.Members.OrderByDescending(x=>x.Id))
                {
                   
                    if (i == 1)
                    {
                        string temptr1 = rowtr1;
                        temptr1 = temptr1.Replace("{RowNum}", j.ToString());
                        temptr1 = temptr1.Replace("{Role}", role.Subject);
                        temptr1 = temptr1.Replace("{row}", role.Members.Count.ToString());
                        temptr1 = temptr1.Replace("{Permission}", Permissions);
                        temptr1 = temptr1.Replace("{Unit}", member.MyUnit.ParentUnit.Subject + "-" + member.MyUnit.Subject );
                        temptr1 = temptr1.Replace("{Name}", member.Name);
                        sb.Append(temptr1);
                    }
                    else
                    {
                        string temptr2 = rowtr2;
                        temptr2 = temptr2.Replace("{RowNum}", j.ToString());
                        temptr2 = temptr2.Replace("{Unit}", member.MyUnit.ParentUnit.Subject + "-" + member.MyUnit.Subject);
                        temptr2 = temptr2.Replace("{Name}", member.Name);
                        sb.Append(temptr2);

                    }

                    i++;
                    j++;
                }

               


            }

            tempBody = tempBody.Replace("{PermissionBody}", sb.ToString());
            string fileName =   "Permission" +DateTime.Now.ToString("yyyyMMddhhmmsss") +".xls";
            System.IO.File.WriteAllText(Server.MapPath("/MailTemp/" + fileName), tempBody, System.Text.Encoding.UTF8);

            return File(Server.MapPath("/MailTemp/" + fileName), "application/msexcel", "權限一覽表.xls");

        }

        


        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //輸出treeView javascript Code 
        public JavaScriptResult TreeScript(int id = 0)
        {
            Role role = db.Roles.Find(id);
            Member me =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            string strPermission = "";
            if (role != null)
            {
                strPermission = role.Permission;
            }
            string strMenu = "";
            if (me.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                strMenu = string.Format("var treeData =[{0}]", Utility.GetMenu(strPermission));
            }
            else
            {
                var web = db.WebSiteNames.FirstOrDefault(x => x.UnitId == me.MyUnit.ParentId);
                strMenu = string.Format("var treeData =[{0}]", Utility.GetMenu(strPermission, "~/Config/" + web.SiteCode + ".xml"));
            }
            string treeScript = System.IO.File.ReadAllText(Server.MapPath("~/Config/PermissionTree.js"));

            return JavaScript(strMenu + treeScript);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}