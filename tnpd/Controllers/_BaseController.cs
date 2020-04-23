﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using MvcPaging;
using System.Web.Mvc;
using TnpdModels;

using System.Web.Security;
using Newtonsoft.Json;
using System.Web.Routing;
using System.IO;
using  Microsoft.Security.Application;

namespace tnpd.Controllers
{
    public class _BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ProcessUploadFileDirectory();   //檔案上傳目錄處理
        }

        #region UploadFile

        /// <summary>
        /// 檔案上傳目錄處理
        /// </summary>
        private void ProcessUploadFileDirectory() 
        {
            string controllerName = this.GetType().Name.Replace("Controller", "");
            controllerName = controllerName.Replace("Files", "");

            string uploadFilePathRoot = Server.MapPath("~/upfiles/files/");
            if (!Directory.Exists(uploadFilePathRoot))
            {
                DirectoryInfo di = Directory.CreateDirectory(uploadFilePathRoot);
            }

            string uploadFilePath = Path.Combine(uploadFilePathRoot, controllerName);
            if (!Directory.Exists(uploadFilePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(uploadFilePath);
            }
            //ViewData["basePath"] = string.Format("/upfiles/files/{0}/", controllerName);

            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                string uploadFilePathUser = Path.Combine(uploadFilePath, userName);
                if (!Directory.Exists(uploadFilePathUser))
                {
                    DirectoryInfo di = Directory.CreateDirectory(uploadFilePathUser);
                }

                ViewData["startupPath"] = string.Format("Files:/{0}/{1}/",controllerName, userName);
            }        
        }


        /// <summary>
        /// 檔案上傳目錄處理(局長信箱)
        /// </summary>
        public string getUploadFileMailboxDirectory(string controllerName = "Mailbox")
        {
            string uploadFilePathRoot = Server.MapPath("~/upfiles/files/");
            if (!Directory.Exists(uploadFilePathRoot))
            {
                DirectoryInfo di = Directory.CreateDirectory(uploadFilePathRoot);
            }

            string uploadFilePath = Path.Combine(uploadFilePathRoot, controllerName);
            if (!Directory.Exists(uploadFilePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(uploadFilePath);
            }

            string uploadFilePathUser = Path.Combine(uploadFilePath, DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(uploadFilePathUser))
            {
                DirectoryInfo di = Directory.CreateDirectory(uploadFilePathUser);
            }

            return uploadFilePathUser;
        }

        #endregion


        //#region Member

        //public Member getLoginMember()
        //{
        //    //取得UserData
        //    string strUserData = Utility.GetAuthenTicket();
        //    Member member = JsonConvert.DeserializeObject<Member>(strUserData);
        //    return member;
        //}

        //public int getLoginUserID()
        //{
        //    Member member = getLoginMember();
        //    return member.Id;
        //}

        //#endregion


        #region 搜尋暫存
        /// <summary>
        /// 取得正確的頁面
        /// </summary>
        /// <param name="page">若-1時需要還原page</param>
        /// <param name="fc">搜尋時時page=0(第一頁)</param>
        /// <param name="customKey">自訂key</param>
        /// <returns></returns>
        public int getCurrentPage(int? page, FormCollection fc, string customKey = null, bool? IsSearch = null)
        {
            if (fc != null && fc.Count > 0 && (IsSearch.HasValue && IsSearch==true)　)
            {
                return 0;
            }


            string Page = string.Format("{0}Page", customKey);

            if (page.HasValue && page == -1)
            {

                if (ViewData[Page] != null && !string.IsNullOrEmpty(ViewData[Page].ToString()))
                {
                    int pageVD = 0;
                    int.TryParse(ViewData[Page].ToString(), out pageVD);
                    page = pageVD;
                }
            }

            return page.HasValue && page != -1 ? page.Value - 1 : 0;
        }

        /// <summary>
        /// 取得CacheKey
        /// </summary>
        /// <returns></returns>
        private string getCacheKey()
        {
            //string cacheKey = string.Format("{0}_{1}_{2}", HttpContext.Session.SessionID, this.GetType().Name, ControllerContext.RouteData.Values["action"]);
            string cacheKey = string.Format("{0}_{1}_{2}", "SessionID", this.GetType().Name, ControllerContext.RouteData.Values["action"]);
            return cacheKey;
        }

        /// <summary>
        /// CacheKey+自訂key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string getCacheKey(string key)
        {
            //string cacheKey = string.Format("{0}_{1}_{2}", HttpContext.Session.SessionID, this.GetType().Name, ControllerContext.RouteData.Values["action"]);
            string cacheKey = string.Format("{0}_{1}_{2}", "SessionID", this.GetType().Name, ControllerContext.RouteData.Values["action"]);
            return string.Format("{0}_{1}", cacheKey, key);
        }

        /// <summary>
        /// 記住搜尋條件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="fc"></param>
        /// <param name="customKey">自訂key</param>
        protected void GetCatcheRoutes(int? page, FormCollection fc, string customKey = null, bool? IsSearch = null)
        {
            string SearchBy = string.Format("{0}SearchBy", customKey);
            string Page = string.Format("{0}Page", customKey);

            if (fc != null && fc.Count > 0)
            {
                //搜尋時
                foreach (var key in fc.AllKeys)
                {
                    if (key.StartsWith(SearchBy))
                    {
                        string cacheKey = getCacheKey(key);
                        //HttpContext.Cache[cacheKey] = fc[key];
                        Session[cacheKey] = Sanitizer.GetSafeHtmlFragment(fc[key]) ;
                        ViewData[key] = Sanitizer.GetSafeHtmlFragment(fc[key]);
                    }
                }


                string pageKey = getCacheKey(Page);
                //HttpContext.Cache[pageKey] = 1;
                if (IsSearch.HasValue && IsSearch == false)
                {
                    setPage(Page, page);          //設定page
                }
                else
                {
                    Session[pageKey] = 1;
                    ViewData[Page] = 1;

                }
            }
            else
            {
                setPage(Page, page);          //設定page

                string cacheKey = getCacheKey(SearchBy);
                string pageKey = getCacheKey(Page);
                //var keysToSearch = (from System.Collections.DictionaryEntry dict in HttpContext.Cache
                //                    let key = dict.Key.ToString()
                //                    where key.StartsWith(cacheKey) || key.Equals(pageKey)
                //                    select key).ToList();

                foreach (string key in Session.Keys)
                {
                    if(key.StartsWith(cacheKey) || key.Equals(pageKey))
                    {
                        string oKey = getCacheKey() + "_";
                        string vkey = key.Replace(oKey, "");
                        //ViewData[vkey] = HttpContext.Cache[key];
                        ViewData[vkey] = Session[key];
                    }

                }

            }
        }

        /// <summary>
        /// 設定page
        /// </summary>
        /// <param name="pageKeyName"></param>
        /// <param name="page"></param>
        private void setPage(string pageKeyName, int? page)
        {
            if (page.HasValue && page != -1)
            {
                string pageKey = getCacheKey(pageKeyName);
                //HttpContext.Cache[pageKey] = page;
                Session[pageKey] = page;
                ViewData[pageKeyName] = page;
            }
        }
        #endregion

        #region ViewData

        /// <summary>
        /// 取得ViewData，回傳時為int
        /// </summary>
        /// <param name="viewDataKey"></param>
        /// <returns></returns>
        public int getViewDateInt(string viewDataKey)
        {
            int id = 0;
            int.TryParse(ViewData[viewDataKey].ToString(), out id);
            return id;
        }

        /// <summary>
        /// 取得ViewData，回傳時為String
        /// </summary>
        /// <param name="viewDataKey"></param>
        /// <returns></returns>
        public string getViewDateStr(string viewDataKey)
        {
            string title = ViewData[viewDataKey].ToString();
            return title;
        }

        public DateTime getViewDateDateTime(string viewDataKey)
        {
            string title = ViewData[viewDataKey].ToString();

            return DateTime.Parse(title);
        }

        /// <summary>
        /// 是否有ViewData
        /// </summary>
        /// <param name="viewDataKey"></param>
        /// <returns></returns>
        public bool hasViewData(string viewDataKey)
        {
            if (ViewData[viewDataKey] != null && !string.IsNullOrEmpty(ViewData[viewDataKey].ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region string id to string[]

        /// <summary>
        /// 將 1,2,3,4 轉成 字串陣列
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string[] StringToIDs(FormCollection fc, string Key)
        {
            string sID = fc[Key] == null ? "" : fc[Key];
            string[] IDs = sID.Split(',');
            return IDs;
        }
        
        #endregion

        #region 共用項目

        /// <summary>
        /// 取得信件編號
        /// </summary>
        /// <returns></returns>
        //public string getNewSN()
        //{
        //    string returnVal = "";
        //    CypdKnowledgeModels.Models.BackendContext dbKM = new CypdKnowledgeModels.Models.BackendContext();

        //    string maxSN = dbKM.Mailbox.Max(d => d.SN);
        //    if (string.IsNullOrEmpty(maxSN))
        //    {
        //        maxSN = "198304130001";
        //    }

        //    string maxDate = maxSN.Substring(0, 8);
        //    string maxNo = maxSN.Substring(8,4);

        //    string nowDate = DateTime.Now.ToString("yyyyMMdd");

        //    if (maxDate == nowDate)
        //    {
        //        int maxNoInt= int.Parse(maxNo);
        //        returnVal = string.Format("{0}{1:0000}", nowDate, ++maxNoInt);
        //    }
        //    else 
        //    {
        //        returnVal = string.Format("{0}{1:0000}", nowDate, 1);
        //    }


        //    return returnVal;
        //}


        /// <summary>
        /// 取得信件編號
        /// </summary>
        /// <returns></returns>
        //public string getNewSN2()
        //{
        //    string returnVal = "";
        //    CypdKnowledgeModels.Models.BackendContext dbKM = new CypdKnowledgeModels.Models.BackendContext();

        //    string maxSN = dbKM.OnLineReport.Max(d => d.SN);
        //    if (string.IsNullOrEmpty(maxSN))
        //    {
        //        maxSN = "198304130001";
        //    }

        //    string maxDate = maxSN.Substring(0, 8);
        //    string maxNo = maxSN.Substring(8, 4);

        //    string nowDate = DateTime.Now.ToString("yyyyMMdd");

        //    if (maxDate == nowDate)
        //    {
        //        int maxNoInt = int.Parse(maxNo);
        //        returnVal = string.Format("{0}{1:0000}", nowDate, ++maxNoInt);
        //    }
        //    else
        //    {
        //        returnVal = string.Format("{0}{1:0000}", nowDate, 1);
        //    }


        //    return returnVal;
        //}

        /// <summary>
        /// 取得驗證碼
        /// </summary>
        /// <returns></returns>
        public string getVerCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        #endregion
    }

}
