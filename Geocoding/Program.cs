using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TnpdModels;

namespace Geocoding
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BackendContext())
            {
                var towns = db.Towns.ToList();
                var stationInfos = db.StationInfos.ToList();
                foreach (StationInfo rsInfo in stationInfos)
                {
                    string targetURI = "https://maps.googleapis.com/maps/api/geocode/json?address=" + rsInfo.Address + "&key=AIzaSyC631kTAWg6pT3b3w3sFaD8xhxxASuM7xI&sensor=false&language=zh-tw";
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
                        rsInfo.Twd97X = geometry.location.lat;
                        rsInfo.Twd97Y = geometry.location.lng;
                        Console.Write(rsInfo.Address + "......Success\n" + text);

                    }
                    else
                    {

                        Console.Write(rsInfo.Address + ".......not find\n" + text);
                    }
                    //foreach (var town in towns)
                    //{
                    //    if (rsInfo.Address.IndexOf(town.Subject) > 0)
                    //    {
                    //        rsInfo.TownId = town.Id;
                    //    }
                    //}

                    db.SaveChanges();
                    System.Threading.Thread.Sleep(500);
                }

            }
        }
    }
}
