using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.DirectoryServices;
using System.EnterpriseServices;

namespace Common
{
    /// <summary>
    /// IISAdmin 的摘要说明
    /// </summary>
    public class IISManager
    {

        #region 建IIS站点
        public int CreateWebSite(string webSiteName, string pathToRoot, string bd) //创建网站
        {
            DirectoryEntry root = new DirectoryEntry("IIS://localhost/W3SVC");
            // Find unused ID value for new web site
            int siteID = 1;
            foreach (DirectoryEntry e in root.Children)
            {
                if (e.SchemaClassName == "IIsWebServer")
                {
                    int ID = Convert.ToInt32(e.Name);
                    if (ID >= siteID)
                    {
                        siteID = ID + 1;
                    }
                }
            }
            // Create web site
            DirectoryEntry site = (DirectoryEntry)root.Invoke("Create", "IIsWebServer", siteID);
            site.Invoke("Put", "ServerComment", webSiteName);//网站名称
            site.Invoke("Put", "ServerBindings", bd);//二级域名绑定
            site.Invoke("Put", "ServerState", 2);//默认4
            site.Invoke("Put", "DefaultDoc", "index.htm,index.asp,index.aspx,Default.aspx");
            site.Invoke("Put", "ServerAutoStart", 1);//开启站点
            site.Invoke("SetInfo");
            DirectoryEntry siteVDir = site.Children.Add("ROOT", "IISWebVirtualDir");
            siteVDir.Invoke("AppCreate", true); //创建应用程序站点
            siteVDir.CommitChanges();
            site.CommitChanges();
            siteVDir.Properties["AppIsolated"][0] = 2;//默认2
            siteVDir.Properties["Path"][0] = pathToRoot;//主目录路径
            siteVDir.Properties["AccessFlags"][0] = 513;
            siteVDir.Properties["FrontPageWeb"][0] = 1;
            siteVDir.Properties["AppRoot"][0] = "/LM/W3SVC/" + siteID + "/Root";
            siteVDir.Properties["AppFriendlyName"][0] = "默认应用程序";
            siteVDir.Properties["AspEnableParentPaths"][0] = true; //父路径启用
            siteVDir.CommitChanges();
            site.CommitChanges();
            return siteID;
        }


        public void CreateSite(string port, string siteName, string siteExplain, string defaultDoc)
        {
            DirectoryEntry de = new DirectoryEntry("IIS://localhost/" + "w3svc");   //从活动目录中获取IIS对象。   
            object[] prams = new object[2] { "IIsWebServer", Convert.ToInt32(siteName) };
            DirectoryEntry site = (DirectoryEntry)de.Invoke("Create", prams); //创建IISWebServer对象。   
            site.Properties["KeyType"][0] = "IIsWebServer";
            site.Properties["ServerComment"][0] = siteExplain; //站点说明   
            site.Properties["ServerState"][0] = 2; //站点初始状态，1.停止，2.启动，3   
            site.Properties["ServerSize"][0] = 1;
            site.Properties["ServerBindings"].Add(":" + port + ":"); //站点端口   
            site.CommitChanges(); //保存改变   
            de.CommitChanges();
            DirectoryEntry root = site.Children.Add("Root", "IIsWebVirtualDir");   //添加虚拟目录对象   
            root.Invoke("AppCreate", true); //创建IIS应用程序   
            root.Properties["path"][0] = @"D:\IISmanage"; //虚拟目录指向的物理目录   
            root.Properties["EnableDirBrowsing"][0] = false;//目录浏览   
            root.Properties["AuthAnonymous"][0] = false;
            root.Properties["AccessExecute"][0] = false;   //可执行权限   
            root.Properties["AccessRead"][0] = true;
            root.Properties["AccessWrite"][0] = true;
            root.Properties["AccessScript"][0] = true;//纯脚本   
            root.Properties["AccessSource"][0] = false;
            root.Properties["FrontPageWeb"][0] = false;
            root.Properties["KeyType"][0] = "IIsWebVirtualDir";
            root.Properties["AppFriendlyName"][0] = siteExplain; //应用程序名     
            root.Properties["AppIsolated"][0] = 2;
            root.Properties["DefaultDoc"][0] = defaultDoc; //默认文档   
            root.Properties["EnableDefaultDoc"][0] = true; //是否启用默认文档   
            root.CommitChanges();
            site.CommitChanges();
            root.Close();
            site.Close();
            de.CommitChanges(); //保存   
            site.Invoke("Start", null); //除了在创建过程中置初始状态外，也可在此调用方法改变状态   
        }


        #endregion

        #region 域名绑定方法
        public static void AddHostHeader(int siteid, string ip, int port, string domain)//增加主机头（站点编号.ip.端口.域名）
        {
            DirectoryEntry site = new DirectoryEntry("IIS://localhost/W3SVC/" + siteid);
            PropertyValueCollection serverBindings = site.Properties["ServerBindings"];
            string headerStr = string.Format("{0}:{1}:{2}", ip, port, domain);
            if (!serverBindings.Contains(headerStr))
            {
                serverBindings.Add(headerStr);
            }
            site.CommitChanges();
        }
        #endregion

        #region 删除主机头
        public static void DeleteHostHeader(int siteid, string ip, int port, string domain)//删除主机头（站点编号.ip.端口.域名）
        {
            DirectoryEntry site = new DirectoryEntry("IIS://localhost/W3SVC/" + siteid);
            PropertyValueCollection serverBindings = site.Properties["ServerBindings"];
            string headerStr = string.Format("{0}:{1}:{2}", ip, port, domain);
            if (serverBindings.Contains(headerStr))
            {
                serverBindings.Remove(headerStr);
            }
            site.CommitChanges();
        }
        #endregion

        #region 删除站点
        public static string DelSite(int siteidon)
        {
            int SiteID = siteidon;
            //if (SiteID == null) return "error:该站点不存在!!";

            DirectoryEntry deRoot = new DirectoryEntry("IIS://localhost/W3SVC");

            DirectoryEntry deVDir = new DirectoryEntry();
            deRoot.RefreshCache();
            deVDir = deRoot.Children.Find("huke8huke", "IIsVirtualDir");
            deRoot.Children.Remove(deVDir);
            //deVDir.Invoke("AppDelete",true);
            deRoot.CommitChanges();
            deRoot.Close();
            return "successful:删除站点成功!";

        }
        #endregion
    }

}