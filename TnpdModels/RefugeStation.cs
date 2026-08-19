using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class RefugeStation : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        [Display(Name = "區別")]
        public string DIstrict { get; set; }

        
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        [Display(Name = "里別")]
        public string Village { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "名稱")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "防空疏散避難設施地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "可容納人數")]
        public int Number { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        [Display(Name = "管轄分局")]
        public string Precinct { get; set; }

        [MaxLength(20)]
        [Display(Name = "twd97x座標")]
        public string Twd97X { get; set; }

        [MaxLength(20)]
        [Display(Name = "twd97y座標")]
        public string Twd97Y { get; set; }

        [Display(Name = "兼作停車空間")]
        public IsEnable? IsParkingLot { get; set; }

        [MaxLength(50)]
        [Display(Name = "電腦編號")]
        public string SerialNumber { get; set; }

        [MaxLength(20)]
        [Display(Name = "地下避難樓層")]
        public string UnderFlood { get; set; }

        [MaxLength(20)]
        [Display(Name = "轄區分駐(派出)所")]
        public string UnitStation { get; set; }
    }
}
