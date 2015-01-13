using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Data;
using System.Reflection;

public static class StringHelper
{
    public static List<T> DataTableToList<T>(DataTable dt)
    {
        var list = new List<T>();
        Type t = typeof(T);
        var plist = new List<PropertyInfo>(typeof(T).GetProperties());

        foreach (DataRow item in dt.Rows)
        {
            T s = System.Activator.CreateInstance<T>();          
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PropertyInfo info = plist.Find(p => p.Name.ToLower() == dt.Columns[i].ColumnName.ToLower());
                if (info != null)
                {
                    string dd = item[i].GetType().ToString();
                    
                   
                    if (!Convert.IsDBNull(item[i]))
                    {
                        if (dd == "System.Int64")
                        {
                            info.SetValue(s,Convert.ToInt32(item[i]), null);
                        }
                        else
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
            }
            list.Add(s);
        }
        return list;
    }

    #region 修改css样式
    /// <summary>
    /// 修改css
    /// </summary>
    /// <param name="csstxt">css文本内容(读取css文件文本)</param>
    /// <param name="cssName">样式名称</param>
    /// <param name="theattr">键名</param>
    /// <param name="thecss">键值</param>
    public static void UpdateCss(ref string csstxt, string cssName, string theattr, string thecss)
    {
        string str = @"(?is)(?<=" + cssName + "(\\s*)\\{)[^\\}]*(?=\\})";
        Match m = new Regex(str, RegexOptions.IgnoreCase).Match(csstxt);
        if (m.Success)
        {
            int start = csstxt.IndexOf(m.Value);
            csstxt = csstxt.Remove(start, m.Value.Length);

            string ff = "";
            string[] arrCss = m.Value.Split(';');
            bool isnew = true;
            foreach (var item in arrCss)
            {
                if (!string.IsNullOrEmpty(item.Trim()))
                {
                    string key = item.Split(':')[0];
                    string value = item.Split(':')[1];

                    if (theattr == key)
                    {
                        isnew = false;
                        value = thecss;
                    }
                    ff += (key + ": " + value + "; ");
                }
            }
            if (isnew)
            {
                ff += (theattr + ": " + thecss + "; ");
            }
            csstxt = csstxt.Insert(start, ff);
        }
        else
        {
            csstxt += ("\r\n" + cssName + " {" + theattr + ": " + thecss + "; }");
        }
    }

    #endregion

    #region 读取css样式
    /// <summary>
    /// 读取css,返回键值
    /// </summary>
    /// <param name="csstxt">css文本内容(读取css文件文本)</param>
    /// <param name="cssName">样式名称</param>
    /// <param name="theattr">键名</param>
    public static string ReadCss(ref string csstxt, string cssName, string theattr)
    {
        string cssvalue = "";
        string str = @"(?is)(?<=" + cssName + "(\\s*)\\{)[^\\}]*(?=\\})";
        Match m = new Regex(str, RegexOptions.IgnoreCase).Match(csstxt);
        if (m.Success)
        {
           
            string[] arrCss = m.Value.Split(';');           
            foreach (var item in arrCss)
            {
                if (!string.IsNullOrEmpty(item.Trim()))
                {
                    string key = item.Split(':')[0];
                    string value = item.Split(':')[1];

                    if (theattr == key)
                    {
                        cssvalue = value;
                        break;
                    }                   
                }
            }  
        }
        return cssvalue;      
    }
    #endregion

    /// <summary>
    /// 过滤非法字符
    /// </summary>
    /// <param name="strValue">字符串</param>
    /// <returns>替换字符</returns>
    public static string RepalceCharacter(string strValue)
    {
        if (string.IsNullOrEmpty(strValue))
            return "";
        strValue = strValue.Replace("'", "＇");
        strValue = strValue.Replace("\"", "＂");       
        return strValue;
    }

    #region 检查是否为数字
    /// <summary>
    /// 检查是否为数字
    /// </summary>
    /// <param name="strData"></param>
    /// <returns></returns>
    public static bool IsNumeric(string strData)
    {
        float fData;
        bool bValid = true;
        try
        {
            fData = float.Parse(strData);
        }
        catch (FormatException)
        {
            bValid = false;
        }
        return bValid;
    }
    #endregion    

    #region 处理输入框字符 InputText(string inputString, int maxLength)
    /// <summary>
    /// 处理输入框
    /// </summary>
    /// <param name="inputString"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string InputText(string inputString, int maxLength)
    {

        StringBuilder retVal = new StringBuilder();

        if ((inputString != null) && (inputString != String.Empty))
        {
            inputString = inputString.Trim();

            if (inputString.Length > maxLength)
                inputString = inputString.Substring(0, maxLength);

            for (int i = 0; i < inputString.Length; i++)
            {
                switch (inputString[i])
                {
                    case '"':
                        retVal.Append("&quot;");
                        break;
                    case '<':
                        retVal.Append("&lt;");
                        break;
                    case '>':
                        retVal.Append("&gt;");
                        break;
                    default:
                        retVal.Append(inputString[i]);
                        break;
                }
            }

            retVal.Replace("'", "   ");
        }

        return retVal.ToString();
    }
    #endregion

    #region 替换非法字符
    public static string ReplaceWord(string str)
    {
        string strWord = "";
        strWord = str.Replace("<", "＜");
        strWord = strWord.Replace(">", "＞");
        strWord = strWord.Replace("'", "’");
        strWord = strWord.Replace("\"", "＂");
        strWord = strWord.Replace("chr(13)", "");
        strWord = strWord.Replace("chr(10)", "");
        return strWord;
    }
    #endregion

    public static string FormatNumber(string str, int n)
    {
        int tmpNum = str.IndexOf(".");
        if (tmpNum != -1)
        {
            if (n <= str.Length - tmpNum - 1)
            {
                str = str.Substring(0, tmpNum + 1 + n);
            }
        }
        return str;
    }

    #region 判断是否为整型数字
    public static bool isNum(string s) //判断是否为整型数字
    {
        if (string.IsNullOrEmpty(s)) return false;
        for (int index = 0; index < s.Length; index++)
        {
            if (48 > (int)s[index] || (int)s[index] > 57)
                return false;
        }
        return true;
    }
    #endregion

    /// <summary>
    /// 格式化数字
    /// </summary>
    /// <param name="Number"></param>
    /// <returns></returns>
    public static string FormatNumber(string Number)
    {
        int tmpNum = Number.IndexOf(".");
        if (tmpNum != -1)
        {
            string tmpString = Number.Substring(tmpNum + 1, Number.Length - tmpNum - 1);
            int tmpNumber = tmpString.Length;
            for (int i = 0; i < tmpNumber; i++)
            {
                if (tmpString.EndsWith("0"))
                {
                    int tmpLength = tmpString.Length;
                    tmpString = tmpString.Substring(0, tmpLength - 1);
                    int numLength = Number.Length;
                    Number = Number.Substring(0, numLength - 1);
                }
                else
                {
                    break;
                }
            }
            if (tmpString.Length == 0)
            {
                Number = Number.Substring(0, tmpNum);
            }
        }
        return Number;
    }

    #region 打开一个新窗口(钟健)
    /// <summary>
    /// 打开一个新窗口(钟健)
    /// </summary>
    /// <param name="url">要链接的web地址</param>
    /// <param name="Wwhith">窗口的宽度</param>
    /// <param name="Wheight">窗口的高度</param>
    /// <param name="scrollbar">是否允许滚动条，1允许，0不允许</param>
    /// <param name="resize">是否允许改变窗口尺寸，1允许，0不允许</param>
    /// <param name="WindowName">弹出窗口的窗口名称</param>
    public static string OpenNewWindow(string url, int Wwhith, int Wheight, int scrollbar, int resize, string WindowName)
    {
        string scrollbars, resizable;
        scrollbars = scrollbar == 1 ? "yes" : "no";
        resizable = resize == 1 ? "yes" : "no";
        string sJs = "window.open('" + url + "','" + WindowName + "','scrollbars=" + scrollbars + ",resizable=" + resizable + ",top=200,left=200,width=" + Wwhith + ",height=" + Wheight + "')";
        return sJs;
    }
    #endregion

    #region 格式化字符串长度 (钟健)StringFormat()
    /// <summary>
    /// 格式化字符串长度
    /// </summary>
    /// <param name="str">需要格式的字符串</param>
    /// <param name="n">保留的程度</param>
    /// <returns></returns>
    public static string StringFormat(string str, int n)//格式化字符串长度
    {
        ///
        ///格式化字符串长度，超出部分显示省略号,区分汉字跟字母。汉字2个字节，字母数字一个字节
        ///
        string temp = string.Empty;
        if (System.Text.Encoding.Default.GetByteCount(str) <= n)//如果长度比需要的长度n小,返回原字符串
        {
            return str;
        }
        else
        {
            int t = 0;
            char[] q = str.ToCharArray();
            Regex r = new Regex(@"[，。；？~！：‘“”’【】（）]");

            for (int i = 0; i < q.Length && t < n; i++)
            {
                if (((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5) || r.IsMatch(q[i].ToString()))//是否汉字或者中文标点
                {
                    temp += q[i];
                    t += 2;
                }
                else
                {
                    temp += q[i];
                    t++;
                }
            }
            return (temp);
        }

    }
    #endregion

    #region 转换星期到中文
    public static string WeekToCh(string week)
    {
        string chWeek = "";
        switch (week.ToLower())
        {
            case "sunday": chWeek = "星期日"; break;
            case "monday": chWeek = "星期一"; break;
            case "tuesday": chWeek = "星期二"; break;
            case "wednesday": chWeek = "星期三"; break;
            case "thursday": chWeek = "星期四"; break;
            case "friday": chWeek = "星期五"; break;
            case "saturday": chWeek = "星期六"; break;                      
        }
        return chWeek;
    }
    #endregion

    #region 读取模板文件
    /// <summary>
    /// 功能：读取模板文件
    /// 作者：ｚｊ
    /// 日期：2007年12月12日
    /// </summary>
    /// <param name="path">模板文件</param>
    /// <returns></returns>
    public static string ReadFile(string path)
    {
        string str = "";
        StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(path), Encoding.GetEncoding("gb2312"));
        try
        {
            str = sr.ReadToEnd();
            sr.Close();
        }
        catch
        {
            sr.Close();
        }
        //while (sr != null)
        //{
        //    str = sr.ReadLine();
        //    //str +="\r\n";
           
        //}
        //sr.Close();
        return str;
    }
    #endregion   

 
    public static string SubString(string str, int length, string strAppend)
    {
        if (str.Length > length)
        {
            str = str.Substring(0, length) + strAppend;
        }
        return str;
    }
  
    public enum DateInterval
    {
        Second, Minute, Hour, Day, Week, Month, Quarter, Year
    }


    #region c#中的datediff功能(DateDiff)
    /// <summary>
    /// c#中的datediff功能
    /// </summary>
    /// <param name="Interval"></param>
    /// <param name="StartDate"></param>
    /// <param name="EndDate"></param>
    /// <returns></returns>
    public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
    {
        long lngDateDiffValue = 0;
        System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
        switch (Interval)
        {
            case DateInterval.Second:
                lngDateDiffValue = (long)TS.TotalSeconds;
                break;
            case DateInterval.Minute:
                lngDateDiffValue = (long)TS.TotalMinutes;
                break;
            case DateInterval.Hour:
                lngDateDiffValue = (long)TS.TotalHours;
                break;
            case DateInterval.Day:
                lngDateDiffValue = (long)TS.Days;
                break;
            case DateInterval.Week:
                lngDateDiffValue = (long)(TS.Days / 7);
                break;
            case DateInterval.Month:
                lngDateDiffValue = (long)(TS.Days / 30);
                break;
            case DateInterval.Quarter:
                lngDateDiffValue = (long)((TS.Days / 30) / 3);
                break;
            case DateInterval.Year:
                lngDateDiffValue = (long)(TS.Days / 365);
                break;
        }
        return (lngDateDiffValue);
    }
    #endregion

    #region 检查是否含有非法字符ChkBadChar
    /// <summary> 
    /// 检查是否含有非法字符 
    /// </summary> 
    /// <param name="str">要检查的字符串 </param> 
    /// <returns> </returns> 
    public static bool IsBadChar(string str)
    {
        bool result = false;
        if (string.IsNullOrEmpty(str))
            return true;
        string strBadChar, tempChar;
        string[] arrBadChar;
        strBadChar = "@@,+,',--,%,^,&,?,(,), <,>,[,],{,},/,\\,;,:,\",\"\"";
        arrBadChar = strBadChar.Split(',');
        tempChar = str;
        for (int i = 0; i < arrBadChar.Length; i++)
        {
            if (tempChar.IndexOf(arrBadChar[i]) >= 0)
                result = true;
        }
        return result;
    }
    #endregion

    #region 过滤非法字符 ReplaceBadChar
    /// <summary> 
    /// 过滤非法字符 
    /// </summary> 
    /// <param name="str"> </param> 
    /// <returns> </returns> 
    public static string ReplaceBadChar(string str)
    {
        if (string.IsNullOrEmpty(str))
            return "";
        string strBadChar, tempChar;
        string[] arrBadChar;
        strBadChar = "@@,+,',=,--,%,^,&,?,(,), <,>,[,],{,},/,\\,;,:,\",\"\"";
        arrBadChar = strBadChar.Split(',');
        tempChar = str.Trim();
        for (int i = 0; i < arrBadChar.Length; i++)
        {
            if (arrBadChar[i].Length > 0)
                tempChar = tempChar.Replace(arrBadChar[i], "");
        }
        return tempChar;
    }
    #endregion

    #region 替换sql语句中的有问题符号 ReplaceBadSQL
    /// <summary> 
    /// 替换sql语句中的有问题符号 
    /// </summary> 
    public static string ReplaceBadSQL(string str)
    {
        string str2 = "";
        if (string.IsNullOrEmpty(str))
        {
            return "";
        }
        string str1 = str;
        string[] strArray = new string[] {"--"};
        StringBuilder builder = new StringBuilder(str1);
        for (int i = 0; i < strArray.Length; i++)
        {
            str2 = builder.Replace(strArray[i], "").ToString();
        }
        return builder.Replace("@@", "@").ToString();


    }
    #endregion
    
    #region 生成静态页面
    /// <summary>
    /// 生成静态页面
    /// </summary>
    /// <param name="sourcePath">需要生成的页面</param>
    /// <param name="saveHtmlPath">生成后的静态页面</param>
    /// <returns></returns>
    public static bool CreatHtmlPage(string sourcePath, string saveHtmlPath)
    {
        if (IsCreatHtmling) return false;
        StreamWriter sw = null;
        try
        {
            IsCreatHtmling = true;
            sw = new StreamWriter(saveHtmlPath, false, Encoding.GetEncoding("utf-8"));
            System.Web.HttpContext.Current.Server.Execute(sourcePath, sw);
            sw.Close();
            IsCreatHtmling = false;
        }
        catch(Exception ex)
        {
            IsCreatHtmling = false;
            System.Web.HttpContext.Current.Response.Write(ex.ToString());
            if (sw != null) sw.Close();            
            return false;
        }
        return true;
    }
    #endregion

   


    #region 获取主机网站地址
    public static string HostUrl
    {
        get
        {
            if (HttpContext.Current.Request.Url.IsDefaultPort)
            {
                return "http://" + HttpContext.Current.Request.Url.Host;

            }
            else
            {
                return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            }
        }
    }
    #endregion

    public static string GetRandomStr()
    {
        return DateTime.Now.ToString("yyMMddHHmmssfff");
    }


    #region 生成缩略图
    //// <summary> 
    /// 生成缩略图 
    /// </summary> 
    /// <param name="originalImagePath">源图路径（物理路径）</param> 
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
    /// <param name="width">缩略图宽度</param> 
    /// <param name="height">缩略图高度</param> 
    /// <param name="mode">生成缩略图的方式</param> 
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
        Image originalImage = Image.FromFile(originalImagePath);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;

        switch (mode)
        {
            case "HW": //指定高宽缩放（可能变形） 
                break;
            case "W": //指定宽，高按比例 
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H": //指定高，宽按比例 
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut": //指定高宽裁减（不变形） 
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }

        //新建一个bmp图片 
        Image bitmap = new Bitmap(towidth, toheight);

        //新建一个画板 
        Graphics g = Graphics.FromImage(bitmap);

        //设置高质量插值法 
        g.InterpolationMode = InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度 
        g.SmoothingMode = SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充 
        g.Clear(Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分 
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
        new Rectangle(x, y, ow, oh),
        GraphicsUnit.Pixel);

        try
        {
            //以jpg格式保存缩略图 
            bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }
    #endregion

    #region 生成缩略图
    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式</param>    
    public static void MakeThumbnail(System.Drawing.Image originalImage, string filename, string uploadDir, int width, int height, string mode)
    {
        MemoryStream ms = new MemoryStream();
        //System.Drawing.Image originalImage = System.Drawing.Image.FromStream(inputStream);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;

        switch (mode)
        {
            case "HW"://指定高宽缩放（可能变形）                
                break;
            case "W"://指定宽，高按比例                    
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://指定高，宽按比例
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://指定高宽裁减（不变形）                
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }

        //新建一个bmp图片
        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(towidth, toheight);


        // Create the thumbnail
        //System.Drawing.Image thumbnail_image = originalImage.GetThumbnailImage(ow, oh, null, System.IntPtr.Zero);
        //新建一个画板
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充
        g.Clear(System.Drawing.Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分
        g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

        try
        {
            //以jpg格式保存缩略图
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //
            FileStream fs = new FileStream(uploadDir + filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(ms.GetBuffer());
            bw.Close();
            fs.Close();
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
            if (ms != null) ms.Close();
        }
    }
    #endregion


    #region 发送邮件
    /// <summary>
    /// 发送电子邮件
    /// </summary>
    /// <param name="strSmtpServer">发送服务器:如:smtp.163.com</param>
    /// <param name="iSmtpPort">发送端口,如:25</param>
    /// <param name="strFrom">发送人:如:sender@163.com</param>
    /// <param name="strFromPass">发送人账号密码,如:123</param>
    /// <param name="strto">接收邮件地址:如receive@sina.com</param>
    /// <param name="strSubject">邮件主题</param>
    /// <param name="strBody">邮件正文</param>
    public static void SendSMTPEMail(string strto, string strSubject, string strBody) //strFrom 要用完整邮件地址格式
    {
        string strSmtpServer = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
        int iSmtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
        string strFrom = System.Configuration.ConfigurationManager.AppSettings["mainAccount"];
        string strFromPass = System.Configuration.ConfigurationManager.AppSettings["mainPwd"];

        System.Net.Mail.SmtpClient client = new SmtpClient(strSmtpServer, iSmtpPort);
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(strFrom, strFromPass);
        client.DeliveryMethod = SmtpDeliveryMethod.Network;

        System.Net.Mail.MailMessage message = new MailMessage(strFrom, strto, strSubject, strBody);
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;
        //Attachment myAttachment = new Attachment("C:\\tradetracker\\InstSummary.xls"); //创建一个附件
        //message.Attachments.Add(myAttachment); //添加附件

        client.Send(message);
    }
    #endregion

    #region url里有key的值，就替换为value,没有的话就追加.
    //url里有key的值，就替换为value,没有的话就追加.
    public static string BuildUrl(string url, string ParamText, string ParamValue)
    {
        Regex reg = new Regex(string.Format("{0}=[^&]*", ParamText), RegexOptions.IgnoreCase);
        Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
        string _url = reg.Replace(url, "");
        //_url = reg1.Replace(_url, "");
        if (_url.IndexOf("?") == -1)
            _url += string.Format("?{0}={1}", ParamText, ParamValue);//?
        else
            _url += string.Format("&{0}={1}", ParamText, ParamValue);//&
        _url = reg1.Replace(_url, "&");
        _url = _url.Replace("?&", "?");
        return _url;
    }
    #endregion

    #region 提前html中的中文
    /// <summary>
    /// 去除HTML标记
    /// </summary>
    /// <param name="strHtml">包括HTML的源码 </param>
    /// <returns>已经去除后的文字</returns>
    public static string StripHTML(string strHtml)
    {
        string[] aryReg ={
          @"<script[^>]*?>.*?</script>",

          @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
          @"([\r\n])[\s]+",
          @"&(quot|#34);",
          @"&(amp|#38);",
          @"&(lt|#60);",
          @"&(gt|#62);", 
          @"&(nbsp|#160);", 
          @"&(iexcl|#161);",
          @"&(cent|#162);",
          @"&(pound|#163);",
          @"&(copy|#169);",
          @"&#(\d+);",
          @"-->",
          @"<!--.*\n"
         
         };

        string[] aryRep = {
           "",
           "",
           "",
           "\"",
           "&",
           "<",
           ">",
           " ",
           "\xa1",//chr(161),
           "\xa2",//chr(162),
           "\xa3",//chr(163),
           "\xa9",//chr(169),
           "",
           "\r\n",
           ""
          };

        string newReg = aryReg[0];
        string strOutput = strHtml;
        for (int i = 0; i < aryReg.Length; i++)
        {
            Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
            strOutput = regex.Replace(strOutput, aryRep[i]);
        }
        strOutput.Replace("<", "");
        strOutput.Replace(">", "");
        strOutput.Replace("\r\n", "");
        return strOutput;
    }
    #endregion


    //处理图片逻辑
    public static string GetUbb(string str)
    {
        str = str.Replace("[img]", "<img src=\"../images/qqface/smilies/default/");
        str = str.Replace("[/img]", ".gif\" />");

        str = str.Replace("[quote]", "<div class=\"yiyong\"><div class=\"zz\">");
        str = str.Replace("的评论:", "的评论:</div>");
        str = str.Replace("[/quote]", "<br />");
        str = str.Replace("'", "");
        str = str.Replace("\"", "");
        //引用部分
        return str;
    }
    public static string ClearUbb(string str)
    {
        int start=str.IndexOf("[quote]");
        int end=str.IndexOf("[/quote]]");
        if (start > 0)
        str=str.Remove(start,end-start);
        return str;
    }


    public static bool IsCreatHtmling = false;



    #region 截取字符串长度
    public static string getSubStr(string str)
    {
        string strResult;

        int MaxCount =12;

        if (str.Length > MaxCount)
        {
            strResult = str.Substring(0, MaxCount);


        }
        else {

            strResult = str;
        
        }


        return strResult;
       
    }
    #endregion
    
    //
    public static string AccessTypeEn(string code)
    {
        string str="";
        switch (code)
        {
            case "2":str="short";break;
            case "3":str="自动编号";break;
            case "4":str="single";break;
            case "5":str="数字";break;
            case "6":str="money";break;
            case "7":str="日期";break;
            case "11":str="bit";break;
            case "17":str="byte";break;
            case "130":str="nvarchar";break;
            case "131":str="float";break;
        }
        return str;
    }

    private static string GetDataTypeNote(string arg)
    {
        switch (arg)
        {
            case "3":
                return "自动编号/数字";
            case "6":
                return "货币";
            case "7":
                return "日期/时间";
            case "11":
                return "是/否";
            case "130":
                return "文本";
            case "203":
                return "备注/超链接";
            case "205":
                return "OLE对象";
        }
        return "未知";
    }


    #region c# post参数
    public static string GetPost(string url, params string[] data)
    {
        string tempMessage = "";
        System.Net.WebClient WebClientObj = new System.Net.WebClient();
        System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
        if (data.Length % 2 == 0)
        {
            for (int i = 0; i < (data.Length / 2); i++)
            {
                PostVars.Add(data[i * 2], data[i * 2 + 1]);
            }
        }

        try
        {
            byte[] byRemoteInfo = WebClientObj.UploadValues(url, "POST", PostVars);
            //下面都没用啦，就上面一句话就可以了
            tempMessage = System.Text.Encoding.Default.GetString(byRemoteInfo);
            //这是获取返回信息

        }
        catch(Exception ex)
        {
            //throw ex;
        }
        return tempMessage;
    }
    #endregion


}