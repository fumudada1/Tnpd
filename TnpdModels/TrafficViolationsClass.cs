using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class TrafficViolationsClass : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "類別名稱必填")]
        [MaxLength(200)]
        [Display(Name = "類別名稱")]
        public string Subject { get; set; }


        [Display(Name = "排序")]
        public int ListNum { get; set; }

    }
}
