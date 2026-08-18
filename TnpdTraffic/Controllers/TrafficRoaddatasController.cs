using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TnpdModels;

namespace TnpdTraffic.Controllers
{
    public class TrafficRoaddatasController : Controller
    {
        private BackendContext _db = new BackendContext();

        public ActionResult GetRegions()
        {
            var regions = _db.TrafficRegions.Where(x=>x.Subject.Contains("快速公路")==false).OrderBy(p => p.ListNum).Select(x => new
            {
                id = x.Id,
                subject = x.Subject
            }).ToList();

            var jsonContent = JsonConvert.SerializeObject(regions, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }
        
        
        public ActionResult GetRoads(int id)
        {
            var towns = _db.TrafficRoaddatas.OrderBy(x=>x.ListNum).Where(x=>x.RegionId==id).OrderBy(x=>x.Subject).Select(x => new
            {
                id = x.Id,
                subject = x.Subject
            }).ToList();

            var jsonContent = JsonConvert.SerializeObject(towns, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

    }
}
