using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    public class NewsSearchView
    {
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "id")]
        public Guid id { get; set; }

        [Display(Name = "關鍵字")]
        [MaxLength(50)]
        public string SearchBySubject { get; set; }

        [Display(Name = "類別")]
        public int? sClass { get; set; }

        public MvcPaging.IPagedList<TnpdModels.News> Newses;
    }
}