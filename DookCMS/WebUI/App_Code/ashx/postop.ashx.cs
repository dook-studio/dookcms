using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.SessionState;

public class postop : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
        if (context.Request.ServerVariables["HTTP_Referer"] == null || context.Request.ServerVariables["REMOTE_HOST"] == null || context.Request.ServerVariables["SERVER_NAME"] != context.Request.Url.Host)
        {
            context.Response.Write("非法访问!");
            context.Response.End();
            return;
        }
        string action = context.Request.QueryString["ac"];
        switch (action)
        {
            case "customfrm": { CustomFrm(context); break; }//键值和表的字段相对应.
            case "autofrm": { AutoFrm(context); break; }//生成的表单
            case "creathtmlboard": { CreateHtmlBoard(context); break; }
            case "createarticle": { CreateArticle(context); break; }
            case "pluginconfig": PluginConfig(context); break;//配置插件        
            case "getdatapluginconfig": GetPluginConfigData(context); break;//配置插件      
            case "plugindelete": PluginDelete(context); break;//删除插件
            case "yzm": CreateYZM(context); break;//生成验证码
            case "getyzm": GetYzm(context); break;//获取验证码
            case "login": Login(context); break;//生成验证码
            case "smartformpost": SmartFormPost(context); break;//智能表单提交数据,            
            default: { break; }
        }
    }
     #region 获取验证码
    private static void GetYzm(HttpContext context)
    {
        try
        {
            context.Response.Write(context.Session["validcode"]);
        }
        catch
        {
        }
    }
     #endregion
    #region 智能表单提交数据
    private static void SmartFormPost(HttpContext context)
    {
        //try
        //{
            string formid = context.Request["formid"];
            if (!StringHelper.isNum(formid))
            {
                context.Response.Write("参数错误!");
                context.Response.End();
                return;
            }
            DataRowView dv = BLL.DataBaseHelper.instance.GetModelView("Form", "tablename,pk,gototips,gotourl", "id=" + formid);
            if (dv != null)
            {
                DataSet ds = BLL.DataBaseHelper.instance.GetList("FormPara", "colname,cname,datatype", 0, "formid=" + formid + " and (isfalse=0 or isfalse is NULL)", "");
                Hashtable hs = new Hashtable();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        if (item["datatype"].ToString() == "bit")
                        {
                            bool cvalue = string.IsNullOrEmpty(context.Request[item["colname"].ToString()]) ? false : true;
                            hs.Add(item["colname"].ToString(), cvalue);//添加到表
                        }
                        else
                        {
                            hs.Add(item["colname"].ToString(), context.Request[item["colname"].ToString()]);//添加到表
                        }
                    }
                }
                if (StringHelper.IsBadChar(context.Request["pkid"]))//插入
                {
                    BLL.DataBaseHelper.instance.Insert(hs, dv["tablename"].ToString());
                }
                else//更新
                {
                    BLL.DataBaseHelper.instance.Update(hs, dv["tablename"].ToString(), "["+dv["pk"].ToString()+"]" + "=" + context.Request["pkid"]);
                }

                if (!StringHelper.IsBadChar(dv["gototips"].ToString()))
                {
                    string gototips = dv["gototips"].ToString();
                    if (string.IsNullOrEmpty(gototips))
                    {
                        gototips = "操作成功!";
                    }
                    string gotourl = dv["gotourl"].ToString();
           
                    if (string.IsNullOrEmpty(gotourl))
                    {
                        context.Response.Write("{\"result\":\""+gototips+"\",\"gotourl\":\""+string.Empty+"\"}");                
                        context.Response.End();
                    }
                    else
                    {
                        context.Response.Write("{\"result\":\"" + gototips + "\",\"gotourl\":\"" + gotourl + "\"}");         
                        context.Response.End();
                    }
                }
            }
            else
            {
                context.Response.Write("参数错误!");
                context.Response.End();
                return;
            }
        //}
        //catch (Exception ex)
        //{
        //    context.Response.Write(ex.Message);
        //}

    }
    #endregion
    #region 登录
    private static void Login(HttpContext context)
    {
        try
        {
            string username = context.Request["username"];
            string pwd = context.Request["pwd"];

            DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("user", "id", "username='" + username + "' and pwd='" + pwd + "'");
            if (dr != null)
            {
                BaseWeb.instance.UserId = int.Parse(dr["id"].ToString());
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write("nouser");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }

    }
    #endregion

    #region 注册
    private static void Register(HttpContext context)
    {
        try
        {
            string username = context.Request["username"];
            string pwd = context.Request["pwd"];

            DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("user", "id", "username='" + username + "' and pwd='" + pwd + "'");
            if (dr != null)
            {
                BaseWeb.instance.UserId = int.Parse(dr["id"].ToString());
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write("nouser");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }

    }
    #endregion

    #region 生成验证码
    private void CreateYZM(HttpContext context)
    {
        // 在此处放置用户代码以初始化页面
        context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); //不缓存
        Common.YZMHelper yz = new Common.YZMHelper();
        context.Session["validcode"] = yz.Text; //将验证字符写入Session，供前台调用
        MemoryStream ms = new MemoryStream();
        yz.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        yz.Image.Dispose();
        context.Response.ClearContent();
        context.Response.ContentType = "image/Gif";
        context.Response.BinaryWrite(ms.ToArray());
    }
    #endregion

    #region 删除插件
    private void PluginDelete(HttpContext context)
    {
        string pluginid = context.Request.Form["id"];
        string type = context.Request.Form["type"];
        string fn = context.Request.Form["fn"];
        try
        {
            Dukey.Model.WebConfig mysite = BLL.WebConfig.instance.GetModelByCache();//网站配置信息
            //删除插件配置
            string xmlpath = context.Server.MapPath("~/template/" + mysite.folder + "/theme.xml");
            XMLHelper.DeleteXmlNodeByXPath(xmlpath, "//config/page[@url='" + fn + "']/plugins/plugin[@id='" + pluginid + "' and @type='" + type + "']");

            //删除模板包中的标签等
            string path = context.Server.MapPath("~/template/" + mysite.folder + "/" + fn + ".html");
            string htmlstr = Common.FileHelper.ReadFile(path, "utf-8");
            string tempstr = "placeholder-" + DateTime.Now.ToString("fff");
            htmlstr = htmlstr.Replace("{$plugin." + type + "." + pluginid + "}", "<!--" + tempstr + "-->");
            htmlstr = htmlstr.Replace("edit=\"" + type + "." + pluginid + "\"", "edit=\"" + tempstr + "\"");
            Common.FileHelper.WriteFile(path, htmlstr, "utf-8");
            context.Response.Write("ok");
        }
        catch (Exception ex) { context.Response.Write(ex.Message); }

    }
    #endregion

    #region 获取配置插件数据
    private void GetPluginConfigData(HttpContext context)
    {
        string pluginid = context.Request.Form["id"];
        string type = context.Request.Form["type"];
        string fn = context.Request.Form["fn"];
        XmlDocument xmlDoc = new XmlDocument();
        Dukey.Model.WebConfig mysite = BLL.WebConfig.instance.GetModelByCache();//网站配置信息
        string path = context.Server.MapPath("~/template/" + mysite.folder + "/theme.xml");
        xmlDoc.Load(path); //加载XML文档
        XmlNode paras = xmlDoc.SelectSingleNode("//config/page[@url='" + fn + "']/plugins/plugin[@id='" + pluginid + "' and @type='" + type + "']/paras");
        if (paras != null)
        {
            context.Response.Write(paras.InnerText);
        }
    }
    #endregion

    #region 配置插件数据
    private void PluginConfig(HttpContext context)
    {
        string str = "<![CDATA[\n" + context.Request.Form["datastr"] + "]]>\n";
        string pluginid = context.Request.Form["id"];
        string type = context.Request.Form["type"];
        string parasstr = "<![CDATA[" + context.Request.Form["paras"] + "]]>";
        string fn = context.Request.Form["fn"];
        Dukey.Model.WebConfig mysite = BLL.WebConfig.instance.GetModelByCache();//网站配置信息
        if (!File.Exists(context.Server.MapPath("~/template/" + mysite.folder + "/theme.xml")))
        {
            using (StreamWriter sw = File.CreateText(context.Server.MapPath("~/template/" + mysite.folder + "/theme.xml")))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<config>");
                sw.WriteLine(string.Format("<page url=\"{0}\">", fn));
                sw.WriteLine("<plugins>");
                sw.WriteLine(string.Format("<plugin id=\"{0}\" type=\"{1}\">", pluginid, type));
                sw.WriteLine(string.Format("<paras>{0}</paras>", parasstr));
                sw.WriteLine("<content>");
                sw.WriteLine(str);
                sw.WriteLine("</content>");
                sw.WriteLine("</plugin>");
                sw.WriteLine("</plugins>");
                sw.WriteLine("</page>");
                sw.WriteLine("</config>");
            }
        }
        else
        {
            string path = context.Server.MapPath("~/template/" + mysite.folder + "/theme.xml");
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path); //加载XML文档
                XmlNode root = xmlDoc.SelectSingleNode("//config");
                if (root == null)
                {
                    root = xmlDoc.CreateNode("element", "config", "");
                    xmlDoc.AppendChild(root);
                }
                XmlNode page = xmlDoc.SelectSingleNode("//config/page[@url='" + fn + "']");
                if (page == null)
                {
                    page = xmlDoc.CreateNode("element", "page", "");
                    XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("url");
                    xmlAttribute.Value = fn;
                    page.Attributes.Append(xmlAttribute);
                    root.AppendChild(page);
                }
                XmlNode plugins = xmlDoc.SelectSingleNode("//config/page[@url='" + fn + "']/plugins");
                if (plugins == null)
                {
                    plugins = xmlDoc.CreateNode("element", "plugins", "");
                    page.AppendChild(plugins);
                }
                XmlNode plugin = xmlDoc.SelectSingleNode("//config/page[@url='" + fn + "']/plugins/plugin[@id='" + pluginid + "' and @type='" + type + "']");
                if (plugin == null)
                {
                    plugin = xmlDoc.CreateNode("element", "plugin", "");
                    XmlAttribute att1 = xmlDoc.CreateAttribute("id");
                    att1.Value = pluginid;
                    XmlAttribute att2 = xmlDoc.CreateAttribute("type");
                    att2.Value = type;
                    plugin.Attributes.Append(att1);
                    plugin.Attributes.Append(att2);
                    plugins.AppendChild(plugin);
                }
                XmlNode paras = xmlDoc.SelectSingleNode("//config/page[@url='" + fn + "']/plugins/plugin[@id='" + pluginid + "' and @type='" + type + "']/paras");
                if (paras == null)
                {
                    paras = xmlDoc.CreateNode("element", "paras", "");
                    plugin.AppendChild(paras);
                }
                paras.InnerXml = parasstr;
                XmlNode content = xmlDoc.SelectSingleNode("//config/page[@url='" + fn + "']/plugins/plugin[@id='" + pluginid + "' and @type='" + type + "']/content");
                if (content == null)
                {
                    content = xmlDoc.CreateNode("element", "content", "");
                    plugin.AppendChild(content);
                }

                content.InnerXml = str;
                xmlDoc.Save(path);
                context.Response.Write("ok");
            }
            catch (Exception ex) { context.Response.Write(ex.Message); }
        }
    }
    #endregion

    private void CreateHtmlBoard(HttpContext context)
    {
        //List<Dukey.Model.Channel> listb = BLL.Boards.instance.GetList("isshow=True and ismain=True");
        List<Dukey.Model.Channel> listb = StringHelper.DataTableToList<Dukey.Model.Channel>(BLL.DataBaseHelper.instance.GetList("channel", "bid,ename,cname,link", 0, "", "").Tables[0]);
        //foreach (var item in listb)
        //{
        //    if (!string.IsNullOrEmpty(item.linkto))
        //    {
        //        string filepath = item.linktohtml;
        //        str.Append("<a href=\"" + item.linkto + "&filehtm=" + filepath + "\"></a>");
        //    }
        //}
        //context.Response.ContentType = "text/plain";
        //context.Response.Clear();
        context.Response.ContentEncoding = Encoding.UTF8;
        context.Response.ContentType = "application/json";
        //context.Response.Write("[{\"bid\":\"1\",\"remark\":\"ddd\"}]");
        context.Response.Write(Common.JsonHelper.ObjectToJson<Dukey.Model.Channel>("board", listb));
        context.Response.Flush();
        context.Response.End();
    }

    private void CreateArticle(HttpContext context)
    {
        // //List<Dukey.Model.Boards> listb=BLL.Boards.instance.GetList("isshow=True");
        //List<Dukey.Model.Article> articles = BLL.DataBaseHelper.instance.GetList("article");
        //foreach (var item in articles)
        //{
        //    string dir = "~/upload/" + item.addtime.ToString("yyyy/MM/dd/");
        //    string filename=item.id + ".html";
        //    if (!Directory.Exists(context.Server.MapPath(dir)))
        //    {
        //        Directory.CreateDirectory(context.Server.MapPath(dir));
        //    }
        //    //string url = "/show.ashx?fn=article_detail&id=" + item.id + "&filehtm=" + dir + filename;
        //    string url = "http://localhost:8033/show.ashx?fn=index&bid=1";
        //    StringHelper.GetPost(url);
        //    context.Response.Write(url);
        //}
        try
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            //context.Response.Write("[{\"bid\":\"1\",\"remark\":\"ddd\"}]");
            List<Dukey.Model.Article> listb = StringHelper.DataTableToList<Dukey.Model.Article>(BLL.DataBaseHelper.instance.GetList("article", "id", 0, "", "").Tables[0]);
            context.Response.Write(Common.JsonHelper.ObjectToJson<Dukey.Model.Article>("board", listb));
            context.Response.Flush();
            context.Response.End();
            return;
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
            //throw ex;
        }
    }

    private static void AutoFrm(HttpContext context)
    {
        NameValueCollection nvl = context.Request.Form;
        Hashtable hs = new Hashtable();
        for (int i = 0; i < nvl.Keys.Count; i++)
        {
            hs.Add(nvl.GetKey(i), nvl.Get(i));
        }
        try
        {
            string formid = context.Request.QueryString["formid"];
            string tbname = BLL.DataBaseHelper.instance.GetSingle("Form", "tablename", "formid=" + formid + "", "").ToString();
            string sql = "insert into " + tbname + "(";
            string sqlval = "(";
            if (context.Request.QueryString["op"] != "update")
            {
                foreach (object objKeys in hs.Keys)
                {
                    sql += "[" + objKeys.ToString() + "],";
                    sqlval += "?,";
                }
                sql = sql.Remove(sql.Length - 1, 1);
                sqlval = sqlval.Remove(sqlval.Length - 1, 1) + ")";
                sql += ") values";
                sql += sqlval;
                BLL.DataBaseHelper.instance.ExecuteSql(sql, hs);
            }
            context.Response.Write("ok");
        }
        catch
        {
            context.Response.Write("error");
        }
    }

    private static void CustomFrm(HttpContext context)
    {
        try
        {
            NameValueCollection nvl = context.Request.Form;
            Hashtable hs = new Hashtable();
            string strwhere = context.Request["where"];
            for (int i = 0; i < nvl.Keys.Count; i++)
            {
                if (nvl.GetKey(i) == "where")
                {
                    strwhere = nvl.Get(i);
                }
                else
                {
                    hs.Add(nvl.GetKey(i), nvl.Get(i));
                }
            }
            string tbname = context.Request.QueryString["tbname"];//获取表名称
            string op = context.Request.QueryString["op"];
            if (op == "update")
            {
                BLL.DataBaseHelper.instance.Update(hs, tbname, strwhere);
                context.Response.Write("ok");
            }
            else
            {
                BLL.DataBaseHelper.instance.Insert(hs, tbname);
                context.Response.Write("ok");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}