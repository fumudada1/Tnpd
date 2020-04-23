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
    public class Ese:BackendBase
    {

        public Ese()
        {
            Fileses = new List<EseFile>();
            Catalogs=new List<EseCatalog>();
        }
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Serno")]
        [MaxLength(100)]
        public string Serno { get; set; }


        [Required(ErrorMessage = "標題必填")]
        [MaxLength(200)]
        [Display(Name = "標題")]
        public string Subject { get; set; }

        [MaxLength(200)]
        [Display(Name = "承辦單位")]
        public string Undertaking_Unit { get; set; }


        [Display(Name = "申請方式")]
        public string Apply_method { get; set; }

        [Display(Name = "應備證件")]
        public string NeedDoc { get; set; }

        [MaxLength(200)]
        [Display(Name = "處理流程")]
        public string Process { get; set; }

        [MaxLength(200)]
        [Display(Name = "辦理期限")]
        public string Pdate { get; set; }

        [MaxLength(200)]
        [Display(Name = "聯絡電話")]
        public string Tel { get; set; }

        [MaxLength(200)]
        [Display(Name = "聯絡傳真")]
        public string Fax { get; set; }


        [MaxLength(200)]
        [Display(Name = "承辦人")]
        public string Mail { get; set; }


        [Display(Name = "申辦項目說明")]
        public string Remark { get; set; }


        [Display(Name = "線上申辦")]
        public string Online { get; set; }

        [Display(Name = "是否上架")]
        public BooleanType Status { get; set; }


        [Display(Name = "備註事項")]
        [JsonIgnore]
        public string Memo { get; set; }

        [Display(Name = "點閱數")]
        public int Views { get; set; }

      
        [Display(Name = "附加檔案")]
        public virtual ICollection<EseFile> Fileses { get; set; }

    

        [Display(Name = "類別")]
        public virtual ICollection<EseCatalog> Catalogs { get; set; }


        public int OwnWebSiteId { get; set; }
   


    }
}
