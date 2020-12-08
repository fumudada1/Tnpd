using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.guiren.Controllers
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

       


        public ActionResult GetTowns()
        {
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            var site = _db.WebSiteNames.FirstOrDefault(x => x.SiteCode == areaName);

            var precincts = _db.refugeStations.Where(x => x.Precinct == site.Subject).Select(x => new
            {
                id = x.DIstrict,
                subject = x.DIstrict
            }).Distinct().OrderBy(x => x.subject); ;
            return Json(precincts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVillage(string id)
        {

            var precincts = _db.refugeStations.Where(x => x.DIstrict == id).Select(x => new
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

        public ActionResult GetPointByPrecinct(string id)
        {

            var stations = _db.refugeStations.Where(x => x.Precinct == id && x.Twd97Y != "").OrderBy(x => x.Id).ToList();

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

        public ActionResult GetPointByAddress(string address)
        {
            string targetURI = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyC631kTAWg6pT3b3w3sFaD8xhxxASuM7xI&sensor=false&language=zh-tw";
            var request = WebRequest.Create(targetURI);
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            var gjsons = JsonConvert.DeserializeObject<GJson>(text);

            if (gjsons.results.Count() > 0)
            {
                var geometry = gjsons.results[0].geometry;
                double lat = Convert.ToDouble(geometry.location.lat);
                double lng = Convert.ToDouble(geometry.location.lng);
                int dis = 5000;
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

                var jsonContent = JsonConvert.SerializeObject(jsonitem, Formatting.Indented);

                return new ContentResult { Content = jsonContent, ContentType = "application/json" };


            }
            var jsonContent1 = JsonConvert.SerializeObject(gjsons.results, Formatting.Indented);

            return new ContentResult { Content = jsonContent1, ContentType = "application/json" };



        }

    }
}
