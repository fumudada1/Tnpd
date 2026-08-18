using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TnpdTraffic.Models;
using TnpdModels;
using TnpdModels.Migrations;

namespace TnpdTraffic.Controllers
{
    public class TrafficMailboxController : Controller
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
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79 }, 4),
                    new FileSignature(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, 0),
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0)


                  
                }
            },
            { ".jpg", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0),
                    new FileSignature(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, 0),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79 }, 4),
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0)
                }
            },
            { ".png", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0),
                    new FileSignature(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, 0),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79 }, 4),
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0)
                }
            },
            { ".bmp", new List<FileSignature>
                {
                    new FileSignature(new byte[] { 0x42, 0x4D }, 0),
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0),
                    new FileSignature(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, 0),
                    new FileSignature(new byte[] { 0x66, 0x74, 0x79 }, 4),
                    new FileSignature(new byte[] { 0xFF, 0xD8, 0xFF }, 0)
                }
            }
        };
        //
        // GET: /ChiefMailbox/

        public ActionResult Index(Guid? id)
        {
            if (!id.HasValue)
            {
                return Redirect("/TrafficMailbox/Index/92929b01-cf5e-99d6-2539-bee668350a6d");
            }
            if (id.ToString() == "670dfd3b-180d-1bfc-abe8-feb9dcb131f7")
            {
                return Redirect("/TrafficMailbox/Index/92929b01-cf5e-99d6-2539-bee668350a6d");
            }
            
            ViewBag.UnId = id.ToString();
            return View();
        }

        public ActionResult Create(Guid? id, string checkRead)
        {
            if (!id.HasValue) 
            {
                return Redirect("/TrafficMailbox/Create/92929b01-cf5e-99d6-2539-bee668350a6d");
            }
            if (id.ToString() == "670dfd3b-180d-1bfc-abe8-feb9dcb131f7")
            {
                return Redirect("/TrafficMailbox/Index/92929b01-cf5e-99d6-2539-bee668350a6d");
            }

            string referer = "";
            if (Request.UrlReferrer != null)
            {
                referer = Request.UrlReferrer.ToString();
            }

            if (string.IsNullOrEmpty(referer) || !referer.Contains("TrafficMailbox"))
            {
                // 拒绝访问，重定向到错误页面或显示错误信息
                return RedirectToAction("Index", "TrafficMailbox", new { id = id });
            }
            ViewBag.UnId = id.ToString();
            CaseTrafficView caseTraffic = new CaseTrafficView();

            caseTraffic.violation_time1 = DateTime.Now.Hour.ToString();
            caseTraffic.violation_time2 = DateTime.Now.Minute.ToString();


            //ViewBag.TrafficFrontEndViolationItems = new SelectList(_db.TrafficFrontEndViolationItems.Where(x => x.IsEnable == BooleanType.是).OrderBy(p => p.ListNum), "Subject", "Subject");

            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.ListNum), "Id", "Subject");

            return View(caseTraffic);
        }

        public ActionResult Append(Guid id, string CaseGuid)
        {

            ViewBag.UnId = id.ToString();
            ViewBag.TrafficFrontEndViolationItems = new SelectList(_db.TrafficFrontEndViolationItems.Where(x => x.IsEnable == BooleanType.是).OrderBy(p => p.ListNum), "Subject", "Subject");

            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
            CaseTrafficAppend caseTrafficAppend = _db.CaseTrafficAppends.FirstOrDefault(x => x.CaseTrafficAppendGuid == CaseGuid);
            if (caseTrafficAppend == null)
            {
                ViewBag.message = "無補件資料或補件時間逾期";
                return View(caseTrafficAppend);
            }
            if (caseTrafficAppend.Append_date.HasValue)
            {
                ViewBag.message = "無補件資料或補件時間逾期";
                return View(caseTrafficAppend);
            }

            if (caseTrafficAppend.deadline.AddDays(1) < DateTime.Now)
            {
                ViewBag.message = "無補件資料或補件時間逾期";
                return View(caseTrafficAppend);
            }




            return View(caseTrafficAppend);
        }

        [HttpPost]
        public ActionResult Append(Guid UnId, CaseTrafficAppend caseTrafficAppend, int? violation_time1, int? violation_time2, int? violation_place_roadID, string checkCode, HttpPostedFileBase Upfile1, HttpPostedFileBase Upfile2, HttpPostedFileBase Upfile3, HttpPostedFileBase Upfile4, HttpPostedFileBase Upfile5, HttpPostedFileBase Upfile6)
        {
            ViewBag.UnId = UnId.ToString();
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : DateTime.Now.Millisecond.ToString();
            string filesName = "";
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                ViewBag.UnId = UnId.ToString();
                Session["CheckCode"] = null;
                CaseTrafficAppend Append = _db.CaseTrafficAppends.FirstOrDefault(x => x.CaseTrafficAppendGuid == caseTrafficAppend.CaseTrafficAppendGuid);
                ViewBag.TrafficFrontEndViolationItems = new SelectList(_db.TrafficFrontEndViolationItems.Where(x => x.IsEnable == BooleanType.是).OrderBy(p => p.ListNum), "Subject", "Subject");

                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(Append);
            }

            if (ModelState.IsValid)
            {
                string trafficFilesPath = ConfigurationManager.AppSettings["TrafficFiles"];
                string relativeAddress = "/";

                if (violation_time1.HasValue && violation_time2.HasValue)
                {
                    caseTrafficAppend.violation_date = caseTrafficAppend.violation_date.Value.AddHours(violation_time1.Value)
                                       .AddMinutes(violation_time2.Value);
                }



                TrafficRoaddata roaddata =
                    _db.TrafficRoaddatas.FirstOrDefault(x => x.Id == violation_place_roadID);
                if (roaddata != null)
                {
                    caseTrafficAppend.violation_place_area = roaddata.Region.Subject;
                    caseTrafficAppend.violation_place_road = roaddata.Subject;


                }




                if (Upfile1 != null)
                {
                    bool checkExtension = CheckExtension(Upfile1);


                    //所有都不是
                    if ((checkExtension == false) ||
                        (Upfile1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 &&
                         Upfile1.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = UnId.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(caseTrafficAppend);

                    }

                    filesName += Upfile1.FileName + "<br/>";
                    caseTrafficAppend.Upfile1 = Utility.SaveTraffFile(Upfile1);


                }


                if (Upfile2 != null)
                {

                    bool checkExtension = CheckExtension(Upfile2);


                    //所有都不是
                    if ((checkExtension == false) ||
                        (Upfile2.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 &&
                         Upfile2.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = UnId.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(caseTrafficAppend);

                    }

                    filesName += Upfile2.FileName + "<br/>";
                    caseTrafficAppend.Upfile2 = Utility.SaveTraffFile(Upfile2);

                }


                if (Upfile3 != null)
                {

                    bool checkExtension = CheckExtension(Upfile3);


                    //所有都不是
                    if ((checkExtension == false) ||
                        (Upfile3.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 &&
                         Upfile3.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = UnId.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(caseTrafficAppend);
                    }

                    filesName += Upfile3.FileName + "<br/>";
                    caseTrafficAppend.Upfile3 = Utility.SaveTraffFile(Upfile3);

                }

                if (Upfile4 != null)
                {
                    bool checkExtension = CheckExtension(Upfile4);


                    //所有都不是
                    if ((checkExtension == false) ||
                        (Upfile4.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 &&
                         Upfile4.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = UnId.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(caseTrafficAppend);
                    }

                    filesName += Upfile4.FileName + "<br/>";
                    caseTrafficAppend.Upfile4 = Utility.SaveTraffFile(Upfile4);


                }


                if (Upfile5 != null)
                {
                    bool checkExtension = CheckExtension(Upfile5);


                    //所有都不是
                    if ((checkExtension == false) ||
                        (Upfile5.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 &&
                         Upfile5.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = UnId.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(caseTrafficAppend);
                    }

                    filesName += Upfile5.FileName + "<br/>";
                    caseTrafficAppend.Upfile5 = Utility.SaveTraffFile(Upfile5);

                }


                if (Upfile6 != null)
                {

                    bool checkExtension = CheckExtension(Upfile6);


                    //所有都不是
                    if ((checkExtension == false) ||
                        (Upfile6.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1 &&
                         Upfile6.ContentType.IndexOf("video", System.StringComparison.Ordinal) == -1))
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        ViewBag.UnId = UnId.ToString();

                        ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                        return View(caseTrafficAppend);
                    }

                    filesName += Upfile6.FileName + "<br/>";
                    caseTrafficAppend.Upfile6 = Utility.SaveTraffFile(Upfile6);

                }
                caseTrafficAppend.Append_date = DateTime.Now;
                _db.Entry(caseTrafficAppend).State = EntityState.Modified;
                _db.SaveChanges();


            }
            return RedirectToAction("AppendSuccess", new { unid = UnId });
        }

        public ActionResult GetViolationItems(int id)
        {
            List<TrafficViolationItem> trafficViolationItems;
            if (id == 0)
            {
                trafficViolationItems = _db.TrafficViolationItems
                    .Where(x => x.TrafficViolationType == TrafficViolationType.平面道路 && x.IsEnable == BooleanType.是).OrderBy(x => x.ListNum).ToList();
            }
            else
            {
                trafficViolationItems = _db.TrafficViolationItems
                    .Where(x => x.TrafficViolationType == TrafficViolationType.快速道路 && x.IsEnable == BooleanType.是).OrderBy(x => x.ListNum).ToList();
            }

            var jsonContent = JsonConvert.SerializeObject(trafficViolationItems, Formatting.Indented);

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };

        }

        // POST: /Mailbox/Create
        [HttpPost]

        public ActionResult Create(Guid id, CaseTrafficView trafficView = null, string checkCode = null, HttpPostedFileBase Upfile1 = null, HttpPostedFileBase Upfile2 = null, HttpPostedFileBase Upfile3 = null, HttpPostedFileBase Upfile4 = null, HttpPostedFileBase Upfile5 = null, HttpPostedFileBase Upfile6 = null)
        {
           
            ViewBag.UnId = id.ToString();
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() : DateTime.Now.Millisecond.ToString();
            string filesName = "";
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                ViewBag.Message = "驗證碼錯誤!!";
                ViewBag.UnId = id.ToString();
                Session["CheckCode"] = null;
                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(trafficView);
            }
            Session["CheckCode"] = null;
            //身分證字號認證
            //if (!isIdentificationId(trafficView.Pid))
            //{
            //    ModelState.AddModelError("Pid", "身分證字號規則驗證錯誤，請填入正確身份證字號!!");
            //    ViewBag.UnId = id.ToString();

            //    ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
            //    return View(trafficView);
            //}
            DateTime Odate = trafficView.violation_date;
            if (DateTime.Today.AddDays(-7) > Odate)
            {
                ViewBag.Message="已逾七日之檢舉。";
                ViewBag.UnId = id.ToString();

                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(trafficView);
            }

            DateTime duDateTime = DateTime.Now.AddMinutes(-30);
            TrafficMailCheck mailCheck = _db.TrafficMailChecks.FirstOrDefault(x => x.Email == trafficView.Email && x.ConfirmDate >= duDateTime);
            if (mailCheck == null)
            {
                ViewBag.Message="E-mail驗證錯誤，請寄送認證郵件，並請至信箱接收認證郵件，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。";
                ViewBag.UnId = id.ToString();

                ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                return View(trafficView);
            }


            CaseTrafficView caseTraffic = new CaseTrafficView();
            if (ModelState.IsValid)
            {
                string trafficFilesPath = ConfigurationManager.AppSettings["TrafficFiles"];
                string relativeAddress = "/";

                CaseTraffic traffic = new CaseTraffic();

                traffic.Address = trafficView.Address;
                traffic.Subject = trafficView.Subject;
                traffic.Content = trafficView.Content;
                traffic.Email = trafficView.Email;
                traffic.Pid = trafficView.Pid;
                traffic.Job = trafficView.Job;
                traffic.Name = trafficView.Name;
                traffic.TEL = trafficView.TEL;
                traffic.itemno = trafficView.itemno;
                traffic.violation_date = trafficView.violation_date;
                traffic.violation_carno = trafficView.violation_carno1 + "-" + trafficView.violation_carno2;
                TrafficRoaddata roaddata =
                    _db.TrafficRoaddatas.FirstOrDefault(x => x.Id == trafficView.violation_place_road);
                if (roaddata != null)
                {
                    traffic.violation_place_area = roaddata.Region.Subject;
                    traffic.violation_place_road = roaddata.Subject;
                    traffic.UnitId = roaddata.Region.UnitId;
                    trafficFilesPath = trafficFilesPath + roaddata.Region.assignUnit.Subject;
                    //if (!System.IO.Directory.Exists(trafficFilesPath))
                    //{
                    //    Directory.CreateDirectory(trafficFilesPath);

                    //}

                    relativeAddress = relativeAddress + roaddata.Region.assignUnit.Subject + "/";

                }
                else
                {
                    return RedirectToAction("Index", "TrafficMailbox");
                }

                traffic.violation_place = trafficView.violation_place;
                traffic.violation_time = trafficView.violation_time1 + ":" + trafficView.violation_time2;
                traffic.Gender = trafficView.Gender;

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
                            return View(trafficView);
                        }

                        // Save the file or process it further
                        filesName += Upfile1.FileName + "<br/>";
                        traffic.Upfile1 = Utility.SaveTraffFile(Upfile1);
                    }
                    


                }


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
                            return View(trafficView);
                        }

                        // Save the file or process it further
                        filesName += Upfile2.FileName + "<br/>";
                        traffic.Upfile2 = Utility.SaveTraffFile(Upfile2);
                    }



                }


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
                            return View(trafficView);
                        }

                        // Save the file or process it further
                        filesName += Upfile3.FileName + "<br/>";
                        traffic.Upfile3 = Utility.SaveTraffFile(Upfile3);
                    }



                }

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
                            return View(trafficView);
                        }

                        // Save the file or process it further
                        filesName += Upfile4.FileName + "<br/>";
                        traffic.Upfile4 = Utility.SaveTraffFile(Upfile4);
                    }



                }


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
                            return View(trafficView);
                        }

                        // Save the file or process it further
                        filesName += Upfile5.FileName + "<br/>";
                        traffic.Upfile5 = Utility.SaveTraffFile(Upfile5);
                    }



                }


                if (Upfile6 != null && Upfile6.ContentLength > 0)
                {


                    var fileExtension = Path.GetExtension(Upfile6.FileName).ToLower();
                    using (var binaryReader = new BinaryReader(Upfile6.InputStream))
                    {
                        var fileData = binaryReader.ReadBytes(Upfile6.ContentLength);
                        bool isValid = CheckFileSignature(fileData, fileExtension);

                        if (!isValid)
                        {

                            ViewBag.Message = "系統檢查檔案格式不符合，請參閱下方上傳檔案限制，再進行上傳。";
                            ViewBag.UnId = id.ToString();

                            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
                            return View(trafficView);
                        }

                        // Save the file or process it further
                        filesName += Upfile6.FileName + "<br/>";
                        traffic.Upfile6 = Utility.SaveTraffFile(Upfile6);
                    }



                }

                traffic.CaseGuid = Guid.NewGuid().ToString();



                Holiday holiday = new Holiday();
                //traffic.Predate = holiday.GetWorkDay(DateTime.Today, 45);
                traffic.Predate = DateTime.Today.AddDays(30);

                traffic.IP = Request.UserHostAddress;
                traffic.InitDate = DateTime.Now;
                _db.CaseTraffics.Add(traffic);

                Trafficdisdata disTrafficdisdata = _db.Trafficdisdatas
                    .FirstOrDefault(x => x.assignMember.MyUnit.ParentId == traffic.UnitId && x.isAutoAssign == BooleanType.是);

                _db.SaveChanges();

                trafficFilesPath = trafficFilesPath + "\\" + traffic.CaseID;
                relativeAddress = relativeAddress + traffic.CaseID + "/";
                //if (!System.IO.Directory.Exists(trafficFilesPath))
                //{
                //    Directory.CreateDirectory(trafficFilesPath);
                //}

                //save file

                //_db.SaveChanges();

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
                string mailbody = System.IO.File.ReadAllText(Server.MapPath("/MailTemp/SendTrafficCustomer.html"));
                mailbody = mailbody.Replace("{CaseID}", traffic.CaseID);
                mailbody = mailbody.Replace("{initDate}", traffic.InitDate.Value.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{CaseType}", "交通違規舉發");
                mailbody = mailbody.Replace("{Name}", traffic.Name);
                mailbody = mailbody.Replace("{itemno}", traffic.itemno);
                mailbody = mailbody.Replace("{Tel}", traffic.TEL);
                mailbody = mailbody.Replace("{Predate}", traffic.Predate.ToString("yyyy/MM/dd"));
                mailbody = mailbody.Replace("{Address}", traffic.Address);
                mailbody = mailbody.Replace("{Email}", traffic.Email);
                mailbody = mailbody.Replace("{violation_date}", traffic.violation_date.ToString("yyyy/MM/dd" + " " + traffic.violation_time));
                mailbody = mailbody.Replace("{violation_place}", traffic.violation_place_area + traffic.violation_place_road + traffic.violation_place);
                mailbody = mailbody.Replace("{Subject}", "【車號：" + traffic.violation_carno + "】" + traffic.Subject);
                mailbody = mailbody.Replace("{Content}", Txt2Html(traffic.Content));
                mailbody = mailbody.Replace("{Files}", filesName);
                

                Utility.SystemSendMail(traffic.Email, "臺南市政府警察局-違規舉發", mailbody);//發信
                //Utility.SendGmailMail("topidea.justin@gmail.com", traffic.Email, "臺南市政府警察局-違規舉發", mailbody, "xuqoqvdvvsbwyrbl");



                return RedirectToAction("CreateSuccess", new { unid = id, id = traffic.CaseGuid });
            }

            //ViewBag.CategoryId = new SelectList(_db.MailboxCatalog.OrderBy(p => p.ListNum), "Id", "Title", mailbox.CategoryId);
            ViewBag.UnId = id.ToString();

            ViewBag.Regions = new SelectList(_db.TrafficRegions.OrderBy(p => p.InitDate), "Id", "Subject");
            return View(trafficView);
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
        public ActionResult AppendSuccess(Guid unid)
        {
            ViewBag.UnId = unid.ToString();
            return View();
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
            DateTime date = DateTime.Now.AddMinutes(-3);

            var caseMailCheck = _db.TrafficMailChecks.Where(x => x.Email == id && x.InitDate > date).OrderByDescending(x => x.Id).FirstOrDefault();
            if (caseMailCheck != null)
            {
                return Content("認證郵件已送出，請檢查信箱垃圾信件，或稍後再試");
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
            TrafficMailCheck mailCheck = _db.TrafficMailChecks.OrderByDescending(x => x.Id).FirstOrDefault(x => x.CaseGuid == id);
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

        public ActionResult SendTnpdMail(string id)
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

                string InternetURL = System.Web.Configuration.WebConfigurationManager.AppSettings["InternetURL"];

                string mailBody = "親愛的市民朋友，您好，這是臺南市政府警察局-交通違規檢舉系統Email認證信件。<br/>";
                mailBody += "本郵件是由系統自動寄出，請勿直接回覆此郵件。";

                //Utility.SendGmailMail("topidea.justin@gmail.com", id, "交通違規檢舉系統Email認證", mailBody, "xuqoqvdvvsbwyrbl");
                MailMessage mailMessage = new MailMessage("webtnpd@tnpd.gov.tw", id);
                mailMessage.Subject = "tnpdTesttest";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = mailBody;
                // SMTP Server
                SmtpClient mailSender = new SmtpClient("172.16.1.68");
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("webtnpd", "6351459@Web");
                mailSender.Credentials = basicAuthenticationInfo;
                mailSender.Send(mailMessage);
                mailMessage.Dispose();


                return Content("認證郵件已送出，請點選信中連結認證您的信箱，完成後即可繼續填寫資料，因信箱設定不同，郵件有可能會被系統歸類為垃圾郵件。");
            }
            return Content("Email 格式錯誤");
        }



    }
}
