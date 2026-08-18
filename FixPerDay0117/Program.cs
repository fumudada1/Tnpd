using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnpdModels;

namespace FixPerDay0117
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendContext _db = new BackendContext();
            DateTime starDateTime = new DateTime(2023, 1, 6);
            DateTime endDateTime = new DateTime(2023, 1, 17);
            List<Case> caseTraffics = _db.Cases.Where(x => x.InitDate >= starDateTime && x.InitDate <= endDateTime).ToList();
            Holiday holiday = new Holiday();
            Console.WriteLine("total:" + caseTraffics.Count);
            foreach (Case traffic in caseTraffics)
            {
                DateTime theday = new DateTime(traffic.InitDate.Value.Year, traffic.InitDate.Value.Month,
                    traffic.InitDate.Value.Day);
                traffic.Predate = holiday.GetWorkDay(theday, 14);
                Console.WriteLine(traffic.CaseID + "-->" + traffic.Predate);
               
            }

            _db.SaveChanges();

            Console.ReadKey();
        }
    }
}
