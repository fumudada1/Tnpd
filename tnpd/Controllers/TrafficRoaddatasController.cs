using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TnpdModels;

namespace tnpd.Controllers
{
    public class TrafficRoaddatasController : Controller
    {
        private BackendContext _db = new BackendContext();

        public ActionResult GetRoads(int id)
        {
            var towns = _db.TrafficRoaddatas.OrderBy(x=>x.ListNum).Where(x=>x.RegionId==id).Select(x => new
            {
                id = x.Id,
                subject = x.Subject
            }).ToList();

            var jsonContent = JsonConvert.SerializeObject(towns, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

    }
}
