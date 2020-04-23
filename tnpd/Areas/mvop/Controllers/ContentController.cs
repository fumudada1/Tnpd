using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.mvop.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /english/Content/

        private BackendContext _db = new BackendContext();
        public ActionResult Article(string id)
        {
            ViewBag.UnId = id;
            var article = _db.WebArticles.FirstOrDefault(x => x.UnId == id);
            if (article == null)
            {
                return RedirectToAction("Index", "Home");
            }
            article.Views = article.Views + 1;
            article.Update(_db, _db.WebArticles);
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            article.Article = article.Article.Replace("src=\"images/", "src=\"/images/" + areaName + "/");
            return View(article);
        }

        public ActionResult Directory(string id)
        {
            ViewBag.UnId = id;
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            WebPresentation web = new WebPresentation(areaName);
            XmlNode nowNode = web.GetNodeById(id);
            if (nowNode == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //預設
            if (nowNode.Attributes != null && !string.IsNullOrEmpty(nowNode.Attributes["Default"].Value))
            {
                string unId = nowNode.Attributes["Default"].Value;
                XmlNode defaultNode = web.GetNodeById(unId);
                NodeLink defaultLink = web.getNodeLink(defaultNode);
                Response.Redirect(defaultLink.url);
            }


            List<NodeLink> nodeLinks = new List<NodeLink>();
            foreach (XmlNode node in nowNode)
            {
                NodeLink link = web.getNodeLink(node);
                nodeLinks.Add(link);
            }

            ViewBag.nodeLinks = nodeLinks;

            return View();
        }

        public ActionResult SiteMap()
        {

            return View();
        }

    }
}
