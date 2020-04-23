using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using TnpdModels;

namespace tnpd.Models
{
    /// <summary>
    /// WebPresentation 的摘要描述
    /// </summary>
    public class WebPresentation
    {
        private int _i;
        private int _intLayer;
        private string _PathID;
        private string _MemberPath;
        private readonly string _orgId;
        private XmlDocument _xmlDoc;
        private readonly string _xml;
        private string _uid;
        private WebSiteName webSite;
        private XmlNode _currXmlNode;
        private string _title;
        private string _theme;
        private string _cake;
        private string _service;
        private Boolean _IsRight;
        private string _openMessage;


        private readonly BackendContext _db = new BackendContext();

        public XmlDocument XmlDocument
        {
            get { return _xmlDoc; }
        }

        #region "初始化"
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="unId"></param>
        public WebPresentation(string orgId, string unId)
        {
            _i = 1;
            _intLayer = 1;
            _orgId = orgId;
            _uid = unId;
            _xmlDoc = new XmlDocument();

            webSite = _db.WebSiteNames.FirstOrDefault(x => x.SiteCode == orgId);

            _xml = webSite.XmlDoc;
            _xmlDoc.LoadXml(_xml);

            _currXmlNode = GetNodeById(unId);
            if (_currXmlNode != null)
            {
                _title = _currXmlNode.Attributes["title"].Value;
                int MetaID = Convert.ToInt32(_currXmlNode.Attributes["MetaID"].Value);
                var meta = _db.MetaIndices.Find(MetaID);
                _theme = meta.Theme;
                _cake = meta.Cake;
                _service = meta.Service;
                _IsRight = true;

            }
            else
            {
                _IsRight = false;
            }

        }

        public WebPresentation(string orgId)
        {
            _i = 1;
            _intLayer = 1;
            _orgId = orgId;

            _xmlDoc = new XmlDocument();

            webSite = _db.WebSiteNames.FirstOrDefault(x => x.SiteCode == orgId);

            _xml = webSite.XmlDoc;
            _xmlDoc.LoadXml(_xml);



        }

        #endregion

        #region "取得標題"
        /// <summary>
        /// 取得標題
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {

            return _title;

        }

        #region "取得標題"
        /// <summary>
        /// 取得Unid
        /// </summary>
        /// <returns></returns>
        public string GetIdByTitle(string title)
        {

            XmlNode node = _xmlDoc.SelectSingleNode("//siteMapNode[@title='" + title + "']");
            return node.Attributes["UnID"].Value;

        }
        #endregion
        #region "判斷unID 是否存在"
        /// <summary>
        /// 取得標題
        /// </summary>
        /// <returns></returns>
        public Boolean IsRight()
        {

            return _IsRight;

        }

        #endregion

        #region "取得施政分類"
        /// <summary>
        /// 取得標題
        /// </summary>
        /// <returns></returns>
        public string GetCake()
        {

            return _cake;

        }

        #endregion

        #region "取得主題分類"
        /// <summary>
        /// 取得主題分類
        /// </summary>
        /// <returns></returns>
        public string GetTheme()
        {

            return _theme;

        }

        #endregion

        #region "取得服務分類"
        /// <summary>
        /// 取得服務分類
        /// </summary>
        /// <returns></returns>
        public string GetService()
        {

            return _service;

        }

        #endregion

        #endregion


        #region "取得麵包屑"
        /// <summary>
        /// 取得麵包屑
        /// </summary>
        /// <returns></returns>
        public string getNodePath()
        {
            if (!string.IsNullOrEmpty(_uid))
            {
                XmlNode node = GetNodeById(_uid);
                if (node == null)
                {
                    return "";
                }

                string nodePath = "";
                Stack stack = new Stack();
                NodeLink linklast = getNodeLink(node);

                stack.Push(string.Format(" <li class='breadcrumb-item active' aria-current='page'>{0}</li>", linklast.Tooltip));
                node = node.ParentNode;
                string className = "breadcrumb-item";
                while (node != _xmlDoc.DocumentElement)
                {
                    NodeLink link = getNodeLink(node);

                    stack.Push(string.Format("<li class=\"{0}\"><a  href=\"{1}\" Target=\"{2}\" title=\"{4}\">{3}</a></li>", className, link.url, link.Target, link.Tooltip,link.title));
                    node = node.ParentNode;

                }
                while (stack.Count != 0)
                {
                    nodePath += "" + stack.Pop();
                }
                return nodePath;
            }
            return "";

        }

