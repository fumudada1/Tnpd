using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class Case : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Display(Name = "案件來源")]
        public CaseType CaseType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(200)]
        [Display(Name = "案件編號")]
        public string CaseID { get; set; }

        [MaxLength(200)]
        public string CaseGuid { get; set; }

        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(200)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

        [Display(Name = "內容")]
        public string Content { get; set; }

       

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "寄件者")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "聯絡電話")]
        public string TEL { get; set; }

        [MaxLength(200)]
        [Display(Name = "職業")]
        public string Job { get; set; }

        [MaxLength(200)]
        [Display(Name = "IP")]
        public string IP { get; set; }

        //ForeignKey
        [Display(Name = "站台")]
        public int? WebSiteId { get; set; }

        [ForeignKey("WebSiteId")]
        public virtual WebSiteName WebSite { get; set; }

        [Display(Name = "預結案日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Predate { get; set; }

        //[MaxLength(200)]
        //[Display(Name = "file1")]
        //public string Upfile1 { get; set; }

        //[MaxLength(200)]
        //[Display(Name = "file2")]
        //public string Upfile2 { get; set; }

        //[MaxLength(200)]
        //[Display(Name = "file3")]
        //public string Upfile3 { get; set; }


        //參觀本局暨所屬機關
        [MaxLength(200)]
        [Display(Name = "參觀對象")]
        public string Unit{ get; set; }


        [MaxLength(200)]
        [Display(Name = "申請單位名稱")]
        public string OrgName { get; set; }

        [Display(Name = "預定參觀人數")]
        [MaxLength(4)]
        public string pcnt { get; set; }


        [Display(Name = "預定參觀日期")]
        public DateTime? ODate { get; set; }

        [Display(Name = "開始時間")]
        [MaxLength(200)]
        public string STime { get; set; }

        [Display(Name = "結束時間")]
        [MaxLength(200)]
        public string ETime { get; set; }

     


        //網路報案

        [Display(Name = "身分證字號")]
        [MaxLength(10)]
        public string Pid { get; set; }

        [Display(Name = "現居郵遞區號")]
        [MaxLength(200)]
        public string PostalCode { get; set; }

        [Display(Name = "現居地址")]
        [MaxLength(200)]
        public string Address { get; set; }


        [Display(Name = "戶籍郵遞區號")]
        [MaxLength(200)]
        public string PPostalCode { get; set; }


        [Display(Name = "戶籍地址")]
        [MaxLength(200)]
        public string PAddress { get; set; }

        [MaxLength(200)]
        [Display(Name = "家裡電話")]
        public string HomeTEL { get; set; }


        [MaxLength(200)]
        [Display(Name = "辦公室電話")]
        public string OfficeTEL { get; set; }

        [Display(Name = "報案人類別")]
        public CaseReportType CaseReportType { get; set; }

        [Display(Name = "發生地點")]
        [MaxLength(200)]
        public string Oplace { get; set; }

        [Display(Name = "是否單位改派")]
        public BooleanType? IsUnitAssign { get; set; }

        //近半年婦幼安全警示地點通報區
        [Display(Name = "區別")]
        [MaxLength(10)]
        public string OArea { get; set; }

        [Display(Name = "處理")]
        public virtual ICollection<CasePoproc> Poprocs { get; set; }


        [Display(Name = "處理log")]
        public virtual ICollection<CasePoprocLog> PoprocslLogs { get; set; }

        [Display(Name = "問卷")]
        public virtual ICollection<Casewq> Casewqs { get; set; }

        //ForeignKey
        [Display(Name = "併案案號")]
        public int? ParentId { get; set; }


        [ForeignKey("ParentId")]
        public virtual Case ParentcCase { get; set; }

        [Display(Name = "所併的案件")]
        public virtual ICollection<Case> Cases { get; set; }

        [Display(Name = "首長信箱四層分案單位編號")]
        public int? ChiefMailboxUnitId { get; set; }


        [Display(Name = "是否自動結案")]
        public BooleanType? IsAutoClose { get; set; }

        [Display(Name = "符合項目")]
        [MaxLength(500)]
        public string FilterString { get; set; }

        [Display(Name = "符合類型")]
        [MaxLength(100)]
        public string FilterType { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件1")]
        public string Upfile1 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件2")]
        public string Upfile2 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件3")]
        public string Upfile3 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件4")]
        public string Upfile4 { get; set; }

        [MaxLength(200)]
        [Display(Name = "附件5")]
        public string Upfile5 { get; set; }

    }
}
