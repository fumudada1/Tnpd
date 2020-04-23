using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TnpdModels;

namespace WayPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            string Json3 = GetJsonContent("https://od.moi.gov.tw/api/v1/rest/datastore/A01010000C-000674-011");
            var wayPoints = JsonConvert.DeserializeObject<WayPoint>(Json3);
            using (var db = new BackendContext())
            {

                var citypoint = wayPoints.result.records.Where(x => x.CityName.Contains("臺南市")).ToList();
                foreach (records item in citypoint )
                {
                    TnpdModels.WayPoint wayPoint=new TnpdModels.WayPoint();
                    wayPoint.Address = item.Address;
                    wayPoint.BranchNm = item.BranchNm;
                    wayPoint.CityName = item.CityName;
                    wayPoint.DeptNm = item.DeptNm;
                    wayPoint.Latitude = item.Latitude;
                    wayPoint.Longitude = item.Longitude;
                    wayPoint.RegionName = item.RegionName;
                    wayPoint.direct = item.direct;
                    wayPoint.limit = item.limit;
                    db.WayPoints.Add(wayPoint);
                }
                db.SaveChanges();

            }
        }
        private static string GetJsonContent(string Url)
        {
            try
            {
                string targetURI = Url;
                var request = WebRequest.Create(targetURI);
                request.ContentType = "application/json; charset=utf-8";
                string text;
                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    text = sr.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }
    }

}
