using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class TrafficSMS:BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [MaxLength(200)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        [Display(Name = "手機")]
        public string Mobile { get; set; }

        [MaxLength(10)]
        [Display(Name = "驗證碼")]
        public string CheckCode { get; set; }

        [Display(Name = "是否啟動")]
        public BooleanType IsAction { get; set; }

        [Display(Name = "GUID")]
        [MaxLength(200)]
        public string CaseGuid { get; set; }

        [Display(Name = "驗證次數")]
        public int Times { get; set; }


        public virtual ICollection<TrafficSMSCarInfo> trafficSmsCarInfos { get; set; }





    }
}
