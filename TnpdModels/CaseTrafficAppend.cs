using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CaseTrafficAppend : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "案件")]
        public int MyCaseId { get; set; }

        [ForeignKey("MyCaseId")]
        public virtual CaseTraffic Case { get; set; }

        [MaxLength(200)]
        [Display(Name = "案件GUID")]
        public string CaseTrafficAppendGuid { get; set; }

        [MaxLength(200)]
        [Display(Name = "案件編號")]
        public string CaseID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "補正期限")]
        public DateTime deadline { get; set; }

        [Display(Name = "補正原因")]
        public string memo { get; set; }

        
        [Display(Name = "異動欄位")]
        [MaxLength(200)]
        public string Append { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "補正日期")]
        public DateTime? Append_date { get; set; }

        [Display(Name = "違規車號")]
        [MaxLength(10)]
        public string violation_carno { get; set; }

        [Display(Name = "違規區域")]
        [MaxLength(100)]
        public string violation_place_area { get; set; }

        [Display(Name = "違規路段")]
        [MaxLength(100)]
        public string violation_place_road { get; set; }

        [Display(Name = "違規地點")]
        [MaxLength(500)]
        public string violation_place { get; set; }

        [Display(Name = "違規事項")]
        [MaxLength(50)]
        public string itemno { get; set; }

        [Display(Name = "違規時間日期")]
        public DateTime? violation_date { get; set; }

       
        [MaxLength(200)]
        [Display(Name = "附件1")]
        public string Upfile1 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件2")]
        public string Upfile2 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件3")]
        public string Upfile3 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件4")]
        public string Upfile4 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件5")]
        public string Upfile5 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件6")]
        public string Upfile6 { get; set; }

        [Display(Name = "是否傳送至交大委外伺服器")]
        public bool IsSend { get; set; }


    }
}
