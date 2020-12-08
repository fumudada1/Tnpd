using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace TnpdAdmin.Models
{
    ///
    /// 使用者帳戶選項。
    ///
    public enum ADAccountOptions
    {
        ///
        /// 本機帳戶選項
        ///
        UF_TEMP_DUPLICATE_ACCOUNT = 0x0100,
        ///
        /// 一般帳戶選項
        ///
        UF_NORMAL_ACCOUNT =0x0200,
        ///
        /// 跨網域信任帳戶選項
        ///
        UF_INTERDOMAIN_TRUST_ACCOUNT =0x0800,
        ///
        /// 工作站信任帳戶選項
        ///
        UF_WORKSTATION_TRUST_ACCOUNT = 0x1000,
        ///
        /// 伺服器信任帳戶選項
        ///
        UF_SERVER_TRUST_ACCOUNT =0x2000,
        ///
        /// 密碼永久有效選項
        ///
        UF_DONT_EXPIRE_PASSWD=0x10000,
        ///
        /// 登錄Script選項。如果通過 ADSI LDAP 協定進行讀或寫操作時，該選項失效。如果通過 ADSI WINNT 協定，該選項為唯讀。
        ///
        UF_SCRIPT =0x0001,
        ///
        /// 帳號禁用選項
        ///
        UF_ACCOUNTDISABLE=0x0002,
        ///
        /// 預設目錄選項
        ///
        UF_HOMEDIR_REQUIRED =0x0008,
        ///
        /// 帳戶鎖定選項
        ///
        UF_LOCKOUT=0x0010,
        ///
        /// 使用者必須在下次登入變更密碼選項
        ///
        UF_PASSWD_NOTREQD=0x0020,
        ///
        /// 使用者不能變更密碼選項
        ///
        UF_PASSWD_CANT_CHANGE=0x0040,
        ///
        /// 帳戶過期選項
        ///
        UF_ACCOUNT_LOCKOUT=0X0010,
        ///
        /// 使用可回復加密來存放密碼選項
        ///
        UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED=0X0080,
        ///
        /// 這個帳戶需要使用DES加密類型選項
        ///
        UF_USE_DES_KEY_ONLY = 0X200000,
        ///
        /// 不需要Kerberos預先驗證選項
        ///
        UF_DONT_REQUIRE_PREAUTH = 0X4000000,
    }
   
    ///
    /// 登入驗證回應結果
    ///
    public enum LoginResult
    {
        ///
        /// 登入成功
        ///
        LOGIN_OK=0,
        ///
        /// 此帳戶不存在
        ///
        LOGIN_USER_DOESNT_EXIST,
        ///
        /// 此帳戶已停用
        ///
        LOGIN_USER_ACCOUNT_INACTIVE,
        ///
        /// 帳戶密碼不正確
        ///
        LOGIN_USER_PASSWORD_INCORRECT
    }
    public class ActiveDirectory
    {
        private static string ADServer ="";
        private static string ADUser = "";
        private static string ADPassword = "";
        private static string ADPath ="";
        private static IdentityImpersonation impersonate ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strADServer">ADServer</param>
        /// <param name="strAdUser">ADAdminUser</param>
        /// <param name="strAdPassword">ADAdminPassword</param>
        /// <param name="strAdPath"></param>
        public ActiveDirectory(string strADServer,string strAdUser,string strAdPassword,string strAdPath)
        {
            ADServer = strADServer;
            ADUser = strAdUser;
            ADPassword = strAdPassword;
            ADPath = strAdPath;
            impersonate = new IdentityImpersonation(ADUser, ADPassword, ADServer);
        }

        public ActiveDirectory(string strAdPath)
        {
           
            ADPath = strAdPath;
            
        }

        #region GetDirectory Object
        ///
        /// 依據Web.Config的ADUser,ADPassword登入Active Directory。
        ///
        ///
        private static DirectoryEntry GetDirectoryObject()
        {
            DirectoryEntry entry = new DirectoryEntry(ADPath, ADUser, ADPassword, AuthenticationTypes.Secure);
            return entry;
        }
        ///
        /// 依據名稱及密碼尋找出互相對應關係的DirectoryEntry。
        ///
        ///
        ///
        ///
        private static DirectoryEntry GetDirectoryObject(string userName, string password)
        {
            DirectoryEntry entry = new DirectoryEntry(ADPath, userName, password, AuthenticationTypes.Secure);
            return entry;
        }
        ///
        /// 依據Domain尋找出互相對應關係的DirectoryEntry(Domain Exp:/CN=Test,DC=MyActiveDirectory,DC=Com,DC=tw)。
        ///
        ///
        ///
        private static DirectoryEntry GetDirectoryObject(string domainReference)
        {
            DirectoryEntry entry = new DirectoryEntry(ADPath + domainReference, ADUser, ADPassword, AuthenticationTypes.Secure);
            return entry;
        }
        ///
        /// 依據Domain及名稱和密碼尋找出互相對應關係的DirectoryEntry(Domain Exp:/CN=Test,DC=MyActiveDirectory,DC=Com,DC=tw)。
        ///
        ///
        ///
        ///
        ///
        private static DirectoryEntry GetDirectoryObject(string domainReference, string userName, string password)
        {
            DirectoryEntry entry = new DirectoryEntry(ADPath + domainReference, userName, password, AuthenticationTypes.Secure);
            return entry;
        }
        #endregion
        #region GetDirectoryEntry
        ///
        /// 根據指定的名稱在Active Directory上搜尋。
        ///
        ///
        ///
        public static DirectoryEntry GetDirectoryEntry(string commonName)
        {
            DirectoryEntry de = GetDirectoryObject();
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(cn=" + commonName + "))";
            deSearch.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }
        ///
        /// 根據指定的名稱及密碼在Active Directory上搜尋。
        ///
        ///
        ///
        ///
        public static DirectoryEntry GetDirectoryEntry(string commonName, string password)
        {
            DirectoryEntry de = GetDirectoryObject(commonName, password);
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(cn=" + commonName + "))";
            deSearch.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }
        ///
        ///根據指定的帳號在Active Directory上搜尋。
        ///
        ///
        ///
        public static DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName)
        {
            DirectoryEntry de = GetDirectoryObject();
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))";
            deSearch.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }
        ///
        /// 根據指定的帳號及密碼在Active Directory上搜尋。
        ///
        ///
        ///
        ///
        public static DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName, string password)
        {
            DirectoryEntry de = GetDirectoryEntryByAccount(sAMAccountName);
            if (de != null)
            {
                string commonName = de.Properties["cn"][0].ToString();
                if (GetDirectoryEntry(commonName, password) != null)
                    return GetDirectoryEntry(commonName, password);
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        ///
        /// 根據指定的群組名稱在Active Directory上搜尋。
        ///
        ///
        ///
        public static DirectoryEntry GetDirectoryEntryOfGroup(string groupName)
        {
            DirectoryEntry de = GetDirectoryObject();
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(objectClass=group)(cn=" + groupName + "))";
            deSearch.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResult result = deSearch.FindOne();
                de = new DirectoryEntry(result.Path);
                return de;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        
        ///新增Active Directory使用者(DN位置,名稱,登入帳號,密碼)。
        
        public static DirectoryEntry CreateNewUser(string LDAPDN, string commonName, string sAMAccountName, string password)
        {
            DirectoryEntry entry = GetDirectoryObject();
            DirectoryEntry subEntry = entry.Children.Find(LDAPDN);
            DirectoryEntry deUser = subEntry.Children.Add("CN=" + commonName, "user");
            deUser.Properties["sAMAccountName"].Value = sAMAccountName;
            deUser.CommitChanges();
            //EnableUser(commonName);
            SetPassword(commonName, password);
            deUser.Close();
            return deUser;
        }
        
        ///刪除Active Directory上的使用者(DN位置,名稱)
        public static DirectoryEntry RemoveUser(string LDAPDN, string commonName)
        {
            DirectoryEntry entry = GetDirectoryObject();
            DirectoryEntry subEntry = entry.Children.Find(LDAPDN);
            subEntry.Children.Remove(GetDirectoryEntry(commonName));
            subEntry.CommitChanges();
            subEntry.Close();
            return subEntry;
        }

        ///驗證使用者：
        ///判斷使用者是否存在於Active Driectory上(名稱)
        public static bool UserExists(string commonName)
        {
            DirectoryEntry de = GetDirectoryObject();
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(&(objectCategory=person)(objectClass=user))(cn=" + commonName + "))";
            SearchResultCollection results = deSearch.FindAll();
            if (results.Count == 0)
                return false;
            else
                return true;
        }
        ///檢查使用者是否驗證通過：
        ///檢查使用者是否驗證通過(使用者名稱,密碼)
        public static bool IsUserValid(string UserName, string Password)
        {
            try
            {
                DirectoryEntry deUser = GetDirectoryEntry(UserName,Password);
                deUser.Close();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        ///驗證使用者是否登入成功：
        ///驗證使用者是否登入成功(使用者名稱,密碼)
        public static TnpdAdmin.Models.LoginResult Login(string UserName, string Password)
        {
            if(IsUserValid(UserName,Password))
            {
                DirectoryEntry de = GetDirectoryEntry(UserName);
                if(de !=null)
                {
                    int userAccountControl = Convert.ToInt32(de.Properties["userAccountControl"][0]);
                    de.Close();
                    if(!IsAccountActive(userAccountControl))
                    {
                        return LoginResult.LOGIN_USER_ACCOUNT_INACTIVE;
                    }
                    else
                    {
                        return LoginResult.LOGIN_OK;
                    }
                }
                else
                {
                    return LoginResult.LOGIN_USER_DOESNT_EXIST;
                }
            }
            else
            {
                return LoginResult.LOGIN_USER_DOESNT_EXIST;
            }
        }

        private static bool IsAccountActive(int userAccountControl)
        {
            return !Convert.ToBoolean(userAccountControl & 0x0002);
        }

        //重設Active Directory上的使用者密碼：
        ///重設Active Directory上的使用者密碼(名稱,新密碼)
        public static void SetPassword(string commonName, string newPassword)
        {
            DirectoryEntry de = GetDirectoryEntry(commonName);
            IdentityImpersonation impersonate = new IdentityImpersonation(ADUser, ADPassword, ADServer);
            impersonate.BeginImpersonate();
            de.Invoke("SetPassword", new Object[]{newPassword});
            impersonate.StopImpersonate();
            de.Close();
        }
        
        //將Active Directory的使用者加入指定的群組：
        ///將Active Directory的使用者加入指定的群組(使用者名稱,群組名稱)
        public static void AddUserToGroup(string userCommonName, string groupName)
        {
            DirectoryEntry oGroup = GetDirectoryEntryOfGroup(groupName);
            DirectoryEntry oUser = GetDirectoryEntry(userCommonName);
            impersonate.BeginImpersonate();
            oGroup.Properties["member"].Add(oUser.Properties["distinguishedName"].Value);
            oGroup.CommitChanges();
            impersonate.StopImpersonate();
            oGroup.Close();
            oUser.Close();
        }
        //將Active Directory的使用者移除指定的群組：
        /// 將Active Directory的使用者移除指定的群組(使用者名稱,群組名稱)
        public static void RemoveUserFromGroup(string userCommonName, string groupName)
        {
            DirectoryEntry oGroup = GetDirectoryEntryOfGroup(groupName);
            DirectoryEntry oUser = GetDirectoryEntry(userCommonName);
            impersonate.BeginImpersonate();
            oGroup.Properties["member"].Remove(oUser.Properties["distinguishedName"].Value);
            oGroup.CommitChanges();
            impersonate.StopImpersonate();
            oGroup.Close();
            oUser.Close();
        }
    }

    public class IdentityImpersonation
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateToken(IntPtr ExistingTokenHandle, int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);
        private String _sImperUsername;
        private String _sImperPassword;
        private String _sImperDomain;
        private WindowsImpersonationContext _imperContext;
        private IntPtr _adminToken;
        private IntPtr _dupeToken;
        private Boolean _bClosed;
        public IdentityImpersonation(String impersonationUsername, String impersonationPassword, String impersonationDomain)
        {
            _sImperUsername = impersonationUsername;
            _sImperPassword = impersonationPassword;
            _sImperDomain = impersonationDomain;
            _adminToken = IntPtr.Zero;
            _dupeToken = IntPtr.Zero;
            _bClosed = true;
        }
        ~IdentityImpersonation()
        {
            if (!_bClosed)
            {
                StopImpersonate();
            }
        }
        //Start Virtual
        public Boolean BeginImpersonate()
        {
            bool bLogined = LogonUser(_sImperUsername, _sImperDomain, _sImperPassword, 2, 0, ref _adminToken);
            if (!bLogined)
            {
                return false;
            }
            bool bDuped = DuplicateToken(_adminToken, 2, ref _dupeToken);
            if (!bDuped)
            {
                return false;
            }
            WindowsIdentity fakeId = new WindowsIdentity(_dupeToken);
            _imperContext = fakeId.Impersonate();
            _bClosed = false;
            return true;
        }
        //Stop Virtual
        public void StopImpersonate()
        {
            CloseHandle(_dupeToken);
            CloseHandle(_adminToken);
            _bClosed = true;
        }
    }
}
