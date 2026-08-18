using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{
    public class ResponsesController : Controller
    {
        private static readonly Dictionary<string, List<FileSignature>> _fileSignatures =
            new Dictionary<string, List<FileSignature>>
        {
            { ".mp4", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x69, 0x73, 0x6F, 0x6D }, 8),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 0)
                }
            },
            { ".3gp", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x33 }, 4),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4)
                }
            },
            { ".avi", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),

                    new FileSignature(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0)
                }
            },
            { ".wmv", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x30, 0x26, 0xB2, 0x75 }, 0)
                }
            },
            { ".mov", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, 4)
                }
            },
            { ".jpeg", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79 }, 4)

                  
                }
            },
            { ".jpg", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0)
                }
            },
            { ".png", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, 0),
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0)
                }
            },
            { ".bmp", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x42, 0x4D }, 0)
                }
            }
        };
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
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : DateTime.Now.Millisecond.ToString();
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                return View(responseViews);
            }
            Session["CheckCode"] = null;

            //驗證E-MAIL
            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            CaseMailCheck mailCheck = _db.caseMailChecks.FirstOrDefault(x => x.Email == responseViews.Email && x.InitDate >= duDateTime && x.CheckCode == responseViews.EmailCode);
            if (mailCheck == null)
            {
                ModelState.AddModelError("CheckCode", "E-mail驗證錯誤，請寄送認證郵件，並請至信箱接收認證郵件，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
                ViewBag.UnId = id.ToString();
                return View(responseViews);
            }
            //驗證手機
            CaseMobileCheck mobileCheck = _db.CaseMobileChecks.FirstOrDefault(x => x.Mobile == responseViews.TEL && x.InitDate >= duDateTime && x.CheckCode == responseViews.SMSCode);
            if (mobileCheck == null)
            {
                ModelState.AddModelError("CheckCode", "手機驗證錯誤，請3分鐘後重新寄送驗證碼，並請至手機簡訊，取得驗證碼。");
                ViewBag.UnId = id.ToString();
                return View(responseViews);
            }

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

                if (Upfile1 != null && Upfile1.ContentLength > 0)
                {


                    var fileExtension = Path.GetExtension(Upfile1.FileName).ToLower();
                    using (var binaryReader = new BinaryReader(Upfile1.InputStream))
                    {
                        var fileData = binaryReader.ReadBytes(Upfile1.ContentLength);
                        bool isValid = CheckFileSignature(fileData, fileExtension);

                        if (!isValid)
                        {

                            ViewBag.Message = "系統檢查檔案格式不符合，請參閱下方上傳檔案限制，再進行上傳。";
                            ViewBag.UnId = id.ToString();
                            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                            return View(responseViews);
                        }

                        // Save the file or process it further
                        mailCase.Upfile1 = Utility.SaveTraffFile(Upfile1);
                        filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile1 + " '> " + Upfile1.FileName + "</a><br/>";
                    }



                }

                
                System.Threading.Thread.Sleep(300);

                if (Upfile2 != null && Upfile2.ContentLength > 0)
                {


                    var fileExtension = Path.GetExtension(Upfile2.FileName).ToLower();
                    using (var binaryReader = new BinaryReader(Upfile2.InputStream))
                    {
                        var fileData = binaryReader.ReadBytes(Upfile2.ContentLength);
                        bool isValid = CheckFileSignature(fileData, fileExtension);

                        if (!isValid)
                        {

                            ViewBag.Message = "系統檢查檔案格式不符合，請參閱下方上傳檔案限制，再進行上傳。";
                            ViewBag.UnId = id.ToString();
                            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                            return View(responseViews);
                        }

                        // Save the file or process it further
                        mailCase.Upfile2 = Utility.SaveTraffFile(Upfile2);
                        filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile2 + " '> " + Upfile2.FileName + "</a><br/>";
                    }



                }
                System.Threading.Thread.Sleep(300);

                if (Upfile3 != null && Upfile3.ContentLength > 0)
                {


                    var fileExtension = Path.GetExtension(Upfile3.FileName).ToLower();
                    using (var binaryReader = new BinaryReader(Upfile3.InputStream))
                    {
                        var fileData = binaryReader.ReadBytes(Upfile3.ContentLength);
                        bool isValid = CheckFileSignature(fileData, fileExtension);

                        if (!isValid)
                        {

                            ViewBag.Message = "系統檢查檔案格式不符合，請參閱下方上傳檔案限制，再進行上傳。";
                            ViewBag.UnId = id.ToString();
                            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                            return View(responseViews);
                        }

                        // Save the file or process it further
                        mailCase.Upfile3 = Utility.SaveTraffFile(Upfile3);
                        filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile3 + " '> " + Upfile3.FileName + "</a><br/>";
                    }



                }

                System.Threading.Thread.Sleep(300);

                if (Upfile4 != null && Upfile4.ContentLength > 0)
                {


                    var fileExtension = Path.GetExtension(Upfile4.FileName).ToLower();
                    using (var binaryReader = new BinaryReader(Upfile4.InputStream))
                    {
                        var fileData = binaryReader.ReadBytes(Upfile4.ContentLength);
                        bool isValid = CheckFileSignature(fileData, fileExtension);

                        if (!isValid)
                        {

                            ViewBag.Message = "系統檢查檔案格式不符合，請參閱下方上傳檔案限制，再進行上傳。";
                            ViewBag.UnId = id.ToString();
                            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                            return View(responseViews);
                        }

                        // Save the file or process it further
                        mailCase.Upfile4 = Utility.SaveTraffFile(Upfile4);
                        filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile4 + " '> " + Upfile4.FileName + "</a><br/>";
                    }



                }
                System.Threading.Thread.Sleep(300);

                if (Upfile5 != null && Upfile5.ContentLength > 0)
                {


                    var fileExtension = Path.GetExtension(Upfile5.FileName).ToLower();
                    using (var binaryReader = new BinaryReader(Upfile5.InputStream))
                    {
                        var fileData = binaryReader.ReadBytes(Upfile5.ContentLength);
                        bool isValid = CheckFileSignature(fileData, fileExtension);

                        if (!isValid)
                        {

                            ViewBag.Message = "系統檢查檔案格式不符合，請參閱下方上傳檔案限制，再進行上傳。";
                            ViewBag.UnId = id.ToString();
                            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                            return View(responseViews);
                        }

                        // Save the file or process it further
                        mailCase.Upfile5 = Utility.SaveTraffFile(Upfile5);
                        filesName += "<a href='https://webmgt.tnpd.gov.tw/TrafficFiles/" + mailCase.Upfile5 + " '> " + Upfile5.FileName + "</a><br/>";
                    }



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

                //發送簡訊通知 (移除原有email發信，改為簡訊通知)
                string notifyMobile = "0911510916";
                string smsMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "[重大災害通報通知]請至管理平台處理，謝謝";

                string username = System.Configuration.ConfigurationManager.AppSettings["tnpdUsername"].Trim();
                string password = System.Configuration.ConfigurationManager.AppSettings["tnpdPassword"].Trim();
                tnpd.Models.SMS.SmsMain smsMain = new tnpd.Models.SMS.SmsMain(username, password);
                tnpd.Models.SMS.SmsBody smsBody = new tnpd.Models.SMS.SmsBody("臺南市政府警察局", notifyMobile, smsMessage);
                smsMain.SmsBody.Add(smsBody);
                string result = smsMain.SendWithWebRequest();

                return RedirectToAction("CreateSuccess", new { unid = id, id = mailCase.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            return View(responseViews);
        }
        private bool CheckFileSignature(byte[] fileData, string fileExtension)
        {
            if (fileData == null || fileData.Length == 0)
            {
                return false;
            }

            var ext = fileExtension.ToLower();
            if (string.IsNullOrEmpty(ext))
            {
                return false;
            }

            if (!_fileSignatures.ContainsKey(ext))
            {
                return false;
            }

            var signatures = _fileSignatures[ext];
            foreach (var signatureInfo in signatures)
            {
                if (fileData.Length >= signatureInfo.Offset + signatureInfo.Signature.Length)
                {
                    var headerBytes = fileData.Skip(signatureInfo.Offset).Take(signatureInfo.Signature.Length).ToArray();
                    if (headerBytes.SequenceEqual(signatureInfo.Signature))
                    {
                        return true;
                    }
                }
            }

            return false;
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
                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件本郵件是由系統自動寄出，請勿直接回覆此郵件。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(id, "臺南市政府警察局-網路報案  Email認證信件", mailBody);//發信
                return Content("認證郵件已送出，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            }
            return Content("Email 格式錯誤");
        }

        public ActionResult SendVerifyMailCode(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Content("Email 不可空白");
            }
            if (id.IndexOf("@vusra.com") > -1)
            {
                return Content("");
            }

            DateTime date = DateTime.Now.AddMinutes(-3);

            var caseMailCheck = _db.caseMailChecks.Where(x => x.Email == id && x.InitDate > date).OrderByDescending(x => x.Id).FirstOrDefault();
            if (caseMailCheck != null)
            {
                return Content("認證郵件已送出，請檢查信箱垃圾信件，或稍後再試");
            }

            if (Utility.IsValidEmail(id))
            {
                CaseMailCheck mailCheck = new CaseMailCheck
                {
                    CaseGuid = Guid.NewGuid().ToString(),
                    Email = id,
                    InitDate = DateTime.Now,
                    ConfirmDate = DateTime.Now.AddYears(-25),
                    IsConfirm = BooleanType.否,
                    CheckCode = GenerateRandomNumbers(6)
                };
                _db.caseMailChecks.Add(mailCheck);
                _db.SaveChanges();
                string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];

                string mailBody = "親愛的市民朋友，您好，這是臺南市政府警察局-重大災害通報專區 Email認證信件。<br/>";
                //mailBody += "<a href='" + InternetURL + "/Responses/VerifyMail/" + mailCheck.CaseGuid + "'>請點選此連結認證您的信箱</a><br/>";
                mailBody += "台端申請臺南市政府警察局重大災害通報E-mail驗證服務，請輸入E-mail驗證碼:<span style='color: blue;font-size: large'>" + mailCheck.CheckCode + "</span>";
                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件，本郵件是由系統自動寄出，請勿直接回覆此郵件。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(id, "臺南市政府警察局-重大災害通報專區  Email認證信件", mailBody);//發信
                return Content("認證郵件已送出，請至信中取得驗證碼，完成後即可回網頁填寫入驗證碼，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            }
            return Content("Email 格式錯誤");
        }

        public ActionResult SendVerifyMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return Content("手機號碼不可空白");
            }

            DateTime date = DateTime.Now.AddMinutes(-3);

            string CheckCode = GenerateRandomNumbers(6);

            CaseMobileCheck caseMobileCheck = _db.CaseMobileChecks.Where(x => x.Mobile == mobile && x.InitDate > date).OrderByDescending(x => x.Id).FirstOrDefault();
            if (caseMobileCheck != null)
            {
                return Content("請等待接收簡訊，若超過3分鐘沒收到再次發送!");
            }
            else
            {
                CaseMobileCheck newCaseMobileCheck = new CaseMobileCheck();
                newCaseMobileCheck.Mobile = mobile;
                newCaseMobileCheck.CheckCode = CheckCode;
                newCaseMobileCheck.InitDate = DateTime.Now;
                newCaseMobileCheck.DueDate = DateTime.Now.AddMinutes(3);
                _db.CaseMobileChecks.Add(newCaseMobileCheck);
                _db.SaveChanges();
            }

            string message = "臺南市政府警察局簡訊服務，請輸入簡訊驗證碼:" + CheckCode + "，如果這不是您的驗證申請，請忽略此簡訊。";

            string username = System.Configuration.ConfigurationManager.AppSettings["tnpdUsername"].Trim();
            string password = System.Configuration.ConfigurationManager.AppSettings["tnpdPassword"].Trim();
            tnpd.Models.SMS.SmsMain smsMain = new tnpd.Models.SMS.SmsMain(username, password);
            tnpd.Models.SMS.SmsBody smsBody = new tnpd.Models.SMS.SmsBody("臺南市政府警察局", mobile, message);
            smsMain.SmsBody.Add(smsBody);

            string result = smsMain.SendWithWebRequest();
            //string result = SendHinetSMS(mobile,message);
            return Content(result);

        }

        static string GenerateRandomNumbers(int length)
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < length; i++)
            {
                // 生成隨機數字並附加到結果字符串
                result += random.Next(10).ToString();
            }

            return result;
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
