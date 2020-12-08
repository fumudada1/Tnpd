using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.Linq;
using System.Net.Mime;
using System.Xml;
using TnpdModels;

namespace SynchronizeAD
{
    class Program
    {
        static BackendContext _db = new BackendContext();
        static List<Unit> Units = _db.Units.ToList();
        static List<Member> members = _db.Members.ToList();
        static void Main(string[] args)
        {
            XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(@"C:\WebSite\tnpd\SynchronizeAD\TNPD.config");

            int PortalID;
            int UserID = -1;
            int timeOut = 0;
            string ADschema = null;
            string Domain = null;
            string adm, pwd, domainOU, title;
            foreach (XmlNode xn in xd.DocumentElement.SelectNodes("//Domain"))
            {
                PortalID = Convert.ToInt16(xn.Attributes["PortalID"].Value);
                adm = xn["LogonID"].InnerText;
                pwd = xn["Password"].InnerText;
                Domain = xn["Name"].InnerText;
                domainOU = xn["OU"].InnerText; 
                title = xn.Attributes["Title"].Value;

                ADschema = GetLDAPPath(Domain, domainOU);
                // System.Collections.ArrayList Users = GetEntryUsers(adm, pwd, Domain, timeOut, domainOU);
                System.Collections.ArrayList nodeList = GetAll(adm, pwd, Domain, timeOut, domainOU);
            }
            //Console.ReadKey();
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
                    var unit = Units.FirstOrDefault(x => x.Alias == "Tainan City Police Department");
                    unit.Subject = "局本部";
                    VisitNode(entry, 1);

                }
                if (entry.Name.IndexOf("Tainan City Police Precinct") > 0)
                {

                    VisitNode(entry, 1);

                }
            }
            return aryObj;
        }

        public static void VisitNode(DirectoryEntry ParentEntry, int ParentId)
        {
            foreach (DirectoryEntry childEntry1 in ParentEntry.Children)
            {
                string objectClass = GetPropertyValue(childEntry1, "objectClass");
                string description = GetPropertyValue(childEntry1, "description");
                string name = GetPropertyValue(childEntry1, "name");
                string direkSearchGuide = GetPropertyValue(childEntry1, "direkSearchGuide");
                if (direkSearchGuide == "30828")
                {
                    Console.WriteLine(name + "," + description + "," + objectClass + "," +
                                      direkSearchGuide);
                    Console.WriteLine("pid=" + ParentId);
                    //Console.ReadKey();
                }
                
                if (objectClass == "organizationalUnit")
                {

                    Console.WriteLine(name + "," + description + "," + objectClass + "," +
                                      direkSearchGuide);
                    Unit unit = Units.FirstOrDefault(x => x.Alias == name);
                    
                    if (unit != null)
                    {
                        unit.Subject = description;
                        unit.Alias = name;
                        if (IsNumber(direkSearchGuide))
                        {
                            unit.ListNum = Convert.ToInt16(direkSearchGuide);
                        }
                        //Console.WriteLine(name + "," + description + "," + objectClass + "," +
                        //                  direkSearchGuide);
                    }
                    else
                    {
                        unit = new Unit();
                        unit.Subject = description;
                        unit.Alias = name;
                        if (IsNumber(direkSearchGuide))
                        {
                            unit.ListNum = Convert.ToInt16(direkSearchGuide);
                        }
                        unit.ParentId = ParentId;
                        _db.Units.Add(unit);
                    }
                    _db.SaveChanges();
                    VisitNode(childEntry1, unit.Id);
                    

                }

                if (objectClass == "user")
                {

                    string displayName = GetPropertyValue(childEntry1, "displayName");
                    string descriptionName = GetPropertyValue(childEntry1, "description");
                    string userPrincipalName = GetPropertyValue(childEntry1, "userPrincipalName");
                    string sAMAccountName = GetPropertyValue(childEntry1, "sAMAccountName");

                    if (string.IsNullOrEmpty(descriptionName))
                    {
                        descriptionName = displayName;
                    }
                    //Console.WriteLine(descriptionName);
                    Member member = members.FirstOrDefault(x => x.Account == sAMAccountName);
                    if (member != null)
                    {
                        member.Name = descriptionName;
                        member.Email = userPrincipalName;
                        member.UnitId = ParentId;
                    }
                    else
                    {
                        member=new Member();
                        member.Account = sAMAccountName;
                        member.Name = descriptionName;
                        member.Email = userPrincipalName;
                        member.UnitId = ParentId;
                        _db.Members.Add(member);
                    }
                    _db.SaveChanges();
                }
            }
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
