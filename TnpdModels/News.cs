using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TnpdModels
{
    public class News:BackendBase
    {

        public News()
        {
            NewsFileses = new List<NewsFiles>();
            NewsLinks=new List<NewsLink>();
            NewsImagses=new List<NewsImage>();
            NewsCatalogs=new List<NewsCatalog>();
        }
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Serno")]
        [MaxLength(100)]
        public string Serno { get; set; }


        [Required(ErrorMessage = "標題必填")]
        [MaxLength(200)]
        [Display(Name = "標題")]
        public string Subject { get; set; }


        [Display(Name = "內容")]
        [JsonIgnore]
        public string Article { get; set; }

        [Display(Name = "點閱數")]
        public int Views { get; set; }

        [Display(Name = "開始時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "結束時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "附加連結")]
        public virtual ICollection<NewsLink> NewsLinks { get; set; }

        [Display(Name = "附加檔案")]
        public virtual ICollection<NewsFiles> NewsFileses { get; set; }

        [Display(Name = "附加圖片")]
        public virtual ICollection<NewsImage> NewsImagses { get; set; }

        [Display(Name = "類別")]
        public virtual ICollection<NewsCatalog> NewsCatalogs { get; set; }

        [Display(Name = "審核日期")]
        public DateTime? AssignDateTime { get; set; }

        [Display(Name = "審核人")]
        public int? MemberId { get; set; }


        [ForeignKey("MemberId")]
        public virtual Member assignMember { get; set; }

        [Display(Name = "是否審核通過")]
        public BooleanType IsConfirm { get; set; }

        public int WebCategoryId { get; set; }

        public int OwnWebSiteId { get; set; }
    }
}