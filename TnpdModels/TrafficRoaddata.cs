using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class TrafficRoaddata : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "區域")]
        public int? RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual TrafficRegion Region { get; set; }

        [Required(ErrorMessage = "必填")]
        [MaxLength(200)]
        [Display(Name = "路")]
        public string Subject { get; set; }

        public int ListNum { get; set; }
    }
}
