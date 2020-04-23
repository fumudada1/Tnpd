using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TnpdModels
{
    public class NewsLink : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(200)]
        [Display(Name = "名稱")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "最新消息")]
        [JsonIgnore]
        public int NewId { get; set; }

        [ForeignKey("NewId")]
        [JsonIgnore]
        public virtual News News { get; set; }


        [MaxLength(500)]
        [Display(Name = "網址")]
        public string LinkUrl { get; set; }

        [Display(Name = "開啟目標")]
        public TargetType Target { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        public int WebCategoryId { get; set; }
    }
}