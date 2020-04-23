using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using tnpd.Models;
using TnpdModels;
using System.Device.Location;

namespace tnpd.Controllers
{
    public class StationInfoController : Controller
    {
        //
        // GET: /StationInfo/
        private BackendContext _db = new BackendContext();

        public ActionResult GetTowns()
        {
            var towns = _db.Towns.Select(x => new
            {
                id = x.Id,
                subject = x.Subject
            }).ToList();

            var jsonContent = JsonConvert.SerializeObject(towns, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

        public ActionResult GetOrgs()
        {
            var towns = _db.StationInfos.Where(x=>x.ParentId==null || x.ParentId==2 && x.Id<160).Select(x=>new
            {
                id=x.Id,
                subject=x.Subject
            }).ToList();

            var jsonContent = JsonConvert.SerializeObject(towns, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

        public ActionResult GetPointByArea(int id)
        {
            var stationslist = _db.StationInfos.ToList();
            var stations = stationslist.Where(x => x.TownId == id);
            foreach (StationInfo station in stations)
            {
                if (station.ParentStation!=null && station.Address == station.ParentStation.Address)
                {
                    station.Subject = string.Format("{0}及{1}", station.ParentStation.Subject, station.Subject);
                }
            }
            var newstations = stations.Select(x => new
            {
                subject=x.Subject,
                tel=x.Tel,
                address = x.Address,
                postalCode = x.PostalCode,
                lat = x.Twd97X,
                lng = x.Twd97Y

            });


            var jsonContent = JsonConvert.SerializeObject(newstations, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
            
        }

        public ActionResult GetPointByStation(int id)
        {
            
            var stationslist = _db.StationInfos.ToList();
            var stations = stationslist.Where(x => x.ParentId == id || x.Id==id);
            foreach (StationInfo station in stations)
            {
                if (station.ParentStation != null && station.Address == station.ParentStation.Address)
                {
                    station.Subject = string.Format("{0}及{1}", station.ParentStation.Subject, station.Subject);
                }
            }
            var newstations = stations.Select(x => new
            {
                subject = x.Subject,
                tel = x.Tel,
                address = x.Address,
                postalCode = x.PostalCode,
                lat = x.Twd97X,
                lng = x.Twd97Y

            });


            var jsonContent = JsonConvert.SerializeObject(newstations, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

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
                double lat =Convert.ToDouble( geometry.location.lat);
                double lng =Convert.ToDouble(geometry.location.lng) ;
                int dis = 5000;
                var coord = new GeoCoordinate(lat, lng);
                var stations = _db.StationInfos.ToList();
                foreach (StationInfo station in stations)
                {
                    if (station.ParentStation != null && station.Address == station.ParentStation.Address)
                    {
                        station.Subject = string.Format("{0}及{1}", station.ParentStation.Subject, station.Subject);
                    }
                }
                var jsonitem = stations.Where(x => !string.IsNullOrEmpty(x.Twd97X) && !string.IsNullOrEmpty(x.Twd97Y)
                                               && double.Parse(x.Twd97X) <= 90 && double.Parse(x.Twd97X) >= -90
                                               && double.Parse(x.Twd97Y) <= 180 && double.Parse(x.Twd97Y) >= -180).Select(
                        c => new
                        {
                            subject=c.Subject,
                            tel=c.Tel,
                            address = c.Address,
                            postalCode = c.PostalCode,
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
