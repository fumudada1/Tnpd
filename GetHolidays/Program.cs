using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HolidayJSON;
using Newtonsoft.Json;
using TnpdModels;

namespace GetHolidays
{
    class Program
    {
        static BackendContext _db = new BackendContext();
        static void Main(string[] args)
        {
            string content = System.IO.File.ReadAllText(@"E:\website\Tnpd\GetHolidays\政府行政機關辦公日曆表.csv").Replace("\"","");
            foreach (var day in content.Split('\n'))
            {
                string[] array = day.Split(',');
                DateTime date = Convert.ToDateTime(array[0]);
                if ((date.Year == 2021 || date.Year == 2020) && array[2] == "是")
                {
                    Holiday holiday = new Holiday();
                    holiday.InitDate = date;
                    
                    holiday.IsHoliday = true;
                    holiday.HolidayCategory = array[3];
                    holiday.Description = array[1]+array[4];
                    _db.Holidays.Add(holiday);
                }
            }

            
            _db.SaveChanges();

            //Holiday holiday=new Holiday();
            //DateTime sdate=new DateTime(2019,2,15);
            //BackendContext db = new BackendContext();

            //DateTime enDate = new DateTime();
            //int i = 1;
            //int j = 1;
            //var holidays = db.Holidays.ToList();
            //while (i <= 7)
            //{
            //    enDate = sdate.AddDays(j);
            //    var day = holidays.FirstOrDefault(x => x.InitDate == enDate);
            //    if (day == null)
            //    {
            //        i++;
            //        Console.WriteLine(enDate);
            //    }
            //    else
            //    {
            //        if (day.IsHoliday == false)
            //        {
            //            i++;
            //            Console.WriteLine(day.HolidayCategory + day.Description + enDate);
            //        }
            //        else
            //        {
            //            Console.WriteLine(day.HolidayCategory + day.Description + enDate);
            //        }
                    
            //    }
               
            //    j++;
            //}
           
            //Console.ReadKey();
            //Holiday sholiday=new Holiday();
            //DateTime eeDateTime = sholiday.GetWorkDay(sdate, 7);
            //Console.WriteLine(eeDateTime);
            //Console.ReadKey();

        }

        private static string GetJsonContent(string Url)
        {
            try
            {
                string targetURI = Url;
                var request = WebRequest.Create(targetURI);
                request.ContentType = "application/json; charset=utf-8";
                string text;
                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    text = sr.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }
    }


}
