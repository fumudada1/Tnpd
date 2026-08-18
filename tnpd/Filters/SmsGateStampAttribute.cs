using System.Web.Mvc;

namespace tnpd.Filters
{
    /// <summary>
    /// 於會呼叫 ChiefMailbox/SendVerifyMobile 的表單頁 GET 載入時，
    /// 在 session 種下 SmsGate 旗標，供 <see cref="SmsGateGuardAttribute"/> 判斷
    /// 「使用者確實由表單頁進入」。全域註冊於 FilterConfig。
    /// 註：ASP.NET Session 為全應用程式共用（跨 Area），故 Area 表單頁種下的旗標，
    /// 主站 ChiefMailboxController.SendVerifyMobile 讀得到。
    /// </summary>
    public class SmsGateStampAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.HttpMethod != "GET")
            {
                return;
            }

            var routeValues = filterContext.RouteData.Values;
            string controller = routeValues["controller"] as string;
            string action = routeValues["action"] as string;
            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                return;
            }

            // 允許清單：會呼叫 /ChiefMailbox/SendVerifyMobile 的表單頁
            // （controller 名稱 "ChiefMailbox" 對主站與各 Area 皆成立）
            bool isFormPage =
                (controller == "ChiefMailbox" && action == "Create") ||
                (controller == "Visit" && action == "Index") ||
                (controller == "CriminalMailbox" && action == "Index");

            if (isFormPage && filterContext.HttpContext.Session != null)
            {
                filterContext.HttpContext.Session["SmsGate"] = true;
            }
        }
    }
}
