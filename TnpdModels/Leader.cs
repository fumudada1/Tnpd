using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class Leader : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "職稱")]
        public int? LeaderCatalogId { get; set; }

        [ForeignKey("LeaderCatalogId")]
        public virtual LeaderCatalog Catalog { get; set; }

        [Required(ErrorMessage = "{0} 是必填欄位。")]
        [StringLength(50, ErrorMessage = "{0} 的長度不能超過 {1} 個字元。")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "照片")]
        public string UpImage1 { get; set; }

        [Required(ErrorMessage = "{0} 是必填欄位。")]
        [StringLength(3000, ErrorMessage = "{0} 的長度不能超過 {1} 個字元。")]
        [Display(Name = "學歷")]
        public string Education { get; set; }

        [Required(ErrorMessage = "{0} 是必填欄位。")]
        [StringLength(3000, ErrorMessage = "{0} 的長度不能超過 {1} 個字元。")]
        [Display(Name = "經歷")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        public string[] getEducation()
        {
            return Education.Trim().Split('\n');
        }
        public string[] getExperience()
        {
            return Experience.Trim().Split('\n');
        }
    }
}
