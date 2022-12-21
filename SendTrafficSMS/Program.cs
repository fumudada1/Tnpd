using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnpdModels;

namespace SendTrafficSMS
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();

            //To get tg ssdfshe location the assembly normally resides on disk or the install directory
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string recodePath = path + "record.txt";
            string logPath = path + "log.txt";
            int rid = 1052879;

            if (!System.IO.File.Exists(recodePath))
            {
                System.IO.File.WriteAllText(recodePath, rid.ToString());
            }
            else
            {
                rid = Convert.ToInt32(System.IO.File.ReadAllText(recodePath));
            }
            List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.Id > rid && x.Id != 1052890).OrderBy(x => x.Id).ToList();
            //List<CaseTraffic> caseTraffics = _db.CaseTraffics.Where(x => x.Id == rid).OrderBy(x => x.Id).ToList();
            List<TrafficSMSCarInfo> carInfos = _db.trafficSmsCarInfos.ToList();

            foreach (CaseTraffic caseTraffic in caseTraffics)
            {
                rid = caseTraffic.Id;
                TrafficSMSCarInfo info = carInfos.FirstOrDefault(x => x.CarNO == caseTraffic.violation_carno);
                try
                {
                    
                    if (info != null)
                    {
                        DateTime vDateTime = caseTraffic.violation_date;
                        //string Message = "台端申請臺南市政府警察局交通違規簡訊服務，於" + caseTraffic.InitDate.Value.ToString("yyyy/MM/dd") + "車號：" + caseTraffic.violation_carno + "，遭民眾由本局交通違規檢舉系統檢舉!";
                        string Message = "臺南市政府警察局通知：" + caseTraffic.violation_carno + "於" + vDateTime.Day + "日在" + caseTraffic.violation_place_area + "違規遭檢舉。是否舉發，以本局送達之違規通知單為主。敬請遵守交通規則，以維行車安全。";
                        SendHinetSMS(info.trafficSms.Mobile, Message);
                        Console.WriteLine(info.trafficSms.Mobile + " " + caseTraffic.violation_carno +"已發送");
                        System.IO.File.AppendAllText(logPath, info.trafficSms.Mobile + " " + caseTraffic.violation_carno + "已發送 " + DateTime.Now.ToString() + '\n');
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    System.IO.File.AppendAllText(logPath, info.trafficSms.Mobile + " " + caseTraffic.violation_carno + "已發送錯誤,時間:" + DateTime.Now.ToString() + '\n');
                    System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
                }




            }
            System.IO.File.WriteAllText(recodePath, rid.ToString());



            //Console.ReadKey();




        }

        private static string SendHinetSMS(string mobile, string message)
        {
            string UserID = ConfigurationManager.AppSettings["SMSAccount"].ToString();
            string Passwd = ConfigurationManager.AppSettings["MailPassword"].ToString();

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
                    return mobile+"已經發送，請留意簡訊訊息!";
                }
                hiair.EndCon();
                Console.WriteLine(retCode + " : " + retContent);
                return retContent;
                

            }
            else
            {
                hiair.EndCon();
                Console.WriteLine(retCode + " : " + retContent);
                return retContent;
                //登入失敗
               
            }


        }

        private static string SendSMS(string mobile, string message)
        {
            string ServerIp = "api.hiair.hinet.net";
            string ServerPort = "8000";
            string UserID = ConfigurationManager.AppSettings["SMSAccount"].ToString();
            string Passwd = ConfigurationManager.AppSettings["MailPassword"].ToString();
            int ret_code;
            string ret_description;

            System.Type objType = System.Type.GetTypeFromProgID("HiAir.HiNetSMS");

            dynamic objSMS = System.Activator.CreateInstance(objType);
            ret_code = objSMS.StartCon(ServerIp, ServerPort, UserID, Passwd);
            if (ret_code == 0)
            {


                ret_code = objSMS.SendMsg(mobile, message);
                ret_description = objSMS.Get_Message();
                //  Interaction.MsgBox(ret_description);
            }
            else
            {
                ret_description = objSMS.Get_Message();

            }

            objSMS.EndCon();
            return ret_description;
        }
    }
}
