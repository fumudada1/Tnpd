using System.Web.Mvc;

namespace tnpd.Areas.yujing
{
    public class yujingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "yujing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "yujing_default",
                "yujing/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
