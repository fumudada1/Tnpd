using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class WebSiteColor : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站名稱")]
        public string Subject { get; set; }

        [MaxLength(100)]
        [Display(Name = "網站代號")]
        public string SiteCode { get; set; }

        [Display(Name = "排序")]
        public int ListNum { get; set; }
    }
}
