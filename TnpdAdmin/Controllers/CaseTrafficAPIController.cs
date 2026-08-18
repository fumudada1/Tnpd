using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Tnpd.Models;
using TnpdModels;

namespace TnpdAdmin.Controllers
{
    public class CaseTrafficAPIController : Controller
    {
        BackendContext _db = new BackendContext();
        //
        // GET: /CaseTrafficAPI/

        public ActionResult GetCases()
        {
            List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.IsSend == false).OrderBy(x => x.Id).Take(100).ToList();

            string JsonText = "";
            var list = caseTraffics.Select(x => new
            {
                Id = x.Id,
                InitDate = x.InitDate,
                CaseID = x.CaseID,
                Subject = x.Subject,
                Content = x.Content,
                Name = x.Name,
                Pid = x.Pid,
                Gender = x.Gender == GenderType.男 ? "男" : "女",
                Email = x.Email,
                TEL = x.TEL,
                Job = x.Job,
                Address = x.Address,
                IP = x.IP,
                assignUnit = x.assignUnit.Subject,
                Upfile1 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1,
                Upfile2 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2,
                Upfile3 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3,
                Upfile4 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4,
                Upfile5 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5,
                Upfile6 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6,
                Upfile7 = "",
                Upfile8 = "",
                Upfile9 = "",
                Upfile10 = "",
                Upfile11 = "",
                Upfile12 = "",
                violation_date = x.violation_date,
                violation_time = x.violation_time,
                Predate = x.Predate,
                violation_place = x.violation_place,
                violation_carno = x.violation_carno,
                violation_place_area = x.violation_place_area,
                violation_place_road = x.violation_place_road,
                itemno = x.itemno,
                checkName = checkMyName(x.Pid),
                //分派 = new
                //{
                //    CaseTime = x.Poprocs.Count == 0 ? x.InitDate : x.Poprocs.FirstOrDefault().CaseTime,
                //    assignUnit = x.Poprocs.Count == 0 ? "" : x.Poprocs.FirstOrDefault().assignUnit.ParentUnit.Subject + " " + x.Poprocs.FirstOrDefault().assignUnit.Subject,
                //    assignMember = x.Poprocs.Count == 0 ? "" : x.Poprocs.FirstOrDefault().assignMember.Name,

                //}

            });
            string data = JsonConvert.SerializeObject(list);
            JsonText = Encrypt3DES(data);
            foreach (CaseTraffic item in caseTraffics)
            {


                item.IsSend = true;

            }
            _db.SaveChanges();


            return Content(JsonText, "application/json");
        }

        public ActionResult GetChtCases(DateTime date)
        {
            DateTime tomorrow = date.AddDays(1);
            List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.InitDate > date && x.InitDate <= tomorrow).OrderBy(x => x.Id).ToList();

            string JsonText = "";
            var list = caseTraffics.Select(x => new
            {
                案號 = x.CaseID,
                日期 = x.InitDate,
                車號 = x.violation_carno,
                檢舉項目 = x.itemno,
                Upfile1 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1,
                Upfile2 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2,
                Upfile3 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3,
                Upfile4 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4,
                Upfile5 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5,
                Upfile6 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6
            });
            string data = JsonConvert.SerializeObject(list);

