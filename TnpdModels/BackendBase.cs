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
        [Display(Name = "最後更新者")]
        [JsonIgnore]
        public string Updater { get; set; }

        [MaxLength(20)]
        [Display(Name = "最後更新單位")]
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

            Member member;
            if(HttpContext.Current.User.Identity.Name!=""){
                
                 member = db.Members.FirstOrDefault(x => x.Account == HttpContext.Current.User.Identity.Name);


            this.Poster = member.Name + "(" + member.Account + ")";
            this.initOrg = string.Format("{0} {1}", member.MyUnit.ParentUnit.Subject, member.MyUnit.Subject);    
             if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.Subject = this.SystemMessage;
                log.InitDate = DateTime.Now;
                log.Poster = member.Name + "(" + member.Account+")";
                log.Org = member.MyUnit.ParentUnit.Subject;
                log.subOrg = member.MyUnit.ParentUnit.Subject;
                log.Unitid = member.MyUnit.ParentId;
                log.Enent = SystemLogEvent.新增;
                log.attr = SystemLogAttr.網站維護;
                db.SystemLogs.Add(log);
            }

            }
            

            dbSet.Add(this);
           
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
                log.Poster = member.Name + "(" + member.Account + ")";
                log.Subject = this.SystemMessage;
                log.Org = member.MyUnit.ParentUnit.Subject;
                log.subOrg = member.MyUnit.Subject;
                log.Unitid = member.MyUnit.ParentId;
                log.Enent = SystemLogEvent.異動;
                log.attr = SystemLogAttr.網站維護;
                db.SystemLogs.Add(log);
            }
            db.SaveChanges();
        }
        public void Update(BackendContext db, System.Data.Entity.DbSet dbSet)
        {

            this.UpdateDate = DateTime.Now;
            this.Updater = System.Web.HttpContext.Current.User.Identity.Name;
            db.Entry(this).State = EntityState.Modified;
            var member = db.Members.FirstOrDefault(x => x.Account == HttpContext.Current.User.Identity.Name);
            if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = member.Name + "(" + member.Account + ")";
                log.Subject = this.SystemMessage;
                log.Org = member.MyUnit.ParentUnit.Subject;
                log.subOrg = member.MyUnit.Subject;
                log.Unitid = member.MyUnit.ParentId;
                log.Enent = SystemLogEvent.異動;
                log.attr = SystemLogAttr.網站維護;

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
        //public void Update(BackendContext db, System.Data.Entity.DbSet dbSet)
        //{

        //    this.UpdateDate = DateTime.Now;
        //    this.Updater = System.Web.HttpContext.Current.User.Identity.Name;
        //    db.Entry(this).State = EntityState.Modified;

        //    var manager = ((IObjectContextAdapter)db).ObjectContext;

        //    var myObjectState = manager.ObjectStateManager.GetObjectStateEntry(manager);
        //    var modifiedProperties = myObjectState.GetModifiedProperties();
        //    StringBuilder sb=new StringBuilder();
        //    foreach (var propName in modifiedProperties)
        //    {
        //        sb.Append(
        //            $"Property {propName} changed from {myObjectState.OriginalValues[propName]} to {myObjectState.CurrentValues[propName]}");

               
        //    }

        //    if (!string.IsNullOrEmpty(this.SystemMessage))
        //    {
        //        SystemLog log = new SystemLog();
        //        log.InitDate = DateTime.Now;
        //        log.Poster = HttpContext.Current.User.Identity.Name;
        //        log.Subject =sb.ToString();
        //        db.SystemLogs.Add(log);
        //    }
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //    {

        //        throw ex;
        //    }

        //}
        /// <summary>
        /// 刪除
        /// </summary>
        public void Delete(BackendContext db, System.Data.Entity.DbSet dbSet)
        {
            dbSet.Remove(this);
            db.Entry(this).State = EntityState.Deleted;
            var member = db.Members.FirstOrDefault(x => x.Account == HttpContext.Current.User.Identity.Name);
            if (!string.IsNullOrEmpty(this.SystemMessage))
            {
                SystemLog log = new SystemLog();
                log.InitDate = DateTime.Now;
                log.Poster = member.Name + "(" + member.Account + ")";
                log.Subject = this.SystemMessage;
                log.Org = member.MyUnit.ParentUnit.Subject;
                log.subOrg = member.MyUnit.Subject;
                log.Unitid = member.MyUnit.ParentId;
                log.Enent = SystemLogEvent.刪除;
                log.attr = SystemLogAttr.網站維護;
                db.SystemLogs.Add(log);
            }

            db.SaveChanges();
        }
    }
}