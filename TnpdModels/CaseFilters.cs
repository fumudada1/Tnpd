using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CaseFilters : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "類型")]
        public CaseFilterType FilterType { get; set; }

        [Required(ErrorMessage = "項目必填")]
        [Display(Name = "項目")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "類別")]
        public int? TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual PoprocsSubType PoprocsSubType { get; set; }

    }
}
