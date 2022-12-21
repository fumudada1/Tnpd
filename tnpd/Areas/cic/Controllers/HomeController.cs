using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace tnpd.Areas.cic.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /english/Home/

        private BackendContext _db = new BackendContext();
        public ActionResult Index()
        {
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();

            DateTime yesterDay = DateTime.Now.AddDays(-1);
            //最新消息
            var newse1 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 1 && y.WebSite.SiteCode == areaName) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(6);
            //活動訊息
            var newse2 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 39 && y.WebSite.SiteCode == areaName) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(6);
            //破案報導
            var newse4 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 3 && y.WebSite.SiteCode == areaName) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(6);
            //好人好事
            var newse5 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 6 && y.WebSite.SiteCode == areaName) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(6);
            ////活動相簿
            //var newse3 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 7 && y.WebSite.SiteCode == areaName) > 0)
            //    .OrderByDescending(p => p.StartDate).Take(4);

            var banners = _db.BigBanners.Where(x => x.Enable == BooleanType.是 && x.WebSite.SiteCode == areaName).OrderBy(x => x.ListNum);
            var homeAds = _db.HomeAds.Where(x => x.Enable == BooleanType.是).OrderBy(x => x.ListNum);

            var homeLinks = _db.HomeLinks.Where(x => x.Enable == BooleanType.是 && x.DataType == 2 && x.WebSite.SiteCode == areaName && ((x.StartDate <= DateTime.Now && x.EndDate >= yesterDay) || (x.EndDate == null))).OrderBy(x => x.ListNum);
            var homeThemes = _db.HomeThemes.Where(x => x.Enable == BooleanType.是).OrderBy(x => x.ListNum);


            ViewBag.newse1 = newse1.ToList();
            ViewBag.newse2 = newse2.ToList();
            //ViewBag.newse3 = newse3.ToList();
            ViewBag.newse4 = newse4.ToList();
            ViewBag.newse5 = newse5.ToList();
            ViewBag.banners = banners.ToList();
            ViewBag.homeAds = homeAds.ToList();
            ViewBag.homeLinks = homeLinks;
            ViewBag.homeThemes = homeThemes.ToList();

            return View();
        }

        public ActionResult HomeLink()
        {
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            var homeLinks = _db.HomeLinks.Where(x => x.Enable == BooleanType.是 && x.DataType == 2 && x.WebSite.SiteCode == areaName && ((x.StartDate <= DateTime.Now && x.EndDate >= yesterDay) || (x.EndDate == null))).OrderBy(x => x.ListNum).ToList();
            ViewBag.Title = "相關連結";
            return View(homeLinks);
        }

    }
}
