using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TnpdModels;

namespace FixDataBaseError
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();

            //To get tg ssdfshe location the assembly normally resides on disk or the install directory
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string recodePath = path + "record.txt";
            string chiefRecodePath = path + "chiefRecord.txt";
            string logPath = path + "log.txt";
            int rid = 486821, cid = 30809;

            MyMap myMap = new MyMap();
            if (!System.IO.Directory.Exists("p:\\"))
            {

                myMap.Connection();
            }



            if (!System.IO.File.Exists(recodePath))
            {
                System.IO.File.WriteAllText(recodePath, rid.ToString());
            }
            else
            {
                rid = Convert.ToInt32(System.IO.File.ReadAllText(recodePath));
            }
            if (!System.IO.File.Exists(chiefRecodePath))
            {
                System.IO.File.WriteAllText(chiefRecodePath, cid.ToString());
            }
            else
            {
                cid = Convert.ToInt32(System.IO.File.ReadAllText(chiefRecodePath));
            }

            string trafficFilesPath = ConfigurationManager.AppSettings["path"].ToString();
            var list = _db.CaseTraffics.Where(x => x.Id > rid).OrderBy(x => x.Id).ToList();
            var clist = _db.Cases.Where(x => x.Id > cid).OrderBy(x => x.Id).ToList();
            foreach (CaseTraffic caseTraffic in list)
            {
                rid = caseTraffic.Id;
                string savePath = @"p:\" + caseTraffic.assignUnit.Subject + "\\" + caseTraffic.InitDate.Value.ToString("yyyy-MM") + "\\" + caseTraffic.CaseID + "\\";

                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }


                if (!string.IsNullOrEmpty(caseTraffic.Upfile1))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + caseTraffic.Upfile1, savePath + caseTraffic.Upfile1);
                        Console.WriteLine("copy " + caseTraffic.Upfile1 + "to " + savePath + caseTraffic.Upfile1);
                    }
                    catch (Exception e)
                    {
                        if (e.ToString().IndexOf("已經存在") == -1)
                        {
                            Console.WriteLine(e.ToString());
                            Console.WriteLine("案件序號:" + caseTraffic.Id.ToString()+ "\n" +e.ToString());
                            System.IO.File.AppendAllText(logPath,"案件序號:" + caseTraffic.Id.ToString()+ "\n"+ e.ToString() + '\n');
                            System.IO.File.AppendAllText(logPath, "copy " + caseTraffic.Upfile1 + "to " + savePath + caseTraffic.Upfile1 + '\n');
                        }

                    }
                }

                if (!string.IsNullOrEmpty(caseTraffic.Upfile2))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + caseTraffic.Upfile2, savePath + caseTraffic.Upfile2);
                        Console.WriteLine("copy " + caseTraffic.Upfile2 + "to " + savePath + caseTraffic.Upfile2);
                    }
                    catch (Exception e)
                    {
                        if (e.ToString().IndexOf("已經存在") == -1)
                        {
                            Console.WriteLine("案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString());
                            System.IO.File.AppendAllText(logPath, "案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString() + '\n');
                            System.IO.File.AppendAllText(logPath, "copy " + caseTraffic.Upfile2 + "to " + savePath + caseTraffic.Upfile2 + '\n');
                        }
                    }

                }
                if (!string.IsNullOrEmpty(caseTraffic.Upfile3))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + caseTraffic.Upfile3, savePath + caseTraffic.Upfile3);
                        Console.WriteLine("copy " + caseTraffic.Upfile3 + "to " + savePath + caseTraffic.Upfile3);
                    }
                    catch (Exception e)
                    {
                        if (e.ToString().IndexOf("已經存在") == -1)
                        {
                            Console.WriteLine("案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString());
                            System.IO.File.AppendAllText(logPath, "案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString() + '\n');
                            System.IO.File.AppendAllText(logPath, "copy " + caseTraffic.Upfile3 + "to " + savePath + caseTraffic.Upfile3 + '\n');
                        }
                    }

                }

                if (!string.IsNullOrEmpty(caseTraffic.Upfile4))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + caseTraffic.Upfile4, savePath + caseTraffic.Upfile4);
                        Console.WriteLine("copy " + caseTraffic.Upfile4 + "to " + savePath + caseTraffic.Upfile4);
                    }
                    catch (Exception e)
                    {
                        if (e.ToString().IndexOf("已經存在") == -1)
                        {
                            Console.WriteLine("案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString());
                            System.IO.File.AppendAllText(logPath, "案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString() + '\n');
                            System.IO.File.AppendAllText(logPath, "copy " + caseTraffic.Upfile4 + "to " + savePath + caseTraffic.Upfile4 + '\n');

                        }
                    }
                }
                if (!string.IsNullOrEmpty(caseTraffic.Upfile5))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + caseTraffic.Upfile5, savePath + caseTraffic.Upfile5);
                        Console.WriteLine("copy " + caseTraffic.Upfile5 + "to " + savePath + caseTraffic.Upfile5);
                    }
                    catch (Exception e)
                    {
                        if (e.ToString().IndexOf("已經存在") == -1)
                        {
                            Console.WriteLine("案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString());
                            System.IO.File.AppendAllText(logPath, "案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString() + '\n');
                            System.IO.File.AppendAllText(logPath, "copy " + caseTraffic.Upfile5 + "to " + savePath + caseTraffic.Upfile5 + '\n');
                        }
                    }

                }

                if (!string.IsNullOrEmpty(caseTraffic.Upfile6))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + caseTraffic.Upfile6, savePath + caseTraffic.Upfile6);
                        Console.WriteLine("copy " + caseTraffic.Upfile6 + "to " + savePath + caseTraffic.Upfile6);
                    }
                    catch (Exception e)
                    {
                        if (e.ToString().IndexOf("已經存在") == -1)
                        {
                            Console.WriteLine("案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString());
                            System.IO.File.AppendAllText(logPath, "案件序號:" + caseTraffic.Id.ToString() + "\n" + e.ToString() + '\n');
                            System.IO.File.AppendAllText(logPath, "copy " + caseTraffic.Upfile6 + "to " + savePath + caseTraffic.Upfile6 + '\n');
                        }
                    }

                }

                caseTraffic.UnitFile = caseTraffic.assignUnit.Subject;
                _db.SaveChanges();

                System.IO.File.WriteAllText(recodePath, rid.ToString());
            }

            foreach (Case itemCase in clist)
            {
                cid = itemCase.Id;
                string savePath = @"p:\ChiefMailbox\" + itemCase.InitDate.Value.ToString("yyyy-MM") + "\\" + itemCase.CaseID + "\\";
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }


                if (!string.IsNullOrEmpty(itemCase.Upfile1))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + itemCase.Upfile1, savePath + itemCase.Upfile1);
                        Console.WriteLine("copy " + itemCase.Upfile1 + "to " + savePath + itemCase.Upfile1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
                    }
                }

                if (!string.IsNullOrEmpty(itemCase.Upfile2))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + itemCase.Upfile2, savePath + itemCase.Upfile2);
                        Console.WriteLine("copy " + itemCase.Upfile2 + "to " + savePath + itemCase.Upfile2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
                    }

                }
                if (!string.IsNullOrEmpty(itemCase.Upfile3))
                {
                    try
                    {
                        System.IO.File.Copy(trafficFilesPath + itemCase.Upfile3, savePath + itemCase.Upfile3);
                        Console.WriteLine("copy " + itemCase.Upfile3 + "to " + savePath + itemCase.Upfile3);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');

                    }

                }
                System.IO.File.WriteAllText(chiefRecodePath, cid.ToString());
            }






            myMap.DisConnection();




        }


    }
}
