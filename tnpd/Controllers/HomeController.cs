using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace tnpd.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private BackendContext _db = new BackendContext();

        [OutputCache(Duration = 1)]
        public ActionResult Index()
        {
            DateTime yesterDay = DateTime.Now.AddDays(-1);
            //最新消息



            var newse1 = _db.Newses.Where(x => (x.NewsCatalogs.Count(y => y.WebCategoryId == 1 && y.WebSiteId == 1) > 0) && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是 )
                .OrderByDescending(p => p.StartDate).Take(7);

            //破案報導
            var newse2 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 3 && y.WebSiteId == 1) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(7);

            //榮譽榜
            var newse3 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 5 && y.WebSiteId == 1) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(7);

            //好人好事
            var newse4 = _db.Newses.Where(x => x.NewsCatalogs.Count(y => y.WebCategoryId == 6 && y.WebSiteId == 1) > 0 && x.StartDate <= DateTime.Now && x.EndDate >= yesterDay && x.IsConfirm == BooleanType.是)
                .OrderByDescending(p => p.StartDate).Take(7);

            




            var homeAds = _db.HomeAds.Where(x => x.Enable == BooleanType.是 && x.WebSiteNameId == 1).OrderBy(x => x.ListNum);

            var homeLinks = _db.HomeLinks.Where(x => x.Enable == BooleanType.是 && x.DataType==2 && x.WebSiteNameId == 1 && ( x.EndDate==null )).OrderBy(x => x.ListNum);
            var homeThemes = _db.HomeThemes.Where(x => x.Enable == BooleanType.是 && x.WebSiteNameId == 1).OrderBy(x => x.ListNum);
            var banners = _db.BigBanners.Where(x => x.Enable == BooleanType.是 && x.WebSiteNameId==1).OrderBy(x => x.ListNum);

           var news1= newse1.ToList();
            ViewBag.newse1 = news1;
           
            ViewBag.newse2 = newse2.ToList();
            ViewBag.newse3 = newse3.ToList();
            ViewBag.newse4 = newse4.ToList();
            ViewBag.banners = banners.ToList();

            ViewBag.homeAds = homeAds.ToList();
            var homelink = homeLinks.ToList();
            ViewBag.homeLinks = homelink;
            ViewBag.homeThemes = homeThemes.ToList();

            return View();
        }

        public ActionResult HomeLink()
        {
            var homeLinks = _db.HomeLinks.Where(x => x.Enable == BooleanType.是 && x.DataType == 2 && x.WebSiteNameId == 1 && (x.EndDate == null)).OrderBy(x => x.ListNum).ToList();

            return View(homeLinks);
        }
        // <summary>
        // 搜尋頁面
        // </summary>
        // <returns></returns>
        //public ActionResult Search()
        //{
        //    return View();
        //}

    }
}
