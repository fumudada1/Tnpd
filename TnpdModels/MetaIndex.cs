using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TnpdModels
{
    public class MetaIndex:BackendBase
    {
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站名稱")]
        public string Subject { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站代號")]
        public string SiteCode { get; set; }

        [Display(Name = "主題分類")]
        public string Theme { get; set; }

        [Display(Name = "主題分類名稱")]
        public string ThemeName { get; set; }

        [Display(Name = "施政分類")]
        public string Cake { get; set; }

        [Display(Name = "施政分類名稱")]
        public string CakeName { get; set; }

        [Display(Name = "服務分類")]
        public string Service { get; set; }

        [Display(Name = "服務分類名稱")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }
    }
}