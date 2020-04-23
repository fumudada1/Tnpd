using System.Web.Mvc;

namespace tnpd.Areas.jialien
{
    public class jialienAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "jialien";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "jialien_default",
                "jiali/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
