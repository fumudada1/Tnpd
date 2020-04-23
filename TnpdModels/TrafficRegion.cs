using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class TrafficRegion : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "必填")]
        [MaxLength(200)]
        [Display(Name = "區域")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "單位")]
        public int? UnitId { get; set; }


        [ForeignKey("UnitId")]
        public virtual Unit assignUnit { get; set; }


        public int ListNum { get; set; }

    }
}
