using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnpd.Models.SMS
{
    public class SmsResponse
    {
        public string MsgId { get; set; }
        public string Status { get; set; }
        public string StatusMsg { get; set; }
        public string OrderNo { get; set; }
        public string PackageNo { get; set; }
        public string PackageCnt { get; set; }
        public ICollection<ErrorUids> ErrorUids { get; set; }
    }
}