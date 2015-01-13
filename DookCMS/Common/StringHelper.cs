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

    #region �޸�css��ʽ
    /// <summary>
    /// �޸�css
    /// </summary>
    /// <param name="csstxt">css�ı�����(��ȡcss�ļ��ı�)</param>
    /// <param name="cssName">��ʽ����</param>
    /// <param name="theattr">����</param>
    /// <param name="thecss">��ֵ</param>
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

    #region ��ȡcss��ʽ
    /// <summary>
    /// ��ȡcss,���ؼ�ֵ
    /// </summary>
    /// <param name="csstxt">css�ı�����(��ȡcss�ļ��ı�)</param>
    /// <param name="cssName">��ʽ����</param>
    /// <param name="theattr">����</param>
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
    /// ���˷Ƿ��ַ�
    /// </summary>
    /// <param name="strValue">�ַ���</param>
    /// <returns>�滻�ַ�</returns>
    public static string RepalceCharacter(string strValue)
    {
        if (string.IsNullOrEmpty(strValue))
            return "";
        strValue = strValue.Replace("'", "��");
        strValue = strValue.Replace("\"", "��");       
        return strValue;
    }

    #region ����Ƿ�Ϊ����
    /// <summary>
    /// ����Ƿ�Ϊ����
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

    #region ����������ַ� InputText(string inputString, int maxLength)
    /// <summary>
    /// ���������
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

    #region �滻�Ƿ��ַ�
    public static string ReplaceWord(string str)
    {
        string strWord = "";
        strWord = str.Replace("<", "��");
        strWord = strWord.Replace(">", "��");
        strWord = strWord.Replace("'", "��");
        strWord = strWord.Replace("\"", "��");
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

    #region �ж��Ƿ�Ϊ��������
    public static bool isNum(string s) //�ж��Ƿ�Ϊ��������
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
    /// ��ʽ������
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

    #region ��һ���´���(�ӽ�)
    /// <summary>
    /// ��һ���´���(�ӽ�)
    /// </summary>
    /// <param name="url">Ҫ���ӵ�web��ַ</param>
    /// <param name="Wwhith">���ڵĿ��</param>
    /// <param name="Wheight">���ڵĸ߶�</param>
    /// <param name="scrollbar">�Ƿ������������1����0������</param>
    /// <param name="resize">�Ƿ�����ı䴰�ڳߴ磬1����0������</param>
    /// <param name="WindowName">�������ڵĴ�������</param>
    public static string OpenNewWindow(string url, int Wwhith, int Wheight, int scrollbar, int resize, string WindowName)
    {
        string scrollbars, resizable;
        scrollbars = scrollbar == 1 ? "yes" : "no";
        resizable = resize == 1 ? "yes" : "no";
        string sJs = "window.open('" + url + "','" + WindowName + "','scrollbars=" + scrollbars + ",resizable=" + resizable + ",top=200,left=200,width=" + Wwhith + ",height=" + Wheight + "')";
        return sJs;
    }
    #endregion

    #region ��ʽ���ַ������� (�ӽ�)StringFormat()
    /// <summary>
    /// ��ʽ���ַ�������
    /// </summary>
    /// <param name="str">��Ҫ��ʽ���ַ���</param>
    /// <param name="n">�����ĳ̶�</param>
    /// <returns></returns>
    public static string StringFormat(string str, int n)//��ʽ���ַ�������
    {
        ///
        ///��ʽ���ַ������ȣ�����������ʾʡ�Ժ�,���ֺ��ָ���ĸ������2���ֽڣ���ĸ����һ���ֽ�
        ///
        string temp = string.Empty;
        if (System.Text.Encoding.Default.GetByteCount(str) <= n)//������ȱ���Ҫ�ĳ���nС,����ԭ�ַ���
        {
            return str;
        }
        else
        {
            int t = 0;
            char[] q = str.ToCharArray();
            Regex r = new Regex(@"[��������~��������������������]");

            for (int i = 0; i < q.Length && t < n; i++)
            {
                if (((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5) || r.IsMatch(q[i].ToString()))//�Ƿ��ֻ������ı��
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

    #region ת�����ڵ�����
    public static string WeekToCh(string week)
    {
        string chWeek = "";
        switch (week.ToLower())
        {
            case "sunday": chWeek = "������"; break;
            case "monday": chWeek = "����һ"; break;
            case "tuesday": chWeek = "���ڶ�"; break;
            case "wednesday": chWeek = "������"; break;
            case "thursday": chWeek = "������"; break;
            case "friday": chWeek = "������"; break;
            case "saturday": chWeek = "������"; break;                      
        }
        return chWeek;
    }
    #endregion

    #region ��ȡģ���ļ�
    /// <summary>
    /// ���ܣ���ȡģ���ļ�
    /// ���ߣ�����
    /// ���ڣ�2007��12��12��
    /// </summary>
    /// <param name="path">ģ���ļ�</param>
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


    #region c#�е�datediff����(DateDiff)
    /// <summary>
    /// c#�е�datediff����
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

    #region ����Ƿ��зǷ��ַ�ChkBadChar
    /// <summary> 
    /// ����Ƿ��зǷ��ַ� 
    /// </summary> 
    /// <param name="str">Ҫ�����ַ��� </param> 
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

    #region ���˷Ƿ��ַ� ReplaceBadChar
    /// <summary> 
    /// ���˷Ƿ��ַ� 
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

    #region �滻sql����е���������� ReplaceBadSQL
    /// <summary> 
    /// �滻sql����е���������� 
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
    
    #region ���ɾ�̬ҳ��
    /// <summary>
    /// ���ɾ�̬ҳ��
    /// </summary>
    /// <param name="sourcePath">��Ҫ���ɵ�ҳ��</param>
    /// <param name="saveHtmlPath">���ɺ�ľ�̬ҳ��</param>
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

   


    #region ��ȡ������վ��ַ
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


    #region ��������ͼ
    //// <summary> 
    /// ��������ͼ 
    /// </summary> 
    /// <param name="originalImagePath">Դͼ·��������·����</param> 
    /// <param name="thumbnailPath">����ͼ·��������·����</param> 
    /// <param name="width">����ͼ���</param> 
    /// <param name="height">����ͼ�߶�</param> 
    /// <param name="mode">��������ͼ�ķ�ʽ</param> 
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
            case "HW": //ָ���߿����ţ����ܱ��Σ� 
                break;
            case "W": //ָ�����߰����� 
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H": //ָ���ߣ������� 
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut": //ָ���߿�ü��������Σ� 
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

        //�½�һ��bmpͼƬ 
        Image bitmap = new Bitmap(towidth, toheight);

        //�½�һ������ 
        Graphics g = Graphics.FromImage(bitmap);

        //���ø�������ֵ�� 
        g.InterpolationMode = InterpolationMode.High;

        //���ø�����,���ٶȳ���ƽ���̶� 
        g.SmoothingMode = SmoothingMode.HighQuality;

        //��ջ�������͸������ɫ��� 
        g.Clear(Color.Transparent);

        //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������ 
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
        new Rectangle(x, y, ow, oh),
        GraphicsUnit.Pixel);

        try
        {
            //��jpg��ʽ��������ͼ 
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

    #region ��������ͼ
    /// <summary>
    /// ��������ͼ
    /// </summary>
    /// <param name="originalImagePath">Դͼ·��������·����</param>
    /// <param name="thumbnailPath">����ͼ·��������·����</param>
    /// <param name="width">����ͼ���</param>
    /// <param name="height">����ͼ�߶�</param>
    /// <param name="mode">��������ͼ�ķ�ʽ</param>    
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
            case "HW"://ָ���߿����ţ����ܱ��Σ�                
                break;
            case "W"://ָ�����߰�����                    
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://ָ���ߣ�������
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://ָ���߿�ü��������Σ�                
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

        //�½�һ��bmpͼƬ
        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(towidth, toheight);


        // Create the thumbnail
        //System.Drawing.Image thumbnail_image = originalImage.GetThumbnailImage(ow, oh, null, System.IntPtr.Zero);
        //�½�һ������
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //���ø�������ֵ��
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //���ø�����,���ٶȳ���ƽ���̶�
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //��ջ�������͸������ɫ���
        g.Clear(System.Drawing.Color.Transparent);

        //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
        g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

        try
        {
            //��jpg��ʽ��������ͼ
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


    #region �����ʼ�
    /// <summary>
    /// ���͵����ʼ�
    /// </summary>
    /// <param name="strSmtpServer">���ͷ�����:��:smtp.163.com</param>
    /// <param name="iSmtpPort">���Ͷ˿�,��:25</param>
    /// <param name="strFrom">������:��:sender@163.com</param>
    /// <param name="strFromPass">�������˺�����,��:123</param>
    /// <param name="strto">�����ʼ���ַ:��receive@sina.com</param>
    /// <param name="strSubject">�ʼ�����</param>
    /// <param name="strBody">�ʼ�����</param>
    public static void SendSMTPEMail(string strto, string strSubject, string strBody) //strFrom Ҫ�������ʼ���ַ��ʽ
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
        //Attachment myAttachment = new Attachment("C:\\tradetracker\\InstSummary.xls"); //����һ������
        //message.Attachments.Add(myAttachment); //��Ӹ���

        client.Send(message);
    }
    #endregion

    #region url����key��ֵ�����滻Ϊvalue,û�еĻ���׷��.
    //url����key��ֵ�����滻Ϊvalue,û�еĻ���׷��.
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

    #region ��ǰhtml�е�����
    /// <summary>
    /// ȥ��HTML���
    /// </summary>
    /// <param name="strHtml">����HTML��Դ�� </param>
    /// <returns>�Ѿ�ȥ���������</returns>
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


    //����ͼƬ�߼�
    public static string GetUbb(string str)
    {
        str = str.Replace("[img]", "<img src=\"../images/qqface/smilies/default/");
        str = str.Replace("[/img]", ".gif\" />");

        str = str.Replace("[quote]", "<div class=\"yiyong\"><div class=\"zz\">");
        str = str.Replace("������:", "������:</div>");
        str = str.Replace("[/quote]", "<br />");
        str = str.Replace("'", "");
        str = str.Replace("\"", "");
        //���ò���
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



    #region ��ȡ�ַ�������
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
            case "3":str="�Զ����";break;
            case "4":str="single";break;
            case "5":str="����";break;
            case "6":str="money";break;
            case "7":str="����";break;
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
                return "�Զ����/����";
            case "6":
                return "����";
            case "7":
                return "����/ʱ��";
            case "11":
                return "��/��";
            case "130":
                return "�ı�";
            case "203":
                return "��ע/������";
            case "205":
                return "OLE����";
        }
        return "δ֪";
    }


    #region c# post����
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
            //���涼û������������һ�仰�Ϳ�����
            tempMessage = System.Text.Encoding.Default.GetString(byRemoteInfo);
            //���ǻ�ȡ������Ϣ

        }
        catch(Exception ex)
        {
            //throw ex;
        }
        return tempMessage;
    }
    #endregion


}