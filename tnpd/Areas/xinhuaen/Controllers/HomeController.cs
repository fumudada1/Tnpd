using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.xinhuaen.Controllers
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

                return RedirectToActionPermanent("Article", "Content", new { id = "85c21d94-14c1-9a41-0dc0-37950e92e95d", Area = areaName });
            }


            return View();
        }

    }
}
