using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TnpdModels
{
    public class SysMenu : BackendBase
    {
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "系統名稱")]
        public string Subject { get; set; }

        [MaxLength(40)]
        [Display(Name = "系統編號")]
        public string Url { get; set; }

        [MaxLength(40)]
        [Display(Name = "值")]
        public string Value { get; set; }

        [Display(Name = "排序")]
        public int ListNum { get; set; }

    }
}