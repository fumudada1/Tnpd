using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tnpd.Models;

namespace Tnpd.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMail(string toMail,string subject,string htmlBody)
        {
            if (toMail == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (subject == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (htmlBody == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
           
            Utility.SystemSendMail(toMail, subject, htmlBody);
            return Content("Success");
        }

    }
}
