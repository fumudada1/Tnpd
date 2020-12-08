using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tnpd.Filters;
using Tnpd.Models;
using Newtonsoft.Json;
using System.Xml;
using TnpdModels;

namespace Tnpd.Controllers
{
    
    [Authorize]
    public class WebSiteController : Controller
    {

        private BackendContext db = new BackendContext();



        /// <summary>
        /// 網站編輯
        /// </summary>
        /// <param name="id">網站編號</param>
        /// <returns></returns>
        [PermissionFilters]
        public ActionResult Index(string id = "tnpd")
        {
            if (id != null)
            {
                //WebSiteName webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
                //string XmlDoc = webSite.XmlDoc;
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.LoadXml(XmlDoc);
                //string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);

                Member member =
                 db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
                WebSiteName webSite=db.WebSiteNames.FirstOrDefault(x => x.UnitId==member.MyUnit.ParentId);
                if (webSite == null)
                {
                    webSite = db.WebSiteNames.FirstOrDefault(x => x.UnitId == member.UnitId);
                }

                string SiteCode = webSite.SiteCode;
                var websites = db.WebSiteNames.Where(x => x.SiteCode == SiteCode && x.Enable == BooleanType.是)
                    .OrderBy(p => p.ListNum).ToList();
                ViewBag.WebSiteId = new SelectList(websites, "SiteCode", "Subject");
                ViewBag.webSiteName = SiteCode;
            }

            return View();
        }


        [PermissionFilters]
        public ActionResult Manage()
        {
           

            return View();
        }

