using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TnpdModels;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.IO;

namespace ExportTrafficCase
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();
            List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.IsSend == false).Take(50).ToList();
            
            string JsonText = "";
            var list = caseTraffics.Select(x => new
            {
                         Id = x.Id,
                         InitDate = x.InitDate,
                    CaseID = x.CaseID,
                    Subject = x.Subject,
                    Content = x.Content,
                    Name = x.Name,
                    Pid = x.Pid,
                    Gender = x.Gender == GenderType.男 ? "男" : "女",
                    Email = x.Email,
                    TEL = x.TEL,
                    Job = x.Job,
                    Address = x.Address,
                    IP = x.IP,
                    assignUnit = x.assignUnit.Subject,
                    Upfile1 = "http://webmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile1 + " http://webmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1,
                    Upfile2 = "http://webmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile2 + " http://webmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2,
                    Upfile3 = "http://webmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile3 + " http://webmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3,
                    Upfile4 = "http://webmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile4 + " http://webmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4,
                    Upfile5 = "http://webmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile5 + " http://webmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5,
                    Upfile6 = "",
                         Upfile7 = "",
                         Upfile8 = "",
                         Upfile9 = "",
                         Upfile10 = "",
                         Upfile11 = "",
                         Upfile12 = "",
                    violation_date = x.violation_date,
                    violation_time = x.violation_time,
                    Predate = x.Predate,
                    violation_place = x.violation_place,
                    violation_carno = x.violation_carno,
                    violation_place_area = x.violation_place_area,
                    violation_place_road = x.violation_place_road,
                    itemno = x.itemno,
                    checkName = checkMyName(x.Pid),
                    分派 = new
                    {
                        CaseTime = x.Poprocs.Count == 0 ? x.InitDate : x.Poprocs.FirstOrDefault().CaseTime,
                        assignUnit = x.Poprocs.Count == 0 ? "" : x.Poprocs.FirstOrDefault().assignUnit.ParentUnit.Subject + " " + x.Poprocs.FirstOrDefault().assignUnit.Subject,
                        assignMember = x.Poprocs.Count == 0 ? "" : x.Poprocs.FirstOrDefault().assignMember.CName,

                    }

            });
            JsonText = Encrypt3DES(JsonConvert.SerializeObject(list));

            string result = Post("https://10.4.2.12/TNPDAPI/PublicReport", JsonText);
            Console.WriteLine("傳送結果:" + result);
            Console.ReadKey();
            System.IO.File.WriteAllText("D:\\website\\Tnpd\\ExportTrafficCases\\sendData\\json.txt", JsonText, System.Text.Encoding.UTF8);
            string desString = System.IO.File.ReadAllText("D:\\website\\Tnpd\\ExportTrafficCases\\sendData\\json.txt", System.Text.Encoding.UTF8);
            string deJsonText = Decrypt3DES(desString);
            System.IO.File.WriteAllText("D:\\website\\Tnpd\\ExportTrafficCases\\sendData\\sjson.txt", deJsonText, System.Text.Encoding.UTF8);

            foreach (CaseTraffic item in caseTraffics)
            {
               

            item.IsSend = true;
            _db.SaveChanges();
            }

        }

        // <summary>
        /// 指定Post地址使用Get 方式獲取全部字符串
        /// </summary>
        /// <param name="url">請求後臺地址</param>
        /// <returns></returns>
        private static string Post(string url,string inputData)
        {
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 參數
            NameValueCollection postParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
            
            postParams.Add("input", inputData);

            byte[] data = Encoding.UTF8.GetBytes(postParams.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //獲取響應內容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
       

        private static string checkMyName(string pid)
        {
            string url = "http://10.128.0.41:2080/Soap/checkId?id=" + pid;
            string result = GetJsonContent(url);

            return result;
        }

        private static string GetJsonContent(string Url)
        {

            var uri = new Uri(Url);
            var request = WebRequest.Create(Url) as HttpWebRequest;

            System.Net.WebClient wc = new WebClient();
            //REF: https://stackoverflow.com/a/39534068/288936
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string res = wc.DownloadString(Url);

            // If required by the server, set the credentials.

            request.UserAgent = "PostmanRuntime/7.26.5";
            request.Accept = "*/*";

            request.Credentials = CredentialCache.DefaultCredentials;



            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            // 重點是修改這行

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;



            // Get the response.


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();




            // Get the stream containing content returned by the server.

            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.

            StreamReader reader = new StreamReader(dataStream);

            // Read the content.

            string responseFromServer = reader.ReadToEnd();

            // Display the content.


            // Cleanup the streams and the response.

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;

        }



        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {

            return true;

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
    }
}
