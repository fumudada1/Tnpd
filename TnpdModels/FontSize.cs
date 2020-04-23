using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class FontSize : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "大字型")]
        public int BSize { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "中字型")]
        public int MSize { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "小字型")]
        public int SSize { get; set; }

    }
}
