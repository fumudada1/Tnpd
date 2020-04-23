using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TnpdModels;

namespace tnpd.Models
{
    public class ReportView
    {
       

        [Required(ErrorMessage = "案情摘要")]
        [Display(Name = "案情摘要")]
        public string Content { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "行動電話")]
        public string TEL { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "身分證字號")]
        [MaxLength(10)]
        public string Pid { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "現居郵遞區號")]
        [MaxLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "現居地址")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "戶籍郵遞區號")]
        [MaxLength(200)]
        public string PPostalCode { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "戶籍地址")]
        [MaxLength(200)]
        public string PAddress { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "家裡電話")]
        public string HomeTEL { get; set; }


        [MaxLength(200)]
        [Display(Name = "辦公室電話")]
        public string OfficeTEL { get; set; }

        [Display(Name = "報案人類別")]
        public CaseReportType CaseReportType { get; set; }


        [Display(Name = "發生日期")]
        public string ODate { get; set; }

        [Display(Name = "發生時段")]
        [MaxLength(10)]
        public string STime1 { get; set; }

        [Display(Name = "發生時段")]
        [MaxLength(10)]
        public string STime2 { get; set; }

        [Display(Name = "發生地點")]
        [MaxLength(200)]
        public string Oplace { get; set; }


    }
}