        /// <summary>
        /// 更新json 回server
        /// </summary>
        /// <param name="id">網站代號</param>
        /// <param name="webDoc">網站JSON內容</param>
        /// <returns></returns>
      
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateWebDocJson(string id, string webDoc)
        {
            if (id == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (webDoc == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }

            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
            //if (webSite.Subject.Contains("英文版"))
            //{
            //    if (isChinese(webDoc))
            //    {
            //        return new ContentResult { Content = "Parameters Error" };
            //    }
            //}

            System.IO.File.WriteAllText(Server.MapPath("/WebSiteHistory/") + webSite.SiteCode + DateTime.Now.ToString("yyyy-MM-dd-hhmmsss"), webSite.XmlDoc, System.Text.Encoding.Default);

            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(webDoc);
            webSite.XmlDoc = xmlDoc.OuterXml;
            webSite.Update(db,db.WebSiteNames);

            return new ContentResult { Content = "Success" };
        }
        /// <summary>
        /// 字串是否包含中文
        /// </summary>
        /// <param name="strChinese">字串</param>
        /// <returns></returns>
        public static bool isChinese(string strChinese)
        {
            bool bresult = true;
            int dRange = 0;
            int dstringmax = Convert.ToInt32("9fff", 16);
            int dstringmin = Convert.ToInt32("4e00", 16);
            for (int i = 0; i < strChinese.Length; i++)
            {
                dRange = Convert.ToInt32(Convert.ToChar(strChinese.Substring(i, 1)));
                if (dRange >= dstringmin && dRange < dstringmax)
                {
                    bresult = true;
                    break;
                }
                else
                {
                    bresult = false;
                }
            }

            return bresult;
        }

        /// <summary>
        /// 新增網站節點
        /// </summary>
        /// <param name="webNode">節點物件</param>
        /// <param name="upFile">上傳檔案</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult AddWebNode(WebNode webNode, HttpPostedFileBase upFile)
        {


            if (webNode.Guid == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (webNode.WebSiteName == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (webNode.DataType == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (webNode.MetaId == 0)
            {
                return new ContentResult { Content = "請先建立分類檢索群組" };

            }
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == webNode.WebSiteName);
            System.IO.File.WriteAllText(Server.MapPath("/WebSiteHistory/") + webSite.SiteCode + DateTime.Now.ToString("yyyy-MM-dd-hhmmsss"), webSite.XmlDoc, System.Text.Encoding.Default);

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(webSite.XmlDoc);


            XmlNode NewXmlNode = XmlDoc.CreateNode(XmlNodeType.Element, "", "siteMapNode", "");
            string saveTime = DateTime.Now.ToString("yyyyMMddhhmmsss");
            string strLink = "";
            string strGuid = Guid.NewGuid().ToString();
            XmlAttribute title = XmlDoc.CreateAttribute("title");
            title.Value = webNode.Subject;
            NewXmlNode.Attributes.Append(title);

            XmlAttribute MetaID = XmlDoc.CreateAttribute("MetaID");
            MetaID.Value = webNode.MetaId.ToString();
            NewXmlNode.Attributes.Append(MetaID);

            //是否顯示
            XmlAttribute xmlVisible = XmlDoc.CreateAttribute("Visible");
            xmlVisible.Value = webNode.Visible.ToString();
            NewXmlNode.Attributes.Append(xmlVisible);

            XmlAttribute dataType = XmlDoc.CreateAttribute("DataType");
            dataType.Value = webNode.DataType;
            NewXmlNode.Attributes.Append(dataType);

            XmlAttribute xaURL = XmlDoc.CreateAttribute("URL");
            XmlAttribute target = XmlDoc.CreateAttribute("Target");
            XmlAttribute sClass = XmlDoc.CreateAttribute("sClass");

            sClass.Value = "";
            //save Article
            WebArticle webArticle = new WebArticle();
            switch (webNode.DataType)
            {
                case "text-Undefine":

                    break;
                case "directory":
                    XmlAttribute xmlDefault = XmlDoc.CreateAttribute("Default");
                    xmlDefault.Value = "";
                    NewXmlNode.Attributes.Append(xmlDefault);
                    break;
                case "text-Edit":
                    string chkMessage = Utility.ChkAccessible(webNode.Article);
                    if (chkMessage != "success")
                    {
                        return new ContentResult { Content = chkMessage };
                    }



                    webArticle.Article = webNode.Article;


                    break;
                case "text-Link":

                    xaURL.Value = webNode.Link;
                    target.Value = webNode.Target;

                    break;
                case "text-Publish":
                    sClass.Value = webNode.Category;
                    break;
                case "text-File":
                    //處理上傳檔案
                    if (upFile != null)
                    {
                        xaURL.Value = Utility.SaveUpFile(upFile);
                    }

                    break;
                default:
                    sClass.Value = webNode.Category;

                    break;
            }
            webArticle.Subject = webNode.Subject;

            webArticle.MetaId = webNode.MetaId;
            webArticle.UnId = webNode.Guid;
            webArticle.SystemMessage = "網站代號:" + webNode.WebSiteName + "<br />新增網站節點-" + webArticle.Subject;
            webArticle.Create(db, db.WebArticles);

            NewXmlNode.Attributes.Append(target);
            NewXmlNode.Attributes.Append(xaURL);
            NewXmlNode.Attributes.Append(sClass);
            //唯一識別碼

            XmlAttribute unId = XmlDoc.CreateAttribute("UnID");
            unId.Value = webNode.Guid;
            NewXmlNode.Attributes.Append(unId);

            //發佈人
            XmlAttribute poster = XmlDoc.CreateAttribute("poster");
            poster.Value = User.Identity.Name;
            NewXmlNode.Attributes.Append(poster);
            ////發布單位
            //XmlAttribute posterUnit = XmlDoc.CreateAttribute("posterUnit");
            //posterUnit.Value ="";
            //NewXmlNode.Attributes.Append(posterUnit);
            //發布時間
            XmlAttribute initDate = XmlDoc.CreateAttribute("initDate");
            initDate.Value = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            NewXmlNode.Attributes.Append(initDate);
            //最後修改人
            XmlAttribute updater = XmlDoc.CreateAttribute("updater");
            updater.Value = User.Identity.Name;
            NewXmlNode.Attributes.Append(updater);
            ////最後修改單位
            //XmlAttribute updaterUnit = XmlDoc.CreateAttribute("updaterUnit");
            //updaterUnit.Value = string.Format("{0}|{1}", strUserData[0], strUserData[1]);
            //NewXmlNode.Attributes.Append(updaterUnit);
            //最後修改時間
            XmlAttribute lastupdated = XmlDoc.CreateAttribute("lastupdated");
            lastupdated.Value = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            NewXmlNode.Attributes.Append(lastupdated);

            //取得父節點
            XmlNode nowXmlNode;
            if (webNode.ParentId != null)
            {
                nowXmlNode = XmlDoc.SelectSingleNode(string.Format("//siteMapNode[@UnID='{0}']", webNode.ParentId));
            }
            else
            {
                nowXmlNode = XmlDoc.DocumentElement;
            }
            nowXmlNode.AppendChild(NewXmlNode);

            //存到資料庫
            webSite.XmlDoc = XmlDoc.OuterXml;
            webSite.Update(db,db.WebSiteNames);
            //XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(webDoc);
            //webSite.XmlDoc = xmlDoc.OuterXml;
            //webSite.Update();
            string jsonContent = JsonConvert.SerializeXmlNode(XmlDoc);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        /// <summary>
        /// 更新網站節點資訊
        /// </summary>
        /// <param name="webNode">網站節點物件</param>
        /// <param name="upFile">上傳檔案</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateWebNode(WebNode webNode, HttpPostedFileBase upFile)
        {
            if (webNode.Guid == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (webNode.WebSiteName == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (webNode.DataType == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == webNode.WebSiteName);
            System.IO.File.WriteAllText(Server.MapPath("/WebSiteHistory/") + webSite.SiteCode + DateTime.Now.ToString("yyyy-MM-dd-hhmmsss"), webSite.XmlDoc, System.Text.Encoding.Default);


            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(webSite.XmlDoc);
            XmlNode nowXmlNode;
            nowXmlNode = XmlDoc.SelectSingleNode(string.Format("//siteMapNode[@UnID='{0}']", webNode.Guid));
            if (nowXmlNode == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            nowXmlNode.Attributes["title"].Value = webNode.Subject;
            nowXmlNode.Attributes["Visible"].Value = webNode.Visible.ToString();
            nowXmlNode.Attributes["DataType"].Value = webNode.DataType;
            if (webNode.DataType == "text-Edit")
            {
                WebArticle webArticle = db.WebArticles.FirstOrDefault(x => x.UnId == webNode.Guid);
                if (webArticle != null)
                {
                    webArticle.Subject = webNode.Subject;
                    webArticle.Article = webNode.Article;
                    webArticle.SystemMessage = "修改網站節點-" + webArticle.Subject;
                    webArticle.Update(db, db.WebArticles);
                }

            }
            if (webNode.DataType == "text-Link")
            {
                nowXmlNode.Attributes["URL"].Value = webNode.Link;
                nowXmlNode.Attributes["Target"].Value = webNode.Target;
            }
            if (webNode.DataType == "text-File")
            {
                //處理上傳檔案
                if (upFile != null)
                {
                    nowXmlNode.Attributes["URL"].Value = Utility.SaveUpFile(upFile);
                }

            }
            if (webNode.DataType == "text-Publish")
            {
                nowXmlNode.Attributes["sClass"].Value = webNode.Category;
            }
            nowXmlNode.Attributes["MetaID"].Value = webNode.MetaId.ToString();
            nowXmlNode.Attributes["updater"].Value = User.Identity.Name;
            nowXmlNode.Attributes["lastupdated"].Value = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            webSite.XmlDoc = XmlDoc.OuterXml;


            webSite.Update(db,db.WebSiteNames);
            string jsonContent = JsonConvert.SerializeXmlNode(XmlDoc);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        /// <summary>
        /// 取得網站節點內容
        /// </summary>
        /// <param name="id">網站代號</param>
        /// <param name="unId">節點編號</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetWebNode(string id, string unId)
        {

            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(webSite.XmlDoc);
            XmlNode nowXmlNode;
            nowXmlNode = XmlDoc.SelectSingleNode(string.Format("//siteMapNode[@UnID='{0}']", unId));
            if (nowXmlNode == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            WebNode node = new WebNode();
            node.Guid = nowXmlNode.Attributes["UnID"].Value;
            node.Subject = nowXmlNode.Attributes["title"].Value;
            node.MetaId = Convert.ToInt16(nowXmlNode.Attributes["MetaID"].Value);

            node.Visible = Convert.ToInt16(nowXmlNode.Attributes["Visible"].Value);
            node.DataType = nowXmlNode.Attributes["DataType"].Value;
            if (node.DataType == "text-Link")
            {
                node.Link = nowXmlNode.Attributes["URL"].Value;
                node.Target = nowXmlNode.Attributes["Target"].Value;
            }
            if (node.DataType == "text-File")
            {
                node.UpFile = nowXmlNode.Attributes["URL"].Value;

            }
            if (node.DataType == "text-Publish")
            {
                node.Category = nowXmlNode.Attributes["sClass"].Value;
            }
            if (node.DataType == "text-Edit")
            {
                WebArticle webArticle = db.WebArticles.FirstOrDefault(x => x.UnId == unId);
                if (webArticle != null)
                {
                    node.Article = webArticle.Article;
                }
            }
            node.Poster = nowXmlNode.Attributes["poster"].Value;
            node.InitDate = Convert.ToDateTime(nowXmlNode.Attributes["initDate"].Value);
            node.Updater = nowXmlNode.Attributes["updater"].Value;
            node.UpdateDate = Convert.ToDateTime(nowXmlNode.Attributes["lastupdated"].Value);

            string jsonContent = JsonConvert.SerializeObject(node, Newtonsoft.Json.Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }
        /// <summary>
        /// 取得子節點內容
        /// </summary>
        /// <param name="id">網站代號</param>
        /// <param name="unId">節點編號</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetChildNodes(string id, string unId)
        {

            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(webSite.XmlDoc);
            XmlNode nowXmlNode;
            nowXmlNode = XmlDoc.SelectSingleNode(string.Format("//siteMapNode[@UnID='{0}']", unId));
            if (nowXmlNode == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            XmlNodeList list = nowXmlNode.ChildNodes;

            string jsonContent = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        /// <summary>
        /// 目錄定義預設值
        /// </summary>
        /// <param name="id">網站代號</param>
        /// <param name="unId">節點編號</param>
        /// <param name="defaultValue">預設節點編號</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult UpdateDefaultValue(string id, string unId, string defaultValue)
        {

            if (unId == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (id == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }

            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
            System.IO.File.WriteAllText(Server.MapPath("/WebSiteHistory/") + webSite.SiteCode + DateTime.Now.ToString("yyyy-MM-dd hhmmsss"), webSite.XmlDoc, System.Text.Encoding.Default);

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(webSite.XmlDoc);
            XmlNode nowXmlNode;
            nowXmlNode = XmlDoc.SelectSingleNode(string.Format("//siteMapNode[@UnID='{0}']", unId));
            if (nowXmlNode == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }
            if (nowXmlNode.Attributes["Default"].Value == null)
            {
                XmlAttribute xmlDefault = XmlDoc.CreateAttribute("Default");
                xmlDefault.Value = defaultValue;
                nowXmlNode.Attributes.Append(xmlDefault);
            }
            else
            {
                nowXmlNode.Attributes["Default"].Value = defaultValue;
            }

            nowXmlNode.Attributes["updater"].Value = User.Identity.Name;
            nowXmlNode.Attributes["lastupdated"].Value = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            webSite.XmlDoc = XmlDoc.OuterXml;
            webSite.Update(db,db.WebSiteNames);
            string jsonContent = JsonConvert.SerializeXmlNode(XmlDoc);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }
        /// <summary>
        /// 複製網站節點
        /// </summary>
        /// <param name="id">網站編號</param>
        /// <param name="oldUnId">要複製的網站節點ID</param>
        /// <param name="newUnId">新網站節點ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CopyWebNode(string id, string oldUnId, string newUnId)
        {

            var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
            XmlDocument XmlDoc = new XmlDocument();
            System.IO.File.WriteAllText(Server.MapPath("/WebSiteHistory/") + webSite.SiteCode + DateTime.Now.ToString("yyyy-MM-dd hhmmsss"), webSite.XmlDoc, System.Text.Encoding.Default);

            XmlDoc.LoadXml(webSite.XmlDoc);
            XmlNode nowXmlNode;
            nowXmlNode = XmlDoc.SelectSingleNode(string.Format("//siteMapNode[@UnID='{0}']", oldUnId));
            if (nowXmlNode == null)
            {
                return new ContentResult { Content = "Parameters Error" };
            }

            XmlNode newNode = nowXmlNode.Clone();
            newNode.Attributes["UnID"].Value = newUnId;
            newNode.Attributes["title"].Value = nowXmlNode.Attributes["title"].Value + "-複製";
            if (nowXmlNode.Attributes["DataType"].Value == "text-Edit")
            {
                string uid = nowXmlNode.Attributes["UnID"].Value;
                WebArticle oldArticle = db.WebArticles.FirstOrDefault(x => x.UnId == uid);
                WebArticle newArticle = new WebArticle();
                newArticle.UnId = newUnId;
                newArticle.Article = oldArticle.Article;
                newArticle.Subject = nowXmlNode.Attributes["title"].Value + "-複製";
                newArticle.MetaId = oldArticle.MetaId;
                newArticle.Create(db, db.WebArticles);
            }
            nowXmlNode.ParentNode.InsertAfter(newNode, nowXmlNode.ParentNode.LastChild);
            string jsonContent = JsonConvert.SerializeXmlNode(XmlDoc);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        /// <summary>
        /// 取得網站內容JSON
        /// </summary>
        /// <param name="id">網站編號</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult WebDocJson(string id)
        {
            string jsonContent = "";
            if (id != null)
            {
                var webSite = db.WebSiteNames.FirstOrDefault(x => x.SiteCode == id);
                string XmlDoc = webSite.XmlDoc;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(XmlDoc);
                jsonContent = JsonConvert.SerializeXmlNode(xmlDoc);
            }

            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }



        /// <summary>
        /// 取得DataType
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetSysMenuJson()
        {
            var item = db.SysMenus.Select(
                            c => new
                            {
                                Id = c.Id,
                                Subject = c.Subject,
                                DataType = c.Url,
                                ListNum = c.ListNum
                            }
                    ).OrderBy(o => o.ListNum);

            string jsonContent = JsonConvert.SerializeObject(item.ToList(), Newtonsoft.Json.Formatting.Indented);
            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }
        /// <summary>
        /// 取得網站代號列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetSiteListJson()
        {
          Member member =
                 db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);
                

               
            string jsonContent="";
            if (!member.Roles.Any(x => x.Subject.Contains("最高權限管理者")))
            {
                var webSite = db.WebSiteNames.Where(x => x.UnitId == member.UnitId);
                if (!webSite.Any())
                {
                    webSite = db.WebSiteNames.Where(x => x.UnitId == member.MyUnit.ParentId);
                }

                var item = webSite.Where(x => x.Enable == BooleanType.是)
                    .Select(
                        c => new
                        {
                            Id = c.Id,
                            Subject = c.Subject,
                            code = c.SiteCode,
                            ListNum = c.ListNum
                        }
                    ).OrderBy(o => o.ListNum).ToList();
                jsonContent = JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented);
            }
            else
            {
                var item = db.WebSiteNames.Where(x => x.Enable == BooleanType.是).Select(
                                           c => new
                                           {
                                               Id = c.Id,
                                               Subject = c.Subject,
                                               code = c.SiteCode,
                                               ListNum = c.ListNum
                                           }
                ).OrderBy(o => o.ListNum).ToList();
                jsonContent = JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented);
            }






            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        [AllowAnonymous]
        public ActionResult GetAllSiteListJson()
        {
            Member member =
                db.Members.FirstOrDefault(d => d.Account == User.Identity.Name);

            string jsonContent;

            var item = db.WebSiteNames.Where(x => x.Enable == BooleanType.是)
                .Select(
                    c => new
                    {
                        Id = c.Id,
                        Subject = c.Subject,
                        code = c.SiteCode,
                        ListNum = c.ListNum
                    }
                ).OrderBy(o => o.ListNum).ToList();
            jsonContent = JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented);






            return new ContentResult { Content = jsonContent, ContentType = "application/json" };
        }

        public ActionResult WebSiteCreate()
        {
            WebSiteName webSite = db.WebSiteNames.FirstOrDefault(x => x.Enable == BooleanType.否);
            return View(webSite);
        }

        //
        // POST: /AreaSearch/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult WebSiteCreate(WebSiteName websitename)
        {
            if (ModelState.IsValid)
            {
                websitename.Enable = BooleanType.是;
                //_db.Entry(websitename).State = EntityState.Modified;
                websitename.Update(db,db.WebSiteNames);
                return View(websitename);
            }
            return View(websitename);
        }
    }
}
