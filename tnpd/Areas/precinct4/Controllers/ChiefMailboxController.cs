using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.precinct4.Controllers
{
    public class ChiefMailboxController : Controller
    {
        //
        // GET: /baihe/ChiefMailbox/

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
            if (checkRead != "agree")
            {
                return RedirectToAction("Index", new { id = id });
            }
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
            if (mailCheck.IsUsed)
            {
                ModelState.AddModelError("CheckCode", "此 E-mail 驗證碼已使用過，請重新申請驗證碼。");
                ViewBag.UnId = id.ToString();
                return View(chief);
            }
            mailCheck.IsUsed = true;
            mailCheck.UsedDate = DateTime.Now;
            mailCheck.UsedIp = Request.UserHostAddress;
            //驗證手機
            CaseMobileCheck mobileCheck = _db.CaseMobileChecks.FirstOrDefault(x => x.Mobile == chief.TEL && x.InitDate >= duDateTime && x.CheckCode == chief.SMSCode);
            if (mobileCheck == null)
            {
                ModelState.AddModelError("CheckCode", "手機驗證錯誤，請3分鐘後重新寄送驗證碼，並請至手機簡訊，取得驗證碼。");
                ViewBag.UnId = id.ToString();


                return View(chief);
            }
            if (mobileCheck.IsUsed)
            {
                ModelState.AddModelError("CheckCode", "此手機驗證碼已使用過，請重新申請驗證碼。");
                ViewBag.UnId = id.ToString();
                return View(chief);
            }
            mobileCheck.IsUsed = true;
            mobileCheck.UsedDate = DateTime.Now;
            mobileCheck.UsedIp = Request.UserHostAddress;
            string filesName = "";
            if (ModelState.IsValid)
            {
                string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
                var site = _db.WebSiteNames.FirstOrDefault(x => x.SiteCode == areaName);
                Case mailCase = new Case();
                mailCase.Subject = chief.Subject;
                mailCase.Content = chief.Content;

                mailCase.CaseGuid = Guid.NewGuid().ToString();
                mailCase.Email = chief.Email;
                mailCase.Job = chief.Job;
                mailCase.Name = chief.Name;
                mailCase.TEL = chief.TEL;
                mailCase.Gender = chief.Gender;
                mailCase.WebSiteId = site.Id;
                mailCase.Address = chief.Address;
                mailCase.CaseType = CaseType.分局長與大隊隊長信箱;
                //mailCase.Subject = chief.Name;
                Holiday holiday = new Holiday();
                mailCase.Predate = holiday.GetWorkDay(DateTime.Today, 14);
                mailCase.ODate = DateTime.Today;
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
                mailbody = mailbody.Replace("{InitDate}", mailCase.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{CaseType}", mailCase.CaseType.ToString());
                mailbody = mailbody.Replace("{Name}", mailCase.Name);
                mailbody = mailbody.Replace("{Tel}", mailCase.TEL);
                mailbody = mailbody.Replace("{Predate}", mailCase.Predate.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{Address}", mailCase.Address);
                mailbody = mailbody.Replace("{Email}", mailCase.Email);
                mailbody = mailbody.Replace("{Subject}", mailCase.Subject);
                mailbody = mailbody.Replace("{Content}", Txt2Html(mailCase.Content));
                //mailbody = mailbody.Replace("{Files}", "");
                mailbody = mailbody.Replace("{Files}", filesName);
                Utility.SystemSendMail(mailCase.Email, "臺南市政府警察局-分局長信箱", mailbody);

                return RedirectToAction("CreateSuccess", new { unid = id, id = mailCase.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            return View(chief);
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

    }
}