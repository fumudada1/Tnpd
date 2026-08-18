using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class CaseMobileCheck
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

      
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "手機")]
        [MaxLength(20)]
        public string Mobile { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime InitDate { get; set; }


        [Display(Name = "驗證碼")]
        public string CheckCode { get; set; }

        [Display(Name = "有效時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DueDate { get; set; }

        // 新增欄位
        public bool IsUsed { get; set; }        // 是否已使用
        public DateTime? UsedDate { get; set; }  // 使用時間
        public string UsedIp { get; set; }       // 使用的 IP
    }
}
