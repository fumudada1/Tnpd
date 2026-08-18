using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    public class CaseTrafficReceiveController : Controller
    {
        private BackendContext _db = new BackendContext();

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CaseTrafficReceive/
        [HttpPost]
        public ActionResult Index(string CaseId,string result)
        {
            CaseTraffic caseTraffic = _db.CaseTraffics.FirstOrDefault(x => x.CaseID == CaseId);
            if (caseTraffic == null)
            {
                return Content("案號錯誤");
            }

            
            var Poprocs = _db.CaseTrafficPoprocs.FirstOrDefault(x => x.CaseId == caseTraffic.Id);
            if (Poprocs!=null)
            {
                Poprocs.Status = CaseProcessStatus.結案;
                Poprocs.AssignMemo = result;
                _db.SaveChanges();
            }
            else
            {
                CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                poproc.CaseId = caseTraffic.Id;

                poproc.CaseTime = caseTraffic.InitDate.Value;
               
                poproc.Status = CaseProcessStatus.結案;
                poproc.AssignDateTime = caseTraffic.InitDate.Value;
                poproc.AssignMemo = result;
                //poproc.process = process;
                //poproc.PoprocsType = PoprocsType;

                //poproc.PoprocsSubType = PoprocsSubType;

                _db.CaseTrafficPoprocs.Add(poproc);
                _db.SaveChanges();
            }

            return Content("Success");
        }

    }
}
