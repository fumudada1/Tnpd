using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using tnpd.Models;
using TnpdModels;

namespace tnpd.Areas.baihe.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /english/Content/

        private BackendContext _db = new BackendContext();
        public ActionResult Article(string id)
        {
            ViewBag.UnId = id;
            string areaName = ControllerContext.RouteData.DataTokens["area"].ToString();
            WebPresentation web = new WebPresentation(areaName);
            XmlNode nowNode = web.GetNodeById(id);
            if (nowNode == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var article = _db.WebArticles.FirstOrDefault(x => x.UnId == id);
            if (article == null)
            {
                return RedirectToAction("Index", "Home");
            }
            article.Views = article.Views + 1;
            article.Update(_db, _db.WebArticles);
            
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

            UrlHelper urlHelper = new UrlHelper(((MvcHandler)System.Web.HttpContext.Current.Handler).RequestContext);

            List<NodeLink> nodeLinks = new List<NodeLink>();
            foreach (XmlNode node in nowNode)
            {
                NodeLink link = web.getNodeLink(node);
                if (link.title == "Leader")
                {
                    var leaderCatalog = _db.LeaderCatalogs.Where(x => x.WebSiteId == web.GetWebsiteID()).ToList();
                    foreach (LeaderCatalog catalog in leaderCatalog.OrderBy(x => x.ListNum))
                    {
                        link.url = urlHelper.Action("Index", "Leader",
                            new { id = node.Attributes["UnID"].Value, lclass = catalog.Id, Area = areaName });
                        link.Target = "_self";
                        link.Tooltip = catalog.Subject + "簡介";
                        link.title = catalog.Subject + "簡介";
                    }
                }
                else
                {
                    nodeLinks.Add(link);
                }

                
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
