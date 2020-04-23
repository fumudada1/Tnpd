using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CheckWebsite
{
    class Program
    {
        static void Main(string[] args)
        {
            string URL = ConfigurationManager.AppSettings["URL"];

            do
            {

                try
                {
                    WebRequest request = WebRequest.Create(URL);
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response == null || response.StatusCode != HttpStatusCode.OK)
                        {
                            Console.WriteLine("Error");
                            string mailTo = ConfigurationManager.AppSettings["mailTo"];
                            SendURLMail( mailTo, "網站無法服務!", "目前網站運作不正常!敬請馬上處理");
                            System.Threading.Thread.Sleep(600000);
                        }
                        else
                        {
                            Console.WriteLine("OK");
                            System.Threading.Thread.Sleep(3000);

                            request = null;
                        }
                    }
                    request = null;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    string mailTo = ConfigurationManager.AppSettings["mailTo"];
                    SendURLMail( mailTo, "網站無法服務!", "目前網站運作不正常!敬請馬上處理");
                    System.Threading.Thread.Sleep(100000);
                }

            } while (true);


        }

        public static string SendURLMail( string toAddress, string Subject, string MailBody)
        {
            string targetUrl = "http://tnpd.madhattertw.com/CheckWebsite/index?id=" + toAddress;

            HttpWebRequest request = HttpWebRequest.Create(targetUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30000;

            string result = "";
            // 取得回應資料
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result; 
        }


        public static void SendGmailMail(string fromAddress, string toAddress, string Subject, string MailBody, string password)
        {

            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = MailBody;
            // SMTP Server
            SmtpClient mailSender = new SmtpClient("smtp.gmail.com");
            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(fromAddress, password);
            mailSender.Credentials = basicAuthenticationInfo;
            mailSender.Port = 587;
            mailSender.EnableSsl = true;
            mailSender.Send(mailMessage);
            mailMessage.Dispose();
            try
            {

               
            }
            catch
            {
                return;
            }
        }
       
    }
}
