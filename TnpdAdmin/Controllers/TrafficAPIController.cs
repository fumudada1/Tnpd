using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Tnpd.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    public class TrafficAPIController : Controller
    {
        //
        // GET: /TrafficAPI/

        private BackendContext _db = new BackendContext();
        
        [HttpPost]
        public ActionResult SendMail(string id,string subject,string email,string content)
        {
            Utility.SystemSendMail(email, subject, content);
            return Content("信件已發送");
        }

        [HttpPost]
        public ActionResult Receive(string input)
        {
            string data = Decrypt3DES(input);
            System.IO.File.WriteAllText(@"D:\website\Tnpd\TnpdAdmin\trafficlog\"+DateTime.Now.ToString("yyyyMMddhhmmsss"),data);
            return Content("接收成功");
        }

        /// <summary>
        /// 使用3DES加密
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strKey">長度為24字元</param>
        /// <returns></returns>
        public static string Encrypt3DES(string strSource, string strKey = "!QAZ!QAZ4rfv3edc")
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = UTF8Encoding.UTF8.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5").Substring(0, 24));
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            byte[] Buffer = UTF8Encoding.UTF8.GetBytes(strSource);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 使用3DES解密
        /// </summary>
        /// <param name="strEncryptData"></param>
        /// <param name="strKey">長度為24字元/param>
        /// <returns></returns>
        public static string Decrypt3DES(string strEncryptData, string strKey = "!QAZ!QAZ4rfv3edc")
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = UTF8Encoding.UTF8.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5").Substring(0, 24));
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = "";
            byte[] Buffer = Convert.FromBase64String(strEncryptData);
            result = UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return result;
        }

        public ActionResult GetTrafficCases()
        {
            var date = DateTime.Now.AddDays(-2);
            var caseTraffics = _db.CaseTraffics.Where(x => x.InitDate > date).ToList();

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
            string jsonContent = JsonConvert.SerializeObject(cases, Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

    }
}
