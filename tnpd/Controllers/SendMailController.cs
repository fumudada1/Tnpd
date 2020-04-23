using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels.Migrations;

namespace tnpd.Controllers
{
    public class SendMailController : Controller
    {
        //
        // GET: /SendMail/

        public ActionResult Index()
        {
            Utility.SystemSendMail("topidea.justin@gmail.com", "發信測試", "TestTest");
            return Content("success");
        }

        public ActionResult Index1()
        {
            Utility.SystemSendMail1("topidea.justin@gmail.com", "臺南市政府警察局全球資訊網", "TestTest");
            return Content("success");
        }

        //public ActionResult myDate()
        //{

        //    Holiday day=new Holiday();
        //    TnpdModels.Holiday holiday = new TnpdModels.Holiday();
        //    var Predate = holiday.GetWorkDay(DateTime.Today.AddDays(-1), 14);
        //    return Content(DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd") + "--" + Predate.ToString("yyyy/MM/dd"));
        //}



    }
}
