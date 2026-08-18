using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace tnpd.Models.SMS
{
    public class SmsBody
    {
        private string _prefix = ConfigurationManager.AppSettings["serviceName"];
        private string _smsText;
        public SmsBody()
        {
            Uid = DateTime.Now.ToString("yyyyMMddHHmmsss");
        }

        public SmsBody(string prefix)
        {
            _prefix = prefix;
        }
        public SmsBody(string prefix,string sendMobile,string sendBody)
        {
            Uid = DateTime.Now.ToString("yyyyMMddHHmmsss");
            _prefix = prefix;
            Mobile = sendMobile;
            SmsText = sendBody;
        }

        [MaxLength(20)]
        [Display(Name = "識別碼")]
        public string Uid { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [MaxLength(10, ErrorMessage = "長度不可超過 10 碼")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "不符合手機格式")]
        [Display(Name = "手機號碼")]
        public string Mobile { get; set; }

        [MaxLength(333)]
        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "簡訊內容")]
        public string SmsText
        {
            get
            {
                return _smsText;
            }
            set
            {
                _smsText = value;
            }
        }
    }
}