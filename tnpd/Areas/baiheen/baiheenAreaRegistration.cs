using System.Web.Mvc;

namespace tnpd.Areas.baiheen
{
    public class baiheenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "baiheen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "baiheen_default",
                "baihe/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
