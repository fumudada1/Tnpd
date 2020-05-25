using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using TnpdModels;

namespace TransformNewsCatalogs
{
    class Program
    {
        private static BackendContext _db = new BackendContext();
        static void Main(string[] args)
        {
            ////最新消息
            //TransformNewsCatalogs("BulletinClass", 1);
            //TransformNews("Bulletin", 1);
            //TransformNewsCatalogNews("BulletinPoster", 1);
            //TransformNewsImages("BulletinFile", 1, "/images/pubtncgb/bulletin/");
            ////破案消息
            //TransformNewsCatalogs("ReportClass", 3);
            //TransformNews("Report", 3);
            //TransformNewsCatalogNews("ReportPoster", 3);
            //TransformNewsImages("ReportFile", 3, "/images/pubtncgb/comm/");
            ////榮譽榜
            //TransformNewsCatalogs("CrimeClass", 5);
            //TransformNews("Crime", 5);
            //TransformNewsCatalogNews("CrimePoster", 5);
            //TransformNewsImages("CrimeFile", 5, "/images/pubtncgb/comm/");
            ////好人好事
            //TransformNewsCatalogs("HonorClass", 6);
            //TransformNews("Honor", 6);
            //TransformNewsCatalogNews("HonorPoster", 6);
            //TransformNewsImages("HonorFile", 6, "/images/pubtncgb/comm/");
            ////活動相簿
            //TransformNewsCatalogs("PictureClass", 7);
            //TransformNews("Picture", 7);
            //TransformNewsCatalogNews("PicturePoster", 7);
            //TransformNewsImages("PictureFile", 7, "/images/pubtncgb/comm/");
            //// 宣導影片
            //TransformNewsCatalogs("VideoClass", 16);
            //TransformNews("Video", 16);
            //TransformNewsCatalogNews("VideoPoster", 16);
            //TransformNewsImages("VideoFile", 16, "/images/pubtncgb/comm/");

            //// 大綜合
            //TransformNewsCatalogsG("GuidanceClass");
            //TransformNews("Guidance", 17);
            //TransformNewsCatalogNews("GuidancePoster", 17);
            //TransformNewsImages("GuidanceFile", 17, "/images/pubtncgb/comm/");

            //// 表單下載
            //TransformNewsCatalogs("FormClass", 14);
            //TransformNews("Form", 14);
            //TransformNewsCatalogNews("FormPoster", 14);
            //TransformNewsImages("FormFile", 14, "/images/pubtncgb/comm/");

            //// 活動訊息
            //TransformNewsCatalogs("ActivityClass", 39);
            //TransformNews("Activities", 39);
            //TransformNewsCatalogNews("ActivityPoster", 39);
            //TransformNewsImages("ActivityFile", 39, "/images/pubtncgb/comm/");

            ////政府公開資訊
            //TransformNewsCatalogs("PGOVClass", 18);
            //TransformNews("PGOV", 18);
            //TransformNewsCatalogNews("PGOVPoster", 18);
            //TransformNewsImages("PGOVFile", 18, "/images/pubtncgb/comm/");

            ////個資保護專區
            //TransformNewsCatalogs("PrivateClass", 15);
            //TransformNews("priv", 15);
            //TransformNewsCatalogNews("privPoster", 15);
            //TransformNewsImages("privFile", 15, "/images/pubtncgb/comm/");

            ////地方自治法規
            //TransformNewsCatalogs("LawClass", 19);
            //TransformNews("LawData", 19);
            //TransformNewsCatalogNews("LawDataPoster", 19);
            //TransformNewsImages("LawFile", 19, "/images/pubtncgb/law/");

            ////常見問答
            //TransformNewsCatalogs("FaqClass", 58);
            //TransformNews("Faq", 58);
            //TransformNewsCatalogNews("FaqPoster", 58);
            //TransformNewsImages("FaqFile", 58, "/images/pubtncgb/comm/");

            //網網相連
            //TransformAboutLinkCategory();
            //TransformAboutLink();


            ////申辦服務	
            //TransformEseCatalogs();
            //TransformEse();
            //TransformEseCatalogEses();
            //TransformEseFiles();

            //首頁相關連結
            //TransformHomeLinks();

            //警局地址
            //TransformPolice();
            //TransformPoliceFile();

            //各區近半年婦幼安全警示地點
            //TransformWanda();

            //景點(新)
            TransformSenceClass("SenceClass", 46);
            TransformSence("Sence", 46);
            TransformNewsImages("SenceFile", 46, "/images/pubtncgb/police/");

            //轉碼
            //TransformHtmlToText();

            //分離單位

            //CutNewsUnits();

            //開啟行政類、法治類、交通類
        }

