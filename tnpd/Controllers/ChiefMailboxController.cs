using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;
using System.Configuration;
using System.IO;
using tnpd.Models.SMS;

namespace tnpd.Controllers
{
    public class ChiefMailboxController : Controller
    {
        private BackendContext _db = new BackendContext();

        private static readonly Dictionary<string, List<FileSignature>> _fileSignatures =
            new Dictionary<string, List<FileSignature>>
        {
            { ".mp4", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, 4),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x69, 0x73, 0x6F, 0x6D }, 8),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 4),
                    new FileSignature(new byte[] { 0x47, 0x40, 0x11, 0x10 }, 0),
                    new FileSignature(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0)
                }
            },
            { ".3gp", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, 4),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x69, 0x73, 0x6F, 0x6D }, 8),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 4),
                    new FileSignature(new byte[] { 0x47, 0x40, 0x11, 0x10 }, 0),
                    new FileSignature(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0)
                }
            },
            { ".avi", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, 4),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x69, 0x73, 0x6F, 0x6D }, 8),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 4),
                    new FileSignature(new byte[] { 0x47, 0x40, 0x11, 0x10 }, 0),
                    new FileSignature(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0)
                }
            },
            { ".wmv", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, 4),
                    new FileSignature(new byte[] { 0x30, 0x26, 0xB2, 0x75 }, 0),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x69, 0x73, 0x6F, 0x6D }, 8),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 4),
                    new FileSignature(new byte[] { 0x47, 0x40, 0x11, 0x10 }, 0),
                    new FileSignature(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0)
                }
            },
            { ".mov", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }, 4),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79, 0x70 }, 4),
                    new FileSignature(new byte[] { 0x69, 0x73, 0x6F, 0x6D }, 8),
                    new FileSignature(new byte[] { 0x33, 0x67, 0x70, 0x35 }, 4),
                    new FileSignature(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, 4),
                    new FileSignature(new byte[] { 0x47, 0x40, 0x11, 0x10 }, 0),
                    new FileSignature(new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0)
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
            },
            { ".gif", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x47, 0x49, 0x46, 0x38 }, 0)
                }
            },
            { ".doc", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0xD0, 0xCF, 0x11, 0xE0 }, 0)
                }
            },
            { ".docx", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x50, 0x4B, 0x03, 0x04 }, 0)
                }
            },
            { ".pdf", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x25, 0x50, 0x44, 0x46 }, 0)
                }
            }
        };
        //
        // GET: /ChiefMailbox/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();
            return View();
        }

        public ActionResult Create(Guid id, string checkRead)
        {
            string referer = "";
            //if (Request.UrlReferrer != null)
            //{
            //    referer = Request.UrlReferrer.ToString();
            //}

            //if (string.IsNullOrEmpty(referer) || !referer.Contains("ChiefMailbox"))
            //{
            //    // 拒绝访问，重定向到错误页面或显示错误信息
            //    return RedirectToAction("Index", "ChiefMailbox", new { id = id });
            //}
            ViewBag.TownId = new SelectList(_db.Towns.OrderBy(x=>x.ListNum).ToList(), "Id", "Subject");
            
            ViewBag.UnId = id.ToString();
            ChiefView chief = new ChiefView();
            return View(chief);
        }

        // POST: /Mailbox/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, ChiefView chief, string checkCode, HttpPostedFileBase Upfile1, HttpPostedFileBase Upfile2, HttpPostedFileBase Upfile3)
        {
            ViewBag.UnId = id.ToString();
            ViewBag.TownId = new SelectList(_db.Towns.OrderBy(x => x.ListNum).ToList(), "Id", "Subject");
            //驗證碼確認

            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : DateTime.Now.Millisecond.ToString();
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                Session["CheckCode"] = null;
                return View(chief);
            }
            Session["CheckCode"] = null;
            //驗證E-MAIL
            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            CaseMailCheck mailCheck = _db.caseMailChecks.FirstOrDefault(x => x.Email == chief.Email && x.InitDate >= duDateTime && x.CheckCode == chief.EmailCode);
            if (mailCheck == null)
            {
                ModelState.AddModelError("CheckCode", "E-mail驗證錯誤，請寄送認證郵件，並請至信箱接收認證郵件，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
                ViewBag.UnId = id.ToString();
                return View(chief);
            }

            // ========== 新增：檢查驗證碼是否已被使用 ==========
            if (mailCheck.IsUsed)
            {
                ModelState.AddModelError("CheckCode", "此 E-mail 驗證碼已使用過，請重新申請驗證碼。");
                ViewBag.UnId = id.ToString();
                return View(chief);
            }

            // 標記為已使用（先標記，稍後 SaveChanges 時儲存）
            mailCheck.IsUsed = true;
            mailCheck.UsedDate = DateTime.Now;
            mailCheck.UsedIp = Request.UserHostAddress;
            // =====================================================
            
            //驗證手機
            CaseMobileCheck mobileCheck = _db.CaseMobileChecks.FirstOrDefault(x => x.Mobile == chief.TEL && x.InitDate >= duDateTime && x.CheckCode == chief.SMSCode);
            if (mobileCheck == null)
            {
                ModelState.AddModelError("CheckCode", "手機驗證錯誤，請3分鐘後重新寄送驗證碼，並請至手機簡訊，取得驗證碼。");
                ViewBag.UnId = id.ToString();
                return View(chief);
            }

            // ========== 新增：檢查驗證碼是否已被使用 ==========
            if (mobileCheck.IsUsed)
            {
                ModelState.AddModelError("CheckCode", "此手機驗證碼已使用過，請重新申請驗證碼。");
                ViewBag.UnId = id.ToString();
                return View(chief);
            }

            // 標記為已使用
            mobileCheck.IsUsed = true;
            mobileCheck.UsedDate = DateTime.Now;
            mobileCheck.UsedIp = Request.UserHostAddress;
            // =====================================================


            string filesName = "";
            if (ModelState.IsValid)
            {



                Case mailCase = new Case();
                mailCase.Subject = chief.Subject;
                mailCase.Content = chief.Content;

                mailCase.CaseGuid = Guid.NewGuid().ToString();
                mailCase.Email = chief.Email;
                mailCase.Job = chief.Job;
                mailCase.Name = chief.Name;
                mailCase.TEL = chief.TEL;
                mailCase.Gender = chief.Gender;
                mailCase.WebSiteId = 1;
                mailCase.Address = chief.Address;
                mailCase.CaseType = CaseType.首長信箱;

                Holiday holiday = new Holiday();
                mailCase.Predate = holiday.GetWorkDay(DateTime.Today, 14);
                mailCase.ODate = DateTime.Today;
                mailCase.IP = Request.UserHostAddress;
                mailCase.InitDate = DateTime.Now;
                mailCase.TownId = chief.TownId;

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

                           
                            return View(chief);
                        }

                        // Save the file or process it further
                        filesName += Upfile1.FileName + "<br/>";
                        mailCase.Upfile1 = Utility.SaveTraffFile(Upfile1);
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


                            return View(chief);
                        }

                        // Save the file or process it further
                        filesName += Upfile2.FileName + "<br/>";
                        mailCase.Upfile2 = Utility.SaveTraffFile(Upfile2);
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


                            return View(chief);
                        }

                        // Save the file or process it further
                        filesName += Upfile3.FileName + "<br/>";
                        mailCase.Upfile3 = Utility.SaveTraffFile(Upfile3);
                    }

                }

                List<CaseFilters> caseFilterses = _db.CaseFilterses.ToList();
                CaseFilters filterItem = null;
                foreach (var filterse in caseFilterses)
                {
                    filterItem = filterse;
                    if (filterse.FilterType == CaseFilterType.姓名)
                    {
                        if (filterse.Subject.Equals(chief.Name.Trim()))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = chief.Name.Trim();
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
                        if (filterse.Subject.Equals(chief.Email.Trim()))
                        {
                            mailCase.IsAutoClose = BooleanType.是;
                            mailCase.FilterString = chief.Email.Trim();
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
                string mailbody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/SendCustomer.html"));
                mailbody = mailbody.Replace("{CaseID}", mailCase.CaseID);
                mailbody = mailbody.Replace("{initDate}", mailCase.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{CaseType}", mailCase.CaseType.ToString());
                mailbody = mailbody.Replace("{Name}", mailCase.Name);
                mailbody = mailbody.Replace("{Tel}", mailCase.TEL);
                mailbody = mailbody.Replace("{Predate}", mailCase.Predate.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{Address}", mailCase.Address);
                mailbody = mailbody.Replace("{Email}", mailCase.Email);
                mailbody = mailbody.Replace("{Subject}", mailCase.Subject);
                mailbody = mailbody.Replace("{Content}", Txt2Html(mailCase.Content));
                mailbody = mailbody.Replace("{Files}", filesName);

                Utility.SystemSendMail(mailCase.Email, "臺南市政府警察局-首長信箱", mailbody);

                return RedirectToAction("CreateSuccess", new { unid = id, id = mailCase.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            return View(chief);
        }
        private bool IsValidFile(HttpPostedFileBase file)
        {
            // 定義文件簽名和對應的 MIME 類型
            var fileSignatures = new Dictionary<string, string>
            {
                { "FFD8", "image/jpeg" }, // JPEG
                { "89504E47", "image/png" }, // PNG
                { "47494638", "image/gif" }, // GIF
                { "424D", "image/bmp" }, // BMP
                { "D0CF11E0", "application/msword" }, // DOC
                { "504B0304", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }, // DOCX
                { "25504446", "application/pdf" } // PDF
            };

            using (var stream = file.InputStream)
            {
                byte[] buffer = new byte[8]; // 讀取前8個字節
                stream.Read(buffer, 0, buffer.Length);
                string fileSignature = BitConverter.ToString(buffer).Replace("-", string.Empty);

                foreach (var signature in fileSignatures)
                {
                    if (fileSignature.StartsWith(signature.Key))
                    {
                        return true;
                    }
                }
            }
            return false;
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
            string exstring = "doc,docx,pdf,jpeg,jpg,png,gif,bmp,";

            string extension = upfile.FileName.Split('.')[upfile.FileName.Split('.').Length - 1].ToLower();
            if (exstring.IndexOf(extension) > -1)
            {
                return true;
            }

            return false;

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

            DateTime date=DateTime.Now.AddMinutes(-3);

            var caseMailCheck = _db.caseMailChecks.Where(x => x.Email == id &&x.InitDate>date).OrderByDescending(x=>x.Id).FirstOrDefault();
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
                    CheckCode=GenerateRandomNumbers(6)
                };
                _db.caseMailChecks.Add(mailCheck);
                _db.SaveChanges();
                string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];

                string mailBody = "親愛的市民朋友，您好，這是臺南市政府警察局-首長信箱 Email認證信件。<br/>";
                mailBody += "<a href='" + InternetURL + "/ChiefMailbox/VerifyMail/" + mailCheck.CaseGuid + "'>請點選此連結認證您的信箱</a><br/>";

                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件，本郵件是由系統自動寄出，請勿直接回覆此郵件。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(id, "臺南市政府警察局-首長信箱  Email認證信件", mailBody);//發信
                return Content("認證郵件已送出，請至信中取得驗證碼，完成後即可回網頁填寫入驗證碼，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
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

                string mailBody = "親愛的市民朋友，您好，這是臺南市政府警察局-首長信箱 Email認證信件。<br/>";
                //mailBody += "<a href='" + InternetURL + "/ChiefMailbox/VerifyMail/" + mailCheck.CaseGuid + "'>請點選此連結認證您的信箱</a><br/>";
                mailBody += "台端申請臺南市政府首長信箱E-mail驗證服務，請輸入E-mail驗證碼:<span style='color: blue;font-size: large'>" + mailCheck.CheckCode + "</span>";
                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件，如果這不是您的驗證申請，請忽略此信。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                Utility.SystemSendMail(id, "臺南市政府警察局-首長信箱  Email認證信件", mailBody);//發信
                return Content("認證郵件已送出，請至信中取得驗證碼，完成後即可回網頁填寫入驗證碼，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            }
            return Content("Email 格式錯誤");
        }

        [HttpPost]
        [tnpd.Filters.SmsGateGuard]
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
                newCaseMobileCheck.InitDate=DateTime.Now;
                newCaseMobileCheck.DueDate=DateTime.Now.AddMinutes(3);
                _db.CaseMobileChecks.Add(newCaseMobileCheck);
                _db.SaveChanges();
            }


            string message = "臺南市政府警察局簡訊服務，請輸入簡訊驗證碼:" + CheckCode + "，如果這不是您的驗證申請，請忽略此簡訊。";

            string username = ConfigurationManager.AppSettings["tnpdUsername"].Trim();
            string password = ConfigurationManager.AppSettings["tnpdPassword"].Trim();
            SmsMain smsMain = new SmsMain(username, password);
            SmsBody smsBody = new SmsBody("臺南市政府警察局", mobile, message);
            smsMain.SmsBody.Add(smsBody);

            string result = smsMain.SendWithWebRequest();
            //string result = SendHinetSMS(mobile,message);
            return Content(result);

        }

        [HttpPost]
        [tnpd.Filters.SmsGateGuard]
        public ActionResult SendVerifyMobile1911(string mobile)
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


            string message = "台端申請臺南市政府警察局簡訊服務，請輸入簡訊驗證碼:" + CheckCode + "，如果這不是您的驗證申請，請忽略此簡訊。";

            string username = ConfigurationManager.AppSettings["tnpdUsername"].Trim();
            string password = ConfigurationManager.AppSettings["tnpdPassword"].Trim();
            SmsMain smsMain = new SmsMain(username, password);
            SmsBody smsBody = new SmsBody("臺南市政府警察局簡訊服務", mobile, message);
            smsMain.SmsBody.Add(smsBody);

            string result = smsMain.SendWithWebRequest();
            //string result = SendHinetSMS(mobile, message);
            return Content(result);

        }

        [HttpPost]
        public ActionResult CheckSmsCode(string mobile, string code)
        {
            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(code))
                return Json(new { success = false, message = "請填寫手機號碼與驗證碼" });

            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            var mobileCheck = _db.CaseMobileChecks.FirstOrDefault(x =>
                x.Mobile == mobile &&
                x.InitDate >= duDateTime &&
                x.CheckCode == code);

            if (mobileCheck == null)
                return Json(new { success = false, message = "驗證碼錯誤，請重試或重新發送" });

            if (mobileCheck.IsUsed)
                return Json(new { success = false, message = "此驗證碼已使用過，請重新申請驗證碼" });

            return Json(new { success = true, message = "簡訊驗證成功" });
        }

        [HttpPost]
        public ActionResult CheckEmailCode(string email, string code)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code))
                return Json(new { success = false, message = "請填寫Email與驗證碼" });

            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            var mailCheck = _db.caseMailChecks.FirstOrDefault(x =>
                x.Email == email &&
                x.InitDate >= duDateTime &&
                x.CheckCode == code);

            if (mailCheck == null)
                return Json(new { success = false, message = "驗證碼錯誤，請重試或重新發送" });

            if (mailCheck.IsUsed)
                return Json(new { success = false, message = "此驗證碼已使用過，請重新申請驗證碼" });

            return Json(new { success = true, message = "Email驗證成功" });
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
        public ActionResult VerifyMail(string id)
        {
            CaseMailCheck mailCheck = _db.caseMailChecks.OrderByDescending(x => x.Id).FirstOrDefault(x => x.CaseGuid == id);
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
            mailCheck.ConfirmDate = DateTime.Now;
            _db.SaveChanges();
            ViewBag.message = "驗證成功，請回原頁面輸入資料";
            return View();
        }

    }
}
