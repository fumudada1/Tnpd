using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayJSON
{
    public class HolidayJSON
    {
        public string success { get; set; }
        public result result { get; set; }
    }
    public class result
    {

        public string resource_id { get; set; }
        public string limit { get; set; }
        public string total { get; set; }
        public fields[] fields { get; set; }
        public records[] records { get; set; }
    }
    public class fields
    {

        public string type { get; set; }
        public string id { get; set; }


    }
    public class records
    {
        public DateTime date { get; set; }
        public string name { get; set; }
        public string isHoliday { get; set; }
        public string holidayCategory { get; set; }
        public string description { get; set; }

    }
}
