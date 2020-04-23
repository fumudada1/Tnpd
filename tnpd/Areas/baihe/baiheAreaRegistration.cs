using System.Web.Mvc;

namespace tnpd.Areas.baihe
{
    public class baiheAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "baihe";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "baihe_default",
                "baihe/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
