using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class TrafficFrontEndViolationItem : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "必填")]
        [MaxLength(200)]
        [Display(Name = "違規事項")]
        public string Subject { get; set; }

        public int ListNum { get; set; }

        [Display(Name = "是否停用")]
        public BooleanType IsEnable { get; set; }
    }
}
