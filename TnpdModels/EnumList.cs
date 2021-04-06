using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TnpdModels
{
    public enum GenderType
    {
        男,
        女
    }

    public enum EnableType
    {
        開,
        關
    }

    public enum TargetType
    {
        另開視窗,
        本地開啟
    }

    public enum BooleanType
    {
        是,
        否
    }

    public enum CaseProcessStatus
    {
        未分派,
        待辦,
        已辦,
        結案,
        退文,
        展延,
        單位改派,
        辦案
        
        
    }

    public enum CaseType
    {
       首長信箱,
       分局長與大隊隊長信箱,
       大隊隊長信箱,
       檢舉貪瀆信箱 ,
       網路報案,
       參觀本局暨所屬機關,
       婦幼安全警示地點

    }

    public enum CaseReportType
    {
        當事人,
        被害人,
        關係人
    }

    public enum CaseFilterType
    {
        姓名,
        IP,
        電子郵件
    }
    

    public enum LanguageType
    {
        中文版,
        英文版
    }

    public enum CarType
    {
        汽車,
        輕型機車,
        重型機車,
        拖車
    }

    public enum CarAllow
    {
        車主本人,
        車主本人同意
    }
 
    /// <summary>
    /// 報名狀態
    /// </summary>
    public enum ActivityRegistrationStatus
    {
        [Description("待處理")]
        待審核 = 1,
        [Description("已通過")]
        已通過 = 2,
        [Description("未通過")]
        未通過 = 3
    }

    public enum SMSStatus
    {
        [Description("待審核")]
        待審核 = 2,
        [Description("已通過")]
        已通過 = 0,
        [Description("未通過")]
        未通過 = 1
    }

}