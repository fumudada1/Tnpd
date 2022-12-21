using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TnpdModels;

namespace tnpd.Models
{
    public class CaseTrafficView
    {
        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(30)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

       
        [Display(Name = "內容")]
        public string Content { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "身分證字號")]
        [MaxLength(10)]
        public string Pid { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "聯絡電話")]
        public string TEL { get; set; }

        [MaxLength(200)]
        [Display(Name = "職業")]
        public string Job { get; set; }

        [Display(Name = "現居地址")]
        [MaxLength(200)]
        public string Address { get; set; }

       

        [MaxLength(200)]
        [Display(Name = "附件1")]
        public string Upfile1 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件2")]
        public string Upfile2 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件3")]
        public string Upfile3 { get; set; }

        [Display(Name = "違規時間日期")]
        public DateTime violation_date { get; set; }

        [Display(Name = "違規時間")]
        [MaxLength(10)]
        public string violation_time1 { get; set; }

        [Display(Name = "違規時間")]
        [MaxLength(10)]
        public string violation_time2 { get; set; }


        [Display(Name = "違規地點")]
        [MaxLength(500)]
        public string violation_place { get; set; }

        [Display(Name = "違規車號")]
        [MaxLength(10)]
        public string violation_carno1 { get; set; }

        [Display(Name = "違規車號")]
        [MaxLength(10)]
        public string violation_carno2 { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "違規區域")]
        public int violation_place_area { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "違規路段")]
        public int violation_place_road { get; set; }

        [MaxLength(50)]
        public string itemno { get; set; }
    }
}