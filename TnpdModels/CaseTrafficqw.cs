using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CaseTrafficqw : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "案件")]
        public int? CaseId { get; set; }

        [ForeignKey("CaseId")]
        public virtual CaseTraffic Case { get; set; }

        [Display(Name = "您對處理本案回復內容及結案的滿意程度?")]
        public int? Q1 { get; set; }

        [Display(Name = "您對處理本案回復內容及結案的滿意程度?")]
        public int? Q2 { get; set; }

        [Display(Name = "您對處理本案回復內容及結案的滿意程度?")]
        public int? Q3 { get; set; }

        [MaxLength(200)]
        [Display(Name = "IP")]
        public string IP { get; set; }


    }
}
