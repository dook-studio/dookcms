using System;
using System.Web;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dukey.Model;
using BLL;
using System.Net;
using Common;
public class crmop : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
        //if (context.Request.ServerVariables["HTTP_Referer"] == null || context.Request.ServerVariables["REMOTE_HOST"] == null || context.Request.ServerVariables["SERVER_NAME"] != context.Request.Url.Host)
        //{
        //    context.Response.Write("非法访问!");
        //    context.Response.End();
        //    return;
        //}
        string action = context.Request.QueryString["ac"];
        switch (action)
        {
            case "filerename": FileRename(context); break;//重命名文件       
            case "renametb": Renametb(context); break;//修改数据表名称.

            case "clearcache": ClearCache(context); break;
            case "rebootsite": RebootSite(context); break;//重启网站
            case "createhtmlprocess": CreateHtmlProcess(context); break;//处理生成静态页面
            case "createhtmlchannel": CreateHtmlChannel(context); break;//生成栏目静态页面
            case "channelsinglecreate": ChannelSingleCreate(context); break;//生成栏目静态页面

            case "createhtmlindex": CreateHtmlIndex(context); break;//生成首页
            case "createhtmlarticleall": CreateHtmlArticleALL(context); break;//生成全部文章
            case "getsingledata": GetSingleData(context); break;//获取单条数据

            case "updatecol": UpdateCol(context); break;//更新智能表单列
            case "singledelete": SingleDel(context); break;//删除单个值.
        }
    }

    private void SingleDel(HttpContext context)
    {
        try
        {
            string tbname = context.Request["tbname"];//表名称
            string strwhere = context.Request["strwhere"];//条件
            BLL.DataBaseHelper.instance.Delete(tbname, strwhere);
            context.Response.Write("ok");
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    #region 更新智能表单列
    private void UpdateCol(HttpContext context)
    {
        try
        {
            string colname = context.Request["colname"];
            string cname = context.Request["cname"];
            string datatype = context.Request["datatype"];
            string ismust = context.Request["ismust"];
            string maxlength = context.Request["maxlength"];
            string htmlstr = context.Request["htmlstr"];
            string rule = context.Request["rule"];
            string defaultvalue = context.Request["defaultvalue"];
            bool isshow = context.Request["isshow"] == "on" ? true : false;
            bool issearch = context.Request["issearch"] == "on" ? true : false;
            bool ispx = context.Request["ispx"] == "on" ? true : false;

            string colid = context.Request["colid"];
            string formid = context.Request["formid"];
            string jsonstr = context.Request["jsonstr"];
            Hashtable hs = new Hashtable();
            hs.Add("colname", colname);
            hs.Add("cname", cname);
            hs.Add("datatype", datatype);
            hs.Add("ismust", ismust);
            hs.Add("maxlength", maxlength);
            hs.Add("htmlstr", htmlstr);
            hs.Add("defaultvalue", defaultvalue);
            hs.Add("isshow", isshow);
            hs.Add("issearch", issearch);
            hs.Add("ispx", ispx);
            hs.Add("rule", rule);
            hs.Add("formid", formid);
            hs.Add("jsonstr", jsonstr);
            if (colid.IsEmpty())
            {
                BLL.DataBaseHelper.instance.Insert(hs, "formpara");
            }
            else
            {
                BLL.DataBaseHelper.instance.Update(hs, "formpara", "id=" + colid);
            }
            context.Response.Redirect("/crm/form/colslist.aspx?id=" + formid);
            context.Response.End();
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }
    #endregion

    #region 获取单个表数据
    private void GetSingleData(HttpContext context)
    {
        string tbname = context.Request["tbname"];//表名称
        string strwhere = context.Request["strwhere"];//条件
        string filed = context.Request["filed"];//字段s
        filed = filed.IsEmpty() ? "*" : filed;
        string encodestr = context.Request["encodestr"];//需要htmlencode的字段
        DataTable dt = BLL.DataBaseHelper.instance.GetModel(tbname, filed, strwhere);
        if (dt != null)
        {
            if (!encodestr.IsEmpty())
            {
                if (encodestr == "all")
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (!item[dc].ObjToStr().IsEmpty())
                            {
                                item[dc] = context.Server.HtmlEncode(item[dc].ToString());
                                //item[""+dc.ColumnName+""] = context.Server.HtmlEncode(item[""+dc.ColumnName+""].ToString());
                            }
                        }
                    }
                }
                else
                {
                    foreach (string item in encodestr.Split(','))
                    {
                        if (!dt.Rows[0]["" + item + ""].ObjToStr().IsEmpty())
                        {
                            dt.Rows[0]["" + item + ""] = context.Server.HtmlEncode(dt.Rows[0]["" + item + ""].ToString());
                        }
                    }
                }
            }
            //context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            context.Response.Write(Common.JsonHelper.DataTableToJson("myjson", dt));
            context.Response.End();
        }
    }
    #endregion

    #region 生成单个栏目静态页面
    private void ChannelSingleCreate(HttpContext context)//生成单个栏目静态页面
    {
        try
        {
            string id = context.Request["bid"];
            DataRowView dv = BLL.DataBaseHelper.instance.GetModelView("channel", "bid,ename,channeltype,patternid", "bid=" + id);
            if (dv != null)
            {
                //先更新数据库链接地址
                Hashtable hs = new Hashtable();
                hs.Add("link", "/html/" + dv["ename"] + ".html");
                BLL.DataBaseHelper.instance.Update(hs, "channel", "bid=" + dv["bid"]);

                //生成静态页面
                CookieContainer cookie = new CookieContainer();
                string param = string.Format("cpage=channel&filehtm=/html/{0}&bid={1}", dv["ename"], dv["bid"]);
                string pagestr = string.Empty;

                switch (dv["channeltype"].ToString())//列表页
                {
                    case "1":
                        {
                            switch (dv["patternid"].ToString())
                            {
                                case "0"://文章
                                    {
                                        pagestr = "list.ashx";
                                        break;
                                    }
                                case "1"://商品
                                    {
                                        pagestr = "prolist.ashx";
                                        break;
                                    }
                                case "2"://图集
                                    {
                                        pagestr = "imglist.ashx";
                                        break;
                                    }
                            }
                            BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/" + pagestr, param, ref cookie);

                            break;
                        }

                    case "0"://封面
                        {
                            pagestr = "index.ashx";
                            BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/" + pagestr, param, ref cookie);
                            break;
                        }
                }

                context.Response.Write("ok");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }
    #endregion

    #region 解析规则
    private string ParseItemRule(string itemrule, DateTime dt, string id, int bid)
    {
        if (itemrule.IsEmpty())
        {
            itemrule = "/html/{yyyy}/{m}/{d}/";
        }
        itemrule = itemrule.Replace("{yyyy}", dt.ToString("yyyy"));
        itemrule = itemrule.Replace("{yy}", dt.ToString("yy"));
        itemrule = itemrule.Replace("{mm}", dt.ToString("MM"));
        itemrule = itemrule.Replace("{m}", dt.Month.ToString());
        itemrule = itemrule.Replace("{dd}", dt.ToString("dd"));
        itemrule = itemrule.Replace("{d}", dt.Day.ToString());
        itemrule = itemrule.Replace("{aid}", id);
        return itemrule;
    }
    #endregion

    #region 生成文章页面
    private void CreateHtmlArticleALL(HttpContext context)//生成文章页面
    {
        try
        {
            BaseWeb.CreateHtmlProcess = "正在生成文章...";
            List<Dukey.Model.Channel> listb = StringHelper.DataTableToList<Dukey.Model.Channel>(BLL.DataBaseHelper.instance.GetList("channel", "bid,ename,cname,link,isshow,channeltype,itemrule", 0, "channeltype=1", "").Tables[0]);
            var list = listb.FindAll(i => i.isshow == true);
            string channelid = context.Request["channelid"];
            if (!channelid.IsEmpty())
            {
                list = listb.FindAll(i => i.isshow == true && i.bid == channelid.ObjToInt());
            }
            //文章开始日期和结束日期
            string iscurdate = context.Request["iscurdate"];

            foreach (var item in list)
            {
                BaseWeb.CreateHtmlProcess = "开始生成 栏目:" + item.cname + " 编号:" + item.bid + "下的文章";
                DataTable dt = BLL.DataBaseHelper.instance.GetList("article", "id,addtime", 0, "typeid=" + item.bid, "").Tables[0];
                DataView dv = dt.DefaultView;
                //解析item.rule
                foreach (DataRow citem in dt.Rows)
                {
                    try
                    {
                        if (iscurdate == "1" && Convert.ToDateTime(citem["addtime"]).ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            continue;
                        }
                        BaseWeb.CreateHtmlProcess = "正在生成栏目->" + item.cname + " 编号:" + item.bid + "下的文章 编号为:" + citem["id"];
                        DateTime addtime = new DateTime();
                        DateTime.TryParse(citem["addtime"].ObjToStr(), out addtime);
                        string filehtm = ParseItemRule(item.itemrule, addtime, citem["id"].ObjToStr(), item.bid);
                        CookieContainer cookie = new CookieContainer();
                        BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/item.ashx", string.Format("cpage=article&filehtm={0}&aid={1}", filehtm, citem["id"]), ref cookie);
                    }
                    catch
                    {
                    }
                }
            }
            BaseWeb.CreateHtmlProcess = "生成完成!";
        }
        catch (Exception ex)
        {
            BaseWeb.CreateHtmlProcess = ex.Message;
        }
    }
    #endregion

    #region 生成首页
    private void CreateHtmlIndex(HttpContext context)
    {
        try
        {
            BaseWeb.CreateHtmlProcess = "正在生成首页...";
            DataRowView model = BLL.DataBaseHelper.instance.GetModelView("channel", "bid,channeltype,patternid", "ename='index'");
            CookieContainer cookie = new CookieContainer();
            if (model != null)
            {
                string pagestr = string.Empty;
                if (model["channeltype"].ToString() == "1")//列表
                {
                    switch (model["patternid"].ToString())
                    {
                        case "0"://文章
                            {
                                pagestr = "list.ashx";
                                break;
                            }
                        case "1"://商品
                            {
                                pagestr = "prolist.ashx";
                                break;
                            }
                        case "2"://图集
                            {
                                pagestr = "imglist.ashx";
                                break;
                            }
                    }
                    BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/" + pagestr, "cpage=channel&filehtm=index&bid=" + model["bid"].ToString(), ref cookie);
                }
                else
                {
                    BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/index.ashx", "cpage=channel&filehtm=index&bid=" + model["bid"].ToString(), ref cookie);
                }
                BaseWeb.CreateHtmlProcess = "生成完成!";
            }
            else
            {
                BaseWeb.CreateHtmlProcess = "没有找到首页,请设置栏目英文名称为index的视为首页!";
            }
        }
        catch { }
    }
    #endregion


    #region 生成栏目页
    private void CreateHtmlChannel(HttpContext context)
    {
        try
        {
            List<Dukey.Model.Channel> listb = StringHelper.DataTableToList<Dukey.Model.Channel>(BLL.DataBaseHelper.instance.GetList("channel", "bid,ename,cname,link,isshow,channeltype,patternid", 0, "", "").Tables[0]);
            var list = listb.FindAll(i => i.isshow == true && (i.channeltype == 0 || i.channeltype == 1));
            int total = list.Count();
            int m = 0;
            foreach (var item in list)
            {
                try
                {
                    BaseWeb.CreateHtmlProcess = "开始生成 " + (m + 1) + "/" + total + " " + item.cname;
                    //更新
                    Hashtable hs = new Hashtable();
                    hs.Add("link", "/html/" + item.ename + ".html");
                    BLL.DataBaseHelper.instance.Update(hs, "channel", "bid=" + item.bid);

                    CookieContainer cookie = new CookieContainer();
                    string param = string.Format("cpage=channel&filehtm=/html/{0}&bid={1}", item.ename, item.bid);

                    if (item.channeltype == 1)//列表
                    {
                        string pagestr = string.Empty;
                        switch (item.patternid.ToString())
                        {
                            case "0"://文章
                                {
                                    pagestr = "list.ashx";
                                    break;
                                }
                            case "1"://商品
                                {
                                    pagestr = "prolist.ashx";
                                    break;
                                }
                            case "2"://图集
                                {
                                    pagestr = "imglist.ashx";
                                    break;
                                }
                        }
                        BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/" + pagestr, param, ref cookie);
                    }
                    else if(item.channeltype==0)//封面页
                    {
                        BaseWeb.instance.SendDataByGET(BaseWeb.BaseUrl + "/index.ashx", param, ref cookie);
                    }
                  
                    BaseWeb.CreateHtmlProcess = "已生成" + item.cname;
                    m++;
                }
                catch
                {
                }
            }
            BaseWeb.CreateHtmlProcess = "生成完成!";
        }
        catch { }
    }
    #endregion

    #region 生成进度
    private void CreateHtmlProcess(HttpContext context)
    {
        try
        {
            context.Response.Write(BaseWeb.CreateHtmlProcess);
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }
    #endregion

    #region 重启网站

    private void RebootSite(HttpContext context)
    {
        try
        {
            HttpRuntime.UnloadAppDomain();
            context.Response.Write("ok");
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }
    #endregion

    #region 清理缓存
    private void ClearCache(HttpContext context)
    {
        BaseWeb.instance.RemoveAllCache();
        context.Response.Write("ok");
    }
    #endregion

    #region 重命名表
    private void Renametb(HttpContext context)
    {
        string old = context.Request.Form["oldname"];
        string newname = context.Request.Form["newname"];
        try
        {
            BLL.DataBaseHelper.instance.RenameTableName(old, newname);
            context.Response.Write("ok");
        }
        catch
        {
            context.Response.Write("error");
        }
    }
    #endregion

    #region 重命名文件
    private void FileRename(HttpContext context)
    {
        string type = "folder";
        type = context.Request.QueryString["type"];
        string localpath = context.Request.QueryString["localpath"];
        string oldname = context.Request.QueryString["oldname"];
        string newname = context.Request.QueryString["newname"];
        if ("folder".Equals(type))
        {
            Directory.Move(context.Server.MapPath(localpath + "/" + oldname), context.Server.MapPath(localpath + "/" + newname));
        }
        else
        {
            File.Move(context.Server.MapPath(localpath + "/" + oldname), context.Server.MapPath(localpath + "/" + newname));
        }
        context.Response.Write("ok");
    }
    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}