using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using Tnpd.Controllers;
using Tnpd.Filters;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class TrafficSMSController : _BaseController
    {
        private BackendContext _db = new BackendContext();
        private const int DefaultPageSize = 15;
        //




        public ActionResult Index(int? page, FormCollection fc)
        {
            //記住搜尋條件
            GetCatcheRoutes(page, fc);

            //取得正確的頁面
            int currentPageIndex = getCurrentPage(page, fc);


            var trafficsmscarinfos = _db.trafficSmsCarInfos.Include(t => t.trafficSms).OrderByDescending(p => p.checkStatus).ThenByDescending(x => x.InitDate).AsQueryable();
            ViewBag.TrafficSMSId = new SelectList(_db.trafficSmses.OrderBy(p => p.InitDate), "Id", "Name");


            if (hasViewData("SearchByCarType"))
            {
                string CarType = getViewDateStr("SearchByCarType");
                TnpdModels.CarType searchByCarType = (TnpdModels.CarType)Enum.Parse(typeof(TnpdModels.CarType), CarType, false);

                trafficsmscarinfos = trafficsmscarinfos.Where(w => w.CarType == searchByCarType);
            }
            if (hasViewData("SearchBySubject"))
            {
                string searchBySubject = getViewDateStr("SearchBySubject");
                trafficsmscarinfos = trafficsmscarinfos.Where(w => w.CarNO.Contains(searchBySubject) || w.Pid.Contains(searchBySubject) || w.CarOwner.Contains(searchBySubject));
            }

            if (hasViewData("SearchByCarAllow"))
            {
                string CarAllow = getViewDateStr("SearchByCarAllow");
                TnpdModels.CarAllow searchByCarAllow = (TnpdModels.CarAllow)Enum.Parse(typeof(TnpdModels.CarAllow), CarAllow, false);

                trafficsmscarinfos = trafficsmscarinfos.Where(w => w.CarAllow == searchByCarAllow);
            }

            if (hasViewData("SearchBycheckStatus"))
            {
                string checkStatus = getViewDateStr("SearchBycheckStatus");
                TnpdModels.SMSStatus searchBycheckStatus = (TnpdModels.SMSStatus)Enum.Parse(typeof(TnpdModels.SMSStatus), checkStatus, false);

                trafficsmscarinfos = trafficsmscarinfos.Where(w => w.checkStatus == searchBycheckStatus);
            }

            if (hasViewData("SearchByTrafficSMSId"))
            {
                int searchByTrafficSMSId = getViewDateInt("SearchByTrafficSMSId");
                trafficsmscarinfos = trafficsmscarinfos.Where(w => w.TrafficSMSId == searchByTrafficSMSId);
            }

            if (hasViewData("SearchByStartDate") && hasViewData("SearchByEndDate"))
            {
                DateTime startDate = Convert.ToDateTime(getViewDateStr("SearchByStartDate"));
                DateTime endDate = Convert.ToDateTime(getViewDateStr("SearchByEndDate")).AddDays(1);
                trafficsmscarinfos = trafficsmscarinfos.Where(w => w.InitDate >= startDate && w.InitDate <= endDate);
            }
            //總件數
            ViewBag.Total = trafficsmscarinfos.Count();

            return View(trafficsmscarinfos.ToPagedList(currentPageIndex, DefaultPageSize));

        }





        //
        // GET: /TrafficSMS/Details/5

        public ActionResult Details(int id = 0)
        {
            TrafficSMSCarInfo trafficsmscarinfo = _db.trafficSmsCarInfos.Find(id);
            if (trafficsmscarinfo == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(trafficsmscarinfo);
        }

        //
        // GET: /TrafficSMS/Create

        public ActionResult Create()
        {
            ViewBag.TrafficSMSId = new SelectList(_db.trafficSmses.OrderBy(p => p.InitDate), "Id", "Name");
            return View();
        }

        //
        // POST: /TrafficSMS/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(TrafficSMSCarInfo trafficsmscarinfo)
        {
            if (ModelState.IsValid)
            {

                _db.trafficSmsCarInfos.Add(trafficsmscarinfo);
                trafficsmscarinfo.Create(_db, _db.trafficSmsCarInfos);
                return RedirectToAction("Index");
            }

            ViewBag.TrafficSMSId = new SelectList(_db.trafficSmses.OrderBy(p => p.InitDate), "Id", "Name", trafficsmscarinfo.TrafficSMSId);
            return View(trafficsmscarinfo);
        }

        //
        // GET: /TrafficSMS/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TrafficSMSCarInfo trafficsmscarinfo = _db.trafficSmsCarInfos.Find(id);
            if (trafficsmscarinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrafficSMSId = new SelectList(_db.trafficSmses.OrderBy(p => p.InitDate), "Id", "Name", trafficsmscarinfo.TrafficSMSId);
            ViewBag.TrafficSMSCarInfoRejectId = new SelectList(_db.TrafficSmsCarInfoRejects.OrderBy(p => p.ListNum), "Id", "Subject", trafficsmscarinfo.TrafficSMSCarInfoRejectId);
            return View(trafficsmscarinfo);
        }

        //
        // POST: /TrafficSMS/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(TrafficSMSCarInfo trafficsmscarinfo, string mobile)
        {
            if (ModelState.IsValid)
            {

                //_db.Entry(trafficsmscarinfo).State = EntityState.Modified;
                if (trafficsmscarinfo.checkStatus != SMSStatus.未通過)
                {
                    trafficsmscarinfo.TrafficSMSCarInfoRejectId = null;
                }
                trafficsmscarinfo.Update();
                if (trafficsmscarinfo.checkStatus == SMSStatus.已通過)
                {
                    string result = SendHinetSMS(mobile, "台端申請臺南市政府警察局交通違規簡訊服務,車號:" + trafficsmscarinfo.CarNO + "已經審查通過!");
                }
                if (trafficsmscarinfo.checkStatus == SMSStatus.未通過)
                {
                    string result = SendHinetSMS(mobile, "台端申請臺南市政府警察局交通違規簡訊服務,車號:" + trafficsmscarinfo.CarNO + "未審查通過!");
                }


                return RedirectToAction("Index", new { Page = -1 });
            }
            ViewBag.TrafficSMSId = new SelectList(_db.trafficSmses.OrderBy(p => p.InitDate), "Id", "Name", trafficsmscarinfo.TrafficSMSId);
            return View(trafficsmscarinfo);
        }

        private static string SendHinetSMS(string mobile, string message)
        {
            string UserID = ConfigurationManager.AppSettings["SMSAccount"].ToString();
            string Passwd = ConfigurationManager.AppSettings["SMSPassword"].ToString();

            HiNet.Hiair2Net hiair = new HiNet.Hiair2Net();
            //連線驗證帳密並回傳狀態碼
            int retCode = hiair.StartCon("202.39.54.130", 8000, UserID, Passwd);
            //取得文字描述
            String retContent = hiair.Get_Message();

            if (retCode == 0)
            {

                //發送文字簡訊並回傳狀態碼
                retCode = hiair.SendMsg(mobile, message);
                //取得messageID或文字描述
                retContent = hiair.Get_Message();
                if (retCode == 0)
                {
                    return "發送成功!";
                }
                hiair.EndCon();
                return retContent;
                //Console.WriteLine(retCode + " : " + retContent);

            }
            else
            {
                hiair.EndCon();
                return retCode + " : " + retContent;
                //登入失敗
                // Console.WriteLine(retCode + " : " + retContent);
            }


        }
        //
        // GET: /TrafficSMS/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TrafficSMSCarInfo trafficsmscarinfo = _db.trafficSmsCarInfos.Find(id);
            if (trafficsmscarinfo == null)
            {
                return HttpNotFound();
            }
            return View(trafficsmscarinfo);
        }

        //
        // POST: /TrafficSMS/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficSMSCarInfo trafficsmscarinfo = _db.trafficSmsCarInfos.Find(id);
            _db.trafficSmsCarInfos.Remove(trafficsmscarinfo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}
