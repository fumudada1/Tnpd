using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TnpdModels
{
    public class SystemLog
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "行為")]
        [MaxLength(1000)]
        [Display(Name = "行為")]
        public string Subject { get; set; }

        [MaxLength(10)]
        [Display(Name = "單位")]
        public string Org { get; set; }

        [MaxLength(10)]
        [Display(Name = "子單位")]
        public string subOrg { get; set; }

        
        [Display(Name = "單位代號")]
        public int? Unitid { get; set; }

        
        [Display(Name = "事件")]
        public SystemLogEvent? Enent { get; set; }

       
        [Display(Name = "屬性")]
        public SystemLogAttr ? attr { get; set; }

        [MaxLength(50)]
        [Display(Name = "網路位置(IP)")]
        public string IP { get; set; }

        [MaxLength(50)]
        [Display(Name = "發布者")]
        public string Poster { get; set; }

        [Display(Name = "發布時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? InitDate { get; set; }

    }
}