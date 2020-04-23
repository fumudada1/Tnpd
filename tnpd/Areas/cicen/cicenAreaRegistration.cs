using System.Web.Mvc;

namespace tnpd.Areas.cicen
{
    public class cicenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "cicen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "cicen_default",
                "cic/en/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },  // Parameter defaults
                namespaces: new[] { "tnpd.Areas.cicen.Controllers" }
            );
        }
    }
}
