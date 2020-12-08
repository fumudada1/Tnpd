using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSService.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult SendCheckCode(string mobile,string message)
        {


            string result = SendHinetSMS(mobile, message);

            return Content(result);
        }

        private static string SendHinetSMS(string mobile, string message)
        {
            string UserID = ConfigurationManager.AppSettings["SMSAccount"].ToString();
            string Passwd = ConfigurationManager.AppSettings["MailPassword"].ToString();

            HiNet.Hiair2Net hiair = new HiNet.Hiair2Net();
            //連線驗證帳密並回傳狀態碼
            int retCode = hiair.StartCon("202.39.54.130", 8000, UserID, Passwd);
            //取得文字描述
            String retContent = hiair.Get_Message();

            if (retCode == 0)
            {

                //發送文字簡訊並回傳狀態碼
                retCode = hiair.SendMsg(mobile, message);
                //取得messageID或文字描述
                retContent = hiair.Get_Message();

                //Console.WriteLine(retCode + " : " + retContent);

            }
            else
            {
                //登入失敗
               // Console.WriteLine(retCode + " : " + retContent);
            }

            hiair.EndCon();
            return retCode + " : " + retContent;
        }

        private string SendSMS(string mobile, string message)
        {
            string ServerIp = "api.hiair.hinet.net";
            string ServerPort = "8000";
            string UserID = ConfigurationManager.AppSettings["SMSAccount"].ToString();
            string Passwd = ConfigurationManager.AppSettings["MailPassword"].ToString();
            int ret_code;
            string ret_description;

            System.Type objType = System.Type.GetTypeFromProgID("HiAir.HiNetSMS");

            dynamic objSMS = System.Activator.CreateInstance(objType);
            ret_code = objSMS.StartCon(ServerIp, ServerPort, UserID, Passwd);
            if (ret_code == 0)
            {


                ret_code = objSMS.SendMsg(mobile, message);
                ret_description = objSMS.Get_Message();
                //  Interaction.MsgBox(ret_description);
            }
            else
            {
                ret_description = objSMS.Get_Message();

            }

            objSMS.EndCon();
            return ret_description;
        }

    }
}
