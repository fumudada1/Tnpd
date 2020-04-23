using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class Template : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "名稱必填")]
        [MaxLength(200)]
        [Display(Name = "名稱")]
        public string Subject { get; set; }

        [MaxLength(200)]
        [Display(Name = "圖片")]
        public string UpImageUrl { get; set; }

        [Display(Name = "選用")]
        public BooleanType BeSelect { get; set; }
    }
}
