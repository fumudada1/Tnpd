using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class NewsImage : BackendBase
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
        [Display(Name = "最新消息")]
        public int? NewId { get; set; }

        [ForeignKey("NewId")]
        public virtual News News { get; set; }


        [MaxLength(500)]
        [Display(Name = "檔案a")]
        public string UpFile { get; set; }


        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        public int WebCategoryId { get; set; }
    }
}
