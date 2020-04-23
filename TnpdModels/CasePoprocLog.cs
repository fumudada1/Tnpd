using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CasePoprocLog : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "案件")]
        public int? CaseId { get; set; }

        [ForeignKey("CaseId")]
        public virtual Case Case { get; set; }

        //ForeignKey
        [Display(Name = "單位")]
        public int? UnitId { get; set; }


        [ForeignKey("UnitId")]
        public virtual Unit assignUnit { get; set; }

        [Display(Name = "承辦人")]
        public int? MemberId { get; set; }


        [ForeignKey("MemberId")]
        public virtual Member assignMember { get; set; }

        [MaxLength(200)]
        [Display(Name = "作業")]
        public string Action { get; set; }

        [Display(Name = "作業內容")]
        public string ActionMemo { get; set; }


    }
}
