using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{
    public class ResponsesController : Controller
    {
        //
        // GET: /Responses/

        private BackendContext _db = new BackendContext();
        //
        // GET: /ChiefMailbox/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            ResponseViews responseViews = new ResponseViews();
            return View(responseViews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Guid id, ResponseViews responseViews, string checkCode, HttpPostedFileBase Upfile1, HttpPostedFileBase Upfile2, HttpPostedFileBase Upfile3, HttpPostedFileBase Upfile4, HttpPostedFileBase Upfile5)
        {
            ViewBag.UnId = id.ToString();
            ////驗證碼確認
            //string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : DateTime.Now.Millisecond.ToString();
            //if (checkCode.ToLower() != sCheckCode)
            //{
            //    ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
            //    return View(responseViews);
            //}

            //bool ischeckROCDate = checkROCDate(responseViews.ODate);
            //if (ischeckROCDate == false)
            //{
            //    ModelState.AddModelError("CheckDate", "發生日期格式錯誤!!");
            //    return View(responseViews);
            //}
            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            //CaseMailCheck mailCheck = _db.caseMailChecks.FirstOrDefault(x => x.Email == reportView.Email && x.ConfirmDate >= duDateTime);
            //if (mailCheck == null)
            //{
            //    ModelState.AddModelError("CheckCode", "E-mail驗證錯誤，請寄送認證郵件，並請至信箱接收認證郵件，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            //    ViewBag.UnId = id.ToString();


            //    return View(reportView);
            //}
            string filesName = "";
            if (ModelState.IsValid)
            {
                Case mailCase = new Case();
                mailCase.Subject = responseViews.Oplace; //記得改
                mailCase.Content = responseViews.Content;

                mailCase.CaseGuid = Guid.NewGuid().ToString();
                mailCase.Email = responseViews.Email;
                mailCase.Address = responseViews.Address;
                mailCase.PostalCode = responseViews.PostalCode;
                mailCase.Name = responseViews.Name;
                mailCase.TEL = responseViews.TEL;

                mailCase.WebSiteId = 1;


                mailCase.HomeTEL = responseViews.HomeTEL;


                mailCase.CaseType = CaseType.重大災害通報專區;

                Holiday holiday = new Holiday();
                mailCase.Predate = holiday.GetWorkDay(DateTime.Today, 14);
                mailCase.ODate = responseViews.ODate.Value.AddHours(Convert.ToInt16(responseViews.STime1)).AddMinutes(Convert.ToInt16(responseViews.STime2));
                mailCase.STime = responseViews.STime1 + ":" + responseViews.STime2;
                mailCase.Oplace = responseViews.Oplace;
                mailCase.IP = Request.UserHostAddress;
                mailCase.InitDate = DateTime.Now;

                if (Upfile1 != null)
                {

                    bool checkExtension = CheckExtension(Upfile1);

                    //所有都不是
                    if ((checkExtension == false) || (Upfile1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 && Upfile1.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = id.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(responseViews);

                    }
                    mailCase.Upfile1 = Utility.SaveTraffFile(Upfile1);
                    filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile1 + " '> " + Upfile1.FileName + "</a><br/>";



                }
                System.Threading.Thread.Sleep(300);

                if (Upfile2 != null)
                {

                    bool checkExtension = CheckExtension(Upfile2);

                    //所有都不是
                    if ((checkExtension == false) || (Upfile2.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 && Upfile2.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = id.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(responseViews);

                    }
                    mailCase.Upfile2 = Utility.SaveTraffFile(Upfile2);
                    filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile2 + " '> " + Upfile2.FileName + "</a><br/>";


                }
                System.Threading.Thread.Sleep(300);

                if (Upfile3 != null)
                {

                    bool checkExtension = CheckExtension(Upfile3);

                    //所有都不是
                    if ((checkExtension == false) || (Upfile3.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 && Upfile3.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = id.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(responseViews);

                    }
                    mailCase.Upfile3 = Utility.SaveTraffFile(Upfile3);
                    filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile3 + " '> " + Upfile3.FileName + "</a><br/>";


                }

                System.Threading.Thread.Sleep(300);

                if (Upfile4 != null)
                {

                    bool checkExtension = CheckExtension(Upfile4);

                    //所有都不是
                    if ((checkExtension == false) || (Upfile4.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 && Upfile4.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = id.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(responseViews);

                    }

                    mailCase.Upfile4 = Utility.SaveTraffFile(Upfile4);
                    filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile4 + " '> " + Upfile4.FileName + "</a><br/>";


                }
                System.Threading.Thread.Sleep(300);

                if (Upfile5 != null)
                {
                    bool checkExtension = CheckExtension(Upfile5);

                    //所有都不是
                    if ((checkExtension == false) || (Upfile5.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 && Upfile5.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1 ))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = id.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(responseViews);

                    }
                    mailCase.Upfile5 = Utility.SaveTraffFile(Upfile5);
                    filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile5 + " '> " + Upfile5.FileName + "</a><br/>";


                }

                List<CaseFilters> caseFilterses = _db.CaseFilterses.ToList();
                CaseFilters filterItem = null;
                foreach (var filterse in caseFilterses)
                {
                    filterItem = filterse;
                    if (filterse.FilterType == CaseFilterType.姓名)
                    {
                        if (filterse.Subject.Equals(responseViews.Name.Trim()))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = responseViews.Name.Trim();
                            mailCase.FilterType = filterse.PoprocsSubType.Subject;
                            break;
                        }
                    }
                    if (filterse.FilterType == CaseFilterType.IP)
                    {
                        if (filterse.Subject.Equals(Request.UserHostAddress))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = Request.UserHostAddress;
                            mailCase.FilterType = filterse.PoprocsSubType.Subject;
                            break;
                        }
                    }
                    if (filterse.FilterType == CaseFilterType.電子郵件)
                    {
                        if (filterse.Subject.Equals(responseViews.Email.Trim()))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = responseViews.Email.Trim();
                            mailCase.FilterType = filterse.PoprocsSubType.Subject;
                            break;
                        }
                    }

                }

                _db.Cases.Add(mailCase);
                if (mailCase.IsAutoClose == BooleanType.是)
                {
                    CasePoproc poproc = new CasePoproc();
                    poproc.CaseId = mailCase.Id;

                    poproc.CaseType = mailCase.CaseType;
                    poproc.CaseTime = mailCase.InitDate.Value;

                    poproc.Status = CaseProcessStatus.結案;
                    poproc.AssignDateTime = DateTime.Now;
                    poproc.AssignMemo = filterItem.PoprocsSubType.Article;
                    poproc.AssignMemo = poproc.AssignMemo.Replace("{Name}", mailCase.Name);
                    poproc.AssignMemo = poproc.AssignMemo.Replace("{InitDate}", Date2CrocFormat(DateTime.Now));
                    poproc.AssignMemo = poproc.AssignMemo.Replace("{subject}", mailCase.Subject);

                    poproc.process = "process";
                    poproc.PoprocsType = 3;
                    poproc.PoprocsSubType = filterItem.PoprocsSubType.Id;
                    _db.CasePoprocs.Add(poproc);
                }
                _db.SaveChanges();

                //發信
                string mailbody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/SendResponses.html"));
                mailbody = mailbody.Replace("{CaseID}", mailCase.CaseID);
                mailbody = mailbody.Replace("{initDate}", mailCase.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{Name}", mailCase.Name);
                mailbody = mailbody.Replace("{Tel}", mailCase.TEL);
                mailbody = mailbody.Replace("{HomeTEL}", mailCase.HomeTEL);
                mailbody = mailbody.Replace("{Address}", mailCase.PostalCode + mailCase.Address);
                mailbody = mailbody.Replace("{Email}", mailCase.Email);
                mailbody = mailbody.Replace("{ODate}", mailCase.ODate.Value.ToString("yyyy-MM-dd") + " " + mailCase.STime);
                mailbody = mailbody.Replace("{Oplace}", mailCase.Oplace);
                mailbody = mailbody.Replace("{Content}", Txt2Html(mailCase.Content));

                mailbody = mailbody.Replace("{Address}", mailCase.Address);


                mailbody = mailbody.Replace("{Files}", filesName);

                //Utility.SystemSendMail(mailCase.Email, "臺南市政府警察局-重大災害通報專區", mailbody);
                Utility.SystemSendMail("net110@mail.tainan.gov.tw,net110@tnpd.gov.tw,tnpd2212@tnpd.gov.tw", "重大災害通報專區-" + mailCase.Name + "案件", mailbody);


                //Utility.SystemSendMail("topidea.justin@gmail.com", "重大災害通報專區-" + mailCase.Name + "案件", mailbody);

                return RedirectToAction("CreateSuccess", new { unid = id, id = mailCase.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            return View(responseViews);
        }


        private bool CheckExtension(HttpPostedFileBase upfile)
        {
            string exstring = "avi,mp4,wmv,mov,3gp,jpeg,jpg,png,bmp";

            string extension = upfile.FileName.Split('.')[upfile.FileName.Split('.').Length - 1].ToLower();
            if (exstring.IndexOf(extension) > -1)
            {
                return true;
            }

            return false;

        }

        private bool checkROCDate(string reportViewODate)
        {
            string[] tempStrings = reportViewODate.Split('-');
            if (tempStrings.Length != 3)
            {
                return false;
            }

            if (!Utility.IsNumber(tempStrings[0]))
            {
                return false;
            }

            if (!Utility.IsNumber(tempStrings[1]))
            {
                return false;
            }

            if (!Utility.IsNumber(tempStrings[2]))
            {
                return false;
            }

            string strDate = (Convert.ToInt16(tempStrings[0]) + 1911) + "-" + tempStrings[1] + "-" + tempStrings[2];
            if (!Utility.IsDate(strDate))
            {
                return false;
            }
            return true;

        }

        private DateTime GetUNCDate(string reportViewODate)
        {
            string[] tempStrings = reportViewODate.Split('-');

            string strDate = (Convert.ToInt16(tempStrings[0]) + 1911) + "-" + tempStrings[1] + "-" + tempStrings[2];

            return Convert.ToDateTime(strDate);

        }


        public string Date2CrocFormat(DateTime dat)
        {
            return "民國" + (dat.Year - 1911).ToString() + "年" + dat.Month.ToString() + "月" + dat.Day.ToString() + "日";
        }
        /// <summary>
        /// 輸入文字方塊的文字轉成HTML
        /// </summary>
        /// <param name="article">要轉換的文字</param>
        /// <returns>回傳HTML用的文字</returns>
        /// <remarks></remarks>

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
            var MyCase = _db.Cases.FirstOrDefault(x => x.CaseGuid == id);
            return View(MyCase);
        }

        public ActionResult SendVerifyMail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Email 不可空白");
            }
            if (id.IndexOf("@vusra.com") > -1)
            {
                return Content("");
            }
            if (Utility.IsValidEmail(id))
            {
                CaseMailCheck mailCheck = new CaseMailCheck
                {
                    CaseGuid = Guid.NewGuid().ToString(),
                    Email = id,
                    InitDate = DateTime.Now,
                    ConfirmDate = DateTime.Now.AddYears(-25),
                    IsConfirm = BooleanType.否
                };
                _db.caseMailChecks.Add(mailCheck);
                _db.SaveChanges();
                string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];

                string mailBody = "親愛的市民朋友，您好，這是臺南市政府警察局-網路報案 Email認證信件。<br/>";
                mailBody += "<a href='" + InternetURL + "/Report/VerifyMail/" + mailCheck.CaseGuid + "'>請點選此連結認證您的信箱</a><br/>";
                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(id, "臺南市政府警察局-網路報案  Email認證信件", mailBody);//發信
                return Content("認證郵件已送出，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            }
            return Content("Email 格式錯誤");
        }

        //public ActionResult VerifyMail(string id)
        //{
        //    CaseMailCheck mailCheck = _db.caseMailChecks.OrderByDescending(x => x.Id).FirstOrDefault(x => x.CaseGuid == id);
        //    if (mailCheck == null)
        //    {
        //        ViewBag.message = "驗證錯誤";
        //        return View();
        //    }
        //    if (mailCheck.InitDate.AddMinutes(30) <= DateTime.Now)
        //    {
        //        ViewBag.message = "驗證超時，請重新驗證";
        //        return View();
        //    }
        //    mailCheck.IsConfirm = BooleanType.是;
        //    mailCheck.ConfirmDate = DateTime.Now;
        //    _db.SaveChanges();
        //    ViewBag.message = "驗證成功，請回原頁面輸入資料";
        //    return View();
        //}

    }
}
