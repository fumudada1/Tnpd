using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class Wanda : BackendBase
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
        [Display(Name = "單位")]
        public int? StationInfoId { get; set; }

        [ForeignKey("StationInfoId")]
        public virtual StationInfo StationInfo { get; set; }

        [Required(ErrorMessage = "起點必填")]
        [MaxLength(200)]
        [Display(Name = "起點")]
        public string beginning { get; set; }

        [Required(ErrorMessage = "終點必填")]
        [MaxLength(200)]
        [Display(Name = "終點")]
        public string destination { get; set; }

        [Display(Name = "說明")]
        public string Article { get; set; }

        [Display(Name = "是否上架")]
        public BooleanType Status { get; set; }

        public int OwnWebSiteId { get; set; }

    }
}