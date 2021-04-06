using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class TrafficSMSCarInfo:BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        [Display(Name = "車號")]
        public string CarNO { get; set; }

        [Display(Name = "車子類型")]
        public CarType CarType { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        [Display(Name = "車主/公司名稱")]
        public string CarOwner { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "身分證字號")]
        [MaxLength(10)]
        public string Pid { get; set; }

        [Display(Name = "同意狀況")]
        public CarAllow CarAllow { get; set; }

        [Display(Name = "審核是否通過")]
        public SMSStatus checkStatus { get; set; }

        //ForeignKey
        [Display(Name = "註冊人")]
        public int? TrafficSMSId { get; set; }

        [ForeignKey("TrafficSMSId")]

        public virtual TrafficSMS trafficSms { get; set; }
    }
}
