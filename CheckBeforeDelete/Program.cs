using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnpdModels;
using System.IO;

namespace CheckBeforeDelete
{
    class Program
    {
        static void Main()
        {

            //目的：要在目錄下檢查 D:\website\Tnpd\tnpdadmin\TrafficFiles\ 下面的檔案是否已在Z:\backup下有備份，若無備份則不刪除

            BackendContext _db = new BackendContext();

            string path = AppDomain.CurrentDomain.BaseDirectory;
            //建立紀錄檔案放置的目錄
            string recordsPath = path + "Records";
            if (!File.Exists(recordsPath))
            {
                Directory.CreateDirectory(recordsPath);
            }
            //交大信箱 Id 紀錄
            string trafficRecodePath = recordsPath + @"\TrafficRecord.txt";
            //首長信箱 Id 紀錄
            string chiefRecodePath = recordsPath + @"\ChiefRecord.txt";
            //Id 預設值
            int tid = 1075100, cid = 31000;
            //若 Id 紀錄檔案不存在，則建立初始檔，若存在，則寫入最後執行的編號
            if (!File.Exists(trafficRecodePath))
            {
                File.WriteAllText(trafficRecodePath, tid.ToString());
            }
            else
            {
                tid = Convert.ToInt32(File.ReadAllText(trafficRecodePath));
            }
            if (!File.Exists(chiefRecodePath))
            {
                System.IO.File.WriteAllText(chiefRecodePath, cid.ToString());
            }
            else
            {
                cid = Convert.ToInt32(File.ReadAllText(chiefRecodePath));
            }
            //錯誤訊息紀錄檔
            string logPath = recordsPath + @"\Log.txt";
            //if (!File.Exists(logPath))
            //{
            //    System.IO.File.WriteAllText(logPath, "");
            //}
            //原始檔案路徑
            string sourcePath = ConfigurationManager.AppSettings["SourcePath"].ToString();
            //備份檔案路徑
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"].ToString();
            DateTime lastWeek = DateTime.Today.AddDays(-7);
            string copyFilePath = "";

            #region 交大信箱

            //要刪除的資料(上次刪除資料的下一筆 ~ 一周前資料)
            var trafficLastWeekId = _db.CaseTraffics.Where(x => x.InitDate <= lastWeek).OrderByDescending(x=>x.Id).FirstOrDefault().Id;
            Console.WriteLine("tid:"+tid+"=>trafficLastWeekId:" + trafficLastWeekId);
            var tlist = _db.CaseTraffics.Where(x => x.Id > tid && x.Id <= trafficLastWeekId).ToList();
            
            //檢查備份目錄中是否有檔案
            foreach (var caseTraffic in tlist)
            {
                tid = caseTraffic.Id;
                copyFilePath = destinationPath + "\\" + caseTraffic.assignUnit.Subject + "\\" + caseTraffic.InitDate.Value.ToString("yyyy-MM") + "\\" + caseTraffic.CaseID;
                if (Directory.Exists(copyFilePath))//檢查是否有備份該案件編號目錄
                {
                    //檢查資料欄位是否有檔案，Upfile1~Upfile6
                    //Upfile1
                    if (!string.IsNullOrEmpty(caseTraffic.Upfile1))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + caseTraffic.Upfile1))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + caseTraffic.Upfile1))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + caseTraffic.Upfile1);
                                    File.Delete(sourcePath + "\\" + caseTraffic.Upfile1);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                    

                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：交大信箱編號"+ tid+ "的 Upfile1 檔案:"+caseTraffic.Upfile1 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：交大信箱編號"+tid+"的 Upfile1 檔案:" +caseTraffic.Upfile1+ '\n');
                        }
                    }
                    //Upfile2
                    if (!string.IsNullOrEmpty(caseTraffic.Upfile2))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + caseTraffic.Upfile2))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + caseTraffic.Upfile2))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + caseTraffic.Upfile2);
                                    File.Delete(sourcePath + "\\" + caseTraffic.Upfile2);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                    
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
        
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：交大信箱編號"+ tid+ "的 Upfile2 檔案:"+caseTraffic.Upfile2 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：交大信箱編號"+tid+"的 Upfile2 檔案:" +caseTraffic.Upfile2+ '\n');
                            
                        }
                    }
                    //Upfile3
                    if (!string.IsNullOrEmpty(caseTraffic.Upfile3))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + caseTraffic.Upfile3))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + caseTraffic.Upfile3))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + caseTraffic.Upfile3);
                                    File.Delete(sourcePath + "\\" + caseTraffic.Upfile3);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：交大信箱編號"+ tid+ "的 Upfile3 檔案:"+caseTraffic.Upfile3 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：交大信箱編號"+tid+"的 Upfile3 檔案:" +caseTraffic.Upfile3+ '\n');
                        }
                    }
                    //Upfile4
                    if (!string.IsNullOrEmpty(caseTraffic.Upfile4))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + caseTraffic.Upfile4))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + caseTraffic.Upfile4))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + caseTraffic.Upfile4);
                                    File.Delete(sourcePath + "\\" + caseTraffic.Upfile4);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：交大信箱編號"+ tid+ "的 Upfile4 檔案:"+caseTraffic.Upfile4 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：交大信箱編號"+tid+"的 Upfile4 檔案:" +caseTraffic.Upfile4+ '\n');
                        }
                    }
                    //Upfile5
                    if (!string.IsNullOrEmpty(caseTraffic.Upfile5))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + caseTraffic.Upfile5))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + caseTraffic.Upfile5))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + caseTraffic.Upfile5);
                                    File.Delete(sourcePath + "\\" + caseTraffic.Upfile5);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                              File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：交大信箱編號"+ tid+ "的 Upfile5 檔案:"+caseTraffic.Upfile5 + '\n');  
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：交大信箱編號"+tid+"的 Upfile5 檔案:" +caseTraffic.Upfile5+ '\n');
                        }
                    }
                    //Upfile6
                    if (!string.IsNullOrEmpty(caseTraffic.Upfile6))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + caseTraffic.Upfile6))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + caseTraffic.Upfile6))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + caseTraffic.Upfile6);
                                    File.Delete(sourcePath + "\\" + caseTraffic.Upfile6);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：交大信箱編號"+ tid+ "的 Upfile6 檔案:"+caseTraffic.Upfile6 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：交大信箱編號"+tid+"的 Upfile6 檔案:" +caseTraffic.Upfile6+ '\n');
                        }
                    }
                }
               
                File.WriteAllText(trafficRecodePath, tid.ToString());
            }

            #endregion

            #region 首長信箱

            //要刪除的資料(上次刪除資料的下一筆 ~ 一周前資料)
            var chiefLastWeekId = _db.Cases.Where(x => x.InitDate <= lastWeek).OrderByDescending(x => x.Id).FirstOrDefault().Id;
            var clist = _db.Cases.Where(x => x.Id > cid && x.Id <= chiefLastWeekId).ToList();
            //檢查備份目錄中是否有檔案
            Console.WriteLine("cid:" + cid + "=>chiefLastWeekId:" + chiefLastWeekId);
            foreach (var item in clist)
            {
                cid = item.Id;
                copyFilePath = destinationPath + "\\"+ "ChiefMailbox" + "\\" + item.InitDate.Value.ToString("yyyy-MM") + "\\" + item.CaseID;
                if (Directory.Exists(copyFilePath))//檢查是否有備份該案件編號目錄
                {
                    //檢查資料欄位是否有檔案，Upfile1~Upfile3
                    //Upfile1
                    if (!string.IsNullOrEmpty(item.Upfile1))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + item.Upfile1))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + item.Upfile1))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + item.Upfile1);
                                    File.Delete(sourcePath + "\\" + item.Upfile1);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {

                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：首長信箱編號"+ tid+ "的 Upfile1 檔案:"+item.Upfile1 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：首長信箱編號"+tid+"的 Upfile1 檔案:" +item.Upfile1+ '\n');
                        }
                    }
                    //Upfile2
                    if (!string.IsNullOrEmpty(item.Upfile2))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + item.Upfile2))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + item.Upfile2))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + item.Upfile2);
                                    File.Delete(sourcePath + "\\" + item.Upfile2);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：首長信箱編號"+ tid+ "的 Upfile2 檔案:"+item.Upfile2 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：首長信箱編號"+tid+"的 Upfile2 檔案:" +item.Upfile2+ '\n');
                        }
                    }
                    //Upfile3
                    if (!string.IsNullOrEmpty(item.Upfile3))
                    {
                        //檢查檔案是否有備份
                        if (File.Exists(copyFilePath + "\\" + item.Upfile3))//如果有備份，則刪除原始檔案
                        {
                            if (File.Exists(sourcePath + "\\" + item.Upfile3))//檢查是否有來源檔案，若有則刪除
                            {
                                try
                                {
                                    Console.WriteLine(sourcePath + "\\" + item.Upfile3);
                                    File.Delete(sourcePath + "\\" + item.Upfile3);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_無法刪除檔案：" + e.ToString());
                                }
                            }
                            else//沒有來源檔案，記錄Log
                            {
                                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm") +"_欲刪除的檔案不存在：首長信箱編號"+ tid+ "的 Upfile3 檔案:"+item.Upfile3 + '\n');
                            }
                        }
                        else//該檔案沒有被備份到，紀錄Log
                        {
                            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy/MM/dd HH:mm")+"_檔案沒有被備份：首長信箱編號"+tid+"的 Upfile3 檔案:" +item.Upfile3+ '\n');
                        }
                    }
                   
                }
               
                File.WriteAllText(chiefRecodePath, cid.ToString());
            }

            #endregion

        }
    }
}
