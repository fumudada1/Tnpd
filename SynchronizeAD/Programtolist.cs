using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.Linq;
using System.Net.Mime;
using System.Xml;
using System.IO;
using TnpdModels;

namespace SynchronizeAD
{
    class Program
    {
        // 移除資料庫相關程式碼
        // static BackendContext _db = new BackendContext();
        // static List<Unit> Units = _db.Units.ToList();
        // static List<Member> members = _db.Members.ToList();

        // 用來儲存CSV資料的清單
        static List<MemberCsvData> csvMemberList = new List<MemberCsvData>();

        static void Main(string[] args)
        {
            int timeOut = 0;
            string Domain = "10.128.0.21";
            string adm = "Maintain\\worldwideweb", pwd = "730@tnpd3edc", domainOU = "";

            // 直接讀取AD資料並產生CSV
            System.Collections.ArrayList nodeList = GetAll(adm, pwd, Domain, timeOut, domainOU);

            // 產生CSV檔案
            GenerateMemberCsv();

            Console.WriteLine("按任意鍵結束程式...");
            Console.ReadKey();
        }

        // 新增：CSV資料結構
        public class MemberCsvData
        {
            public string FirstLevelUnit { get; set; }
            public string SecondLevelUnit { get; set; }
            public string Account { get; set; }
            public string Name { get; set; }
            public string UnitPath { get; set; } // 完整的單位路徑
        }

        public static string GetLDAPPath(string ADschema, string OU)
        {
            string[] domainCtrls = ADschema.Split('.');
            string strDomains = "";
            //get all dc and add them in the following order

            for (int i = 0; i < domainCtrls.GetLength(0); i++)
            {
                if (strDomains == "") //first
                    strDomains = strDomains + "dc=" + domainCtrls[i];
                else //add comma
                    strDomains = strDomains + ",dc=" + domainCtrls[i];
            }
            string strPath = "";
            string strOU = "";
            if (OU != "") //retrieve users from just the specified OU
            {
                strOU = "OU=" + OU;
                strPath = "LDAP://" + strOU + "," + strDomains; //path e.g. LDAP://OU=IT,dc=pk,dc=ultimusdc=com
            }
            else //if ou was not checked and schema path was
                strPath = "LDAP://" + ADschema; //path e.g. LDAP://pk.ultimus.com

            //strPath would be empty if user has not checked OU and path
            return strPath;
        }

        public static ArrayList GetAll(string UserName, string PassWord, string ADschema, int TimeOut, string OU)
        {
            ArrayList aryObj = new ArrayList();
            string strDomain = ADschema.Split('.')[0];
            DirectoryEntry ADentry;
            string strPath = GetLDAPPath(ADschema, OU);
            ADentry = new DirectoryEntry("LDAP://10.128.0.21/OU=everyone,DC=tncpb,DC=gov", UserName, PassWord);

            DirectorySearcher Searcher = new DirectorySearcher(ADentry);
            Searcher.Filter = ("(objectClass=*)");  // Search all.

            // The first item in the results is always the domain. Therefore, we just get that and retrieve its children.
            foreach (DirectoryEntry entry in Searcher.FindOne().GetDirectoryEntry().Children)
            {
                if (entry.Name.IndexOf("Tainan City Police Department") > 0)
                {
                    Console.WriteLine("處理局本部資料...");
                    VisitNode(entry, "局本部", "");
                }
                if (entry.Name.IndexOf("Tainan City Police Precinct") > 0)
                {
                    Console.WriteLine("處理分局資料...");
                    VisitNode(entry, "分局", "");
                }
                if (entry.Name.IndexOf("停用帳號") > 0)
                {
                    Console.WriteLine("處理停用帳號資料...");
                    VisitStopNode(entry, "停用帳號", "");
                }
            }
            return aryObj;
        }

