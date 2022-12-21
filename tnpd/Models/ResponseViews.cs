using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    public class ResponseViews
    {
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "報案人姓名(Name)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "現居郵遞區號")]
        [MaxLength(10)]
        public string PostalCode { get; set; }
  
        [Display(Name = "現居地址Current Address")]
        [MaxLength(200)]
        public string Address { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "行動電話 Mobile Number")]
        public string TEL { get; set; }


        [MaxLength(200)]
        [Display(Name = "連絡電話(住家)Contact Number (Home)")]
        public string HomeTEL { get; set; }

        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "發生日期 Date of Incident)")]
        public DateTime? ODate { get; set; }

        [Display(Name = "發生時段 Time of Incident")]
        [MaxLength(10)]
        public string STime1 { get; set; }

        [Display(Name = "發生時段")]
        [MaxLength(10)]
        public string STime2 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "發生地點 Location")]
        [MaxLength(200)]
        public string Oplace { get; set; }

        [Required(ErrorMessage = "案情摘要")]
        [Display(Name = "案情摘要")]
        public string Content { get; set; }


     

      
    }
}