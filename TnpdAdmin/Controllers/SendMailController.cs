using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tnpd.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    public class SendMailController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /SendMail/

        public ActionResult Index()
        {
            Case mailCase = _db.Cases.Find(131);
            
            //發信
            string mailbody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/SendReport.html"));
            mailbody = mailbody.Replace("{CaseID}", mailCase.CaseID);
            mailbody = mailbody.Replace("{initDate}", mailCase.InitDate.Value.ToString("yyyy/MM/dd"));
            mailbody = mailbody.Replace("{CaseType}", mailCase.CaseType.ToString());
            mailbody = mailbody.Replace("{Name}", mailCase.Name);
            mailbody = mailbody.Replace("{Tel}", mailCase.TEL);
            mailbody = mailbody.Replace("{Predate}", mailCase.Predate.ToString("yyyy/MM/dd"));
            mailbody = mailbody.Replace("{Address}", mailCase.Address);
            mailbody = mailbody.Replace("{Email}", mailCase.Email);
            mailbody = mailbody.Replace("{Pid}", mailCase.Pid);
            mailbody = mailbody.Replace("{Gender}", mailCase.Gender.ToString());
            mailbody = mailbody.Replace("{HomeTEL}", mailCase.HomeTEL);
            mailbody = mailbody.Replace("{OfficeTEL}", mailCase.OfficeTEL);
            mailbody = mailbody.Replace("{PAddress}", mailCase.PAddress);
            mailbody = mailbody.Replace("{Address}", mailCase.Address);
            mailbody = mailbody.Replace("{CaseReportType}", mailCase.CaseReportType.ToString());
            mailbody = mailbody.Replace("{ODate}", mailCase.ODate.ToString());
            mailbody = mailbody.Replace("{Oplace}", mailCase.Oplace);
            mailbody = mailbody.Replace("{Content}", Txt2Html(mailCase.Content));

            Utility.SystemSendMail(mailCase.Email, "臺南市政府警察局-網路報案", mailbody);
            //Utility.SystemSendMail("topidea.justin@gmail.com", "臺南市政府警察局-網路報案", mailbody);
            return Content("success");
        }

        string Txt2Html(object article)
        {
            if (article != null)
            {
                string fstr = (string)article;
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace("<", "&lt;");
                fstr = fstr.Replace("\"", "&quot;");
                fstr = fstr.Replace("\'", "&quot;");
                fstr = fstr.Replace("\n", "<br>");
                fstr = fstr.Replace("|", "&brvbar;");
                fstr = fstr.Replace(" ", "&nbsp;");
                fstr = fstr.Replace("[", "<");
                fstr = fstr.Replace("]", ">");
                fstr = fstr.Replace("url=", "a href=");
                fstr = fstr.Replace("/url", "/a");
                return fstr;
            }
            return "";
        }

    }
}
