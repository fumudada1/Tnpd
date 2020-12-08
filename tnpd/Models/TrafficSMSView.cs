using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    public class TrafficSMSView
    {
        [Required]
        [Display(Name = "手機號碼")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "簡訊驗證碼")]
        public string SMSCode { get; set; }

        [Required]
        [Display(Name = "申請姓名")]
        public string Name { get; set; }

      
        //[DataType(DataType.Password)]
        //[Display(Name = "申請密碼")]
        //public string Password { get; set; }


        //[NotMapped] // Does not effect with your database 
        //[Compare("Password")] 
        //[Display(Name = "確認密碼")]
        //public string ConfirmPassword { get; set; }
    }
}