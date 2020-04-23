using System.Web.Mvc;

namespace tnpd.Areas.xinyingen
{
    public class xinyingenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "xinyingen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "xinyingen_default",
                "xinying/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
