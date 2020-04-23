using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TnpdModels
{
    public class NewsCatalog:BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Display(Name = "Serno")]
        [MaxLength(100)]
        public string Serno { get; set; }


        //ForeignKey
        [Display(Name = "類別")]
        public int? WebCategoryId { get; set; }

        [ForeignKey("WebCategoryId")]
        [JsonIgnore]
        public virtual WebNewsCatalog WebCatalog { get; set; }

        //ForeignKey
        [Display(Name = "分類檢索")]
        public int MetaId { get; set; }

        [ForeignKey("MetaId")]
        public virtual MetaIndex MetaIndex { get; set; }

     
        //ForeignKey
        [Display(Name = "站台")]
        public int? WebSiteId { get; set; }

        [ForeignKey("WebSiteId")]
        [JsonIgnore]
        public virtual WebSiteName WebSite { get; set; }


        [Required(ErrorMessage = "類別名稱必填")]
        [MaxLength(200)]
        [Display(Name = "類別名稱")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        [JsonIgnore]
        public int ListNum { get; set; }

        [Display(Name = "略過審核")]
        [JsonIgnore]
        public BooleanType Confirm { get; set; }

         [JsonIgnore]
        public virtual ICollection<News> Newses { get; set; }

      
    }
}