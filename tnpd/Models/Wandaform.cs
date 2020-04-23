using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TnpdModels;

namespace tnpd.Models
{
    public class Wandaform
    {
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "聯絡人")]
        public string Name { get; set; }

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
        [Display(Name = "地址")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Display(Name = "區別")]
        [MaxLength(10)]
        public string OArea { get; set; }

        [Display(Name = "通報地點")]
        [MaxLength(200)]
        public string Oplace { get; set; }

        [Required(ErrorMessage = "通報原因")]
        [Display(Name = "通報原因")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "通報內容")]
        [Display(Name = "通報內容")]
        public string Content { get; set; }

    }
}