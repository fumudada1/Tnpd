using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    public class UnitsController : Controller
    {
        private BackendContext db = new BackendContext();
        //
        // GET: /Units/

        public ActionResult GetfistUnit()
        {
            var units = db.Units.Where(x => x.ParentId == null);


            var jsonContent = JsonConvert.SerializeObject(units.OrderBy(x => x.ListNum), Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

        public ActionResult GetUnit(int id)
        {
            var units = db.Units.Where(x => x.ParentId == id || x.Id==id).OrderBy(x=>x.Subject).ToList();


            var jsonContent = JsonConvert.SerializeObject(units, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

        public ActionResult GetMemberByUnitId(int id,int RoleID)
        {
            
            Role role = db.Roles.Find(RoleID);
            var jsonContent = "";
            if (role != null)
            {
                var members = db.Members.Where(x => x.UnitId == id).ToList().Where(p => !(role.Members.Contains(p))).Select(x => new
                {
                    Id = x.Id,
                    Name =x.Account+ "-" + x.Name + "("  + x.MyUnit.Subject + ")",
                    Unit =  x.MyUnit.Subject

                }).ToList(); ; ;
                jsonContent = JsonConvert.SerializeObject(members, Formatting.Indented);

            }
            else
            {
              var  members = db.Members.Where(x => x.UnitId == id).Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name + "(" + x.MyUnit.Subject + ")",
                    Unit =  x.MyUnit.Subject

                }).ToList();
                jsonContent = JsonConvert.SerializeObject(members, Formatting.Indented);
            }


        

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }
    }
}