        #endregion



        #region "取得所有選單"
        /// <summary>
        /// 取得所有選單
        /// </summary>
        /// <returns></returns>
        public string getAllMenu()
        {
            if (_xmlDoc.DocumentElement.ChildNodes.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("<ul>");
                recursionMenu(_xmlDoc.DocumentElement, sb);
                sb.Append("</ul>");
                return sb.ToString();
            }
            else
            {
                return "";
            }

        }

        #endregion

        #region "取得左邊選單"
        public string GetLeftMenu()
        {
            DataTable xmlTable = XmlToDataTable();
            DataRow[] Layer1Row = xmlTable.Select("Layer=1 and Visible=1");
            if (!string.IsNullOrEmpty(_uid))
            {
                DataRow[] selectRows = xmlTable.Select(("UnID='" + _uid.Replace("\'", "") + "'"));
                if (selectRows.Length > 0)
                {
                    DataRow selectRow = selectRows[0];
                    string[] nowMemberPath = selectRow["MemberPath"].ToString().Split('.');
                    XmlNode firstXmlNode = _xmlDoc.DocumentElement.ChildNodes[Convert.ToInt16(nowMemberPath[0])];

                    if (firstXmlNode.ChildNodes.Count > 0)
                    {
                        System.Text.StringBuilder sb =
                            new System.Text.StringBuilder("<h2><i class='icon-folder-open'></i>" +
                                                          firstXmlNode.Attributes["title"].Value +
                                                          "</h2>\n<div class='leftbox_before'></div>\n<ul class='list-icon-arrow'>\n");
                        recursionLeftMenu(firstXmlNode, sb);
                        sb.Append("</ul>\n");
                        return sb.ToString();
                    }

                }

            }


            return "";

        }

        public string GetL1LeftMenu()
        {


            XmlNode firstXmlNode = _xmlDoc.DocumentElement;

            if (firstXmlNode.ChildNodes.Count > 0)
            {
                System.Text.StringBuilder sb =
                                  new System.Text.StringBuilder("<h2><i class='icon-folder-open'></i>Menu</h2>\n<div class='leftbox_before'></div>\n<ul class='list-icon-arrow'>\n");
                foreach (XmlNode node in firstXmlNode.ChildNodes)
                {
                    if (node.Attributes["Visible"].Value == "1")
                    {
                        NodeLink nlink = getNodeLink(node);


                        sb.AppendFormat("<li ><a href=\"{0}\" target=\"{1}\" title=\"{3}\">{2}</a>\n", nlink.url, nlink.Target, nlink.Tooltip,nlink.title);
                    }

                }

                // recursionLeftMenu(firstXmlNode, sb);
                sb.Append("</ul>\n");
                return sb.ToString();
            }


            return "";

        }

        public string GetEnLeftMenu()
        {



            XmlNode firstXmlNode = _xmlDoc.DocumentElement;
            System.Text.StringBuilder sb =
                new System.Text.StringBuilder("<ul class=\"list-icon-arrow\">");
            recursionLeftMenu(firstXmlNode, sb);
            sb.AppendLine("</ul>");
            return sb.ToString();




        }
        #endregion

