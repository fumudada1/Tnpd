using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayPoint
{
    class WayPoint
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
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string Address { get; set; }
        public string DeptNm { get; set; }
        public string BranchNm { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string direct { get; set; }
        public string limit { get; set; }
    }
}
