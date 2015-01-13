using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;


namespace Common
{
    //采集html类
    public class DownLoadHelper
    {

        /// <summary>
        /// 获取网页编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Encoding GetPageEncoding(string url)
        {
            try
            {
                WebRequest wr = WebRequest.Create(url);
                WebResponse wrs = wr.GetResponse();
                string code = null;
                //根据Headers判断编码 
                string ctype = wrs.Headers["content-type"];
                if (ctype != null)
                {
                    ctype = ctype.ToLower();
                    int i = ctype.IndexOf("charset");
                    if (i > 0)
                        code = ctype.Substring(ctype.IndexOf("=") + 1);
                }
                //如果Headers无法判断，则从网页meta标签中读取 
                if (code == null)
                {
                    Stream stream = wrs.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string line;
                    //Regex regex = new Regex("<meta.*? content=(['\"]?).*?charset=(?<code>[^\\s]+)\\1.*?>", RegexOptions.IgnoreCase);
                    Regex regex = new Regex("<meta.*? charset=(['\"]?).*?(?<code>[^\\s]+)\\1.*?>", RegexOptions.IgnoreCase);
                    Match m;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (regex.IsMatch(line))
                        {
                            m = regex.Match(line);
                            code = m.Groups["code"].Value;
                            code = code.Replace("\"", "");
                            break;
                        }
                    }
                }
                Encoding e = Encoding.ASCII;
                if (code != null)
                {
                    try
                    {
                        e = Encoding.GetEncoding(code);
                    }
                    catch
                    {
                        e = Encoding.GetEncoding("gb2312");
                    }
                }
                try
                {
                    wrs.Close();
                }
                catch (Exception ex) { }
                return e;
            }
            catch
            {
                return GetPageEncodingOfAgent(url);
            }
        }
        private static HttpWebRequest GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            return request;
        }

        #region 尝试使用代理处理

        #region 使用代理获取内容
        public static string HtmlofAgent(string strLink, Encoding encoding, string containercookies)//使用代理
        {
            if (encoding == null)
            {
                encoding = GetPageEncoding(strLink);
            }
            string strHtml;
            try
            {

                HttpWebRequest request = HttpWebRequest.Create(strLink) as HttpWebRequest;
                request.Headers["Accept-Encoding"] = "gzip,deflate";
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.Credentials = CredentialCache.DefaultCredentials;//获取或这设置发送到主机并用于请求进行身份验证的网络凭据      
                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7";
                request.Referer = strLink;
                //request.Headers["Host"] = "zhanlan11a50.site3.idccenter.net";
                request.KeepAlive = true;
                //request.Method[

                request.AllowAutoRedirect = false;
                if (!string.IsNullOrEmpty(containercookies))
                {
                    CookieContainer cc = new CookieContainer();
                    string[] cookies = containercookies.Split(';');
                    foreach (var item in cookies)
                    {
                        string key = item.Split('=')[0].Trim();
                        string value = item.Replace(key + "=", "").Replace(",", "%2").Trim();
                        cc.Add(new Uri(strLink), new Cookie(key, value));
                    }
                    request.CookieContainer = cc;
                }

                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream sm = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(sm, encoding))
                    {
                        strHtml = sr.ReadToEnd();
                        sr.Close();
                        sm.Close();
                        response.Close();
                    }
                }
            }
            catch
            {
                strHtml = "";
            }
            return strHtml;
        }
        #endregion
        public static string Html(string strLink, Encoding encoding, bool isagent, string cookies)
        {
            if (isagent)
            {
                return HtmlofAgent(strLink, encoding, cookies);//代理
            }
            if (encoding == null)
            {
                encoding = GetPageEncoding(strLink);
            }
            string strHtml;
            try
            {
                // HttpWebRequest request = HttpWebRequest.Create(strLink);
                //HttpWebRequest request = HttpWebRequest.Create(strLink);
                HttpWebRequest request = HttpWebRequest.Create(strLink) as HttpWebRequest;
                request.Headers["Accept-Encoding"] = "gzip,deflate";
                request.AutomaticDecompression = DecompressionMethods.GZip;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream sm = response.GetResponseStream())
                {
                    //var gzip = true;
                    //if (gzip)
                    //{
                    //    using (StreamReader sr = new StreamReader(new GZipStream(sm, CompressionMode.Decompress), encoding))
                    //    {
                    //        strHtml = sr.ReadToEnd();
                    //        sr.Close();
                    //        sm.Close();
                    //        response.Close();
                    //    }
                    //}
                    //else
                    //{
                    using (StreamReader sr = new StreamReader(sm, encoding))
                    {
                        strHtml = sr.ReadToEnd();
                        sr.Close();
                        sm.Close();
                        response.Close();
                    }
                    //}
                }
                if (strHtml.Trim() == "")
                {
                    return HtmlofAgent(strLink, encoding, cookies);
                }
            }
            catch
            {
                //出错尝试使用代理
                return HtmlofAgent(strLink, encoding, cookies);
            }
            return strHtml;
        }
        #region 使用代理获取网页编码
        public static Encoding GetPageEncodingOfAgent(string url)
        {
            try
            {
                HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
                wr.Credentials = CredentialCache.DefaultCredentials;//获取或这设置发送到主机并用于请求进行身份验证的网络凭据               
                wr.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7";
                //myWebClient.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.7 (KHTML, like Gecko) Chrome/16.0.912.75 Safari/535.7 MSIE 9.0;");
                WebResponse wrs = wr.GetResponse();
                string code = null;
                //根据Headers判断编码 
                string ctype = wrs.Headers["content-type"];
                if (ctype != null)
                {
                    ctype = ctype.ToLower();
                    int i = ctype.IndexOf("charset");
                    if (i > 0)
                        code = ctype.Substring(ctype.IndexOf("=") + 1);
                }
                //如果Headers无法判断，则从网页meta标签中读取 
                if (code == null)
                {
                    Stream stream = wrs.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string line;
                    Regex regex = new Regex("<meta.*? content=(['\"]?).*?charset=(?<code>[^\\s]+)\\1.*?>", RegexOptions.IgnoreCase);
                    Match m;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (regex.IsMatch(line))
                        {
                            m = regex.Match(line);
                            code = m.Groups["code"].Value;
                            break;
                        }
                    }
                }
                Encoding e = Encoding.ASCII;
                if (code != null)
                {
                    try
                    {
                        e = Encoding.GetEncoding(code);
                    }
                    catch
                    {
                        e = Encoding.ASCII;
                    }
                }
                try
                {
                    wrs.Close();
                }
                catch (Exception ex) { }
                return e;
            }
            catch { }

            return Encoding.GetEncoding("gb2312");

        }
        #endregion
        #endregion
    }
}
