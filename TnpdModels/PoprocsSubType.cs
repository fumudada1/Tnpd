using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class PoprocsSubType : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "項目必填")]
        [Display(Name = "項目")]
        public string Subject { get; set; }

        [Display(Name = "內容")]
        public string Article { get; set; }

        public virtual ICollection<CaseFilters> CaseFilterses { get; set; }
    }
}
