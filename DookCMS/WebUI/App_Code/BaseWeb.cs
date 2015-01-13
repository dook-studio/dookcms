using System.Web;
using System.Collections;
using System;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
///BaseWeb 的摘要说明
/// </summary>
public class BaseWeb
{
  
    public static readonly BaseWeb instance = new BaseWeb();

    #region 清除所有缓存
    /// <summary>
    /// 清除所有缓存
    /// </summary>
    public void RemoveAllCache()
    {

        System.Web.Caching.Cache cache = HttpRuntime.Cache;
        IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
        ArrayList al = new ArrayList();
        while (CacheEnum.MoveNext())
        {
            al.Add(CacheEnum.Key);
        }

        foreach (string key in al)
        {
            cache.Remove(key);
        }
    }
    #endregion

    public static string BaseUrl
    {
        get
        {
            string url = string.Empty;
            if (System.Web.HttpContext.Current.Request.Url.Port == 80)
            {
                url= "http://" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath;
            }
            else
            {
                if (Common.MyWeb.config["cdn"] == "true")
                {
                    url = "http://" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    url = "http://" + System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + System.Web.HttpContext.Current.Request.ApplicationPath;
                }
            }
            if (url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1, 1);
            }
            return url;
        }
    }


    #region 用户登录后的userid
    public int UserId
    {
        get
        {
            try
            {
                return Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            System.Web.HttpContext.Current.Session["UserId"] = value;
        }
    }
    #endregion

    #region 用户登录后的userid
    public System.Data.DataRowView UserInfo
    {
        get
        {
            try
            {
                return System.Web.HttpContext.Current.Session["userinfo"] as System.Data.DataRowView;
            }
            catch
            {
                return null;
            }
        }
        set
        {
            System.Web.HttpContext.Current.Session["userinfo"] = value;
        }
    }
    #endregion

    #region 后台登录后的AdminID
    public int AdminId
    {
        get
        {
            try
            {
                return Convert.ToInt32(System.Web.HttpContext.Current.Session["AdminId"]);
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            System.Web.HttpContext.Current.Session["AdminId"] = value;
        }
    }
    #endregion

    public static bool IsAccessData
    {
        get
        {
            string dal = ConfigurationManager.AppSettings["DAL"];
            if (dal.ToLower() == "SQLServerDAL".ToLower())//sqlserver数据库
            {
                return false;
            }
            return true;
        }
    }

    
    public static string CreateHtmlProcess = "正在准备生成...";//线程阻塞

    #region get方式访问页面
    public string SendDataByGET(string Url, string postDataStr, ref CookieContainer cookie)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
        if (cookie.Count == 0)
        {
            request.CookieContainer = new CookieContainer();
            cookie = request.CookieContainer;
        }
        else
        {
            request.CookieContainer = cookie;
        }
        request.Method = "GET";
        request.ContentType = "text/html;charset=UTF-8";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        string retString = myStreamReader.ReadToEnd();
        myStreamReader.Close();
        myResponseStream.Close();

        return retString;
    }
    #endregion
}
