using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{
    public class TrafficSMSController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /TrafficSMS/

        public ActionResult Index()
        {
            //ViewBag.UnId = id.ToString();
            return View();
        }

        public ActionResult Create(string checkRead)
        {
            if (Session["TrafficSMSGUID"] != null)
            {
                return RedirectToAction("AddCarInfo", new { id = Session["TrafficSMSGUID"] });
            }

            if (checkRead != "agree")
            {
                return RedirectToAction("Index");
            }
           
            TrafficSMSView trafficSms = new TrafficSMSView();

          

            return View(trafficSms);
        }

        [HttpPost]

        public ActionResult Create( TrafficSMSView smsView, string checkCode)
        {
           
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : "000";
            string filesName = "";
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");

                return View(smsView);
            }
            
            if (ModelState.IsValid)
            {
                TrafficSMS trafficSms = _db.trafficSmses.FirstOrDefault(x => x.Mobile == smsView.Mobile);
                if (trafficSms == null)
                {
                    ViewBag.Message = "驗證碼錯誤!";
                    return View(smsView);
                }

                if (trafficSms.CheckCode != smsView.SMSCode)
                {
                    ViewBag.Message = "驗證碼錯誤!";
                    return View(smsView);
                }

                trafficSms.Name = smsView.Name;
                trafficSms.IsAction = BooleanType.是;
                trafficSms.CaseGuid = System.Guid.NewGuid().ToString();
                trafficSms.Update(_db,_db.trafficSmses);
                Session["TrafficSMSGUID"] = trafficSms.CaseGuid;
                return RedirectToAction("AddCarInfo", new { id = trafficSms.CaseGuid });
            }


            return View(smsView);
        }
        public ActionResult AddCarInfo(string id)
        {
            TrafficSMS trafficSms = _db.trafficSmses.FirstOrDefault(x => x.CaseGuid == id);
            if (trafficSms == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.trafficSms = trafficSms;
            TrafficSMSCarInfo trafficSmsCarInfo = new TrafficSMSCarInfo();
            return View(trafficSmsCarInfo);
        }

        [HttpPost]
        public ActionResult AddCarInfo(string id,TrafficSMSCarInfo trafficSmsCarInfo)
        {
            TrafficSMS trafficSms = _db.trafficSmses.FirstOrDefault(x => x.CaseGuid == id);
            var count = _db.trafficSmsCarInfos.Count(x => x.CarNO == trafficSmsCarInfo.CarNO);
            if (count > 0)
            {
                ViewBag.trafficSms = trafficSms;
                return View(trafficSmsCarInfo);
            }

            trafficSmsCarInfo.TrafficSMSId = trafficSms.Id;
             ModelState.Remove("id");
             if (ModelState.IsValid)
            {
               
                trafficSmsCarInfo.InitDate=DateTime.Now;
                //trafficSmsCarInfo.checkStatus = BooleanType.否;
                _db.trafficSmsCarInfos.Add(trafficSmsCarInfo);
                _db.SaveChanges();
                ViewBag.isSuccess = true;
                ViewBag.guid = id;

            }
            ViewBag.trafficSms = trafficSms;
            return View(trafficSmsCarInfo);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            TrafficSMSCarInfo trafficsmscarinfo = _db.trafficSmsCarInfos.Find(id);
            string guid = trafficsmscarinfo.trafficSms.CaseGuid;
            _db.trafficSmsCarInfos.Remove(trafficsmscarinfo);
            _db.SaveChanges();
            return RedirectToAction("AddCarInfo", new { id = guid });
        }

        //public ActionResult CreateSuccess(string id)
        //{
        //    ViewBag.UnId = unid.ToString();
        //    var MyCase = _db.CaseTraffics.FirstOrDefault(x => x.CaseGuid == id);
        //    return View(MyCase);
        //}



        [HttpPost]
        public ActionResult SendCheckCode(string mobile)
        {
            if (!Utility.IsValidCellPhone(mobile))
            {
                return Content("手機格式錯誤!");
            }

            var str = Guid.NewGuid().ToString().Replace("-", "");
            string CheckCode = str.Substring(0, 5);
            TrafficSMS sms = _db.trafficSmses.FirstOrDefault(x => x.Mobile == mobile);
            if (sms != null)
            {
                //if (sms.InitDate.Value.AddMinutes(3) >= DateTime.Now)
                //{
                //    return Content("請等待接收簡訊，若超過3分鐘沒收到再次發送!");
                //}

                sms.CheckCode = CheckCode;
                sms.InitDate = DateTime.Now;
                sms.Update(_db, _db.trafficSmses);
            }
            else
            {
                TrafficSMS trafficSms = new TrafficSMS();
                trafficSms.Mobile = mobile;
                trafficSms.CheckCode = CheckCode;
                trafficSms.Create(_db, _db.trafficSmses);
            }

            string message = "台端申請臺南市政府警察局交通違規簡訊服務，請輸入驗證碼" + CheckCode;

            string result = SendHinetSMS(mobile, message);

            return Content(result);
        }
       
        public ActionResult CheckCode(string mobile)
        {
            if (!Utility.IsValidCellPhone(mobile))
            {
                return Content("手機格式錯誤!");
            }

            var str = Guid.NewGuid().ToString().Replace("-", "");
            string CheckCode = str.Substring(0, 5);
            TrafficSMS sms = _db.trafficSmses.FirstOrDefault(x => x.Mobile == mobile);
            if (sms != null)
            {
                if (sms.InitDate.Value.AddMinutes(3) >= DateTime.Now)
                {
                    return Content("請等待接收簡訊，若超過3分鐘沒收到再次發送!");
                }

                sms.CheckCode = CheckCode;
                sms.InitDate = DateTime.Now;
                sms.Update(_db, _db.trafficSmses);
            }
            else
            {
                TrafficSMS trafficSms = new TrafficSMS();
                trafficSms.Mobile = mobile;
                trafficSms.CheckCode = CheckCode;
                trafficSms.Create(_db, _db.trafficSmses);
            }

            string message = "台端申請臺南市政府警察局交通違規簡訊服務，請輸入驗證碼" + CheckCode;

            string result = SendHinetSMS(mobile, message);

            return Content(result);
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
                    return "已經發送，請留意簡訊訊息!";
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
    }
}
