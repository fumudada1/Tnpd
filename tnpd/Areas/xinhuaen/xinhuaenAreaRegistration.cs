using System.Web.Mvc;

namespace tnpd.Areas.xinhuaen
{
    public class xinhuaenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "xinhuaen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "xinhuaen_default",
                "xinhua/en/{controller}/{action}/{id}",
                new { Controller = "Home",action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
