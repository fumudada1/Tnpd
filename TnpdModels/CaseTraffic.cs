using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CaseTraffic : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "案件類別")]
        public CaseType CaseType { get; set; }
        
        [MaxLength(200)]
        [Display(Name = "案件編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CaseID { get; set; }

        [MaxLength(200)]
        public string CaseGuid { get; set; }

        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(200)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

        [Display(Name = "內容")]
        public string Content { get; set; }

       

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "身分證字號")]
        [MaxLength(10)]
        public string Pid { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "聯絡電話")]
        public string TEL { get; set; }

        [MaxLength(200)]
        [Display(Name = "職業")]
        public string Job { get; set; }

        [Display(Name = "現居地址")]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(200)]
        [Display(Name = "IP")]
        public string IP { get; set; }

        //ForeignKey
        [Display(Name = "單位")]
        public int? UnitId { get; set; }


        [ForeignKey("UnitId")]
        public virtual Unit assignUnit { get; set; }

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

        [Display(Name = "違規時間日期")]
        public DateTime violation_date { get; set; }

        [Display(Name = "違規時間")]
        [MaxLength(10)]
        public string violation_time { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "預結案日")]
        public DateTime Predate { get; set; }

        [Display(Name = "違規地點")]
        [MaxLength(500)]
        public string violation_place { get; set; }

        [Display(Name = "違規車號")]
        [MaxLength(10)]
        public string violation_carno { get; set; }

        [Display(Name = "違規區域")]
        [MaxLength(100)]
        public string violation_place_area { get; set; }

        [Display(Name = "違規路段")]
        [MaxLength(100)]
        public string violation_place_road { get; set; }

        [MaxLength(50)]
        public string itemno { get; set; }

        [Display(Name = "是否單位改派")]
        public BooleanType? IsUnitAssign { get; set; }


        [Display(Name = "處理")]
        public virtual ICollection<CaseTrafficPoproc> Poprocs { get; set; }


        [Display(Name = "處理log")]
        public virtual ICollection<CaseTrafficPoprocLog> PoprocslLogs { get; set; }

        [Display(Name = "問卷")]
        public virtual ICollection<CaseTrafficqw> Casewqs { get; set; }

    }
}
