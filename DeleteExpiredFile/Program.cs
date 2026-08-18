using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnpdModels;

namespace DeleteExpiredFile
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();


            string path = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = path + "log.txt";
            DateTime now=DateTime.Now;
            var newsList = _db.Newses.Where(x => x.EndDate < now && (x.NewsFileses.Count > 0 || x.NewsImagses.Count > 0)).ToList();
            
            foreach (News news in newsList)
            {
                for (int i = news.NewsFileses.Count - 1; i == 0; i--)
                {
                    var item = news.NewsFileses.ElementAt(i);
                    try
                    {
                        Console.WriteLine("D:\\website\\Tnpd\\TnpdAdmin" + item.UpFile);
                        FileInfo fileInfo = new FileInfo("D:\\website\\Tnpd\\TnpdAdmin" + item.UpFile);
                        fileInfo.Delete();
                    }
                    catch (Exception e)
                    {
                       File.WriteAllText(logPath,e.Message + "\n");
                    }
                    
                    _db.NewsFileses.Remove(item);
                }
                for (int i = news.NewsImagses.Count - 1; i == 0; i--)
                {
                    var item = news.NewsImagses.ElementAt(i);
                    try
                    {
                        Console.WriteLine("D:\\website\\Tnpd\\TnpdAdmin" + item.UpFile);
                        FileInfo fileInfo = new FileInfo("D:\\website\\Tnpd\\TnpdAdmin" + item.UpFile);
                        fileInfo.Delete();
                    }
                    catch (Exception e)
                    {
                        File.WriteAllText(logPath,e.Message + "\n");
                       
                    }
                   
                    _db.NewsImages.Remove(item);
                }
            }

            _db.SaveChanges();
        }
    }
}
