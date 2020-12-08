using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixDataBaseError;
using TnpdModels;

namespace FixDataBaseError2
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();
            Console.WriteLine("Start...........");
            //To get tg ssdfshe location the assembly normally resides on disk or the install directory
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string recodePath = path + "record.txt";
            string logPath = path + "log.txt";
            int rid = 1;
            MyMap myMap = new MyMap();
            if (!System.IO.Directory.Exists("p:\\"))
            {

                myMap.Connection();
            }

            Console.WriteLine("Connection...........");

            if (!System.IO.File.Exists(recodePath))
            {
                System.IO.File.WriteAllText(recodePath, rid.ToString());
            }
            else
            {
                rid = Convert.ToInt32(System.IO.File.ReadAllText(recodePath));
            }
            Console.WriteLine("recodePath...........");
            string trafficFilesPath = @"Z:\TrafficFiles\";
            var list = _db.CaseTraffics.Where(x => x.Id > rid).OrderBy(x => x.Id).Take(100000).ToList();
            
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
                        Console.WriteLine(e.ToString());
                        if (e.ToString().IndexOf("存在") == -1)
                        {
                            System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        Console.WriteLine(e.ToString());
                        if (e.ToString().IndexOf("存在") == -1)
                        {
                            System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        Console.WriteLine(e.ToString());
                        if (e.ToString().IndexOf("存在") == -1)
                        {
                            System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        Console.WriteLine(e.ToString());
                        if (e.ToString().IndexOf("存在") == -1)
                        {
                            System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        Console.WriteLine(e.ToString());
                        if (e.ToString().IndexOf("存在") == -1)
                        {
                            System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        Console.WriteLine(e.ToString());
                        if (e.ToString().IndexOf("存在")==-1)
                        {
                            System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
                        }

                       
                    }

                }


                System.IO.File.WriteAllText(recodePath, rid.ToString());
            }
           
            myMap.DisConnection();




        }


    }
}
