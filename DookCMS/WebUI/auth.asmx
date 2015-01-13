<%@ WebService Language="C#" Class="dukeyauth" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[WebService(Namespace = "http://www.dukeycms.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class dukeyauth : System.Web.Services.WebService
{

    #region 方法
    [WebMethod]
    public string AuthUser(string email, string machinekey, string regcode, string sid, ref string authtip, int version)
    {
        try
        {
            if (sid == "")
                return "dieuser";
            email = email.Replace("-", ".");
            object o = BLL.DataBaseHelper.instance.GetColumnValue("U_Orders", "updatetime", "email='" + email + "'");
            if (o != null)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(o.ToString(), out dt);
                if (dt >= DateTime.Now)
                {
                    return "ok";
                }
                else
                {
                    return "expire";
                }

            }
            else
            {
                return "nouser";
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {

        }
    }
    #endregion

    #region 方法
    [WebMethod]
    public string SffAuthUser(string email, string machinekey, string regcode, string sid, ref string authtip, int version)
    {
        try
        {
            authtip = "1";            
            if (sid == "")
                return "dieuser";
            email = email.Replace("-", ".");          
            System.Data.DataRowView o = BLL.DataBaseHelper.instance.GetModelView("U_OrdersSff", "updatetime,portmax", "email='" + email + "'");
            if (o != null)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(o["updatetime"].ToString(), out dt);
                if (dt >= DateTime.Now)
                {
                    authtip = o["portmax"].ToString();
                    return "ok";
                }
                else
                {                    
                    return "expire";
                }
            }
            else
            {
                return "nouser";
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {

        }
    }
    #endregion

    [WebMethod]
    public List<string> MatchImgPath(string content, string regexPattern, int version, ref string tip)
    {
        List<string> ret = new List<string>();
        Regex reg = new Regex(regexPattern, RegexOptions.IgnoreCase);
        MatchCollection mc = reg.Matches(content);
        if (mc != null && mc.Count > 0)
        {
            foreach (Match match in mc)
            {
                if (!ret.Contains(match.Value))
                {
                    ret.Add(match.Value);
                }
            }
        }
        //判断权限
        //封ip
        tip = "ok";
        string ip = HttpContext.Current.Request.UserHostAddress;
        if (LimitIP.Contains(ip))
        {
            tip = "你是非法用户,请使用正版!错误代码x0ip" + ip;
        }
        if (version != 262)
        {
            tip = "该版本已经不能使用,请先升级!";
        }
        return ret;
    }

    #region 淘客精灵软件
    [WebMethod]
    public string AuthUserTbkSoft(string email, string machinekey, string regcode, string sid, ref string authtip, int version)
    {
        try
        {
            if (sid == "")
                return "dieuser";
            email = email.Replace("-", ".");
            if (email == "都客超级用户")
            {
                return "ok";
            }
            string strwhere = string.Empty;
            strwhere = string.Format("email='{0}'", email);
            strwhere += string.Format(" and Instr(','+signkey+',',',{0},')>0", machinekey);//access数据库
            object o = BLL.DataBaseHelper.instance.GetColumnValue("U_OrdersTbkSoft", "updatetime", strwhere);
            if (o != null)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(o.ToString(), out dt);
                if (dt >= DateTime.Now)
                {
                    return "ok";
                }
                else
                {
                    return "expire";
                }
            }
            else
            {
                return "nouser";
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {

        }
    }
    #endregion


    #region 都客网店助手
    [WebMethod]
    public string AuthUserTBS(string email, string machinekey, string regcode, string sid, ref string authtip, int version)
    {
        try
        {
            if (sid == "")
                return "dieuser";
            email = email.Replace("-", ".");
            object o = BLL.DataBaseHelper.instance.GetColumnValue("U_OrdersTBS", "updatetime", "email='" + email + "'");
            if (o != null)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(o.ToString(), out dt);
                if (dt >= DateTime.Now)
                {
                    return "ok";
                }
                else
                {
                    return "expire";
                }

            }
            else
            {
                return "nouser";
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {

        }
    }
    #endregion

    #region 都客定时压缩工具
    [WebMethod]
    public string AuthUserAutoZip(string email, string machinekey, string regcode, string sid, ref string authtip, int version)
    {
        try
        {
            if (sid == "")
                return "dieuser";
            email = email.Replace("-", ".");
            object o = BLL.DataBaseHelper.instance.GetColumnValue("U_OrdersAutoZip", "updatetime", "email='" + email + "'");
            if (o != null)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(o.ToString(), out dt);
                if (dt >= DateTime.Now)
                {
                    return "ok";
                }
                else
                {
                    return "expire";
                }

            }
            else
            {
                return "nouser";
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {

        }
    }
    #endregion
    
    private List<string> LimitIP = new List<string>()
    {
        "192.168.1.1",
        "202.158.3.4",
        "434.353.4.6",
        "27.8.40.154"
    };

}