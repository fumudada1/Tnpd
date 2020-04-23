using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.precinct4en.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /english/Home/

        private BackendContext _db = new BackendContext();
        public ActionResult Index()
        {
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            WebPresentation web = new WebPresentation(areaName);
            XmlDocument xmlDocument = web.XmlDocument;
            XmlNode rootNode = xmlDocument.DocumentElement;
            if (rootNode.ChildNodes.Count > 0)
            {
                XmlNode firNode = rootNode.FirstChild;

                return RedirectToActionPermanent("Article", "Content", new { id = firNode.Attributes["UnID"].Value, Area = areaName });
            }


            return View();
        }

    }
}
