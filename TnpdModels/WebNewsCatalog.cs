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
    public class WebNewsCatalog : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "語言")]
        [JsonIgnore]
        public LanguageType Language { get; set; }


        [Required(ErrorMessage = "類別名稱必填")]
        [MaxLength(200)]
        [Display(Name = "類別名稱")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        [JsonIgnore]
        public int ListNum { get; set; }

        [Display(Name = "略過審核")]
        [JsonIgnore]
        public BooleanType Confirm { get; set; }

        [JsonIgnore]
        public virtual ICollection<NewsCatalog> NewsCatalogs { get; set; }
    }
}
