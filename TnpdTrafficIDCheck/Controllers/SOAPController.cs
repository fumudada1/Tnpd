using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TnpdTrafficIDCheck.Controllers
{
    public class SOAPController : Controller
    {
        //
        // GET: /SOAP/

        public ActionResult checkId(string id)
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

    }
}
