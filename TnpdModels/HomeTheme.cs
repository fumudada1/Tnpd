using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class HomeTheme : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "名稱必填")]
        [MaxLength(200)]
        [Display(Name = "網站名稱")]
        public string Subject { get; set; }

        [MaxLength(200)]
        [Display(Name = "圖片")]
        public string UpImage { get; set; }

        [MaxLength(200)]
        [Display(Name = "網址")]
        public string Url { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        [Display(Name = "是否顯示")]
        public BooleanType Enable { get; set; }

        //ForeignKey
        [Display(Name = "發布單位")]
        public int WebSiteNameId { get; set; }

        [ForeignKey("WebSiteNameId")]
        public virtual WebSiteName WebSite { get; set; }
    }
}
