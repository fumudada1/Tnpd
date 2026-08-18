using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Controllers;
using Tnpd.Filters;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
	[PermissionFilters]
    [Authorize]
    public class SMTPController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //

        


        public ActionResult Index(int? page, FormCollection fc )
        {
			//記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);
            

            var smtpserversettings = _db.SmtpServerSettings.OrderByDescending(p => p.InitDate).AsQueryable();
            if (hasViewData("SearchBySubject")) 
            { 
            string searchBySubject = getViewDateStr("SearchBySubject");             
                smtpserversettings = smtpserversettings.Where(w => w.Subject.Contains(searchBySubject)); 
            } 
 
            if (hasViewData("SearchBySmtpDeliveryMethod")) 
            { 
            string SmtpDeliveryMethod = getViewDateStr("SearchBySmtpDeliveryMethod");             
             System.Net.Mail.SmtpDeliveryMethod searchBySmtpDeliveryMethod= (System.Net.Mail.SmtpDeliveryMethod)Enum.Parse(typeof(System.Net.Mail.SmtpDeliveryMethod), SmtpDeliveryMethod, false); 
             
                smtpserversettings = smtpserversettings.Where(w => w.SmtpDeliveryMethod == searchBySmtpDeliveryMethod); 
            } 
 

//            ViewBag.Subject = Subject;
            return View(smtpserversettings.OrderByDescending(p => p.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));

        }



        

        //
        // GET: /SMTP/Details/5

        public ActionResult Details(int id = 0)
        {
            SMTPServerSetting smtpserversetting = _db.SmtpServerSettings.Find(id);
            if (smtpserversetting == null)
            {
                //return HttpNotFound();
				 return View();
            }
            return View(smtpserversetting);
        }

        //
        // GET: /SMTP/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SMTP/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(SMTPServerSetting smtpserversetting )
        {
            if (ModelState.IsValid)
            {

                _db.SmtpServerSettings.Add(smtpserversetting);
                smtpserversetting.Create(_db,_db.SmtpServerSettings);
                return RedirectToAction("Index");
            }

            return View(smtpserversetting);
        }

        //
        // GET: /SMTP/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SMTPServerSetting smtpserversetting = _db.SmtpServerSettings.Find(id);
            if (smtpserversetting == null)
            {
                return HttpNotFound();
            }
            return View(smtpserversetting);
        }

        //
        // POST: /SMTP/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(SMTPServerSetting smtpserversetting)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(smtpserversetting).State = EntityState.Modified;
                smtpserversetting.Update();
                int oid = 1;
                if (smtpserversetting.Id == 1)
                {
                    oid = 2;
                }

                SMTPServerSetting OServerSetting = _db.SmtpServerSettings.Find(oid);

                OServerSetting.Enable = !smtpserversetting.Enable;
                OServerSetting.Update();

                return RedirectToAction("Index",new{Page=-1});
            }
            return View(smtpserversetting);
        }

        //
        // GET: /SMTP/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SMTPServerSetting smtpserversetting = _db.SmtpServerSettings.Find(id);
            if (smtpserversetting == null)
            {
                return HttpNotFound();
            }
            return View(smtpserversetting);
        }

        //
        // POST: /SMTP/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMTPServerSetting smtpserversetting = _db.SmtpServerSettings.Find(id);
            _db.SmtpServerSettings.Remove(smtpserversetting);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
