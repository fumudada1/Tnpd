using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnpdModels;

namespace CopyFileToNAS
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();


            string path = AppDomain.CurrentDomain.BaseDirectory;
            string trafficRecodePath = path + "TrafficRecord.txt";
            string chiefRecodePath = path + "chiefRecord.txt";
            string logPath = path + "log.txt";
            int tid = 486821, cid = 21500;
            string trafficFilesPath = ConfigurationManager.AppSettings["SourcePath"].ToString();
            string DestinationPath = ConfigurationManager.AppSettings["DestinationPath"].ToString();
            if (!System.IO.File.Exists(trafficRecodePath))
            {
                System.IO.File.WriteAllText(trafficRecodePath, tid.ToString());
            }
            else
            {
                tid = Convert.ToInt32(System.IO.File.ReadAllText(trafficRecodePath));
            }
            if (!System.IO.File.Exists(chiefRecodePath))
            {
                System.IO.File.WriteAllText(chiefRecodePath, cid.ToString());
            }
            else
            {
                cid = Convert.ToInt32(System.IO.File.ReadAllText(chiefRecodePath));
            }
            var tlist = _db.CaseTraffics.Where(x => x.Id >= tid && x.Id <=tid+10000).OrderBy(x => x.Id).ToList();
            var clist = _db.Cases.Where(x => x.Id > cid).OrderBy(x => x.Id).ToList();
            foreach (CaseTraffic caseTraffic in tlist)
            {
                tid = caseTraffic.Id;
                string savePath = DestinationPath + caseTraffic.assignUnit.Subject + "\\" + caseTraffic.InitDate.Value.ToString("yyyy-MM") + "\\" + caseTraffic.CaseID + "\\";
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


                System.IO.File.WriteAllText(trafficRecodePath, tid.ToString());
            }


            foreach (Case itemCase in clist)
            {
                cid = itemCase.Id;
                string savePath = DestinationPath + "ChiefMailbox" + "\\" + itemCase.InitDate.Value.ToString("yyyy-MM") + "\\" + itemCase.CaseID + "\\";
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




        }



    }
}
