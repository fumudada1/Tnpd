using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{

    public class SendMailController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /SendMail/

        public ActionResult Index(string account)
        {
            Utility.SystemSendMail(account, "發信測試", "TestTest");
            return Content("success");
        }
        public ActionResult Index2(string account)
        {
            SMTPServerSetting serverSetting = _db.SmtpServerSettings.Where(x => x.Enable == true).FirstOrDefault();
            
                // 建立郵件訊息
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(serverSetting.MailFrom);
                mailMessage.To.Add("topidea.justin@gmail.com");
                mailMessage.Subject = "台南市政府發信測試";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "台南市政府發信測試";

                // 設定 SMTP 客戶端
                SmtpClient mailSender = new SmtpClient();
                mailSender.Host = serverSetting.Address;
                mailSender.Port = serverSetting.Port;
                mailSender.EnableSsl = serverSetting.EnableSsl;
                mailSender.UseDefaultCredentials = serverSetting.UseDefaultCredentials;
                mailSender.Credentials = new NetworkCredential(serverSetting.MailAccount, serverSetting.MailPassword);

                // 設定與內送伺服器相同的設定
                mailSender.DeliveryMethod = serverSetting.SmtpDeliveryMethod;



                // 發送郵件
                mailSender.Send(mailMessage);
                mailMessage.Dispose();
            

            
            return Content("success");
        }

        //public ActionResult Index1()
        //{
        //    Utility.SystemSendMail1("topidea.justin@gmail.com", "臺南市政府警察局全球資訊網", "TestTest");
        //    return Content("success");
        //}

        //public ActionResult myDate()
        //{

        //    Holiday day=new Holiday();
        //    TnpdModels.Holiday holiday = new TnpdModels.Holiday();
        //    var Predate = holiday.GetWorkDay(DateTime.Today.AddDays(-1), 14);
        //    return Content(DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd") + "--" + Predate.ToString("yyyy/MM/dd"));
        //}



    }
}
