using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using tnpd.Models;
using TnpdModels;

namespace tnpd.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/
        private BackendContext _db = new BackendContext();
        public ActionResult Article(string id)
        {
            ViewBag.UnId = id;
            WebPresentation web = new WebPresentation("tnpd");
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
            //article.Update(_db, _db.WebArticles);
            _db.SaveChanges();
            article.Article = article.Article.Replace("src=\"images/", "src=\"/images/");
            return View(article);
        }

        public ActionResult Detail(string id)
        {
            ViewBag.UnId = id;
            WebPresentation web = new WebPresentation("tnpd");
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
            return View(article);
        }

        public ActionResult SiteMap()
        {

            return View();
        }

        public ActionResult Directory(string id)
        {
            ViewBag.UnId = id;
            WebPresentation web = new WebPresentation("tnpd");
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
                    int websiteID = web.GetWebsiteID();
                    var leaderCatalog = _db.LeaderCatalogs.Where(x => x.WebSiteId == websiteID).ToList();
                    foreach (LeaderCatalog catalog in leaderCatalog.OrderBy(x => x.ListNum))
                    {
                        NodeLink nlink = new NodeLink();
                        nlink.url = urlHelper.Action("Index", "Leader",
                            new { id = node.Attributes["UnID"].Value, lclass = catalog.Id, Area = "" });
                        nlink.Target = "_self";
                        nlink.Tooltip = catalog.Subject + "簡介";
                        nlink.title = catalog.Subject + "簡介";
                        nodeLinks.Add(nlink);
                    }
                }
                else
                {
                    nodeLinks.Add(link);
                }
            }

            ViewBag.title = nowNode.Attributes["title"].Value;
            ViewBag.nodeLinks = nodeLinks;

            return View();
        }

    }
}
