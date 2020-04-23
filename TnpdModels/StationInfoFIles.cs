using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class StationInfoFIles : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "名稱必填")]
        [MaxLength(200)]
        [Display(Name = "名稱")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "單位")]
        public int? StationInfoId { get; set; }

        [ForeignKey("StationInfoId")]
        public virtual StationInfo StationInfo { get; set; }


        [MaxLength(500)]
        [Display(Name = "上傳檔案")]
        public string UpFile { get; set; }


        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

    }
}