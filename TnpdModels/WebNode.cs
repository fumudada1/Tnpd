using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TnpdModels
{
    public class WebNode
    {
         [Display(Name = "編號")]
        public string Guid { get; set; }

        [Display(Name = "標題")]
        public string Subject { get; set; }

        [Display(Name = "網站名稱")]
        public string WebSiteName { get; set; }

        [Display(Name = "父節點ID")]
        public string ParentId { get; set; }

        [Display(Name = "分類索引")]
        public int MetaId { get; set; }

        [Display(Name = "是否顯示")]
        public int Visible { get; set; }

        [Display(Name = "類型")]
        public string DataType { get; set; }

        [Display(Name = "子類別")]
        public string Category { get; set; }

        [Display(Name = "網站內容")]
        public string Article { get; set; }

        [Display(Name = "連結")]
        public string Link { get; set; }

        [Display(Name = "開啟位置")]
        public string Target { get; set; }

        [Display(Name = "上傳檔案")]
        public string UpFile { get; set; }

        [MaxLength(20)]
        [Display(Name = "發布者")]
        public string Poster { get; set; }

        [Display(Name = "最後發布時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? InitDate { get; set; }

        [MaxLength(20)]
        [Display(Name = "更新者")]
        public string Updater { get; set; }



        [Display(Name = "最後更新時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? UpdateDate { get; set; }
    }
}