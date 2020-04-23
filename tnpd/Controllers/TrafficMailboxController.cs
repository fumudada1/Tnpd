using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{
    public class TrafficMailboxController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /ChiefMailbox/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            return View();
        }

        public ActionResult Create(Guid id, string checkRead)
        {
            if (checkRead != "agree")
            {
                return RedirectToAction("Index", new { id = id });
            }
            ViewBag.UnId = id.ToString();
            CaseTrafficView caseTraffic = new CaseTrafficView();
            caseTraffic.violation_dateYear = (DateTime.Now.Year-1911).ToString();
            caseTraffic.violation_dateMonth = DateTime.Now.Month.ToString();
            caseTraffic.violation_dateday = DateTime.Now.Day.ToString();
            caseTraffic.violation_time1 = DateTime.Now.Hour.ToString();
            caseTraffic.violation_time2 = DateTime.Now.Minute.ToString();

            


            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");

            return View(caseTraffic);
        }

        // POST: /Mailbox/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, CaseTrafficView trafficView, string checkCode, HttpPostedFileBase Upfile1, HttpPostedFileBase Upfile2, HttpPostedFileBase Upfile3, HttpPostedFileBase Upfile4, HttpPostedFileBase Upfile5, HttpPostedFileBase Upfile6)
        {
            ViewBag.UnId = id.ToString();
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : "000";
            string filesName = "";
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                ViewBag.UnId = id.ToString();
               
                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(trafficView);
            }
            //身分證字號認證
            //if (!isIdentificationId(trafficView.Pid))
            //{
            //    ModelState.AddModelError("Pid", "身分證字號規則驗證錯誤，請填入正確身份證字號!!");
            //    ViewBag.UnId = id.ToString();

            //    ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
            //    return View(trafficView);
            //}
            DateTime Odate = Convert.ToDateTime((Convert.ToInt32(trafficView.violation_dateYear) + 1911).ToString() + "/" + trafficView.violation_dateMonth + "/" + trafficView.violation_dateday);
            if (DateTime.Today.AddDays(-8) > Odate)
            {
                ModelState.AddModelError("CheckCode", "已逾七日之檢舉。");
                ViewBag.UnId = id.ToString();

                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(trafficView);
            }

            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            TrafficMailCheck mailCheck = _db.TrafficMailChecks.FirstOrDefault(x => x.Email == trafficView.Email && x.ConfirmDate >= duDateTime);
            if (mailCheck == null)
            {
                ModelState.AddModelError("CheckCode", "E-mail驗證錯誤，請寄送認證郵件，並請至信箱接收認證郵件，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
                ViewBag.UnId = id.ToString();

                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(trafficView);
            }

           

            if (ModelState.IsValid)
            {
               CaseTraffic traffic=new CaseTraffic();

                traffic.Address = trafficView.Address;
                traffic.Subject = trafficView.Subject;
                traffic.Content = trafficView.Content;
                traffic.Email = trafficView.Email;
                traffic.Pid = trafficView.Pid;
                traffic.Job = trafficView.Job;
                traffic.Name = trafficView.Name;
                traffic.TEL = trafficView.TEL;
                traffic.itemno = trafficView.itemno;
                traffic.violation_date = Convert.ToDateTime((Convert.ToInt32(trafficView.violation_dateYear)+1911).ToString() + "/" + trafficView.violation_dateMonth + "/" + trafficView.violation_dateday) ;
                traffic.violation_carno = trafficView.violation_carno1 + "-" + trafficView.violation_carno2;
                TrafficRoaddata roaddata =
                    _db.TrafficRoaddatas.FirstOrDefault(x => x.Id == trafficView.violation_place_road);
                if (roaddata != null)
                {
                    traffic.violation_place_area = roaddata.Region.Subject;
                    traffic.violation_place_road = roaddata.Subject;
                    traffic.UnitId = roaddata.Region.UnitId;
                }
                else
                {
                    return RedirectToAction("Index", "TrafficMailbox");
                }

                traffic.violation_place = trafficView.violation_place;
                traffic.violation_time = trafficView.violation_time1 + ":" + trafficView.violation_time2;
                traffic.Gender = trafficView.Gender;
                if (Upfile1 != null)
                {

                    //if (Upfile1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    //{
                    //    ViewBag.Message = "檔案型態錯誤!";
                     
                    //}
                    filesName += Upfile1.FileName + "<br/>";
                    traffic.Upfile1 = Utility.SaveTraffFile(Upfile1);


                }
                System.Threading.Thread.Sleep(300);

                if (Upfile2 != null)
                {

                    //if (Upfile1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    //{
                    //    ViewBag.Message = "檔案型態錯誤!";

                    //}
                    filesName += Upfile2.FileName + "<br/>";
                    traffic.Upfile2 = Utility.SaveTraffFile(Upfile2);

                }
                System.Threading.Thread.Sleep(300);

                if (Upfile3 != null)
                {

                    //if (Upfile1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    //{
                    //    ViewBag.Message = "檔案型態錯誤!";

                    //}
                    filesName += Upfile3.FileName + "<br/>";
                    traffic.Upfile3 = Utility.SaveTraffFile(Upfile3);

                }
                System.Threading.Thread.Sleep(300);
                if (Upfile4 != null)
                {

                  
                    filesName += Upfile4.FileName + "<br/>";
                    traffic.Upfile4 = Utility.SaveTraffFile(Upfile4);


                }
                System.Threading.Thread.Sleep(300);

                if (Upfile5 != null)
                {

                  
                    filesName += Upfile5.FileName + "<br/>";
                    traffic.Upfile5 = Utility.SaveTraffFile(Upfile5);

                }
                System.Threading.Thread.Sleep(300);

                if (Upfile6 != null)
                {

                    //if (Upfile1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    //{
                    //    ViewBag.Message = "檔案型態錯誤!";

                    //}
                    filesName += Upfile6.FileName + "<br/>";
                    traffic.Upfile6 = Utility.SaveTraffFile(Upfile6);

                }


                traffic.CaseGuid = Guid.NewGuid().ToString();

               
             
                Holiday holiday = new Holiday();
                traffic.Predate = holiday.GetWorkDay(DateTime.Today, 45);

                traffic.IP = Request.UserHostAddress;
                traffic.InitDate = DateTime.Now;
                _db.CaseTraffics.Add(traffic);

                Trafficdisdata disTrafficdisdata = _db.Trafficdisdatas
                    .FirstOrDefault(x => x.assignMember.MyUnit.ParentId == traffic.UnitId && x.isAutoAssign==BooleanType.是);
                
                _db.SaveChanges();

                if (disTrafficdisdata != null)
                {
                    CaseTrafficPoproc poproc = new CaseTrafficPoproc();
                    poproc.CaseId = traffic.Id;
                    poproc.MemberId = disTrafficdisdata.MemberId;
                    poproc.CaseTime = traffic.InitDate.Value;
                    poproc.UnitId = disTrafficdisdata.assignMember.UnitId;
                    poproc.Status = CaseProcessStatus.待辦;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = "";
                    //poproc.process = process;
                    //poproc.PoprocsType = PoprocsType;
                    //poproc.PoprocsSubType = PoprocsSubType;

                    _db.CaseTrafficPoprocs.Add(poproc);
                    CaseTrafficPoprocLog log = new CaseTrafficPoprocLog();
                    log.CaseId = traffic.Id;
                    log.MemberId = disTrafficdisdata.MemberId;
                    log.InitDate = DateTime.Now;
                    log.Action = "派案";
                    log.UnitId = disTrafficdisdata.assignMember.UnitId;
                    log.ActionMemo = "案件指派";
                    _db.CaseTrafficPoprocLogs.Add(log);
                    _db.SaveChanges();
                }

                //發信
                string mailbody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/SendCustomer.html"));
                mailbody = mailbody.Replace("{CaseID}", traffic.CaseID);
                mailbody = mailbody.Replace("{initDate}", traffic.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{CaseType}", "違規舉發");
                mailbody = mailbody.Replace("{Name}", traffic.Name);
                mailbody = mailbody.Replace("{Tel}", traffic.TEL);
                mailbody = mailbody.Replace("{Predate}", traffic.Predate.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{Address}", traffic.Address);
                mailbody = mailbody.Replace("{Email}", traffic.Email);
                mailbody = mailbody.Replace("{Subject}","【車號：" + traffic.violation_carno + "】" + traffic.Subject);
                mailbody = mailbody.Replace("{Content}", Txt2Html(traffic.Content));
                mailbody = mailbody.Replace("{Files}", filesName);

                Utility.SystemSendMail(traffic.Email, "臺南市政府警察局-違規舉發", mailbody);//發信
                //Utility.SendGmailMail("topidea.justin@gmail.com", traffic.Email, "臺南市政府警察局-違規舉發", mailbody, "xuqoqvdvvsbwyrbl");
                


                return RedirectToAction("CreateSuccess", new { unid = id, id = traffic.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            ViewBag.UnId = id.ToString();
            CaseTrafficView caseTraffic = new CaseTrafficView();
            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
            return View(caseTraffic);
        }

        #region checkID
        public static bool isIdentificationId(string arg_Identify)
        {
            var d = false;
            if (arg_Identify.Length == 10)
            {
                arg_Identify = arg_Identify.ToUpper();
                if (arg_Identify[0] >= 0x41 && arg_Identify[0] <= 0x5A)
                {
                    var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                    var b = new int[11];
                    b[1] = a[(arg_Identify[0]) - 65] % 10;
                    var c = b[0] = a[(arg_Identify[0]) - 65] / 10;
                    for (var i = 1; i <= 9; i++)
                    {
                        b[i + 1] = arg_Identify[i] - 48;
                        c += b[i] * (10 - i);
                    }
                    if (((c % 10) + b[10]) % 10 == 0)
                    {
                        d = true;
                    }
                }
            }
            return d;
        }
        #endregion

        string Txt2Html(object article)
        {
            if (article != null)
            {
                string fstr = (string)article;
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace("<", "&lt;");
                fstr = fstr.Replace("\"", "&quot;");
                fstr = fstr.Replace("\'", "&quot;");
                fstr = fstr.Replace("\n", "<br>");
                fstr = fstr.Replace("|", "&brvbar;");
                fstr = fstr.Replace(" ", "&nbsp;");
                fstr = fstr.Replace("[", "<");
                fstr = fstr.Replace("]", ">");
                fstr = fstr.Replace("url=", "a href=");
                fstr = fstr.Replace("/url", "/a");
                return fstr;
            }
            return "";
        }
        public ActionResult CreateSuccess(Guid unid, string id)
        {
            ViewBag.UnId = unid.ToString();
            var MyCase = _db.CaseTraffics.FirstOrDefault(x => x.CaseGuid == id);
            return View(MyCase);
        }

        public ActionResult SendVerifyMail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Email 不可空白");
            }
            if (Utility.IsValidEmail(id))
            {
                TrafficMailCheck mailCheck = new TrafficMailCheck
                {
                    CaseGuid = Guid.NewGuid().ToString(),
                    Email = id,
                    InitDate = DateTime.Now,
                    ConfirmDate = DateTime.Now.AddYears(-25),
                    IsConfirm = BooleanType.否
                };
                _db.TrafficMailChecks.Add(mailCheck);
                _db.SaveChanges();
                string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];

                string mailBody = "親愛的市民朋友，您好，這是臺南市政府警察局-交通違規檢舉系統Email認證信件。<br/>";
                mailBody += "<a href='" + InternetURL + "/TrafficMailbox/VerifyMail/" + mailCheck.CaseGuid + "'>請點選此連結認證您的信箱</a><br/>";
                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(id, "臺南市政府警察局-違規舉發", mailBody);//發信
                return Content("認證郵件已送出，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            }
            return Content("Email 格式錯誤");
        }

        public ActionResult VerifyMail(string id)
        {
            TrafficMailCheck mailCheck = _db.TrafficMailChecks.OrderByDescending(x=>x.Id).FirstOrDefault(x => x.CaseGuid == id);
            if (mailCheck == null)
            {
                ViewBag.message = "驗證錯誤";
                return View();
            }
            if (mailCheck.InitDate.AddMinutes(30) <= DateTime.Now)
            {
                ViewBag.message = "驗證超時，請重新驗證";
                return View();
            }
            mailCheck.IsConfirm = BooleanType.是;
            mailCheck.ConfirmDate=DateTime.Now;
            _db.SaveChanges();
            ViewBag.message = "驗證成功，請回原頁面輸入資料";
            return View();
        }

    }
}
