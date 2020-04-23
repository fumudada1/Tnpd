using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TnpdModels;

namespace tnpd.Models
{
    public class ChiefView
    {
        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(200)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "主旨內容")]
        [Display(Name = "內容")]
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

        [MaxLength(200)]
        [Display(Name = "聯絡電話")]
        public string TEL { get; set; }

        [MaxLength(200)]
        [Display(Name = "職業")]
        public string Job { get; set; }

        [Display(Name = "現居地址")]
        [MaxLength(200)]
        public string Address { get; set; }

    }
}