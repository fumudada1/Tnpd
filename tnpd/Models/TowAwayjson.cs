using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    public class TowAwayjson
    {
        public string car_no { get; set; }
        public string dt_drag { get; set; }
        public string no_spot { get; set; }
        public string events_name { get; set; }
        public string drag_name { get; set; }
        public string amt_poli { get; set; }


    }

    public class TowAwayjson2
    {
        public string id { get; set; }
        public string carNo { get; set; }
        public string towedLocation { get; set; }
        public string towedDate { get; set; }
        public string towedFine { get; set; }
       
    }

    public class TowAwayjson3
    {
        public string CAR_NO { get; set; }
        public string YN { get; set; }
        public string FIELDNAME { get; set; }
        public string FIELDTEL { get; set; }
        public string FIELDADDRESS { get; set; }
        public string FIELDLATI { get; set; }
        public string FIELDLONGI { get; set; }

    }
}
