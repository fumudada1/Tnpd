using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TnpdModels;

namespace tnpd.Controllers
{
    public class MailboxController : Controller
    {
        private BackendContext _db = new BackendContext();
        //
        // GET: /Mailbox/

        public ActionResult Index(Guid id)
        {
            ViewBag.UnId = id.ToString();


            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email, string CaseID, Guid id, string checkCode)
        {
            ViewBag.UnId = id.ToString();
            //驗證碼確認
            string sCheckCode = Session["CheckCode"] != null ? Session["CheckCode"].ToString().ToLower() :DateTime.Now.Millisecond.ToString() ;
            if (checkCode.ToLower() != sCheckCode)
            {
                ModelState.AddModelError("CheckCode", "驗證碼錯誤!!");
                Session["CheckCode"] = null;
                return View();
            }
            if (CaseID.IndexOf('A') == 0)
            {
                CaseTraffic traffic = _db.CaseTraffics.FirstOrDefault(x => x.CaseID == CaseID && x.Email == Email);
                if (traffic == null)
                {
                    SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = "select * from trafficCasedata where serno=@serno and Email=@Email";
                    command.Parameters.Add("serno", CaseID);
                    command.Parameters.Add("Email", Email);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        ViewBag.repost = "案件找不到";

                    }
                    else
                    {
                        DataRow dataRow = dt.Rows[0];
                        SqlCommand command1 = new SqlCommand();
                        command1.Connection = conn;
                        command1.CommandText = "select * from trafficpoproc where serno=@serno";
                        command1.Parameters.Add("serno", CaseID);
                        SqlDataAdapter da1 = new SqlDataAdapter(command1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        CaseTraffic oldCase = new CaseTraffic();
                        oldCase.CaseID = dataRow["serno"].ToString();
                        oldCase.Predate = GetDateTime(dataRow["predate"].ToString());
                        oldCase.Email = dataRow["Email"].ToString();
                        oldCase.Subject = dataRow["subject"].ToString();
                        oldCase.Content = dataRow["content"].ToString();
                        oldCase.violation_carno = dataRow["violation_carno"].ToString();
                        oldCase.violation_date =Convert.ToDateTime(dataRow["violation_date"].ToString()) ;
                        oldCase.violation_time = dataRow["violation_time"].ToString();
                        oldCase.violation_place = dataRow["violation_place"].ToString();
                        if (dt1.Rows.Count > 0)
                        {
                            DataRow poprocRow = dt1.Rows[0];
                            CaseTrafficPoproc oldCasePoproc = new CaseTrafficPoproc();
                            if (poprocRow["status"].ToString() != "3")
                            {
                                oldCasePoproc.Status = CaseProcessStatus.待辦;
                            }
                            else
                            {
                                oldCasePoproc.Status = CaseProcessStatus.結案;
                            }
                            oldCasePoproc.AssignMemo = poprocRow["ancontent"].ToString();
                            oldCase.Poprocs = new List<CaseTrafficPoproc>();
                            oldCase.Poprocs.Add(oldCasePoproc);
                            traffic = oldCase;
                        }

                    }
                    

                     ViewBag.repost = "案件找不到";
                   
                }

                ViewBag.traffic = traffic;

            }
            else
            {
                Case traffic = _db.Cases.FirstOrDefault(x => x.CaseID == CaseID && x.Email == Email);
                if (traffic == null)
                {
                    SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = "select * from Casedata where serno=@serno and Email=@Email";
                    command.Parameters.Add("serno", CaseID);
                    command.Parameters.Add("Email", Email);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        ViewBag.repost = "案件找不到";
                       
                    }
                    else
                    {
                        DataRow dataRow = dt.Rows[0];
                        SqlCommand command1 = new SqlCommand();
                        command1.Connection = conn;
                        command1.CommandText = "select * from poproc where serno=@serno";
                        command1.Parameters.Add("serno", CaseID);
                        SqlDataAdapter da1 = new SqlDataAdapter(command1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        Case oldCase=new Case();
                        oldCase.CaseID = dataRow["serno"].ToString();
                        oldCase.Predate = GetDateTime(dataRow["predate"].ToString());
                        oldCase.Email = dataRow["Email"].ToString();
                        oldCase.Subject = dataRow["subject"].ToString();
                        oldCase.Content = dataRow["content"].ToString();
                        if (dt1.Rows.Count > 0)
                        {
                            DataRow poprocRow = dt1.Rows[0];
                            CasePoproc oldCasePoproc=new CasePoproc();
                            if (poprocRow["status"].ToString() != "3")
                            {
                                oldCasePoproc.Status = CaseProcessStatus.待辦;
                            }
                            else
                            {
                                oldCasePoproc.Status = CaseProcessStatus.結案;
                            }
                            oldCasePoproc.AssignMemo = poprocRow["ancontent"].ToString();
                            oldCase.Poprocs=new List<CasePoproc>();
                            oldCase.Poprocs.Add(oldCasePoproc);
                            traffic = oldCase;
                        }

                    }
                    
                }
                ViewBag.MyCase = traffic;

            }

            return View();
        }

        static DateTime GetDateTime(string datetime)
        {
            datetime = datetime.Insert(4, "/");
            datetime = datetime.Insert(7, "/");

            DateTime d = Convert.ToDateTime(datetime);
            return d;
        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
