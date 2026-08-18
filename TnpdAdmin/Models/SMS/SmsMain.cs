using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TnpdAdmin.Models.SMS
{
    public class SmsMain
    {
        private string _userName;
        private string _password;
        private string _prefix = ConfigurationManager.AppSettings["serviceName"];
        public SmsMain(string userName, string password)
        {
            SmsBody = new List<SmsBody>();
            PackageNo = "1";
            PackageCnt = "1";
            //預約發送時間，若為即時發送則為空白
            TimeUp = "";
            //簡訊回覆網址，若不需回覆則為空白
            ResponseUrl = "";
            //簡訊標籤，此則簡訊的關鍵字，以利日後查詢及統計用
            SmsTitle = "臺南市政府警察局簡訊";
            startSignature = _prefix;
            OrderNo = DateTime.Now.ToString("yyyyMMddHHmmsss");
            _userName = userName;
            _password = password;
        }

        [MaxLength(36)]
        [Required]
        [Display(Name = "交易代號")]
        public string OrderNo { get; set; }

        [MaxLength(5)]
        [Required]
        [Display(Name = "交易批號")]
        public string PackageNo { get; set; }

        //數字範圍1~500
        [Required]
        [Display(Name = "這批共有幾筆")]
        public string PackageCnt { get; set; }

        //時間字串格式yyyymmddHHmm
        [MaxLength(12)]
        [Display(Name = "預約時間")]
        public string TimeUp { get; set; }

        [MaxLength(256)]
        [Display(Name = "主動回傳網址")]
        public string ResponseUrl { get; set; }

        [MaxLength(30)]
        [Display(Name = "簡訊標籤")]
        public string SmsTitle { get; set; }

        [MaxLength(30)]
        [Display(Name = "開頭署名")]
        public string startSignature { get; set; }

        [Display(Name = "datalist")]
        public List<SmsBody> SmsBody { get; set; }

        /// <summary>
        /// 發送簡訊(非同步)
        /// </summary>
        /// <returns></returns>
        public async Task<string> SendAsync()
        {
            //JsonConvert首文字小寫
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            //產生Json字串
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
            string result = await SmsApiAsync(json);

            return result;
        }

        /// <summary>
        /// 發送簡訊
        /// </summary>
        /// <returns></returns>
        public string Send()
        {
            //JsonConvert首文字小寫
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            //產生Json字串
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
            string result = SmsApi(json);

            return result;
        }

        /// <summary>
        /// 發送簡訊(使用WebRequest)
        /// </summary>
        /// <returns></returns>
        public string SendWithWebRequest()
        {
            //JsonConvert首文字小寫
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            //產生Json字串
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, settings);
            string result = SmsWebApi(json);

            return result;
        }

        /// <summary>
        /// 取得Basic Authentication 字串
        /// </summary>
        /// <returns></returns>
        private string GetBasicAuthentication()
        {
            string credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_userName + ":" + _password));
            return string.Format("Basic {0}", credentials);
        }

        /// <summary>
        /// 簡訊API(非同步)
        /// </summary>
        /// <param name="json">requset Json</param>
        /// <returns></returns>
        private async Task<string> SmsApiAsync(string json)
        {
            //舊版 .NET Framework 必須加上這一行來設定TLS1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //新版設定TLS1.2及TLS1.3
            //var handler = new HttpClientHandler
            //{
            //    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
            //};

            StringBuilder sb = new StringBuilder();
            try
            {
                var client = new HttpClient();

                var url = ConfigurationManager.AppSettings["apiUrl"];
                //取得API網址
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                //取得Basic Authentication 字串
                var basicAuth = GetBasicAuthentication();
                //加入Authorization Header
                request.Headers.Add("Authorization", basicAuth);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseJson = await response.Content.ReadAsStringAsync();
                var smsResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SmsResponse>(responseJson);
                var status = smsResponse.Status;

                var result = "";


                if (status != "0000")
                {
                    sb.Append("簡訊發送失敗<br/>");
                    sb.Append("狀態碼:" + smsResponse.Status + "<br/>");
                    sb.Append("狀態訊息:" + GetStatusMsg(smsResponse.Status) + "<br/>");
                    sb.Append("錯誤明細:");
                    foreach (var error in smsResponse.ErrorUids)
                    {
                        sb.Append("狀態碼:" + error.UidStatus + "<br/>");
                        sb.Append("狀態訊息:" + GetStatusMsg(error.UidStatus) + "<br/>");
                    }
                }
                else
                {
                    sb.Append("簡訊發送成功");
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException != null)
                {
                    sb.Append("簡訊發送失敗，錯誤訊息：");
                    sb.Append(ex.InnerException.Message);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 簡訊API
        /// </summary>
        /// <param name="json">requset Json</param>
        /// <returns></returns>
        private string SmsApi(string json)
        {
            //舊版 .NET Framework 必須加上這一行來設定TLS1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //新版設定TLS1.2及TLS1.3
            //var handler = new HttpClientHandler
            //{
            //    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
            //};

            StringBuilder sb = new StringBuilder();
            try
            {
                var client = new HttpClient();

                var url = ConfigurationManager.AppSettings["apiUrl"];
                //取得API網址
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                //取得Basic Authentication 字串
                var basicAuth = GetBasicAuthentication();
                //加入Authorization Header
                request.Headers.Add("Authorization", basicAuth);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = client.SendAsync(request).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                string responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var smsResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SmsResponse>(responseJson);
                var status = smsResponse.Status;

                var result = "";

                if (status != "0000")
                {
                    sb.Append("簡訊發送失敗<br/>");
                    sb.Append("狀態碼:" + smsResponse.Status + "<br/>");
                    sb.Append("狀態訊息:" + GetStatusMsg(smsResponse.Status) + "<br/>");
                    sb.Append("錯誤明細:");
                    foreach (var error in smsResponse.ErrorUids)
                    {
                        sb.Append("狀態碼:" + error.UidStatus + "<br/>");
                        sb.Append("狀態訊息:" + GetStatusMsg(error.UidStatus) + "<br/>");
                    }
                }
                else
                {
                    sb.Append("簡訊發送成功");
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException != null)
                {
                    sb.Append("簡訊發送失敗，錯誤訊息：");
                    sb.Append(ex.InnerException.Message);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 使用WebRequest來接簡訊API
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string SmsWebApi(string json)
        {
            //舊版 .NET Framework 必須加上這一行來設定TLS1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            StringBuilder sb = new StringBuilder();

            try
            {
                var url = ConfigurationManager.AppSettings["apiUrl"];
                var request = System.Net.WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("Authorization", GetBasicAuthentication());
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }
                var response = request.GetResponse();
                string responseJson = "";
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseJson = streamReader.ReadToEnd();
                }
                var smsResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SmsResponse>(responseJson);
                var status = smsResponse.Status;

                var result = "";

                if (status != "0000")
                {
                    sb.Append("簡訊發送失敗<br/>");
                    sb.Append("狀態碼:" + smsResponse.Status + "<br/>");
                    sb.Append("狀態訊息:" + GetStatusMsg(smsResponse.Status) + "<br/>");
                    sb.Append("錯誤明細:");
                    foreach (var error in smsResponse.ErrorUids)
                    {
                        sb.Append("狀態碼:" + error.UidStatus + "<br/>");
                        sb.Append("狀態訊息:" + GetStatusMsg(error.UidStatus) + "<br/>");
                    }
                }
                else
                {
                    sb.Append("已經發送，請留意簡訊訊息");
                }
            }
            catch (Exception ex)
            {
                sb.Append("簡訊發送失敗，錯誤訊息：");
                sb.Append(ex.Message);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 將狀態碼轉換成訊息
        /// </summary>
        /// <param name="status">狀態碼</param>
        /// <returns></returns>
        private string GetStatusMsg(string status)
        {
            switch (status)
            {
                case "0000":
                    return "簡訊平臺處理成功";
                case "0001":
                    return "帳號或密碼錯誤";
                case "0002":
                    return "暫時停止服務";
                case "0003":
                    return "請確認packageCnt或smsBody筆數";
                case "0004":
                    return "處理異常";
                case "0005":
                    return "MessageId資料重複";
                case "0006":
                    return "請確認mobile或smsText是否有資料";
                case "0007":
                    return "請確認mobile或smsText資料長度";
                case "0008":
                    return "uId資料重複";
                case "0009":
                    return "必要欄位空值";
                case "0010":
                    return "資料長度或格式錯誤";
                case "0011":
                    return "OrderNo資料重複";
                case "0012":
                    return "查無資料";
                case "0500":
                    return "簡訊暫停發送";
                case "0998":
                    return "簡訊已逾時無法發送";
                case "0999":
                    return "簡訊已刪除";
                case "1000":
                    return "準備傳送電信業者";
                case "1017":
                    return "帳號已停權";
                case "1400":
                    return "部份處理失敗請檢視明細";
                case "1500":
                    return "整批格式或內容異常無法傳送";
                case "2000":
                    return "已送達電信業者";
                default:
                    return "未知錯誤";
            }
        }
    }
}