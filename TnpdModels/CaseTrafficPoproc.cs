using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CaseTrafficPoproc : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "案件")]
        public int CaseId { get; set; }

        [ForeignKey("CaseId")]
        public virtual CaseTraffic Case { get; set; }


        [Display(Name = "案件日期")]
        public DateTime CaseTime { get; set; }

        //ForeignKey
        [Display(Name = "單位")]
        public int? UnitId { get; set; }


        [ForeignKey("UnitId")]
        public virtual Unit assignUnit { get; set; }

        [Display(Name = "承辦人")]
        public int? MemberId { get; set; }


        [ForeignKey("MemberId")]
        public virtual Member assignMember { get; set; }

        [Display(Name = "案件類別")]
        public CaseProcessStatus Status { get; set; }

        [Display(Name = "分派日期")]
        public DateTime? AssignDateTime { get; set; }

        [Display(Name = "分派回覆內容")]
        public string AssignMemo { get; set; }

        [Display(Name = "結案日期")]
        public DateTime? EndDateTime { get; set; }

        [Display(Name = "展期理由")]
        public string DelyMemo { get; set; }

        [Display(Name = "是否允許展期")]
        public BooleanType? delyflag { get; set; }

        [Display(Name = "展期日期")]
        public DateTime? DelyDateTime { get; set; }

        [MaxLength(200)]
        [Display(Name = "處理附件")]
        public string Upfile1 { get; set; }


        [MaxLength(200)]
        [Display(Name = "案件分類")]
        public string Type1 { get; set; }

        [MaxLength(200)]
        [Display(Name = "案件屬性")]
        public string type2 { get; set; }

        [Display(Name = "退文理由")]
        public string noplyreason { get; set; }

        //save file
        [Display(Name = "process")]
        [MaxLength(10)]
        public string process { get; set; }

        [Display(Name = "PoprocsType")]
        public int? PoprocsType { get; set; }


        //ForeignKey
        [Display(Name = "舉發原因")]
        public int? TrafficViolationsClassId { get; set; }


        [ForeignKey("TrafficViolationsClassId")]
        public virtual TrafficViolationsClass ViolationsClass { get; set; }

        //ForeignKey
        [Display(Name = "不舉發原因")]
        public int? ViolationsRejectclassId { get; set; }


        [ForeignKey("ViolationsRejectclassId")]
        public virtual TrafficViolationsRejectclass ViolationsRejectclass { get; set; }

    }
}
