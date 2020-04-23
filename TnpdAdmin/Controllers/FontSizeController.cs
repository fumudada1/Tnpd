using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Filters;
using TnpdModels;

namespace Tnpd.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class FontSizeController : _BaseController
    {
        private BackendContext _db = new BackendContext();

        //

        




        //
        // GET: /FontSize/Edit/5

        public ActionResult Edit(int id = 1)
        {
            FontSize fontsize = _db.FontSizes.Find(id);
            if (fontsize == null)
            {
                return HttpNotFound();
            }
            return View(fontsize);
        }

        //
        // POST: /FontSize/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public ActionResult Edit(FontSize fontsize)
        {
            if (ModelState.IsValid)
            {

               //_db.Entry(fontsize).State = EntityState.Modified;
                fontsize.Update();
                return View(fontsize);
            }
            return View(fontsize);
        }

        //
        // GET: /FontSize/Delete/5

       
       
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
