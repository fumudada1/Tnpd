using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    public class APIController : Controller
    {
        //
        // GET: /API/
        private BackendContext _db = new BackendContext();

        public ActionResult GetTrafficCases()
        {
            DateTime day2 = _db.CaseTraffics.OrderByDescending(x => x.InitDate).FirstOrDefault().InitDate.Value
                .AddDays(-2);
            var caseTraffics = _db.CaseTraffics.Where(x => x.InitDate > day2).ToList();

            var cases = caseTraffics.Select(x => new
            {
                案件編號 = x.CaseID,
                姓名 = x.Name,
                電話 = x.TEL,
                身分證字號或居留證號 = x.Pid,
                地址 = x.Address,
                Email = x.Email,
                主題 = x.Subject,
                違規區域 = x.violation_place_area,
                違規路段 = x.violation_place_road,
                違規地點 = x.violation_place,
                違規日期 = x.violation_date,
                違規時間 = x.violation_time,
                違規車號 = x.violation_carno,
                違規事項 = x.itemno,
                違規內容 = x.Content
            });

            return Json(cases, JsonRequestBehavior.AllowGet);

        }

    }
}
