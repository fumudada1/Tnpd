using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TnpdModels
{
    public abstract class BackendBase
    {

        [MaxLength(20)]
        [Display(Name = "發佈者")]
        [JsonIgnore]
        public string Poster { get; set; }

        [MaxLength(20)]
        [Display(Name = "發布單位")]
        [JsonIgnore]
        public string initOrg { get; set; }

        [Display(Name = "發佈時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [JsonIgnore]
        public DateTime? InitDate { get; set; }

        [MaxLength(20)]
        [Display(Name = "更新者")]
        [JsonIgnore]
        public string Updater { get; set; }

        [MaxLength(20)]
        [Display(Name = "更新單位")]
        [JsonIgnore]
        public string UpdateOrg { get; set; }



        [Display(Name = "最後更新時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }


        [Display(Name = "更新說明文字")]
        [NotMapped]
        [JsonIgnore]
        [ScaffoldColumn(false)]
        public string SystemMessage { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public void Create(BackendContext db, System.Data.Entity.DbSet dbSet)
        {
            if (this.InitDate == null)
            {
                this.InitDate = DateTime.Now;
            }
            if(HttpContext.Current.User.Identity.Name!=""){
                
                var member = db.Members.FirstOrDefault(x => x.Account == HttpContext.Current.User.Identity.Name);


            this.Poster = member.Name;
            this.initOrg = string.Format("{0} {1}", member.MyUnit.ParentUnit.Subject, member.MyUnit.Subject);       





            }
            

            dbSet.Add(this);
            if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.Subject = this.SystemMessage;
                log.InitDate = DateTime.Now;
                log.Poster = HttpContext.Current.User.Identity.Name;
                db.SystemLogs.Add(log);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            BackendContext db = new BackendContext();
            this.UpdateDate = DateTime.Now;
            var member = db.Members.FirstOrDefault(x => x.Account == HttpContext.Current.User.Identity.Name);
            this.UpdateOrg = string.Format("{0} {1}", member.MyUnit.ParentUnit.Subject, member.MyUnit.Subject);
            this.Updater = member.Name;

            db.Entry(this).State = EntityState.Modified;
            if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = HttpContext.Current.User.Identity.Name;
                log.Subject = this.SystemMessage;
                db.SystemLogs.Add(log);
            }
            db.SaveChanges();
        }
        public void Update(BackendContext db, System.Data.Entity.DbSet dbSet)
        {

            this.UpdateDate = DateTime.Now;
            this.Updater = System.Web.HttpContext.Current.User.Identity.Name;
            db.Entry(this).State = EntityState.Modified;

            if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = HttpContext.Current.User.Identity.Name;
                log.Subject = this.SystemMessage;
                db.SystemLogs.Add(log);
            }
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 刪除
        /// </summary>
        public void Delete(BackendContext db, System.Data.Entity.DbSet dbSet)
        {
            dbSet.Remove(this);
            db.Entry(this).State = EntityState.Deleted;
            if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = HttpContext.Current.User.Identity.Name;
                log.Subject = this.SystemMessage;
                db.SystemLogs.Add(log);
            }

            db.SaveChanges();
        }
    }
}