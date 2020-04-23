using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TnpdModels;

namespace tnpd.Models
{
    public class VisitView
    {


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "聯絡人")]
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
        [Display(Name = "聯絡電話")]
        public string TEL { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "參觀對象")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "申請單位名稱")]
        public string OrgName { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "預定參觀人數")]
        [MaxLength(4)]
        public string pcnt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "預定參觀日期")]
        public DateTime? ODate { get; set; }

        [Display(Name = "開始時間")]
        [MaxLength(200)]
        public string STime { get; set; }

        [Display(Name = "結束時間")]
        [MaxLength(200)]
        public string ETime { get; set; }

        [Required(ErrorMessage = "備註")]
        [Display(Name = "備註")]
        public string Content { get; set; }


    }
}