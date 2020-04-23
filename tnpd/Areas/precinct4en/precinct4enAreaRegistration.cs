using System.Web.Mvc;

namespace tnpd.Areas.precinct4en
{
    public class precinct4enAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct4en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct4en_default",
                "precinct4/en/{controller}/{action}/{id}",
                new { Controller = "Home",action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
