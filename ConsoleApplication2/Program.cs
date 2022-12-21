using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApplication2
{
    class Program
    {
        static WebClient webclient = new WebClient();
        private static string rootPath = ConfigurationManager.AppSettings["rootPath"].ToString();
        private static string strConn = ConfigurationManager.ConnectionStrings["TnpdConnection"].ConnectionString;
        static void Main(string[] args)
        {
            List<RootWebSite> rootWebSites = new List<RootWebSite>();
            rootWebSites.Add(new RootWebSite() { Name = "臺南市政府警察局附件網站", Url = "http://appendix.tnpd.gov.tw/default.aspx", UnitsID = 1 });
            rootWebSites.Add(new RootWebSite() { Name = "白河分局附件網站", Url = "http://appendix.tnpd.gov.tw/baihe/default.aspx", UnitsID = 165 });
            rootWebSites.Add(new RootWebSite() { Name = "新營分局附件網站", Url = "http://appendix.tnpd.gov.tw/xinying/default.aspx", UnitsID = 391 });
            rootWebSites.Add(new RootWebSite() { Name = "麻豆分局附件網站", Url = "http://appendix.tnpd.gov.tw/madou/default.aspx", UnitsID = 276 });
            rootWebSites.Add(new RootWebSite() { Name = "佳里分局附件網站", Url = "http://appendix.tnpd.gov.tw/jiali/default.aspx", UnitsID = 242 });
            rootWebSites.Add(new RootWebSite() { Name = "學甲分局附件網站", Url = "http://appendix.tnpd.gov.tw/xuejia/default.aspx", UnitsID = 420 });
            rootWebSites.Add(new RootWebSite() { Name = "善化分局附件網站", Url = "http://appendix.tnpd.gov.tw/shanhua/default.aspx", UnitsID = 305 });
            rootWebSites.Add(new RootWebSite() { Name = "新化分局附件網站", Url = "http://appendix.tnpd.gov.tw/xinhua/default.aspx", UnitsID = 369 });
            rootWebSites.Add(new RootWebSite() { Name = "歸仁分局附件網站", Url = "http://appendix.tnpd.gov.tw/guiren/default.aspx", UnitsID = 213 });
            rootWebSites.Add(new RootWebSite() { Name = "玉井分局附件網站", Url = "http://appendix.tnpd.gov.tw/yujing/default.aspx", UnitsID = 469 });
            rootWebSites.Add(new RootWebSite() { Name = "永康分局附件網站", Url = "http://appendix.tnpd.gov.tw/yongkang/default.aspx", UnitsID = 446 });
            rootWebSites.Add(new RootWebSite() { Name = "第一分局附件網站", Url = "http://appendix.tnpd.gov.tw/1st/default.aspx", UnitsID = 32 });
            rootWebSites.Add(new RootWebSite() { Name = "第二分局附件網站", Url = "http://appendix.tnpd.gov.tw/2nd/default.aspx", UnitsID = 54 });
            rootWebSites.Add(new RootWebSite() { Name = "第三分局附件網站", Url = "http://appendix.tnpd.gov.tw/3rd/default.aspx", UnitsID = 76 });
            rootWebSites.Add(new RootWebSite() { Name = "第四分局附件網站", Url = "http://appendix.tnpd.gov.tw/4th/default.aspx", UnitsID = 101 });
            rootWebSites.Add(new RootWebSite() { Name = "第五分局附件網站", Url = "http://appendix.tnpd.gov.tw/5th/default.aspx", UnitsID = 120 });
            rootWebSites.Add(new RootWebSite() { Name = "第六分局附件網站", Url = "http://appendix.tnpd.gov.tw/6th/default.aspx", UnitsID = 142 });
            rootWebSites.Add(new RootWebSite() { Name = "刑事警察大隊附件網站", Url = "http://appendix.tnpd.gov.tw/cic/default.aspx", UnitsID = 194 });
            rootWebSites.Add(new RootWebSite() { Name = "交通警察大隊附件網站", Url = "http://appendix.tnpd.gov.tw/traffic/default.aspx", UnitsID = 346 });
            rootWebSites.Add(new RootWebSite() { Name = "保安警察大隊附件網站", Url = "http://appendix.tnpd.gov.tw/spc/default.aspx", UnitsID = 333 });
            rootWebSites.Add(new RootWebSite() { Name = "少年警察大隊附件網站", Url = "http://appendix.tnpd.gov.tw/jdpb/default.aspx", UnitsID = 270 });
            rootWebSites.Add(new RootWebSite() { Name = "婦幼警察大隊附件網站", Url = "http://appendix.tnpd.gov.tw/wcpb/default.aspx", UnitsID = 364 });

            int ParentId = 0;
            if (!System.IO.Directory.Exists(rootPath + "局本部、分局、大隊附件網站"))
            {
                System.IO.Directory.CreateDirectory(rootPath + "局本部、分局、大隊附件網站");



            }

            int rootParentId = WriteRootFolder("局本部、分局、大隊附件網站", 1, new DateTime(2011, 1, 1), "");
            foreach (var rootWeb in rootWebSites)
            {
                if (!System.IO.Directory.Exists(rootPath + "局本部、分局、大隊附件網站/" + rootWeb.Name))
                {
                    System.IO.Directory.CreateDirectory(rootPath + "局本部、分局、大隊附件網站/" + rootWeb.Name);


                }
                ParentId = WriteFolder(rootWeb.Name, rootWeb.UnitsID, rootParentId, new DateTime(2011, 1, 1),"");
                ReadRootPage(rootWeb.Url, rootPath + "局本部、分局、大隊附件網站/" + rootWeb.Name, ParentId,rootWeb.UnitsID);
            }

            List<RootWebSite> rootWebSites1 = new List<RootWebSite>();

            rootWebSites1.Add(new RootWebSite() { Name = "電子公文區", Url = "http://appendix.tnpd.gov.tw/edoc/default.aspx", UnitsID = 1 });
            rootWebSites1.Add(new RootWebSite() { Name = "關老師專區", Url = "http://appendix.tnpd.gov.tw/kuan/default.aspx", UnitsID = 1 });
            rootWebSites1.Add(new RootWebSite() { Name = "簿冊電子化專區", Url = "http://appendix.tnpd.gov.tw/office/default.aspx", UnitsID = 1 });
            rootWebSites1.Add(new RootWebSite() { Name = "ISMS四階文件", Url = "http://appendix.tnpd.gov.tw/isms/ISMS/Forms/AllItems.aspx", UnitsID = 1 });
            rootWebSites1.Add(new RootWebSite() { Name = "選舉專區", Url = "http://appendix.tnpd.gov.tw/election/default.aspx", UnitsID = 1 });
            foreach (var rootWeb in rootWebSites1)
            {
                if (!System.IO.Directory.Exists(rootPath + rootWeb.Name))
                {
                    System.IO.Directory.CreateDirectory(rootPath + rootWeb.Name);

                }
                ParentId = WriteRootFolder(rootWeb.Name, rootWeb.UnitsID, new DateTime(2011, 1, 1), "");
                ReadRootPage(rootWeb.Url, rootPath + rootWeb.Name, ParentId,rootWeb.UnitsID);
            }
            //System.IO.File.WriteAllText("F:\\website\\aaa1.html",content);
            //Console.Write(content);
            Console.ReadKey();

        }

        private static int WriteRootFolder(string Subject, int UnitId, DateTime InitDate, string Poster)
        {
            int id;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand("CreateFolders1", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Subject", Subject);
            comm.Parameters.AddWithValue("@UnitId", UnitId);
            comm.Parameters.AddWithValue("@InitDate", InitDate);
            comm.Parameters.AddWithValue("@Poster", Poster);
            comm.Parameters.Add("@Id", SqlDbType.Int);
            comm.Parameters["@Id"].Direction = ParameterDirection.Output;
            conn.Open();
            comm.ExecuteNonQuery();
            id = Convert.ToInt32(comm.Parameters["@Id"].Value);
            conn.Close();
            return id;

        }
        private static int WriteFolder(string Subject, int UnitId, int ParentId, DateTime InitDate,string Poster)
        {
            int id;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand("CreateFolders", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Subject", Subject);
            comm.Parameters.AddWithValue("@UnitId", UnitId);
            comm.Parameters.AddWithValue("@ParentId", ParentId);
            comm.Parameters.AddWithValue("@InitDate", InitDate);
            comm.Parameters.AddWithValue("@Poster", Poster);
            comm.Parameters.Add("@Id", SqlDbType.Int);
            comm.Parameters["@Id"].Direction = ParameterDirection.Output;
            conn.Open();
            comm.ExecuteNonQuery();
            id = Convert.ToInt32(comm.Parameters["@Id"].Value);
            conn.Close();
            return id;

        }

        private static void ReadRootPage(string rootWebUrl, string filePath, int ParentId, int UnitId)
        {
            string content = GetContent(rootWebUrl);
            //string content = GetContent();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);

            var hrefs = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'ms-navitem')]");
            foreach (var href in hrefs)
            {
                if (!System.IO.Directory.Exists(filePath + "\\" + href.InnerText)) ;
                {
                    System.IO.Directory.CreateDirectory(filePath + "\\" + href.InnerText);

                }
                int sParentId = WriteFolder(href.InnerText, UnitId, ParentId, new DateTime(2011, 1, 1),"");
                Console.WriteLine("read " + href.InnerText + href.Attributes["href"].Value);

                ReadPage("http://appendix.tnpd.gov.tw" + href.Attributes["href"].Value, filePath + "\\" + href.InnerHtml, sParentId, UnitId);
            }

        }

        private static void ReadPage(string Url, string filePath, int ParentId, int UnitId)
        {
            string content = GetContent(Url).Replace(" \r\n            ", "");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var Dtables = htmlDoc.DocumentNode.SelectNodes("//table[@ctype='資料夾']");
            if (Dtables != null)
            {
                foreach (var table in Dtables)
                {
                    string directoryName = table.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
                    
                    string initDate = "";
                    string Poster = "";
                    if (table.ParentNode.NextSibling.NextSibling.NextSibling == null)
                    {
                         Poster= table.ParentNode.NextSibling.ChildNodes[0].InnerText;
                         initDate= table.ParentNode.NextSibling.NextSibling.ChildNodes[0].InnerText;
                    }
                    else
                    {
                        initDate = table.ParentNode.NextSibling.NextSibling.ChildNodes[0].InnerText;
                        Poster = table.ParentNode.NextSibling.NextSibling.NextSibling.ChildNodes[0].InnerText;
                    }

                    if (initDate.Length > 10)
                    {
                        if (!System.IO.Directory.Exists(filePath + "\\" + directoryName)) ;
                        {
                            System.IO.Directory.CreateDirectory(filePath + "\\" + directoryName);

                        }
                        int sParentId = WriteFolder(directoryName, UnitId, ParentId, Convert.ToDateTime(initDate),Poster);
                        Console.WriteLine("write " + filePath + "\\" + directoryName);
                        string ReadUrl = table.ChildNodes[0].ChildNodes[0].ChildNodes[0].Attributes["href"].Value;
                        if (ReadUrl.IndexOf("appendix.tnpd.gov.tw") == -1)
                        {
                            ReadUrl = "http://appendix.tnpd.gov.tw" + ReadUrl;
                        }

                        ReadPage(ReadUrl, filePath + "\\" + directoryName, sParentId, UnitId);
                    }

                }
            }


            var Ftables = htmlDoc.DocumentNode.SelectNodes("//table[@ctype='文件']");
            if (Ftables != null)
            {
                foreach (var table in Ftables)
                {
                    //Console.WriteLine("download:" + table.Attributes["url"].Value);
                    string ReadUrl = table.ChildNodes[0].ChildNodes[0].ChildNodes[0].Attributes["href"].Value;
                    string initDate = "";
                    string Poster = "";
                    if (table.ParentNode.NextSibling.NextSibling.NextSibling == null)
                    {
                        Poster= table.ParentNode.NextSibling.ChildNodes[0].InnerText;
                         initDate = table.ParentNode.NextSibling.NextSibling.ChildNodes[0].InnerText;
                    }
                    else
                    {
                        initDate=table.ParentNode.NextSibling.NextSibling.ChildNodes[0].InnerText;
                        Poster=table.ParentNode.NextSibling.NextSibling.NextSibling.ChildNodes[0].InnerText;
                    }

                   
                    if (initDate.Length > 10)
                    {
                        if (ReadUrl.IndexOf("appendix.tnpd.gov.tw") == -1)
                        {
                            ReadUrl = "http://appendix.tnpd.gov.tw" + ReadUrl;
                        }

                        string[] fileNames = ReadUrl.Split('/');
                        string fileName = fileNames[fileNames.Length - 1];
                        try
                        {
                            webclient.DownloadFile(ReadUrl, filePath + "\\" + fileName);
                            FileInfo fileInfo = new FileInfo(filePath + "\\" + fileName);
                            WriteFiles(fileName, FormatSize(fileInfo.Length), ParentId, Convert.ToDateTime(initDate), Poster);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }
                    }



                }
            }
            var moreTd = htmlDoc.DocumentNode.SelectSingleNode("//td[contains(@class, 'ms-paging')]");
            if (moreTd != null)
            {
                if (moreTd.NextSibling != null)
                {
                    string moreUrl = moreTd.NextSibling.ChildNodes[0].Attributes["OnClick"].Value;
                    moreUrl = moreUrl.Replace("javascript:SubmitFormPost(\"", "");
                    moreUrl = moreUrl.Replace("\");javascript:return false", "");
                    moreUrl = DecodeEncodedNonAsciiCharacters(moreUrl);
                    Console.WriteLine(moreUrl);
                    ReadPage(moreUrl, filePath, ParentId, UnitId);
                }


            }

        }

        private static void WriteFiles(string fileName, string Size, int FolderId, DateTime InitDate,string Poster)
        {

            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand("INSERT INTO FolderFiles (Subject, Size, FolderId,InitDate,UpdateDate,Poster,Updater) VALUES  (@Subject, @Size, @FolderId,@InitDate,@InitDate,@Poster,@Poster)", conn);

            comm.Parameters.AddWithValue("@Subject", fileName);
            comm.Parameters.AddWithValue("@Size", Size);
            comm.Parameters.AddWithValue("@FolderId", FolderId);
            comm.Parameters.AddWithValue("@InitDate", InitDate);
            comm.Parameters.AddWithValue("@Poster", Poster);
            conn.Open();
            comm.ExecuteNonQuery();

            conn.Close();
        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }
        #region 自動將bytes轉換單位
        // Load all suffixes in an array  
        static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }
        #endregion
        private static string GetContent()
        {
            string content = System.IO.File.ReadAllText(@"F:\\website\\aaa1.html");
            return content;
        }

        private static string GetContent(string Url)
        {
            var uri = new Uri(Url);
            var request = WebRequest.Create(Url) as HttpWebRequest;
            WebClient wc = new WebClient();
            //REF: https://stackoverflow.com/a/39534068/288936
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string res = wc.DownloadString(Url);
            // If required by the server, set the credentials.
            request.UserAgent = "PostmanRuntime/7.26.5";
            request.Accept = "*/*";
            request.Credentials = CredentialCache.DefaultCredentials;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            // 重點是修改這行
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }


}
