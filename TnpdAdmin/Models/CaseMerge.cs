using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TnpdModels;

namespace TnpdAdmin.Models
{
    public class CaseMerge
    {
        [Key]
        [Display(Name = "編號")]
        public int Id { get; set; }


        [Display(Name = "案件來源")]
        public CaseType CaseType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(200)]
        [Display(Name = "案件編號")]
        public string CaseID { get; set; }


        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(200)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

        [Display(Name = "寄件者")]
        public string Name { get; set; }

        [Display(Name = "日期")]
        public DateTime? InitDate { get; set; }
    }
}