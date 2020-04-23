using System.Web.Mvc;

namespace tnpd.Areas.shanhuaen
{
    public class shanhuaenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "shanhuaen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "shanhuaen_default",
                "shanhua/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
