using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TnpdTrafficIDCheck.Controllers
{
    public class SOAPController : Controller
    {
        //
        // GET: /SOAP/

        public ActionResult checkIdOld(string id)
        {
            
            string result = Soap(id.ToUpper());
            return Content(result);

        }

        static string Soap(string id)
        {
            //try
            //{
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string idSoap = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://ws/oi2/npa/xsd"" xmlns:xsdl=""http://ws.oi2.npa/xsd"">
  <soapenv:Header/>
  <soapenv:Body>
        <xsd:getNameByIdnoForLog>
            <xsd:systemID>AC001</xsd:systemID>
            <xsd:systemPWD>2496-2519-1c6c</xsd:systemPWD>
        <xsd:SSOuserID>16773CKM</xsd:SSOuserID>
            <xsd:SSOuserName>蘇莞筑</xsd:SSOuserName>
             <xsd:SSOunitID>AC061</xsd:SSOunitID>
             <xsd:idno>A1234567890</xsd:idno>
             <xsd:encoding>unicode</xsd:encoding>
              <xsd:userIP>10.128.0.42</xsd:userIP>
        </xsd:getNameByIdnoForLog>
  </soapenv:Body>
</soapenv:Envelope>";

            idSoap = idSoap.Replace("A1234567890", id);



            soapEnvelopeXml.LoadXml(idSoap);


            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    //File.WriteAllText("c:\\NoResult.xml",soapResult,System.Text.Encoding.Default);
                    //Console.WriteLine(soapResult);
                    //Console.ReadKey();
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(soapResult);
                    XmlNode eNode = xmldoc.DocumentElement;
                    XmlNode tomeNode = eNode.FirstChild.FirstChild.FirstChild;
                    string body = tomeNode.InnerText;
                    body = body.Replace("&lt;", "<").Replace("&gt;", ">");

                    //File.WriteAllText("c:\\NoResult.xml", body, System.Text.Encoding.Default);
                    //Console.WriteLine(body);
                    //Console.ReadKey();

                    XmlDocument xmlbody = new XmlDocument();
                    xmlbody.LoadXml(body);
                    XmlNode xmlbodyEn = xmlbody.DocumentElement;
                    XmlNode statusNode = xmlbodyEn.FirstChild.ChildNodes[1];
                    if (statusNode.InnerXml == "查有資料")
                    {
                        XmlNode rowNode = xmlbodyEn.FirstChild.FirstChild;
                        //Console.WriteLine(rowNode.FirstChild.InnerXml);
                        //Console.WriteLine(rowNode.ChildNodes[1].InnerXml);
                        return rowNode.ChildNodes[1].InnerXml;
                    }
                    else
                    {
                        return statusNode.InnerXml;
                    }





                }
            }
            //}
            //catch (System.Net.WebException ex)
            //{

            //    throw;
            //}



        }

        public ActionResult GetXML(string id)
        {
            string result = SoapXML(id.ToUpper());
            return Content(result, "application/xml");
        }

        static string SoapXML(string id)
        {
            //try
            //{
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string idSoap = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://ws/oi2/npa/xsd"" xmlns:xsdl=""http://ws.oi2.npa/xsd"">
  <soapenv:Header/>
  <soapenv:Body>
        <xsd:getNameByIdnoForLog>
            <xsd:systemID>AC001</xsd:systemID>
            <xsd:systemPWD>2496-2519-1c6c</xsd:systemPWD>
        <xsd:SSOuserID>16773CKM</xsd:SSOuserID>
            <xsd:SSOuserName>蘇莞筑</xsd:SSOuserName>
             <xsd:SSOunitID>AC061</xsd:SSOunitID>
             <xsd:idno>A1234567890</xsd:idno>
             <xsd:encoding>unicode</xsd:encoding>
              <xsd:userIP>10.128.0.42</xsd:userIP>
        </xsd:getNameByIdnoForLog>
  </soapenv:Body>
</soapenv:Envelope>";

            idSoap = idSoap.Replace("A1234567890", id);



            soapEnvelopeXml.LoadXml(idSoap);


            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    return soapResult;
                    //File.WriteAllText("c:\\NoResult.xml",soapResult,System.Text.Encoding.Default);
                    //Console.WriteLine(soapResult);
                    //Console.ReadKey();
                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.LoadXml(soapResult);
                    //XmlNode eNode = xmldoc.DocumentElement;
                    //XmlNode tomeNode = eNode.FirstChild.FirstChild.FirstChild;
                    //string body = tomeNode.InnerText;
                    //body = body.Replace("&lt;", "<").Replace("&gt;", ">");

                    ////File.WriteAllText("c:\\NoResult.xml", body, System.Text.Encoding.Default);
                    ////Console.WriteLine(body);
                    ////Console.ReadKey();

                    //XmlDocument xmlbody = new XmlDocument();
                    //xmlbody.LoadXml(body);
                    //XmlNode xmlbodyEn = xmlbody.DocumentElement;
                    //XmlNode statusNode = xmlbodyEn.FirstChild.ChildNodes[1];
                    //if (statusNode.InnerXml == "查有資料")
                    //{
                    //    XmlNode rowNode = xmlbodyEn.FirstChild.FirstChild;
                    //    //Console.WriteLine(rowNode.FirstChild.InnerXml);
                    //    //Console.WriteLine(rowNode.ChildNodes[1].InnerXml);
                    //    return rowNode.ChildNodes[1].InnerXml;
                    //}
                    //else
                    //{
                    //    return statusNode.InnerXml;
                    //}





                }
            }
            //}
            //catch (System.Net.WebException ex)
            //{

            //    throw;
            //}



        }

        public static HttpWebRequest CreateWebRequest()
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://soa2.npa.gov.tw:7001/Webservice/OI2/OI2Info1");
            //webRequest.Headers.Add("SOAPAction", "\"http://tempuri.org/" + MethodName + "\"");
            webRequest.Headers.Add("authorization", "Basic QUMwMDE6MjQ5Ni0yNTE5LTFjNmM=");
            webRequest.ContentType = "application/soap+xml;charset=UTF-8";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;


        }


        public async Task<ActionResult> checkId(string id)
        {
            id = Microsoft.VisualBasic.Strings.StrConv(
                id, Microsoft.VisualBasic.VbStrConv.Narrow);
            var request_bearer = await Getbearer(id);

            //
            var request_result = GetResult("https://SOA2.eportal.npa.gov.tw:7002/Webservice/OI2/OI2Info1JWT", request_bearer, id);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(request_result);
            var root = doc.DocumentElement;
            var val = root.FirstChild.FirstChild.FirstChild.InnerText;

            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(val);
            var root2 = doc2.DocumentElement;
            var result = root2.FirstChild.FirstChild.ChildNodes[1].InnerText;
            return Content(result);
        }

        public static async Task<string> Getbearer(string id)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //string loginurl = "https://SOA2.eportal.npa.gov.tw:7002/generate";
            //string requesturl = "https://SOA2.eportal.npa.gov.tw:7002/Webservice/OI2/OI2Info1JWT";


            string text = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://ws/oi2/npa/xsd"">
   <soapenv:Header/>
   <soapenv:Body>
      <xsd:getNameByIdnoForLog>
         <!--Optional:-->
         <xsd:systemID>AC001</xsd:systemID>
         <!--Optional:-->
         <xsd:systemPWD>2496-2519-1c6c</xsd:systemPWD>
         <!--Optional:-->
         <xsd:SSOuserID>16773CKM</xsd:SSOuserID>
         <!--Optional:-->
         <xsd:SSOuserName>交通檢舉信箱</xsd:SSOuserName>
         <!--Optional:-->
         <xsd:SSOunitID>AC001</xsd:SSOunitID>
         <!--Optional:-->
         <xsd:idno>2496-2519-1c6c</xsd:idno>
         <!--Optional:-->
         <xsd:encoding>unicode</xsd:encoding>
         <!--Optional:-->
         <xsd:userIp>10.128.0.42</xsd:userIp>
      </xsd:getNameByIdnoForLog>
   </soapenv:Body>
