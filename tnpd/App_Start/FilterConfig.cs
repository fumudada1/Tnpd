using System.Web;
using System.Web.Mvc;

namespace tnpd
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // 於表單頁 GET 載入時種下 SmsGate 旗標，供 SmsGateGuard 判斷簡訊發送來源
            filters.Add(new tnpd.Filters.SmsGateStampAttribute());
        }
    }
}