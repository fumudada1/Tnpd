using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TnpdModels
{
    public class WebSiteName:BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站名稱")]
        public string Subject { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站代號")]
        public string SiteCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "部門編號")]
        public string OrgNumber { get; set; }

        [Display(Name = "內容")]
        public string XmlDoc { get; set; }

         [MaxLength(100)]
        [Display(Name = "color")]
        public string ColorName { get; set; }

         [MaxLength(200)]
         [Display(Name = "圖片")]
         public string UpImageUrl { get; set; }

        [Display(Name = "啟用")]
        public BooleanType Enable { get; set; }

        [Display(Name = "排序")]
        public int ListNum { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站英文名稱")]
        public string SubjectEn { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站path")]
        public string Path { get; set; }

        [Display(Name = "語言")]
        public LanguageType Language { get; set; }

        //ForeignKey
        [Display(Name = "管理單位")]
        public int UnitId { get; set; }


        [ForeignKey("UnitId")]
        public virtual Unit MyUnit { get; set; }

        [MaxLength(100)]
        [Display(Name = "PubUnitDN")]
        public string PubUnitDN { get; set; }

    }
}