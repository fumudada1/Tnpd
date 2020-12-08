using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{
    public class RefugeStationController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /RefugeStation/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            return View();
        }

        public ActionResult GetOrgs()
        {
            var units = _db.Units.Where(x=>x.Subject.Contains("分局") && x.ParentId==null).OrderBy(x=>x.ListNum).Select(x => new
            {
                id=x.Subject.Substring(3,x.Subject.Length-3),
                subject = x.Subject.Substring(3, x.Subject.Length - 3),
            });
            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTowns()
        {
            var precincts = _db.refugeStations.Select(x => new
            {
                id = x.DIstrict,
                subject = x.DIstrict
            }).Distinct().OrderBy(x => x.subject); ;
            return Json(precincts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVillage(string id)
        {
            var precincts = _db.refugeStations.Where(x=>x.DIstrict==id).Select(x => new
            {
                id = x.Village,
                subject = x.Village
            }).Distinct().OrderBy(x => x.subject); ;
            return Json(precincts, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult GetPointByVillage(string id, string district)
        {
            var stations = _db.refugeStations.Where(x => x.Village == id && x.DIstrict == district && x.Twd97Y != "").ToList();

            var newstations = stations.Select(x => new
            {
                subject = x.Subject,
                address = x.Address,
                Number = x.Number,
                x.Precinct,
                x.DIstrict,
                x.Village,
                lat = x.Twd97X,
                lng = x.Twd97Y

            });

            return Json(newstations, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPointByDistrict(string id)
        {
            var stations = _db.refugeStations.Where(x =>  x.DIstrict == id && x.Twd97Y != "").ToList();

            var newstations = stations.Select(x => new
            {
                subject = x.Subject,
                address = x.Address,
                Number = x.Number,
                x.Precinct,
                x.DIstrict,
                x.Village,
                lat = x.Twd97X,
                lng = x.Twd97Y

            });

            return Json(newstations, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPointByPrecinct(string id)
        {

            var stations = _db.refugeStations.Where(x => x.Precinct == id && x.Twd97Y !="").OrderBy(x=>x.Id).ToList();

            var newstations = stations.Select(x => new
            {
                subject = x.Subject,
                address = x.Address,
                Number = x.Number,
                x.Precinct,
                x.DIstrict,
                x.Village,
                lat =x.Twd97X ,
                lng = x.Twd97Y

            });

            return Json(newstations, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPointByAddress(string address)
        {
            string targetURI = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyC631kTAWg6pT3b3w3sFaD8xhxxASuM7xI&sensor=false&language=zh-tw";
            string text = GetJsonContent(targetURI);
            var gjsons = JsonConvert.DeserializeObject<GJson>(text);

            if (gjsons.results.Count() > 0)
            {
                var geometry = gjsons.results[0].geometry;
                double lat = Convert.ToDouble(geometry.location.lat);
                double lng = Convert.ToDouble(geometry.location.lng);
                int dis = 3000;
                var coord = new GeoCoordinate(lat, lng);
                var stations = _db.refugeStations.Where(x=> x.Twd97Y !="").ToList();
                
                var jsonitem = stations.Where(x => !string.IsNullOrEmpty(x.Twd97X) && !string.IsNullOrEmpty(x.Twd97Y)
                                               && double.Parse(x.Twd97X) <= 90 && double.Parse(x.Twd97X) >= -90
                                               && double.Parse(x.Twd97Y) <= 180 && double.Parse(x.Twd97Y) >= -180).Select(
                        c => new
                        {
                            subject = c.Subject,
                            address = c.Address,
                            Number = c.Number,
                            c.Precinct,
                            c.DIstrict,
                            c.Village,
                            lat = c.Twd97X,
                            lng = c.Twd97Y,
                            Dis = (int)(new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord),
                            DisShow = (new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord) > 1000 ? string.Format("{0:0.0}公里", (new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord) / 1000) : string.Format("{0}公尺", (int)(new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord))
                        }
                    ).Where(o => o.Dis <= dis)
                    .OrderBy(o => o.Dis)
                    .ToList();

                var jsonContent = JsonConvert.SerializeObject(jsonitem, Formatting.Indented);

                return new ContentResult { Content = jsonContent, ContentType = "application/json" };


            }
            var jsonContent1 = JsonConvert.SerializeObject(gjsons.results, Formatting.Indented);

            return new ContentResult { Content = jsonContent1, ContentType = "application/json" };



        }

        public ActionResult GetPointByPosition(double lat, double lng)
        {
        
         
            int dis = 3000;
            var coord = new GeoCoordinate(lat, lng);
            var stations = _db.refugeStations.Where(x => x.Twd97Y != "").ToList();

            var jsonitem = stations.Where(x => !string.IsNullOrEmpty(x.Twd97X) && !string.IsNullOrEmpty(x.Twd97Y)
                                           && double.Parse(x.Twd97X) <= 90 && double.Parse(x.Twd97X) >= -90
                                           && double.Parse(x.Twd97Y) <= 180 && double.Parse(x.Twd97Y) >= -180).Select(
                    c => new
                    {
                        subject = c.Subject,
                        address = c.Address,
                        Number = c.Number,
                        c.Precinct,
                        c.DIstrict,
                        c.Village,
                        lat = c.Twd97X,
                        lng = c.Twd97Y,
                        Dis = (int)(new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord),
                        DisShow = (new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord) > 1000 ? string.Format("{0:0.0}公里", (new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord) / 1000) : string.Format("{0}公尺", (int)(new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord))
                    }
                ).Where(o => o.Dis <= dis)
                .OrderBy(o => o.Dis)
                .ToList();
            var jsonContent = "";
            if (jsonitem.Count == 0)
            {
                stations = _db.refugeStations.Where(x => x.Precinct == "新營分局" && x.Twd97Y != "").OrderBy(x => x.Id).ToList();

                var jsonitem1 = stations.Select(x => new
                {
                    subject = x.Subject,
                    address = x.Address,
                    Number = x.Number,
                    x.Precinct,
                    x.DIstrict,
                    x.Village,
                    lat = x.Twd97X,
                    lng = x.Twd97Y

                });
                 jsonContent = JsonConvert.SerializeObject(jsonitem1, Formatting.Indented);
            }
            else
            {
                 jsonContent = JsonConvert.SerializeObject(jsonitem, Formatting.Indented);
            }


            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
           



        }

        public ActionResult GetPointCuntByPosition(double lat, double lng)
        {


            int dis = 3000;
            var coord = new GeoCoordinate(lat, lng);
            var stations = _db.refugeStations.Where(x => x.Twd97Y != "").ToList();

            var jsonitem = stations.Where(x => !string.IsNullOrEmpty(x.Twd97X) && !string.IsNullOrEmpty(x.Twd97Y)
                                           && double.Parse(x.Twd97X) <= 90 && double.Parse(x.Twd97X) >= -90
                                           && double.Parse(x.Twd97Y) <= 180 && double.Parse(x.Twd97Y) >= -180).Select(
                    c => new
                    {
                        subject = c.Subject,
                        address = c.Address,
                        Number = c.Number,
                        c.Precinct,
                        c.DIstrict,
                        c.Village,
                        lat = c.Twd97X,
                        lng = c.Twd97Y,
                        Dis = (int)(new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord),
                        DisShow = (new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord) > 1000 ? string.Format("{0:0.0}公里", (new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord) / 1000) : string.Format("{0}公尺", (int)(new GeoCoordinate(double.Parse(c.Twd97X), double.Parse(c.Twd97Y))).GetDistanceTo(coord))
                    }
                ).Where(o => o.Dis <= dis)
                .OrderBy(o => o.Dis)
                .Count();
            var jsonContent = "";



            return new ContentResult { Content = jsonitem.ToString()};




        }

        private static string GetJsonContent(string Url)
        {

            var uri = new Uri(Url);
            var request = WebRequest.Create(Url) as HttpWebRequest;

            WebClient wc = new WebClient();
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



    }
}
