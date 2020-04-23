using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class WayPoint : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Display(Name = "設置縣市")]
        public string CityName { get; set; }

        [MaxLength(200)]
        [Display(Name = "設置市區鄉鎮")]
        public string RegionName { get; set; }

        [MaxLength(200)]
        [Display(Name = "設置地址")]
        public string Address { get; set; }

        [MaxLength(200)]
        [Display(Name = "管轄警局")]
        public string DeptNm { get; set; }

        [MaxLength(200)]
        [Display(Name = "管轄分局")]
        public string BranchNm { get; set; }

        [MaxLength(200)]
        [Display(Name = "經度")]
        public string Longitude { get; set; }

        [MaxLength(200)]
        [Display(Name = "緯度")]
        public string Latitude { get; set; }

        [MaxLength(200)]
        [Display(Name = "拍攝方向")]
        public string direct { get; set; }

        [MaxLength(200)]
        [Display(Name = "速限")]
        public string limit { get; set; }

    }
}
