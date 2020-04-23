using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class AboutLink : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "名稱必填")]
        [MaxLength(200)]
        [Display(Name = "名稱")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "類別")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual AboutLinkCatalog Catalog { get; set; }

        [MaxLength(200)]
        [Display(Name = "圖片")]
        public string UpImageUrl { get; set; }

        [Display(Name = "連結網址")]
        public string URL { get; set; }

        [Display(Name = "是否上架")]
        public BooleanType Status { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        
    }
}