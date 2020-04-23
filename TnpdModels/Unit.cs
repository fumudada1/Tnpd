using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TnpdModels
{
    public class Unit : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "上層編號")]
        public int? ParentId { get; set; }

        [JsonIgnore]
        [ForeignKey("ParentId")]
        public virtual Unit ParentUnit { get; set; }

        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(100)]
        [Display(Name = "單位名稱")]
        public string Subject { get; set; }

        [Display(Name = "別名")]
        [MaxLength(50)]
        public string Alias { get; set; }


        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        [JsonIgnore]
        public ICollection<Member> Members { get; set; }


    }
}