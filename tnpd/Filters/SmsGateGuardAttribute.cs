using System.Web.Mvc;

namespace tnpd.Filters
{
    /// <summary>
    /// 局長信箱簡訊發送端點（ChiefMailbox/SendVerifyMobile）之保護閘門。
    /// 需同時滿足以下條件，否則一律回 HTTP 403，避免此 API 被直接呼叫濫發簡訊：
    ///   1. 必須為 POST（擋掉 GET / &lt;img&gt; drive-by CSRF）。
    ///   2. 具備有效之 anti-forgery token（machineKey 密碼學簽章驗證）。
    ///   3. session 已由表單頁種下 SmsGate 旗標（代表確實由表單頁進入，而非直接打 API）。
    /// </summary>
    public class SmsGateGuardAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.RequestContext.HttpContext;

            // 1. 僅接受 POST
            if (httpContext.Request.HttpMethod != "POST")
            {
                filterContext.Result = new HttpStatusCodeResult(403, "僅接受 POST");
                return;
            }

            // 2. 強制驗證 anti-forgery token，無效即回 403（沿用 MVC 內建之簽章驗證）
            try
            {
                new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
            }
            catch (System.Web.HttpException)
            {
                // anti-forgery 驗證失敗（HttpAntiForgeryException 繼承自 HttpException）一律 fail-closed 回 403
                filterContext.Result = new HttpStatusCodeResult(403, "無效的驗證權杖");
                return;
            }
            // 內建 attribute 於驗證失敗時亦可能直接設定 Result，此時不再往下走
            if (filterContext.Result != null)
            {
                return;
            }

            // 3. session 閘：必須先載入過表單頁（由 SmsGateStampAttribute 種下旗標）
            var session = httpContext.Session;
            if (session == null || session["SmsGate"] == null)
            {
                filterContext.Result = new HttpStatusCodeResult(403, "請由表單頁面操作");
                return;
            }
        }
    }
}