        private static void CutNewsUnits()
        {
            var newses = _db.Newses.Where(x => x.OwnWebSiteId != 1 && x.NewsCatalogs.Count(y => y.WebCategoryId == 18) > 0).ToList();
            var websites = _db.WebSiteNames.ToList();
            foreach (var news in newses)
            {
                //if (news.Id == 27279)
                //{
                    var webSite = websites.FirstOrDefault(x => x.UnitId == news.OwnWebSiteId && x.Language == LanguageType.中文版);
                    if (news.NewsCatalogs.Count > 1)
                    {
                        for (int i = news.NewsCatalogs.Count - 1; i >= 0; i--)
                        {
                            var catalog = news.NewsCatalogs.ElementAt(i);
                            if (catalog.WebSiteId != webSite.Id)
                            {
                                Console.WriteLine("移除類別" + news.Id + "-" + news.Subject);
                                NewsCatalog catalog1 = _db.NewsCatalogs.Find(catalog.Id);
                                catalog1.Newses.Remove(news);
                                _db.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        for (int i = news.NewsCatalogs.Count - 1; i == 0; i--)
                        {
                            var catalog = news.NewsCatalogs.ElementAt(i);
                            if (catalog.WebSiteId != webSite.Id)
                            {
                                // news.NewsCatalogs.Remove(catalog);
                                Console.WriteLine(news.Id + "-" + news.Subject);
                            }

                        }
                    }
                //}
                

            }
            _db.SaveChanges();
            Console.WriteLine("over");
            Console.ReadKey();
        }

        private static void TransformHtmlToText()
        {
            var tempNews = _db.Newses.Find(6545);
            string aa = WebUtility.HtmlDecode(tempNews.Article);
            char b = aa[7];

            var news = _db.Newses.Where(x => x.Article.Contains("&lt")).ToList();
            foreach (var item in news)
            {
                item.Article = WebUtility.HtmlDecode(item.Article);
                item.Article = item.Article.Replace(b.ToString(), " ");
            }
            _db.SaveChanges();

        }

        private static void Transroaddata()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from roaddata";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var regions = _db.TrafficRegions.ToList();
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                TrafficRoaddata roaddata = new TrafficRoaddata();
                TrafficRegion region = regions.FirstOrDefault(x => x.Subject == dataRow["area"].ToString());
                roaddata.RegionId = region.Id;
                roaddata.Subject = dataRow["road"].ToString();
                _db.TrafficRoaddatas.Add(roaddata);
            }
            _db.SaveChanges();
        }

        static void TransformSenceClass(string tableName, int webCategoryId)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName + " where Serno='201104080001'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var websitenames = _db.WebSiteNames.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //資料要改名字 中文網改總局  社區治安中文網改社區治安服務網



                NewsCatalog catalog = new NewsCatalog();
                string Serno = dataRow["Serno"].ToString();
                catalog.Serno = Serno;
                catalog.ListNum = 0;
                catalog.MetaId = 1;
                catalog.Subject = dataRow["ClassName"].ToString();
                catalog.WebCategoryId = webCategoryId;
                catalog.Poster = dataRow["PostName"].ToString();
                catalog.initOrg = dataRow["PubUnitName"].ToString();


                catalog.WebSiteId = 23;



