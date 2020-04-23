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
    public class Village : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "鄉")]
        public int TownId { get; set; }

        [ForeignKey("TownId")]
        [JsonIgnore]
        public virtual Town Town { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "鄉鎮")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

    }
}