        #region "上選單遞回運算"
        /// <summary>
        /// 選單遞回運算
        /// </summary>
        /// <param name="pnode"></param>
        /// <param name="sb"></param>
        void recursionMenu(XmlNode pnode, System.Text.StringBuilder sb)
        {
            foreach (XmlNode node in pnode.ChildNodes)
            {
                if (node.Attributes["Visible"].Value == "1")
                {
                    NodeLink nlink = getNodeLink(node);
                    string beSelect = "";
                    if (node.Attributes["UnID"].Value == _uid)
                    {
                        beSelect = "class='selected'";
                    }

                    if (node.HasChildNodes) //目錄
                    {

                        sb.AppendFormat("<li><a href=\"{0}\" target=\"{1}\" title=\"{2}\"> <div>{2}</div></a><ul>", nlink.url, nlink.Target, nlink.Tooltip);
                        recursionMenu(node, sb);
                        sb.Append("</ul></li>");
                    }
                    else
                    { //子結點
                        sb.AppendFormat("<li><a href=\"{0}\" target=\"{1}\" title=\"{3}\"><div>{2}</div></a></li>", nlink.url, nlink.Target, nlink.Tooltip,nlink.title);
                    }
                }

            }
        }
        #endregion

        #region "左選單遞回運算"
        /// <summary>
        /// 選單遞回運算
        /// </summary>
        /// <param name="pnode"></param>
        /// <param name="sb"></param>
        void recursionLeftMenu(XmlNode pnode, System.Text.StringBuilder sb)
        {
            foreach (XmlNode node in pnode.ChildNodes)
            {
                if (node.Attributes["Visible"].Value == "1")
                {
                    NodeLink nlink = getNodeLink(node);
                    string beSelect = "";
                    if (node.Attributes["UnID"].Value == _uid)
                    {
                        beSelect = "class='active'";
                    }

                    if (node.HasChildNodes) //目錄
                    {

                        sb.AppendFormat("<li ><a {0} href=\"{1}\" target=\"{2}\" title=\"{3}\">{3}</a>\n<ul>\n", beSelect, nlink.url, nlink.Target, nlink.Tooltip);
                        recursionLeftMenu(node, sb);
                        sb.Append("</ul>\n</li>");
                    }
                    else
                    { //子結點
                        sb.AppendFormat("<li ><a {0} href=\"{1}\" target=\"{2}\" title=\"{4}\">{3}</a></li>\n", beSelect, nlink.url, nlink.Target, nlink.Tooltip,nlink.title);
                    }
                }

            }
        }
        #endregion

