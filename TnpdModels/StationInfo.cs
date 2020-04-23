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
    public class StationInfo : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "上層編號")]
        public int? ParentId { get; set; }


        [ForeignKey("ParentId")]
        public virtual StationInfo ParentStation { get; set; }

        //ForeignKey
        [Display(Name = "鄉鎮")]
        public int? TownId { get; set; }

        [ForeignKey("TownId")]
        [JsonIgnore]
        public virtual Town Town { get; set; }


        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(100)]
        [Display(Name = "單位名稱")]
        public string Subject { get; set; }

        [MaxLength(500)]
        [Display(Name = "電話")]
        public string Tel { get; set; }

        [MaxLength(100)]
        [Display(Name = "郵遞區號")]
        public string PostalCode { get; set; }


        [MaxLength(500)]
        [Display(Name = "住址")]
        public string Address { get; set; }

        [MaxLength(20)]
        [Display(Name = "twd97x座標")]
        public string Twd97X { get; set; }

        [MaxLength(20)]
        [Display(Name = "twd97y座標")]
        public string Twd97Y { get; set; }

        [Display(Name = "內容")]
        [JsonIgnore]
        public string Article { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        [Display(Name = "Serno")]
        [MaxLength(100)]
        public string Serno { get; set; }

        public int OwnWebSiteId { get; set; }

        [Display(Name = "StationInfo")]
        public virtual ICollection<StationInfo> StationInfos { get; set; }

        [Display(Name = "圖")]
        public virtual ICollection<StationInfoFIles> StationInfoFIleses { get; set; }
    }
}
