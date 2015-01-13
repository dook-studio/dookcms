using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using ADOX;//该命名空间包含创建ACCESS的类(方法)--解决方案 ==> 引用 ==> 添加引用 ==> 游览找到.dll 
using JRO;//该命名空间包含压缩ACCESS的类(方法) 
namespace Common
{
    /// <summary> 
    /// 数据库恢复和备份 
    /// </summary> 
    public class DataBaseBackUpHelper
    {
        public DataBaseBackUpHelper()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
        }

        #region SQL数据库备份
        /// <summary> 
        /// SQL数据库备份 
        /// </summary> 
        /// <param name="ServerIP">SQL服务器IP或(Localhost)</param> 
        /// <param name="LoginName">数据库登录名</param> 
        /// <param name="LoginPass">数据库登录密码</param> 
        /// <param name="DBName">数据库名</param> 
        /// <param name="BackPath">备份到的路径</param> 
        public static void SQLBACK(string ServerIP, string LoginName, string LoginPass, string DBName, string BackPath)
        {
            SQLDMO.Backup oBackup = new SQLDMO.BackupClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect(ServerIP, LoginName, LoginPass);
                oBackup.Database = DBName;
                oBackup.Files = BackPath;
                oBackup.BackupSetName = DBName;
                oBackup.BackupSetDescription = "数据库备份";
                oBackup.Initialize = true;
                oBackup.SQLBackup(oSQLServer);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                oSQLServer.DisConnect();
            }
        }
        #endregion

        #region SQL恢复数据库
        /// <summary> 
        /// SQL恢复数据库 
        /// </summary> 
        /// <param name="ServerIP">SQL服务器IP或(Localhost)</param> 
        /// <param name="LoginName">数据库登录名</param> 
        /// <param name="LoginPass">数据库登录密码</param> 
        /// <param name="DBName">要还原的数据库名</param> 
        /// <param name="BackPath">数据库备份的路径</param> 
        public static void SQLDbRestore(string ServerIP, string LoginName, string LoginPass, string DBName, string BackPath)
        {
            SQLDMO.Restore orestore = new SQLDMO.RestoreClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect(ServerIP, LoginName, LoginPass);
                orestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                orestore.Database = DBName;
                orestore.Files = BackPath;
                orestore.FileNumber = 1;
                orestore.ReplaceDatabase = true;
                orestore.SQLRestore(oSQLServer);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                oSQLServer.DisConnect();
            }
        }
        #endregion

        #region 根据指定的文件名称创建Access数据库
        /// <summary> 
        /// 根据指定的文件名称创建数据 
        /// </summary> 
        /// <param name="DBPath">绝对路径+文件名称</param> 
        public static void CreateAccess(string DBPath)
        {
            if (File.Exists(DBPath))//检查数据库是否已存在 
            {
                throw new Exception("目标数据库已存在,无法创建");
            }
            DBPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            //创建一个CatalogClass对象实例 
            ADOX.CatalogClass cat = new ADOX.CatalogClass();
            //使用CatalogClass对象的Create方法创建ACCESS数据库 
            cat.Create(DBPath);
        }
        #endregion

        #region 压缩Access数据库
        /// <summary> 
        /// 压缩Access数据库 
        /// </summary> 
        /// <param name="DBPath">数据库绝对路径</param> 
        public static void CompactAccess(string DBPath)
        {
            if (!File.Exists(DBPath))
            {
                throw new Exception("目标数据库不存在,无法压缩");
            }
            //声明临时数据库名称 
            string temp = DateTime.Now.Year.ToString();
            temp += DateTime.Now.Month.ToString();
            temp += DateTime.Now.Day.ToString();
            temp += DateTime.Now.Hour.ToString();
            temp += DateTime.Now.Minute.ToString();
            temp += DateTime.Now.Second.ToString() + ".bak";
            temp = DBPath.Substring(0, DBPath.LastIndexOf("\\") + 1) + temp;
            //定义临时数据库的连接字符串 
            string temp2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + temp;
            //定义目标数据库的连接字符串 
            string DBPath2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath;
            //创建一个JetEngineClass对象的实例 
            JRO.JetEngineClass jt = new JRO.JetEngineClass();
            //使用JetEngineClass对象的CompactDatabase方法压缩修复数据库 
            jt.CompactDatabase(DBPath2, temp2);
            
            //拷贝临时数据库到目标数据库(覆盖) 
            File.Copy(temp, DBPath, true);
            //最后删除临时数据库 
            File.Delete(temp);
        }
        #endregion

        #region 备份Access数据库
        /// <summary> 
        /// 备份Access数据库 
        /// </summary> 
        /// <param name="srcPath">要备份的数据库绝对路径</param> 
        /// <param name="aimPath">备份到的数据库绝对路径</param> 
        /// <returns></returns> 
        public static void Backup(string srcPath, string aimPath)
        {
            if (!File.Exists(srcPath))
            {
                throw new Exception("源数据库不存在,无法备份");
            }
            try
            {
                File.Copy(srcPath, aimPath, true);
            }
            catch (IOException ixp)
            {
                throw new Exception(ixp.ToString());
            }
        }
        #endregion

        #region 还原Access数据库
        /// <summary> 
        /// 还原Access数据库 
        /// </summary> 
        /// <param name="bakPath">备份的数据库绝对路径</param> 
        /// <param name="dbPath">要还原的数据库绝对路径</param> 
        public static void RecoverAccess(string bakPath, string dbPath)
        {
            if (!File.Exists(bakPath))
            {
                throw new Exception("备份数据库不存在,无法还原");
            }
            try
            {
                File.Copy(bakPath, dbPath, true);
            }
            catch (IOException ixp)
            {
                throw new Exception(ixp.ToString());
            }
        }
        #endregion

    
    }
}