        #region "取得下方選單"
        /// <summary>
        /// 取得下方選單
        /// </summary>
        /// <returns></returns>
        public string getfootMenu()
        {

            if (_xmlDoc.DocumentElement.ChildNodes.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("");

                foreach (XmlNode node in _xmlDoc.ChildNodes[0].ChildNodes)
                {
                    NodeLink nlinkTitle = getNodeLink(node);

                    sb.AppendLine(" <div class='col'>");
                    sb.AppendLine("<div class='footerbox'>");

                    sb.AppendLine(" <h4>" + nlinkTitle.Tooltip + "</h4>");
                    sb.AppendLine("<ul>");
                    foreach (XmlNode c2Node in node.ChildNodes)
                    {
                        NodeLink c2link = getNodeLink(c2Node);
                        sb.AppendLine("<li><a href='" + c2link.url + "' title='" + c2link.title + "' target='" + c2link.Target + "'>" + c2link.Tooltip + "</a></li>");
                    }
                    sb.AppendLine("</ul>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("</div>");




                }

                return sb.ToString();
            }

            return "";


        }

        #endregion


        #region "取得SiteMap"
        public string getSiteMap()
        {
            return getSiteMap(_xmlDoc.DocumentElement);
        }



        /// <summary>
        /// 取得SiteMap
        /// </summary>
        /// <param name="firstXmlNode"></param>
        /// <returns></returns>
        public string getSiteMap(XmlNode firstXmlNode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            _i = 1;
            _intLayer = 0;
            int intSort = 1;//排序編號
            string memberPath = "";
            foreach (XmlNode node in firstXmlNode.ChildNodes)
            {
                if (node.Attributes["Visible"].Value == "1")
                {
                    NodeLink nlink = getNodeLink(node);
                    string tempMemberPath = (memberPath + "-" + intSort).Trim('-');
                    sb.AppendLine("<div class=\"col-12 col-md-4\">");
                    sb.AppendLine("<h4 class=\"h5title\"><a href=\"" + nlink.url + "\" title=\"" + nlink.title + "\">" + tempMemberPath + "." + nlink.Tooltip + "</a></h4>");


                    if (node.HasChildNodes) //目錄
                    {
                        sb.AppendLine(" <ul class=\"ulsitemap\">");

                        RecursionSiteMap(node, sb, tempMemberPath, _intLayer);

                        sb.Append("</ul>");
                    }
                    sb.AppendLine("</div>");
                    //else
                    //{ //子結點
                    //    sb.Append("<li class=\"level1\"><a href=\"" + nlink.url + "\" target=\"" + nlink.Target + "\" title=\"" + nlink.Tooltip + "\">" + tempMemberPath + ". " + nlink.Tooltip + "</a></li>");

                    //}

                    intSort++;

                }
            }

            sb.Append("</div>");
            //recursionSiteMap(firstXmlNode, sb, "", _intLayer);

            return sb.ToString();
        }

        void RecursionSiteMap(XmlNode pnode, System.Text.StringBuilder sb, string memberPath, int intLayer)
        {
            int intSort = 1;//排序編號
            intLayer++;
            foreach (XmlNode node in pnode.ChildNodes)
            {

                if (node.Attributes["Visible"].Value == "1")
                {

                    NodeLink nlink = getNodeLink(node);
                    string tempMemberPath = (memberPath + "-" + intSort).Trim('-');
                    if (node.HasChildNodes) //目錄
                    {

                        sb.AppendLine("<li ><a href=\"" + nlink.url + "\" target=\"" + nlink.Target + "\" title=\"" + nlink.Tooltip + "\">" + tempMemberPath + ". " + nlink.Tooltip + "</a></li>");
                        sb.AppendLine("<ul>");

                        RecursionSiteMap(node, sb, tempMemberPath, intLayer);
                        sb.AppendLine("</ul>");
                    }
                    else
                    { //子結點
                        sb.AppendLine("<li><a href=\"" + nlink.url + "\" target=\"" + nlink.Target + "\" title=\"" + nlink.title + "\">" + tempMemberPath + ". " + nlink.Tooltip + "</a></li>");


                    }

                    intSort++;
                }

            }
        }
        #endregion


        #region "取得XML"
        /// <summary>
        /// 取得XML
        /// </summary>
        /// <returns></returns>
        public string getXML()
        {
            return _xml;
        }
        #endregion


        #region "取得XML 物件"
        /// <summary>
        /// 取得XML 物件
        /// </summary>
        /// <returns></returns>
        public XmlDocument getXmlDocument()
        {
            return _xmlDoc;
        }
        #endregion


        #region "取得目前使用XML節點"
        /// <summary>
        /// 取得目前使用XML節點
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public XmlNode GetNodeById(string ID)
        {
            XmlNode node = _xmlDoc.SelectSingleNode("//siteMapNode[@UnID='" + ID + "']");
            return node;
        }
        /// <summary>
        ///  取得目前使用XML節點
        /// </summary>
        /// <returns></returns>
        public XmlNode GetCurrNode()
        {

            return _currXmlNode;
        }

        #endregion


        #region "定義連結"
        /// <summary>
        /// 定義連結
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public NodeLink getNodeLink(XmlNode node)
        {
            UrlHelper urlHelper = new UrlHelper(((MvcHandler)HttpContext.Current.Handler).RequestContext);
            NodeLink link = new NodeLink();
            string area = "";
            if (_orgId != "tnpd")
            {
                area = _orgId;
            }
            //carl 20151128暫時修正
            if (node == null || node.Attributes["DataType"] == null)
            {
                return link;
            }

            switch (node.Attributes["DataType"].Value)
            {
                    
                case "text-Undefine":
                    
                    link.url = "#";
                    link.Target = "_self";
                    link.Tooltip = node.Attributes["title"].Value;
                    link.title = node.Attributes["title"].Value;
                    break;
                case "directory":
                    //link.url = "Directory.aspx?UnID=" + node.Attributes["UnID"].Value;
                    link.url = urlHelper.Action("Directory", "Content", new { id = node.Attributes["UnID"].Value, Area = area }); ;
                    link.Target = "_self";
                    link.Tooltip = node.Attributes["title"].Value;
                    link.title = node.Attributes["title"].Value;
                    break;
                case "text-Edit":
                    //link.url = "WebArticle.aspx?UnID=" + node.Attributes["UnID"].Value;
                    link.url = urlHelper.Action("Article", "Content", new { id = node.Attributes["UnID"].Value, Area = area });
                    link.Target = "_self";
                    link.Tooltip = node.Attributes["title"].Value;
                    link.title = node.Attributes["title"].Value;
                    break;
                case "text-Link":
                    string openMessage = _orgId == "en" ? "(Open New Window)" : "(另開新視窗)";
                    if (node.Attributes["Target"].Value == "_blank")
                    {
                        link.url = node.Attributes["URL"].Value;
                    }
                    else
                    {
                        if (node.Attributes["URL"].Value.IndexOf("?", StringComparison.Ordinal) == -1)
                        {
                            link.url = node.Attributes["URL"].Value + "?UnID=" + node.Attributes["UnID"].Value;
                        }
                        else
                        {
                            link.url = node.Attributes["URL"].Value + "&UnID=" + node.Attributes["UnID"].Value;
                        }
                    }

                    link.Target = node.Attributes["Target"].Value ;
                    link.Tooltip = node.Attributes["title"].Value ;
                    link.title = node.Attributes["title"].Value + openMessage;
                    break;


                case "text-File":
                    link.url = node.Attributes["URL"].Value;

                    link.Target = "_blank";
                    link.Tooltip = node.Attributes["title"].Value;
                    link.title = node.Attributes["title"].Value;
                    break;
                case "text-Publish":
                    if (node.Attributes["sClass"].Value == null || node.Attributes["sClass"].Value.ToString() == "")
                    {
                        link.url = link.url = urlHelper.Action("Index", "News", new { id = node.Attributes["UnID"].Value, Area = area });
                    }
                    else
                    {
                        link.url = urlHelper.Action("Index", "News", new { id = node.Attributes["UnID"].Value, sClass = node.Attributes["sClass"].Value, Area = area });

                    }

                    link.Target = "_self";
                    link.Tooltip = node.Attributes["title"].Value;
                    link.title = node.Attributes["title"].Value;
                    break;
                default:

                    link.url = urlHelper.Action("Index", node.Attributes["DataType"].Value, new { id = node.Attributes["UnID"].Value, Area = area });
                    link.Target = "_self";
                    link.Tooltip = node.Attributes["title"].Value;
                    link.title = node.Attributes["title"].Value;
                    break;

            }
            return link;
        }
        #endregion


        #region "取得類型"
        /// <summary>
        /// 取得類型
        /// </summary>
        /// <param name="DataTypeCode"></param>
        /// <returns></returns>
        public string GetDataTypeName(string DataTypeCode)
        {
            switch (DataTypeCode)
            {

                case "directory":
                    return "網站目錄";

                case "text-Edit":
                    return "編輯網站";

                case "text-Link":
                    return "超連結";

                case "text-Publish":
                    return "連結動態發佈系統";
                case "text-File":
                    return "檔案下載";
                case "text-Undefine":
                    return "未指定";
                default:
                    return DataTypeCode;
            }
        }
        #endregion




        #region "把XML 轉成DataTable 運算"
        /// <summary>
        /// 把XML 轉成DataTable 運算
        /// </summary>
        /// <returns></returns>
        public DataTable XmlToDataTable()
        {
            DataTable xmlTable = new DataTable();
            xmlTable.Columns.Add("ID", typeof(int));
            xmlTable.Columns.Add("PID", typeof(int));
            xmlTable.Columns.Add("Layer", typeof(int));
            xmlTable.Columns.Add("PathID", typeof(string));
            xmlTable.Columns.Add("MemberPath", typeof(string));
            xmlTable.Columns.Add("title", typeof(string));
            xmlTable.Columns.Add("URL", typeof(string)); //可以加欄位
            xmlTable.Columns.Add("DataType", typeof(string));
            xmlTable.Columns.Add("IsShow", typeof(bool));
            xmlTable.Columns.Add("IsOpen", typeof(bool));
            xmlTable.Columns.Add("SortString", typeof(string));
            xmlTable.Columns.Add("UnID", typeof(string));
            xmlTable.Columns.Add("Visible", typeof(int));
            xmlTable.Columns.Add("ModuleID", typeof(string));
            xmlTable.Columns.Add("poster", typeof(string));
            xmlTable.Columns.Add("posterUnit", typeof(string));
            xmlTable.Columns.Add("initDate", typeof(DateTime));
            xmlTable.Columns.Add("updater", typeof(string));
            xmlTable.Columns.Add("lastupdated", typeof(DateTime));
            xmlTable.Columns.Add("sClass", typeof(string));


            recursionXml(_xmlDoc, ref xmlTable);
            //加入root
            DataRow row = xmlTable.NewRow();
            row["ID"] = 0;
            row["PID"] = 0;
            row["Layer"] = 0;
            row["MemberPath"] = "";
            row["title"] = "網站架構";
            row["DataType"] = "root";
            row["URL"] = ""; //可以加欄位
            row["IsShow"] = true;
            row["IsOpen"] = true;
            row["SortString"] = "";
            row["UnID"] = 0;
            row["Visible"] = true;
            row["ModuleID"] = 0;
            row["poster"] = "";
            row["posterUnit"] = "";
            row["initDate"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            row["updater"] = "";
            row["lastupdated"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            row["sClass"] = "";



            xmlTable.Rows.InsertAt(row, 0);
            return xmlTable;
        }

        void recursionXml(XmlNode pnd, ref DataTable xmltable)
        {
            int pid = _i - 1;
            _intLayer += 1;
            string tmpPathID = "";
            string tmpMemberPath = "";
            int intSort = 0;//排序編號
            foreach (XmlNode node in pnd.ChildNodes)
            {
                if (!node.HasChildNodes) //子節點
                {
                    if (node.Name == "siteMapNode")
                    {
                        DataRow row = xmltable.NewRow();
                        row["ID"] = _i;
                        row["PID"] = pid;
                        row["Layer"] = _intLayer - 2;
                        row["PathID"] = _PathID + "/" + node.Attributes["title"].Value;
                        if (string.IsNullOrEmpty(_MemberPath))
                        {
                            row["MemberPath"] = intSort;
                        }
                        else
                        {
                            row["MemberPath"] = _MemberPath + "." + intSort;
                        }
                        row["title"] = node.Attributes["title"].Value;
                        if (node.Attributes["URL"] != null)
                        {
                            row["URL"] = node.Attributes["URL"].Value;
                        }
                        if (node.Attributes["sClass"] != null)
                        {
                            row["sClass"] = node.Attributes["sClass"].Value;
                        }

                        row["DataType"] = node.Attributes["DataType"].Value;
                        if (node.Attributes["DataType"].Value == "directory")
                        {
                            if (_intLayer - 2 == 1)
                            {
                                row["IsShow"] = true;
                                row["IsOpen"] = true;
                            }
                            else
                            {
                                row["IsShow"] = false;
                                row["IsOpen"] = false;
                            }
                        }
                        else
                        {
                            if (_intLayer - 2 == 1)
                            {
                                row["IsShow"] = true;
                                row["IsOpen"] = false;
                            }
                            else
                            {
                                row["IsShow"] = false;
                                row["IsOpen"] = false;
                            }

                        }
                        if (intSort == pnd.ChildNodes.Count - 1)
                        {
                            row["SortString"] = "Last";
                        }
                        else
                        {
                            row["SortString"] = intSort.ToString();
                        }
                        if (pnd.ChildNodes.Count == 1)
                        {
                            row["SortString"] = "OnlyOne";
                        }
                        row["UnID"] = node.Attributes["UnID"].Value;
                        row["Visible"] = node.Attributes["Visible"].Value;
                        //row["ModuleID"] = node.Attributes["ModuleID"].Value;
                        row["poster"] = node.Attributes["poster"].Value;
                        row["initDate"] = node.Attributes["initDate"].Value;
                        row["updater"] = node.Attributes["updater"].Value;
                        row["lastupdated"] = node.Attributes["lastupdated"].Value;

                        xmltable.Rows.Add(row);


                        _i++;
                    }
                }
                else //目錄
                {
                    if (node.Name == "siteMapNode")
                    {
                        DataRow row = xmltable.NewRow();
                        row["ID"] = _i;
                        row["PID"] = pid;
                        row["Layer"] = _intLayer - 2;
                        row["PathID"] = _PathID + "/" + node.Attributes["title"].Value;
                        if (string.IsNullOrEmpty(_MemberPath))
                        {
                            row["MemberPath"] = intSort;
                        }
                        else
                        {
                            row["MemberPath"] = _MemberPath + "." + intSort;
                        }
                        row["title"] = node.Attributes["title"].Value;
                        //if (node.Attributes["URL"] == null)
                        //{
                        //    row["URL"] = node.Attributes["URL"].Value;
                        //}
                        row["DataType"] = node.Attributes["DataType"].Value;
                        if (_intLayer - 2 == 1)
                        {
                            row["IsShow"] = true;
                            row["IsOpen"] = false;
                        }
                        else
                        {
                            row["IsShow"] = false;
                            row["IsOpen"] = false;
                        }

                        if (intSort == pnd.ChildNodes.Count - 1)
                        {
                            row["SortString"] = "Last";
                        }
                        else
                        {
                            row["SortString"] = intSort.ToString();
                        }
                        if (pnd.ChildNodes.Count == 1)
                        {
                            row["SortString"] = "OnlyOne";
                        }
                        row["UnID"] = node.Attributes["UnID"].Value;
                        row["Visible"] = node.Attributes["Visible"].Value;
                        //row["ModuleID"] = node.Attributes["ModuleID"].Value;
                        row["poster"] = node.Attributes["poster"].Value;
                        row["initDate"] = node.Attributes["initDate"].Value;
                        row["updater"] = node.Attributes["updater"].Value;
                        row["lastupdated"] = node.Attributes["lastupdated"].Value;
                        xmltable.Rows.Add(row);
                        _i++;
                        tmpPathID = _PathID;
                        tmpMemberPath = _MemberPath;
                        _PathID += "/" + node.Attributes["title"].Value;
                        if (string.IsNullOrEmpty(_MemberPath))
                        {
                            _MemberPath = intSort.ToString();
                        }
                        else
                        {
                            _MemberPath += "." + intSort;
                        }

                    }
                    recursionXml(node, ref xmltable);
                    if (node.Name == "siteMapNode")
                    {
                        _PathID = tmpPathID;
                        _MemberPath = tmpMemberPath;
                    }
                    _intLayer -= 1;
                }
                intSort++;
            }

        }
        #endregion

        #region "取得GetWebPath"
        public string GetWebPath()
        {
            return webSite.Path;
        }
        #endregion

        #region "取得GetWebPath"
        public string GetAlias()
        {
            return webSite.SubjectEn;
        }
        #endregion
    }


}