</soapenv:Envelope>";
            text = text.Replace("A123456789", id);
            // var json = JsonConvert.SerializeObject(text);
            var data = new StringContent(text, Encoding.UTF8, "application/xml");

            var yourusername = "AC001";      // 修改為署提供的使者帳號
            var yourpwd = "2496-2519-1c6c";             // 修改為署提供的使者密碼
            byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(yourusername + ":" + yourpwd);
            string encrypted = Convert.ToBase64String(bytes);

            var login_url = "https://SOA2.eportal.npa.gov.tw:7002/generate";            // 修改為署提供的網址


            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            var login_client = new HttpClient(handler);

            login_client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic", encrypted);

            var request_bearer = "";
            var login_response = await login_client.PostAsync(login_url, data);
            //Console.WriteLine("login response status=" + login_response.StatusCode + ", " + (int)login_response.StatusCode);
            if (!login_response.IsSuccessStatusCode)
            {
                return "IsNotSuccessStatusCode";
            }
            HttpHeaders headers = login_response.Headers;
            var bearer = headers.GetValues("Authorization").SingleOrDefault();
            if (bearer != null)
            {
                var expectedValues = bearer.Split(' ');
                if (expectedValues.Length > 1)
                {
                    request_bearer = expectedValues[1];
                    return expectedValues[1];

                }
            }
            return "找不到資料";


        }

        static string GetResult(string url, string request_bearer, string id)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

            string targetURI = url;
            var request = (HttpWebRequest)System.Net.WebRequest.Create(targetURI);
            request.ContentType = "application/xml";
            //post
            request.Method = WebRequestMethods.Http.Post;
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "Bearer " + request_bearer);
            //request.Accept = "application/xml";
            request.Headers.Add("X-User", "AC001");

            string rawData = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://ws/oi2/npa/xsd"">
   <soapenv:Header/>
   <soapenv:Body>
      <xsd:getNameByIdnoForLog>
         <!--Optional:-->
         <xsd:systemID>AC001</xsd:systemID>
         <!--Optional:-->
         <xsd:systemPWD>2496-2519-1c6c</xsd:systemPWD>
         <!--Optional:-->
         <xsd:SSOuserID>16773CKM</xsd:SSOuserID>
         <!--Optional:-->
         <xsd:SSOuserName>交通檢舉信箱</xsd:SSOuserName>
         <!--Optional:-->
         <xsd:SSOunitID>AC061</xsd:SSOunitID>
         <!--Optional:-->
         <xsd:idno>A123456789</xsd:idno>
         <!--Optional:-->
         <xsd:encoding>unicode</xsd:encoding>
         <!--Optional:-->
         <xsd:userIp>10.128.0.42</xsd:userIp>
      </xsd:getNameByIdnoForLog>
   </soapenv:Body>
</soapenv:Envelope>";
            rawData = rawData.Replace("A123456789", id);
            byte[] postData = Encoding.UTF8.GetBytes(rawData.ToString());
            request.ContentLength = postData.Length;
            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(postData, 0, postData.Length);
            }


            string text;
            var response = (System.Net.HttpWebResponse)request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }

    }
}
