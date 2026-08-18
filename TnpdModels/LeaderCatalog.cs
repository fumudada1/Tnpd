using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TnpdModels
{
    public class LeaderCatalog : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "站台")]
        public int? WebSiteId { get; set; }

        [ForeignKey("WebSiteId")]
        [JsonIgnore]
        public virtual WebSiteName WebSite { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "首長職稱")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "排序")]
        [JsonIgnore]
        public int ListNum { get; set; }


        public virtual ICollection<Leader> Leaders { get; set; }
    }
}
