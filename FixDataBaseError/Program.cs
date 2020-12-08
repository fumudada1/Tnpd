using System;
using System.Collections.Generic;
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
            string logPath = path + "log.txt";
            int rid = 486821;
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

            string trafficFilesPath = @"C:\website\Tnpd\tnpd\TrafficFiles\";
            var list = _db.CaseTraffics.Where(x => x.Id > rid).OrderBy(x => x.Id).ToList();
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
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');

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
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
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
                        System.IO.File.AppendAllText(logPath, e.ToString() + '\n');
                    }

                }



            }
            System.IO.File.WriteAllText(recodePath, rid.ToString());
            myMap.DisConnection();

           


        }


    }
}
