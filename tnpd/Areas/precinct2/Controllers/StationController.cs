using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using tnpd.Controllers;
using TnpdModels;

namespace tnpd.Areas.precinct2.Controllers
{
    public class StationController : _BaseController
    {

        private BackendContext _db = new BackendContext();
      
        //
        // GET: /About/

        public ActionResult Index(Guid id,int? page, FormCollection fc)
        {
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            ViewBag.UnId = id.ToString();
            var site = _db.WebSiteNames.FirstOrDefault(x => x.SiteCode == areaName);
            StationInfo station = _db.StationInfos.FirstOrDefault(x => x.Subject == site.Subject);


          


            //ViewBag.SearchByCategoryId = sClass;




          

            return View(station.StationInfos.OrderBy(x=>x.ListNum).ToList());
        }
        public ActionResult Details(string unid,  int id = 0)
        {
            ViewBag.UnId = unid;
          
            StationInfo station = _db.StationInfos.Find(id);
            if (station == null)
            {
                return RedirectToAction("Index", "Home");
            }



            return View(station);
        }
    }
}
