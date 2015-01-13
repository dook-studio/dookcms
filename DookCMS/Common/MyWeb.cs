using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Common
{
    public static class MyWeb
    {
        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <returns></returns>
        public static bool IsAuthPass()
        {
            try
            {
                object objModel = Common.DataCache.GetCache("authpass");
                if (objModel == null)
                {
                    bool ispass = false;
                    string email = config["authemail"].ObjToStr();
                    string domain = System.Web.HttpContext.Current.Request.Url.Host;
                    if (domain.Contains("localhost") || domain.Contains("127.0.0.1"))
                    {
                        return true;
                    }
                    using (com.dukeycms.www.dukeyauthcms auth = new com.dukeycms.www.dukeyauthcms())
                    {
                        if (auth.AuthUser(email, domain) == "ok")
                        {
                            ispass = true;
                        }
                    }
                    Common.DataCache.SetCache("authpass", ispass);
                    return ispass;
                }
                else
                {
                    return (bool)objModel;
                }
            }
            catch
            {
                return false;
            }

        }

        #region 缓存数据
        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="cachekey">缓存键值</param>
        /// <param name="CacheMinutes">缓存时间,单位分</param>
        /// <returns></returns>
        public static object GetByCache(object data, string cachekey, int CacheMinutes)
        {
            object objModel = Common.DataCache.GetCache(cachekey);
            if (objModel == null)
            {
                try
                {
                    objModel = data;
                    if (objModel != null)
                    {
                        Common.DataCache.SetCache(cachekey, objModel, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel;
        }
        #endregion

        #region 淘宝客设置部分

        public static string Appkey = "12410451";
        public static string Secret = "ad55d584c7472e10259a1fe1f61baa55";
        public static string ServerURL = "http://gw.api.taobao.com/router/rest";

        #endregion

        public static int DbType
        {
            get
            {
                switch (ConfigHelper.GetConfigString("DAL"))
                {
                    case "Dukey.SQLServerDAL": return 1;
                    case "Dukey.AceessDAL": return 0;
                    case "Dukey.SQLiteDAL": return 2;
                    default: return 0;
                }
            }
        }

        #region 获取webconfig配置文件add节点集合
        /// <summary>
        /// 获取webconfig配置文件add节点集合  2012年8月16日11:37:47
        /// </summary>
        public static NameValueCollection config
        {
            get
            {
                try
                {
                    var cacheObj = DataCache.GetCache("cachewebconfig");
                    if (cacheObj != null)
                    {
                        return cacheObj as NameValueCollection;
                    }
                    else
                    {
                        var rs = System.Web.Configuration.WebConfigurationManager.AppSettings;
                        DataCache.SetCache("cachewebconfig", rs);
                        return rs;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
