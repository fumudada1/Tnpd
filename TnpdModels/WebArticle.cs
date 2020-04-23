using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TnpdModels
{
    public class WebArticle : BackendBase
    {
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "分類檢索")]
        public int MetaId { get; set; }

        [ForeignKey("MetaId")]
        [JsonIgnore]
        public virtual MetaIndex MetaIndex { get; set; }

        [MaxLength(100)]
        [Display(Name = "ModelID")]
        public string UnId { get; set; }

        [MaxLength(100)]
        [Display(Name = "網頁主題")]
        public string Subject { get; set; }

        [MaxLength(100)]
        [Display(Name = "網頁主題")]
        public string MemberPath { get; set; }

        [Display(Name = "內容")]
        public string Article { get; set; }

        [Display(Name = "點閱數")]
        public int Views { get; set; }

        [Display(Name = "是否啟動")]
        public BooleanType Enable { get; set; }


    }
}