            return Content(data, "application/json");
        }

        public ActionResult GetCasesById(string id)
        {
            List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.CaseID == id.Trim()).ToList();

            string JsonText = "";
            var list = caseTraffics.Select(x => new
            {
                Id = x.Id,
                InitDate = x.InitDate,
                CaseID = x.CaseID,
                Subject = x.Subject,
                Content = x.Content,
                Name = x.Name,
                Pid = x.Pid,
                Gender = x.Gender == GenderType.男 ? "男" : "女",
                Email = x.Email,
                TEL = x.TEL,
                Job = x.Job,
                Address = x.Address,
                IP = x.IP,
                assignUnit = x.assignUnit.Subject,
                Upfile1 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1,
                Upfile2 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2,
                Upfile3 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3,
                Upfile4 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4,
                Upfile5 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5,
                Upfile6 = "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.assignUnit.Subject + "/" + x.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6,
                Upfile7 = "",
                Upfile8 = "",
                Upfile9 = "",
                Upfile10 = "",
                Upfile11 = "",
                Upfile12 = "",
                violation_date = x.violation_date,
                violation_time = x.violation_time,
                Predate = x.Predate,
                violation_place = x.violation_place,
                violation_carno = x.violation_carno,
                violation_place_area = x.violation_place_area,
                violation_place_road = x.violation_place_road,
                itemno = x.itemno,
                checkName = checkMyName(x.Pid),
                分派 = new
                {
                    CaseTime = x.Poprocs.Count == 0 ? x.InitDate : x.Poprocs.FirstOrDefault().CaseTime,
                    assignUnit = x.Poprocs.Count == 0 ? "" : x.Poprocs.FirstOrDefault().assignUnit.ParentUnit.Subject + " " + x.Poprocs.FirstOrDefault().assignUnit.Subject,
                    assignMember = x.Poprocs.Count == 0 ? "" : x.Poprocs.FirstOrDefault().assignMember.Name,

                }

            });
            string data = JsonConvert.SerializeObject(list);
            JsonText = Encrypt3DES(data);




            return Content(JsonText, "application/json");
        }

        public ActionResult GetCaseIDbyDate(DateTime checkDate)
        {

            DateTime tomorrow = checkDate.AddDays(1);
            List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.InitDate >= checkDate && x.InitDate <= tomorrow).OrderBy(x => x.Id).ToList();

            string JsonText = "";
            var list = caseTraffics.Select(x => new
            {
                x.CaseID

            });
            string data = JsonConvert.SerializeObject(list);
            JsonText = Encrypt3DES(data);
            foreach (CaseTraffic item in caseTraffics)
            {


                item.IsSend = true;

            }
            _db.SaveChanges();


            return Content(JsonText, "application/json");
        }

        public ActionResult GetCasesAppend()
        {
            string data = "";
            string JsonText = "";
            //傳送補件資訊
            List<CaseTrafficAppend> caseTrafficsAppends = _db.CaseTrafficAppends.Where(x => x.IsSend == false && x.Append_date != null).Take(50).ToList();
            if (caseTrafficsAppends.Count > 0)
            {

                var Appendlist = caseTrafficsAppends.Select(x => new
                {
                    Id = x.Id,
                    InitDate = x.InitDate,
                    CaseID = x.CaseID,
                    CaseTrafficAppendGuid = x.CaseTrafficAppendGuid,
                    Memo = x.memo,
                    Append = x.Append,
                    Deadline = x.deadline,

                    Append_date = x.Append_date,

                    Upfile1 = x.Upfile1 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1,
                    Upfile2 = x.Upfile2 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2,
                    Upfile3 = x.Upfile3 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3,
                    Upfile4 = x.Upfile4 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4,
                    Upfile5 = x.Upfile5 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5,
                    Upfile6 = x.Upfile6 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6,

                    violation_date = x.violation_date,
                    violation_place = x.violation_place,
                    violation_carno = x.violation_carno,
                    violation_place_area = x.violation_place_area,
                    violation_place_road = x.violation_place_road,
                    itemno = x.itemno,


                });

                data = JsonConvert.SerializeObject(Appendlist);
                JsonText = Encrypt3DES(data);


                foreach (CaseTrafficAppend item in caseTrafficsAppends)
                {


                    item.IsSend = true;

                }
                _db.SaveChanges();
            }
            return Content(JsonText, "application/json");
        }

        public ActionResult GetCasesAppendById(string id)
        {
            string data = "";
            string JsonText = "";
            //傳送補件資訊
            List<CaseTrafficAppend> caseTrafficsAppends = _db.CaseTrafficAppends.Where(x => x.CaseID == id).ToList();
            if (caseTrafficsAppends.Count > 0)
            {

                var Appendlist = caseTrafficsAppends.Select(x => new
                {
                    Id = x.Id,
                    InitDate = x.InitDate,
                    CaseID = x.CaseID,
                    CaseTrafficAppendGuid = x.CaseTrafficAppendGuid,
                    Memo = x.memo,
                    Append = x.Append,
                    Deadline = x.deadline,

                    Append_date = x.Append_date,

                    Upfile1 = x.Upfile1 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile1,
                    Upfile2 = x.Upfile2 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile2 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2+ " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile2,
                    Upfile3 = x.Upfile3 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile3 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3+ " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile3,
                    Upfile4 = x.Upfile4 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile4 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4+ " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile4,
                    Upfile5 = x.Upfile5 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile5 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5+ " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile5,
                    Upfile6 = x.Upfile6 == null ? "" : "https://trmgt.tnpd.gov.tw/TrafficFiles/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles2/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6 + " ,https://trmgt.tnpd.gov.tw/TrafficFiles3/" + x.Case.assignUnit.Subject + "/" + x.Case.InitDate.Value.ToString("yyyy-MM") + "/" + x.CaseID + "/" + x.Upfile6,

                    violation_date = x.violation_date,
                    violation_place = x.violation_place,
                    violation_carno = x.violation_carno,
                    violation_place_area = x.violation_place_area,
                    violation_place_road = x.violation_place_road,
                    itemno = x.itemno,


                });

                data = JsonConvert.SerializeObject(Appendlist);
                JsonText = Encrypt3DES(data);

                _db.SaveChanges();
            }
            return Content(JsonText, "application/json");
        }

        public ActionResult ResetSendCaseById(string CaseId)
        {
            CaseTraffic caseTraffic = _db.CaseTraffics.Where(x => x.CaseID == CaseId).FirstOrDefault();
            if (caseTraffic == null)
            {
                return Content("無此案號");
            }
            else
            {
                string sql = "update CaseTraffics set IsSend=0 where id>=" + caseTraffic.Id;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TnpdConnection"].ToString());
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                return Content("重設成功");
            }


        }
        public ActionResult ResetSendCaseAppendById(string CaseId)
        {
            CaseTrafficAppend caseTrafficAppend =
                _db.CaseTrafficAppends.Where(x => x.CaseID == CaseId).OrderBy(x => x.Id).FirstOrDefault();
            if (caseTrafficAppend == null)
            {
                return Content("無此案件");
            }
            else
            {
                string sql = "update CaseTrafficAppends set IsSend=0 where id>=" + caseTrafficAppend.Id;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TnpdConnection"].ToString());
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                return Content("重設成功");
            }


        }

        public ActionResult GetUnProcessedCasesCount()
        {
            int count = _db.CaseTraffics.Where(x => x.IsSend == false).Count();

            return Content(count.ToString());
        }

        public ActionResult GetUnProcessedCaseAppendsCount()
        {
            int count = _db.CaseTrafficAppends.Where(x => x.IsSend == false).Count();

            return Content(count.ToString());
        }

        //private static string checkMyName(string pid)
        //{


        //    return pid;
        //}
        private static string checkMyName(string pid)
        {
            if (pid.Length == 10)
            {
                string url = "http://10.128.0.41:2080/Soap/checkId?id=" + pid.ToUpper();
                string result = GetJsonContent(url);
                return result;
            }



            return "身分證格式錯誤";
        }

        private static string GetJsonContent(string Url)
        {
            try
            {
                var uri = new Uri(Url);
                var request = WebRequest.Create(Url) as HttpWebRequest;

                System.Net.WebClient wc = new WebClient();
                //REF: https://stackoverflow.com/a/39534068/288936
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                    SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                string res = wc.DownloadString(Url);

                // If required by the server, set the credentials.

                request.UserAgent = "PostmanRuntime/7.26.5";
                request.Accept = "*/*";

                request.Credentials = CredentialCache.DefaultCredentials;



                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                // 重點是修改這行

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;



                // Get the response.


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();




                // Get the stream containing content returned by the server.

                Stream dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.

                StreamReader reader = new StreamReader(dataStream);

                // Read the content.

                string responseFromServer = reader.ReadToEnd();

                // Display the content.


                // Cleanup the streams and the response.

                reader.Close();

                dataStream.Close();

                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                return "";
            }



        }



        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {

            return true;

        }

        /// <summary>
        /// 使用3DES加密
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strKey">長度為24字元</param>
        /// <returns></returns>
        public static string Encrypt3DES(string strSource, string strKey = "!QAZ!QAZ4rfv3edc")
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = UTF8Encoding.UTF8.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5").Substring(0, 24));
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            byte[] Buffer = UTF8Encoding.UTF8.GetBytes(strSource);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 使用3DES解密
        /// </summary>
        /// <param name="strEncryptData"></param>
        /// <param name="strKey">長度為24字元/param>
        /// <returns></returns>
        public static string Decrypt3DES(string strEncryptData, string strKey = "!QAZ!QAZ4rfv3edc")
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = UTF8Encoding.UTF8.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strKey, "md5").Substring(0, 24));
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = "";
            byte[] Buffer = Convert.FromBase64String(strEncryptData);
            result = UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return result;
        }
    }
}
