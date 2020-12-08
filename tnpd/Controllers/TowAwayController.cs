using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using tnpd.Models;

namespace tnpd.Controllers
{
    public class TowAwayController : Controller
    {
        //
        // GET: /TowAway/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Guid id, string carNum,string checkCode)
        {
            ViewBag.UnId = id.ToString();

            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : "000";
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                ViewBag.UnId = id.ToString();
                ViewBag.message = "驗證碼錯誤!!";
               
                return View();
            }

            try
            {
                string Json1 = GetJsonContent("http://172.16.1.143/takecarquery.ashx?carno=" + carNum);
                TowAwayjson3[] towAwayjsons1 = JsonConvert.DeserializeObject<TowAwayjson3[]>(Json1);
                TowAwayjson3 awayjson1 = towAwayjsons1.FirstOrDefault();

                if (awayjson1.CAR_NO == "")
                {
                    ViewBag.message = "查詢不到資料!!";
                }
                else
                {
                    ViewBag.TowAway1 = awayjson1;
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "查詢不到資料!!!";
            }


            try
            {
                string Json3 = GetJsonContent("http://tow.tainan.gov.tw/CarInformationService.ashx?type=JSON");
                TowAwayjson[] towAwayjsons = JsonConvert.DeserializeObject<TowAwayjson[]>(Json3);
                TowAwayjson awayjson = towAwayjsons.FirstOrDefault(x => x.car_no == carNum);
                if (awayjson != null)
                {
                    ViewBag.TowAway = awayjson;
                    return View();
                }
                
                
            }
            catch (Exception e)
            {
                ViewBag.message = "查詢不到資料!";
            }

            

            return View();
        }
        private static string GetJsonContent(string Url)
        {

            var uri = new Uri(Url);
            var request = WebRequest.Create(Url) as HttpWebRequest;

            // If required by the server, set the credentials.

            request.UserAgent = "*/*";

            request.Credentials = CredentialCache.DefaultCredentials;



            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            // 重點是修改這行

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2;



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
    }
}