        // 簡化：只處理AD資料，不更新資料庫
        public static void VisitNode(DirectoryEntry ParentEntry, string firstLevelUnit, string secondLevelUnit)
        {
            foreach (DirectoryEntry childEntry1 in ParentEntry.Children)
            {
                string objectClass = GetPropertyValue(childEntry1, "objectClass");
                string description = GetPropertyValue(childEntry1, "description");
                string name = GetPropertyValue(childEntry1, "name");

                if (objectClass == "organizationalUnit")
                {
                    Console.WriteLine("處理單位: " + description);

                    // 遞迴時更新第二層單位資訊
                    string newSecondLevel = string.IsNullOrEmpty(secondLevelUnit) ? description : secondLevelUnit;
                    VisitNode(childEntry1, firstLevelUnit, newSecondLevel);
                }

                if (objectClass == "user")
                {
                    string displayName = GetPropertyValue(childEntry1, "displayName");
                    string descriptionName = GetPropertyValue(childEntry1, "description");
                    string sAMAccountName = GetPropertyValue(childEntry1, "sAMAccountName");

                    if (string.IsNullOrEmpty(displayName))
                    {
                        displayName = descriptionName;
                    }

                    // 將使用者資料加入CSV清單
                    csvMemberList.Add(new MemberCsvData
                    {
                        FirstLevelUnit = firstLevelUnit,
                        SecondLevelUnit = secondLevelUnit,
                        Account = sAMAccountName,
                        Name = displayName,
                        UnitPath = firstLevelUnit + "/" + secondLevelUnit
                    });

                    Console.WriteLine("新增使用者: " + displayName + " (" + sAMAccountName + ")");
                }
            }
        }

        // 簡化：只處理AD停用帳號資料，不更新資料庫
        public static void VisitStopNode(DirectoryEntry ParentEntry, string firstLevelUnit, string secondLevelUnit)
        {
            foreach (DirectoryEntry childEntry1 in ParentEntry.Children)
            {
                string objectClass = GetPropertyValue(childEntry1, "objectClass");

                if (objectClass == "user")
                {
                    string displayName = GetPropertyValue(childEntry1, "displayName");
                    string descriptionName = GetPropertyValue(childEntry1, "description");
                    string sAMAccountName = GetPropertyValue(childEntry1, "sAMAccountName");

                    if (string.IsNullOrEmpty(descriptionName))
                    {
                        descriptionName = displayName;
                    }

                    // 將停用使用者也加入CSV清單
                    csvMemberList.Add(new MemberCsvData
                    {
                        FirstLevelUnit = firstLevelUnit,
                        SecondLevelUnit = secondLevelUnit,
                        Account = sAMAccountName,
                        Name = descriptionName,
                        UnitPath = firstLevelUnit + "/" + secondLevelUnit
                    });

                    Console.WriteLine("新增停用使用者: " + descriptionName + " (" + sAMAccountName + ")");
                }
            }
        }

        // 產生CSV檔案的方法
        public static void GenerateMemberCsv()
        {
            try
            {
                string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "member.csv");

                using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    // 寫入CSV標題行
                    writer.WriteLine("第一層單位,第二層單位,帳號,姓名");

                    // 寫入資料行
                    foreach (var member in csvMemberList.OrderBy(x => x.FirstLevelUnit).ThenBy(x => x.SecondLevelUnit).ThenBy(x => x.Account))
                    {
                        // 處理可能包含逗號的資料，用引號包圍
                        string firstLevel = EscapeCsvField(member.FirstLevelUnit);
                        string secondLevel = EscapeCsvField(member.SecondLevelUnit);
                        string account = EscapeCsvField(member.Account);
                        string name = EscapeCsvField(member.Name);

                        writer.WriteLine(firstLevel + "," + secondLevel + "," + account + "," + name);
                    }
                }

                Console.WriteLine("CSV檔案已產生：" + csvPath);
                Console.WriteLine("共匯出 " + csvMemberList.Count + " 筆人員資料");
            }
            catch (Exception ex)
            {
                Console.WriteLine("產生CSV檔案時發生錯誤：" + ex.Message);
            }
        }

        // 處理CSV欄位中的特殊字元
        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            // 如果欄位包含逗號、引號或換行，就用引號包圍
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                // 將引號替換成兩個引號
                field = field.Replace("\"", "\"\"");
                return "\"" + field + "\"";
            }

            return field;
        }

        public static string GetPropertyValue(DirectoryEntry childEntry, string key)
        {
            string result = "";
            PropertyCollection myResultPropColl = childEntry.Properties;
            foreach (Object myCollection in myResultPropColl[key])
            {
                result = myCollection.ToString();
            }
            return result;
        }

        #region "判斷是否為數字"
        /// <summary>
        /// 判斷是否為數字
        /// </summary>
        /// <param name="inputData">輸入字串</param>
        /// <returns>bool</returns>
        public static bool IsNumber(string inputData)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(inputData, "^[0-9]+$");
        }
        #endregion
    }
}