                catalog.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                catalog.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                _db.NewsCatalogs.Add(catalog);


            }
            _db.SaveChanges();
        }

        private static void TransformSence(string tableName, int webCategoryId)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName + " where  Mserno='201104080001'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                string Mserno = dataRow["Mserno"].ToString();
                NewsCatalog catalog = _db.NewsCatalogs.FirstOrDefault(x => x.Serno == Mserno);

                News news = new News();
                news.Serno = dataRow["Serno"].ToString();
                news.Subject = dataRow["Subject"].ToString();
                news.Article = Txt2Html(dataRow["Content"].ToString());

                var WebSiteNames = _db.WebSiteNames.ToList();
                string PubUnitDN = dataRow["PubUnitDN"].ToString();
                if (PubUnitDN.IndexOf("tncgb01") >= 0)
                {
                    news.OwnWebSiteId = 1;
                }
                else
                {
                    var site = WebSiteNames.FirstOrDefault(x => x.PubUnitDN == PubUnitDN);
                    news.OwnWebSiteId = site.UnitId;
                }

                if (!string.IsNullOrEmpty(dataRow["ReadNumber"].ToString()))
                {
                    news.Views = Convert.ToInt32(dataRow["ReadNumber"]);
                }
                news.WebCategoryId = webCategoryId;
                news.Poster = dataRow["PostName"].ToString();
                news.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                news.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                news.initOrg = dataRow["PubUnitName"].ToString();
                _db.Newses.Add(news);
                string RelateUrl = dataRow["RelateUrl"].ToString();
                string RelateName = dataRow["RelateName"].ToString();
                if (RelateUrl != "http://" && RelateUrl != "")
                {
                    NewsLink newsLink = new NewsLink();
                    newsLink.LinkUrl = RelateUrl;
                    newsLink.ListNum = 0;
                    newsLink.Subject = RelateName;
                    if (string.IsNullOrEmpty(RelateName))
                    {
                        newsLink.Subject = RelateUrl;
                    }

                    newsLink.Target = TargetType.另開視窗;
                    newsLink.WebCategoryId = webCategoryId;
                    news.NewsLinks.Add(newsLink);
                }
                _db.SaveChanges();
                if (catalog != null)
                {
                    TransformNewsCatalogNews(catalog.Id, news.Id);
                }


            }

        }

        private static void TransformWanda()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from safe02 where StartUsing=1";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<StationInfo> stationInfos = _db.StationInfos.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                StationInfo station = stationInfos.FirstOrDefault(x => x.Subject == dataRow["unitName"].ToString());
                if (station != null)
                {
                    Wanda wanda = new Wanda();
                    wanda.StationInfoId = station.Id;
                    wanda.Subject = dataRow["Subject"].ToString();
                    wanda.beginning = dataRow["roads"].ToString();
                    wanda.destination = dataRow["roade"].ToString();
                    wanda.Article = Txt2Html(dataRow["Content"].ToString());
                    wanda.Poster = dataRow["PostName"].ToString();
                    wanda.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                    wanda.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                    var WebSiteNames = _db.WebSiteNames.ToList();
                    string PubUnitDN = dataRow["PubUnitDN"].ToString();
                    if (PubUnitDN.IndexOf("tncgb01") >= 0)
                    {
                        wanda.OwnWebSiteId = 1;
                    }
                    else
                    {
                        var site = WebSiteNames.FirstOrDefault(x => x.PubUnitDN == PubUnitDN);
                        wanda.OwnWebSiteId = site.UnitId;
                    }



                    if (dataRow["StartUsing"].ToString() == "1")
                    {
                        wanda.Status = BooleanType.是;
                    }
                    else
                    {
                        wanda.Status = BooleanType.否;
                    }

                    _db.Wandas.Add(wanda);
                    _db.SaveChanges();


                }


            }
        }


        private static void TransformPoliceFile()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from PoliceFile";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<StationInfo> stationInfos = _db.StationInfos.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                StationInfo news = stationInfos.FirstOrDefault(x => x.Serno == dataRow["Serno"].ToString());
                if (news != null)
                {
                    if (!string.IsNullOrEmpty(dataRow["ExpFile"].ToString().Trim()))
                    {
                        if (dataRow["flag"].ToString() == "pic")
                        {
                            StationInfoFIles file = new StationInfoFIles();
                            file.StationInfoId = news.Id;
                            file.ListNum = 0;
                            file.Subject = dataRow["ExpFile"].ToString();
                            file.UpFile = "/images/pubtncgb/police/" + dataRow["ServerFile"].ToString();
                            file.Poster = dataRow["PostName"].ToString();
                            file.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                            file.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());

                            _db.StationInfoFIleses.Add(file);
                            _db.SaveChanges();
                        }

                    }

                }


            }
        }

        private static void TransformPolice()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from police";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<StationInfo> stationInfos = _db.StationInfos.ToList();
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                StationInfo station = stationInfos.FirstOrDefault(x => x.Subject == dataRow["Subject"].ToString());
                if (station != null)
                {
                    station.Article = Txt2Html(dataRow["Content"].ToString());
                    station.Serno = dataRow["Serno"].ToString();
                }
                else
                {
                    StationInfo stationInfo = new StationInfo();
                    stationInfo.Article = Txt2Html(dataRow["Content"].ToString());
                    stationInfo.Serno = dataRow["Serno"].ToString();
                    stationInfo.Subject = dataRow["Subject"].ToString();
                    stationInfo.Address = dataRow["SecSubject"].ToString();
                    stationInfo.ListNum = 0;

                    var WebSiteNames = _db.WebSiteNames.ToList();
                    string PubUnitDN = dataRow["PubUnitDN"].ToString();
                    if (PubUnitDN.IndexOf("tncgb01") >= 0)
                    {
                        stationInfo.OwnWebSiteId = 1;
                    }
                    else
                    {
                        var site = WebSiteNames.FirstOrDefault(x => x.PubUnitDN == PubUnitDN);
                        stationInfo.OwnWebSiteId = site.UnitId;
                    }

                    stationInfo.Poster = dataRow["PostName"].ToString();
                    stationInfo.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                    stationInfo.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                    stationInfo.initOrg = dataRow["PubUnitName"].ToString();
                    _db.StationInfos.Add(stationInfo);
                }

                _db.SaveChanges();

            }
        }


        private static void TransformHomeLinks()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT  BannerRight.*, BannerRightPoster.WebSiteName FROM BannerRight LEFT OUTER JOIN BannerRightPoster ON BannerRight.Serno = BannerRightPoster.Serno";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var websitenames = _db.WebSiteNames.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //資料要改名字 中文網改總局  社區治安中文網改社區治安服務網
                string webSiteName = dataRow["WebSiteName"].ToString();
                var item = websitenames.FirstOrDefault(x => webSiteName.Contains(x.Subject));

                HomeLink link = new HomeLink();

                link.ListNum = Convert.ToInt32(dataRow["Fsort"].ToString());
                link.DataType = Convert.ToInt32(dataRow["DataType"].ToString());

                if (dataRow["StartUsing"].ToString() == "1")
                {
                    link.Enable = BooleanType.是;
                }
                else
                {
                    link.Enable = BooleanType.否;
                }
                link.UpImage = "/images//banner/" + dataRow["ServerFile"].ToString();
                link.Url = dataRow["RelateUrl"].ToString();
                link.Subject = dataRow["Subject"].ToString();

                link.Poster = dataRow["PostName"].ToString();
                link.initOrg = dataRow["PubUnitName"].ToString();

                if (item != null)
                {
                    link.WebSiteNameId = item.Id;
                }



                if (dataRow["WebSiteName"].ToString() == "婦幼園地中文網")
                {
                    link.WebSiteNameId = 21;
                }
                if (dataRow["WebSiteName"].ToString() == "中文網")
                {
                    link.WebSiteNameId = 1;
                }
                if (dataRow["WebSiteName"].ToString() == "社區治安中文網")
                {
                    link.WebSiteNameId = 24;
                }


                link.StartDate = GetDateTime(dataRow["PosterDate"].ToString());
                if (!string.IsNullOrEmpty(dataRow["CloseDate"].ToString()))
                {
                    link.EndDate = GetDateTime(dataRow["CloseDate"].ToString());
                }


                var WebSiteNames = _db.WebSiteNames.ToList();
                string PubUnitDN = dataRow["PubUnitDN"].ToString();
                if (PubUnitDN.IndexOf("tncgb01") >= 0)
                {
                    link.OwnWebSiteId = 1;
                }
                else
                {
                    var site = WebSiteNames.FirstOrDefault(x => x.PubUnitDN == PubUnitDN);
                    link.OwnWebSiteId = site.UnitId;
                }


                link.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                link.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                _db.HomeLinks.Add(link);


            }
            _db.SaveChanges();
        }

        private static void TransformEseFiles()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from EseFile where CreateDate > '20180830'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var newses = _db.Eses.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                Ese news = newses.FirstOrDefault(x => x.Serno == dataRow["Serno"].ToString());
                if (news != null)
                {
                    if (!string.IsNullOrEmpty(dataRow["ExpFile"].ToString().Trim()))
                    {
                        if (dataRow["flag"].ToString() == "doc")
                        {
                            EseFile file = new EseFile();
                            file.EseId = news.Id;
                            file.ListNum = 0;
                            file.Subject = dataRow["ExpFile"].ToString();
                            file.UpFile = "/images/pubtncgb/apply/" + dataRow["ServerFile"].ToString();
                            file.Poster = dataRow["PostName"].ToString();
                            file.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                            file.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());

                            _db.EseFiles.Add(file);
                            _db.SaveChanges();

                        }

                    }

                }


            }
        }

        private static void TransformEseCatalogEses()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlConnection conn1 = new SqlConnection(GetConnectionStringByName("TnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from EsePoster";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var newses = _db.Eses.ToList();

            var nwsCatalogs = _db.EseCatalogs.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                string Serno = dataRow["Serno"].ToString();
                string Mserno = dataRow["Mserno"].ToString();

                var news = newses.FirstOrDefault(x => x.Serno == Serno);
                var newsCatalog = nwsCatalogs.FirstOrDefault(x => x.Serno == Mserno);
                if (news != null && newsCatalog != null)
                {
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = conn1;
                    command1.CommandText = "INSERT INTO EseCatalogEses ([EseCatalog_Id],[Ese_Id]) VALUES (" + newsCatalog.Id + "," + news.Id + ")";
                    conn1.Open();
                    try
                    {
                        command1.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        conn1.Close();
                    }


                }


            }
        }


        private static void TransformEse()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from Ese where CreateDate > '20180830'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                Ese ese = new Ese();
                ese.Serno = dataRow["Serno"].ToString();
                ese.Subject = dataRow["Subject"].ToString();
                ese.Memo = Txt2Html(dataRow["Content"].ToString());
                ese.Apply_method = dataRow["Apply_method"].ToString();
                ese.Fax = dataRow["Fax"].ToString();
                ese.Mail = dataRow["Mail"].ToString();
                ese.NeedDoc = dataRow["NeedDoc"].ToString();
                ese.Online = dataRow["Online"].ToString();
                ese.Pdate = dataRow["Pdate"].ToString();
                ese.Process = dataRow["Process"].ToString();
                ese.Remark = dataRow["Remark"].ToString();
                ese.Undertaking_Unit = dataRow["Undertaking_Unit"].ToString();
                if (dataRow["Startusing"].ToString() == "1")
                {
                    ese.Status = BooleanType.是;
                }
                else
                {
                    ese.Status = BooleanType.否;
                }
                ese.Apply_method = dataRow["Apply_method"].ToString();

                ese.Tel = dataRow["Tel"].ToString();



                if (!string.IsNullOrEmpty(dataRow["ReadNumber"].ToString()))
                {
                    ese.Views = Convert.ToInt32(dataRow["ReadNumber"]);
                }
                var WebSiteNames = _db.WebSiteNames.ToList();
                string PubUnitDN = dataRow["PubUnitDN"].ToString();
                if (PubUnitDN.IndexOf("tncgb01") >= 0)
                {
                    ese.OwnWebSiteId = 1;
                }
                else
                {
                    var site = WebSiteNames.FirstOrDefault(x => x.PubUnitDN == PubUnitDN);
                    ese.OwnWebSiteId = site.UnitId;
                }

                ese.Poster = dataRow["PostName"].ToString();
                ese.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                ese.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                ese.initOrg = dataRow["PubUnitName"].ToString();
                _db.Eses.Add(ese);
                _db.SaveChanges();

            }

        }


        private static void TransformEseCatalogs()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from EseClass where CreateDate > '20180830'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var websitenames = _db.WebSiteNames.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //資料要改名字 中文網改總局  社區治安中文網改社區治安服務網
                string webSiteName = dataRow["WebSiteName"].ToString();
                var item = websitenames.FirstOrDefault(x => webSiteName.Contains(x.Subject));

                EseCatalog catalog = new EseCatalog();
                string Serno = dataRow["Serno"].ToString();
                catalog.Serno = Serno;
                catalog.ListNum = 0;
                catalog.MetaId = 1;
                catalog.Subject = dataRow["ClassName"].ToString();
                catalog.Poster = dataRow["PostName"].ToString();
                catalog.initOrg = dataRow["PubUnitName"].ToString();

                if (item != null)
                {
                    catalog.WebSiteId = item.Id;
                }


                if (dataRow["WebSiteName"].ToString() == "婦幼園地中文網")
                {
                    catalog.WebSiteId = 21;
                }
                if (dataRow["WebSiteName"].ToString() == "中文網")
                {
                    catalog.WebSiteId = 1;
                }
                if (dataRow["WebSiteName"].ToString() == "社區治安中文網")
                {
                    catalog.WebSiteId = 24;
                }

                catalog.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                catalog.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                _db.EseCatalogs.Add(catalog);


            }
            _db.SaveChanges();
        }

        static void TransformAboutLinkCategory()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT  departmentdetail.unitname as WebSiteName, LinkClass.* FROM   LinkClass LEFT OUTER JOIN departmentdetail ON LinkClass.PubUnitDn = departmentdetail.unitdn";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var websitenames = _db.WebSiteNames.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //資料要改名字 中文網改總局  社區治安中文網改社區治安服務網
                string webSiteName = dataRow["WebSiteName"].ToString();
                var item = websitenames.FirstOrDefault(x => webSiteName.Contains(x.Subject));

                AboutLinkCatalog catalog = new AboutLinkCatalog();
                string Serno = dataRow["Serno"].ToString();
                catalog.Serno = Serno;
                catalog.ListNum = 0;
                catalog.Subject = dataRow["ClassName"].ToString();

                catalog.Poster = dataRow["PostName"].ToString();
                catalog.initOrg = dataRow["PubUnitName"].ToString();

                if (item != null)
                {
                    catalog.WebSiteId = item.Id;
                }
                else
                {
                    catalog.WebSiteId = 1;
                }


                if (dataRow["WebSiteName"].ToString() == "婦幼園地中文網")
                {
                    catalog.WebSiteId = 21;
                }
                if (dataRow["WebSiteName"].ToString() == "中文網")
                {
                    catalog.WebSiteId = 1;
                }
                if (dataRow["WebSiteName"].ToString() == "社區治安中文網")
                {
                    catalog.WebSiteId = 24;
                }

                catalog.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                catalog.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                _db.AboutLinkCatalogs.Add(catalog);


            }
            _db.SaveChanges();
        }

        private static void TransformAboutLink()
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from LinkData where CreateDate > '20180830'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var aboutLinkCatalogs = _db.AboutLinkCatalogs.ToList();
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                AboutLink aboutLink = new AboutLink();
                var item = aboutLinkCatalogs.FirstOrDefault(x => x.Serno == dataRow["Mserno"].ToString());
                if (item != null)
                {
                    aboutLink.CategoryId = item.Id;
                }
                aboutLink.Subject = dataRow["Subject"].ToString();
                aboutLink.URL = dataRow["RelateUrl"].ToString();
                if (dataRow["Startusing"].ToString() == "1")
                {
                    aboutLink.Status = BooleanType.是;
                }
                else
                {
                    aboutLink.Status = BooleanType.否;
                }

                aboutLink.ListNum = Convert.ToInt32(dataRow["Fsort"]);



                aboutLink.Poster = dataRow["PostName"].ToString();
                aboutLink.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                aboutLink.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                aboutLink.initOrg = dataRow["PubUnitName"].ToString();
                _db.AboutLinks.Add(aboutLink);
                _db.SaveChanges();
            }

        }



        private static void TransformNewsImages(string tableName, int webCategoryId, string path)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName + " where Serno in (select Serno from  Sence where  Mserno='201104080001' )";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var newses = _db.Newses.Where(x => x.WebCategoryId == webCategoryId).ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                News news = newses.FirstOrDefault(x => x.Serno == dataRow["Serno"].ToString());
                if (news != null)
                {
                    if (!string.IsNullOrEmpty(dataRow["ExpFile"].ToString().Trim()))
                    {
                        if (dataRow["flag"].ToString() == "doc")
                        {
                            NewsFiles file = new NewsFiles();
                            file.NewId = news.Id;
                            file.ListNum = 0;
                            file.Subject = dataRow["ExpFile"].ToString();
                            file.UpFile = path + dataRow["ServerFile"].ToString();
                            file.Poster = dataRow["PostName"].ToString();
                            file.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                            file.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                            file.WebCategoryId = webCategoryId;
                            _db.NewsFileses.Add(file);
                            _db.SaveChanges();

                        }
                        if (dataRow["flag"].ToString() == "pic")
                        {
                            NewsImage file = new NewsImage();
                            file.NewId = news.Id;
                            file.ListNum = 0;
                            file.Subject = dataRow["ExpFile"].ToString();
                            file.UpFile = path + dataRow["ServerFile"].ToString();
                            file.Poster = dataRow["PostName"].ToString();
                            file.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                            file.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                            file.WebCategoryId = webCategoryId;
                            _db.NewsImages.Add(file);
                            _db.SaveChanges();
                        }
                        if (dataRow["flag"].ToString() == "media")
                        {
                            NewsFiles file = new NewsFiles();
                            file.NewId = news.Id;
                            file.ListNum = 0;
                            file.Subject = dataRow["ExpFile"].ToString();
                            file.UpFile = path + dataRow["SMediaFile"].ToString();
                            file.Poster = dataRow["PostName"].ToString();
                            file.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                            file.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                            file.WebCategoryId = webCategoryId;
                            _db.NewsFileses.Add(file);
                            _db.SaveChanges();

                        }
                    }

                }


            }
        }

        private static void TransformNewsCatalogNews(string tableName, int webCategoryId)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlConnection conn1 = new SqlConnection(GetConnectionStringByName("TnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName + " where CreateDate > '20180830'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var newses = _db.Newses.Where(x => x.WebCategoryId == webCategoryId).ToList();

            var nwsCatalogs = _db.NewsCatalogs.Where(x => x.WebCategoryId == webCategoryId).ToList();
            if (webCategoryId == 17)
            {
                nwsCatalogs = _db.NewsCatalogs.Where(x => x.UpdateOrg == "17").ToList();
            }

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                string Serno = dataRow["Serno"].ToString();
                string Mserno = dataRow["Mserno"].ToString();

                var news = newses.FirstOrDefault(x => x.Serno == Serno);
                var newsCatalog = nwsCatalogs.FirstOrDefault(x => x.Serno == Mserno);
                if (news != null && newsCatalog != null)
                {
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = conn1;
                    command1.CommandText = "INSERT INTO NewsCatalogNews ([NewsCatalog_Id],[News_Id]) VALUES (" + newsCatalog.Id + "," + news.Id + ")";
                    conn1.Open();
                    try
                    {
                        command1.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        conn1.Close();
                    }


                }


            }
        }

        private static void TransformNewsCatalogNews(int NewsCatalog_Id, int News_Id)
        {

            SqlConnection conn1 = new SqlConnection(GetConnectionStringByName("TnpdConnection"));


            SqlCommand command1 = new SqlCommand();
            command1.Connection = conn1;
            command1.CommandText = "INSERT INTO NewsCatalogNews ([NewsCatalog_Id],[News_Id]) VALUES (" + NewsCatalog_Id + "," + News_Id + ")";
            conn1.Open();
            try
            {
                command1.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn1.Close();
            }

        }

        private static void TransformNews(string tableName, int webCategoryId)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName + " where CreateDate > '20180830'";
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                News news = new News();
                news.Serno = dataRow["Serno"].ToString();
                news.Subject = dataRow["Subject"].ToString();
                news.Article = Txt2Html(dataRow["Content"].ToString());
                if (webCategoryId != 19)
                {
                    news.StartDate = GetDateTime(dataRow["PosterDate"].ToString());
                    if (string.IsNullOrEmpty(dataRow["CloseDate"].ToString()))
                    {
                        news.EndDate = GetDateTime(dataRow["PosterDate"].ToString());
                    }
                    else
                    {
                        news.EndDate = GetDateTime(dataRow["CloseDate"].ToString());
                    }
                }


                if (!string.IsNullOrEmpty(dataRow["ReadNumber"].ToString()))
                {
                    news.Views = Convert.ToInt32(dataRow["ReadNumber"]);
                }
                news.WebCategoryId = webCategoryId;

                var WebSiteNames = _db.WebSiteNames.ToList();
                string PubUnitDN = dataRow["PubUnitDN"].ToString();
                if (PubUnitDN.IndexOf("tncgb01") >= 0)
                {
                    news.OwnWebSiteId = 1;
                }
                else
                {
                    var site = WebSiteNames.FirstOrDefault(x => x.PubUnitDN == PubUnitDN);
                    news.OwnWebSiteId = site.UnitId;
                }



                news.Poster = dataRow["PostName"].ToString();
                news.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                news.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                news.initOrg = dataRow["PubUnitName"].ToString();
                _db.Newses.Add(news);
                string RelateUrl = dataRow["RelateUrl"].ToString();
                string RelateName = dataRow["RelateName"].ToString();
                if (RelateUrl != "http://" && RelateUrl != "")
                {
                    NewsLink newsLink = new NewsLink();
                    newsLink.LinkUrl = RelateUrl;
                    newsLink.ListNum = 0;
                    newsLink.Subject = RelateName;
                    if (string.IsNullOrEmpty(RelateName))
                    {
                        newsLink.Subject = RelateUrl;
                    }

                    newsLink.Target = TargetType.另開視窗;
                    newsLink.WebCategoryId = webCategoryId;
                    news.NewsLinks.Add(newsLink);
                }
            }

            _db.SaveChanges();
        }

        static void TransformNewsCatalogs(string tableName, int webCategoryId)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var websitenames = _db.WebSiteNames.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //資料要改名字 中文網改總局  社區治安中文網改社區治安服務網
                string webSiteName = dataRow["WebSiteName"].ToString();
                var item = websitenames.FirstOrDefault(x => webSiteName.Contains(x.Subject));

                NewsCatalog catalog = new NewsCatalog();
                string Serno = dataRow["Serno"].ToString();
                catalog.Serno = Serno;
                catalog.ListNum = 0;
                catalog.MetaId = 1;
                catalog.Subject = dataRow["ClassName"].ToString();
                catalog.WebCategoryId = webCategoryId;
                catalog.Poster = dataRow["PostName"].ToString();
                catalog.initOrg = dataRow["PubUnitName"].ToString();

                if (item != null)
                {
                    catalog.WebSiteId = item.Id;
                }


                if (dataRow["WebSiteName"].ToString() == "婦幼園地中文網")
                {
                    catalog.WebSiteId = 21;
                }
                if (dataRow["WebSiteName"].ToString() == "中文網")
                {
                    catalog.WebSiteId = 1;
                }
                if (dataRow["WebSiteName"].ToString() == "社區治安中文網")
                {
                    catalog.WebSiteId = 24;
                }

                catalog.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                catalog.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                _db.NewsCatalogs.Add(catalog);


            }
            _db.SaveChanges();
        }

        static void TransformNewsCatalogsG(string tableName)
        {
            SqlConnection conn = new SqlConnection(GetConnectionStringByName("OldTnpdConnection"));
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "select * from " + tableName;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            var websitenames = _db.WebSiteNames.ToList();

            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //資料要改名字 中文網改總局  社區治安中文網改社區治安服務網
                string webSiteName = dataRow["WebSiteName"].ToString();
                var item = websitenames.FirstOrDefault(x => webSiteName.Contains(x.Subject));

                NewsCatalog catalog = new NewsCatalog();
                string Serno = dataRow["Serno"].ToString();
                catalog.Serno = Serno;
                catalog.ListNum = 0;
                catalog.MetaId = 1;
                catalog.Subject = dataRow["ClassName"].ToString();
                catalog.UpdateOrg = "17";
                switch (catalog.Subject.Trim())
                {
                    case "加強重要節日安全維護工作":
                        catalog.WebCategoryId = 8;
                        break;
                    case "犯罪預防宣導":
                        catalog.WebCategoryId = 11;
                        break;
                    case "預防犯罪宣導":
                        catalog.WebCategoryId = 11;
                        break;
                    case "預防詐騙宣導":
                        catalog.WebCategoryId = 9;
                        break;

                    case "性別主流化":
                        catalog.WebCategoryId = 10;
                        break;
                    case "性別主流化專區":
                        catalog.WebCategoryId = 10;
                        break;
                    case "性別主流專區":
                        catalog.WebCategoryId = 10;
                        break;
                    case "性別統計專區":
                        catalog.WebCategoryId = 41;
                        break;
                    case "民防事務專區":
                        catalog.WebCategoryId = 12;
                        break;
                    case "婦幼安全宣導":
                        catalog.WebCategoryId = 13;
                        break;

                    case "自然法規公告":
                        catalog.WebCategoryId = 47;
                        break;

                    case "公務資料安全宣導":
                        catalog.WebCategoryId = 36;
                        break;
                    case "反詐騙宣導":
                        catalog.WebCategoryId = 9;
                        break;
                    case "自衛槍枝":
                        catalog.WebCategoryId = 31;
                        break;
                    case "自衛槍枝宣導":
                        catalog.WebCategoryId = 31;
                        break;
                    case "防制酒駕":
                        catalog.WebCategoryId = 4;
                        break;
                    case "交通安全宣導":
                        catalog.WebCategoryId = 4;
                        break;

                    case "其他宣導":
                        catalog.WebCategoryId = 23;
                        break;
                    case "法規宣導":
                        catalog.WebCategoryId = 38;
                        break;
                    case "治安宣導":
                        catalog.WebCategoryId = 25;
                        break;
                    case "社區警政宣導":
                        catalog.WebCategoryId = 32;
                        break;
                    case "青少年宣導":
                        catalog.WebCategoryId = 24;
                        break;
                    case "青春專案宣導":
                        catalog.WebCategoryId = 24;
                        break;
                    case "保安民防宣導":
                        catalog.WebCategoryId = 12;
                        break;
                    case "保防宣導":
                        catalog.WebCategoryId = 12;
                        break;
                    case "保護您小祕方":
                        catalog.WebCategoryId = 35;
                        break;
                    case "宣導影片":
                        catalog.WebCategoryId = 16;
                        break;
                    case "拾得物專欄":
                        catalog.WebCategoryId = 26;
                        break;
                    case "政令宣導":
                        catalog.WebCategoryId = 49;
                        break;
                    case "春安工作宣導":
                        catalog.WebCategoryId = 8;
                        break;
                    case "個人資料保護區":
                        catalog.WebCategoryId = 15;
                        break;
                    case "個人資料保護專區":
                        catalog.WebCategoryId = 15;
                        break;
                    case "個資保護專區":
                        catalog.WebCategoryId = 15;
                        break;
                    case "員工協助方案":
                        catalog.WebCategoryId = 33;
                        break;
                    case "員工協助方案專區":
                        catalog.WebCategoryId = 33;
                        break;
                    case "員工協助專區":
                        catalog.WebCategoryId = 33;
                        break;
                    case "員工專區":
                        catalog.WebCategoryId = 33;
                        break;
                    case "桌布背景區":
                        catalog.WebCategoryId = 13;
                        break;
                    case "海報區":
                        catalog.WebCategoryId = 13;
                        break;
                    case "退休人員及志工":
                        catalog.WebCategoryId = 29;
                        break;
                    case "校園安全宣導":
                        catalog.WebCategoryId = 24;
                        break;
                    case "清廉共同監督專區":
                        catalog.WebCategoryId = 22;
                        break;
                    case "廉政反貪宣導":
                        catalog.WebCategoryId = 22;
                        break;
                    case "廉政宣導":
                        catalog.WebCategoryId = 22;
                        break;
                    case "廉政倫理公開專區":
                        catalog.WebCategoryId = 22;
                        break;
                    case "廉政倫理事件公開專區":
                        catalog.WebCategoryId = 22;
                        break;
                    case "廉政倫理事件專區":
                        catalog.WebCategoryId = 22;
                        break;
                    case "廉政倫理事件暨清廉共同監督專區":
                        catalog.WebCategoryId = 22;
                        break;
                    case "會計公告":
                        catalog.WebCategoryId = 21;
                        break;
                    case "電子賀卡":
                        catalog.WebCategoryId = 13;
                        break;
                    case "預防經濟犯罪":
                        catalog.WebCategoryId = 11;
                        break;
                    case "預防經濟犯罪宣導專區":
                        catalog.WebCategoryId = 11;
                        break;
                    case "榮譽榜":
                        catalog.WebCategoryId = 5;
                        break;
                    case "影音宣導":
                        catalog.WebCategoryId = 16;
                        break;
                    case "遺失物專欄":
                        catalog.WebCategoryId = 25;
                        break;
                    case "關懷卡":
                        catalog.WebCategoryId = 12;
                        break;
                    case "106年加強春節期間安全維護工作專區":
                        catalog.WebCategoryId = 50;
                        break;
                    case "106年加強春節期間安全維護工作貼心服務網頁":
                        catalog.WebCategoryId = 50;
                        break;
                    case "106年加強春節期間安全維護貼心服務網頁":
                        catalog.WebCategoryId = 50;
                        break;
                    case "BULLETIN BOARD":
                        catalog.WebCategoryId = 51;
                        break;
                    case "人事業務宣導":
                        catalog.WebCategoryId = 28;
                        break;
                    case "人權兩公約宣導":
                        catalog.WebCategoryId = 52;
                        break;
                    case "中央遙控警報系統測試期前宣導":
                        catalog.WebCategoryId = 53;
                        break;
                    case "公告具高再犯危險之性侵害加害人人數專區":
                        catalog.WebCategoryId = 54;
                        break;
                    case "戶口組宣導專區":
                        catalog.WebCategoryId = 55;
                        break;
                    case "即時路況":
                        catalog.WebCategoryId = 56;
                        break;
                    case "其他宣導 ":
                        catalog.WebCategoryId = 23;
                        break;
                    case "政風宣導 ":
                        catalog.WebCategoryId = 34;
                        break;
                }

                catalog.Poster = dataRow["PostName"].ToString();
                catalog.initOrg = dataRow["PubUnitName"].ToString();



                if (item != null)
                {
                    catalog.WebSiteId = item.Id;
                }


                if (dataRow["WebSiteName"].ToString() == "婦幼園地中文網")
                {
                    catalog.WebSiteId = 21;
                }
                if (dataRow["WebSiteName"].ToString() == "中文網")
                {
                    catalog.WebSiteId = 1;
                }
                if (dataRow["WebSiteName"].ToString() == "社區治安中文網")
                {
                    catalog.WebSiteId = 24;
                }

                catalog.InitDate = GetDateTime(dataRow["CreateDate"].ToString());
                catalog.UpdateDate = GetDateTime(dataRow["UpdateDate"].ToString());
                _db.NewsCatalogs.Add(catalog);


            }
            _db.SaveChanges();
        }

        static DateTime GetDateTime(string datetime)
        {
            datetime = datetime.Insert(4, "/");
            datetime = datetime.Insert(7, "/");

            DateTime d = Convert.ToDateTime(datetime);
            return d;
        }


        /// <summary>
        /// 輸入文字方塊的文字轉成HTML
        /// </summary>
        /// <param name="article">要轉換的文字</param>
        /// <returns>回傳HTML用的文字</returns>
        /// <remarks></remarks>

        static public string Txt2Html(object article)
        {
            if (article != null)
            {
                string fstr = (string)article;
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace("<", "&lt;");
                fstr = fstr.Replace("\"", "&quot;");
                fstr = fstr.Replace("\'", "&quot;");
                fstr = fstr.Replace("\n", "<br>");
                fstr = fstr.Replace("|", "&brvbar;");
                fstr = fstr.Replace(" ", "&nbsp;");
                fstr = fstr.Replace("[", "<");
                fstr = fstr.Replace("]", ">");
                fstr = fstr.Replace("url=", "a href=");
                fstr = fstr.Replace("/url", "/a");
                return fstr;
            }
            return "";
        }

        static public string Html2Text(object article)
        {
            if (article != null)
            {
                string fstr = (string)article;
                fstr = fstr.Replace("&gt;", ">");
                fstr = fstr.Replace(">", "&gt;");
                fstr = fstr.Replace("&lt;", "<");
                fstr = fstr.Replace("&quot;", "\"");
                fstr = fstr.Replace("&quot;", "\'");
                fstr = fstr.Replace("<br>", "\n");
                fstr = fstr.Replace("&brvbar;", "|");
                fstr = fstr.Replace("&nbsp;", " ");

                return fstr;
            }
            return "";
        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
