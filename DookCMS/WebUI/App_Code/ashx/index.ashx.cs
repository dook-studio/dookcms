﻿using System;
using System.Web;
using Dukey.Engine;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.Collections;
using System.Web.SessionState;

partial class index : BasePage, IRequiresSessionState
{

    #region 解析模板
    private Hashtable hs = new Hashtable();
    protected override void InitPageTemplate()
    {        
        this.Document.SetValue("sys", hs);
        this.Document.SetValue("url", Request.QueryString);
        this.Document.SetValue("copyright", "<span>X1.0 © 2012-2013 <a href=\"http://www.dukeycms.com\">DukeyCMS.</a></span>");
        this.Document.Variables.SetValue("shophost", Common.MyWeb.config["shophost"]);
        this.Document.Variables.SetValue("mainhost", Common.MyWeb.config["mainhost"]);
        this.Document.SetValue("session.userid", BaseWeb.instance.UserId);
    }
    #endregion


    protected override void LoadTemplateFile(string fileName)
    {
        BindCommData();
        //开始加载栏目
        string bid = Request.QueryString["bid"];//
        if (!StringHelper.isNum(bid))
        {
            Response.Write("参数格式不正确!");
            Response.End();
            return;
        }
        string templatefile = "index.html";
        if (!string.IsNullOrEmpty(bid))
        {
            DataRowView model = BLL.DataBaseHelper.instance.GetModelView("channel", "diyfileurl", "bid=" + bid);
            if (model != null)
            {
                templatefile = model["diyfileurl"].ToString();//模板文件                
            }
        }
        string template = "default";
        if (hs["template"] == null || hs["template"] == "")
        {
            throw new Exception("webconfig表缺少template参数");
        }
        else
        {
            template = hs["template"].ToString();
        }

        if (string.IsNullOrEmpty(templatefile))//判断是否设置了模板.
        {
            Response.Write("你还没有设置该栏目(编号为:" + bid + ",类型:封面模板)下的模板文件!请在后台栏目管理->高级->封面模板中设置.设置示例:index.html");
            Response.End();
            return;
        }
        fileName = this.Server.MapPath("~/templets/" + template + "/" + templatefile);
        base.LoadTemplateFile(fileName);
    }
    private void BindCommData()
    {
        DataSet ds = BLL.DataBaseHelper.instance.GetListByCache("webconfig", "keytext,keyvalue", 0, "", "px", "webconfig");
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            if (!hs.ContainsKey(item["keytext"]))
            {
                hs.Add(item["keytext"], item["keyvalue"]);
            }
        }
        hs.Add("base", BaseWeb.BaseUrl + "/templets/" + hs["template"] + "/");
        hs.Add("url", BaseWeb.BaseUrl);
    }

    #region 通用获取数据方法
    /// <summary>
    /// 通用获取数据方法
    /// </summary>
    /// <returns></returns>
    public static DataTable GetData()
    {
        TemplateDocument td = TemplateDocument.CurrentRenderingDocument;
        if (td != null)
        {
            if (td.CurrentRenderingTag != null)
            {
                string filename = "index";
                if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["fn"]))
                {
                    filename = System.Web.HttpContext.Current.Request.QueryString["fn"];
                }
                string bid = System.Web.HttpContext.Current.Request.QueryString["bid"];
                Tag tag = td.CurrentRenderingTag;
                //string vard = tag.Attributes.GetValue("var");
                string tablename = tag.Attributes.GetValue("data");
                string filedshow = tag.Attributes.GetValue("show");
                int top = 0;
                int.TryParse(tag.Attributes.GetValue("top"), out top);
                string strwhere = tag.Attributes.GetValue("filter");
                string orderstr = tag.Attributes.GetValue("px");
                try
                {
                    DataSet ds = BLL.DataBaseHelper.instance.GetList(tablename, filedshow, top, strwhere, orderstr);
                    if (ds.Tables.Count > 0)
                        return ds.Tables[0];
                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }
        return null;
    }



    #endregion

    #region 获取单条记录
    /// <summary>
    /// 通用获取数据方法
    /// </summary>
    /// <returns></returns>
    public static DataRowView GetSingleData()
    {
        TemplateDocument td = TemplateDocument.CurrentRenderingDocument;
        if (td != null)
        {
            if (td.CurrentRenderingTag != null)
            {
                Tag tag = td.CurrentRenderingTag;
                string tablename = tag.Attributes.GetValue("table");
                string filedshow = tag.Attributes.GetValue("show");
                string strwhere = tag.Attributes.GetValue("where");
                string para = tag.Attributes.GetValue("para");

                if (!string.IsNullOrEmpty(para))
                {
                    strwhere = strwhere.Replace("?", para);
                }
                try
                {
                    return BLL.DataBaseHelper.instance.GetModelView(tablename, filedshow, strwhere);
                }
                catch
                {
                    return null;
                }
            }
        }
        return null;
    }
    #endregion


    #region 判断是否需要session，没有则转向
    /// <summary>
    /// 通用获取数据方法
    /// </summary>
    /// <returns></returns>
    public static void Go2Url()
    {
        TemplateDocument td = TemplateDocument.CurrentRenderingDocument;
        if (td != null)
        {
            if (td.CurrentRenderingTag != null)
            {
                Tag tag = td.CurrentRenderingTag;
                string url = tag.Attributes.GetValue("url");
                if (BaseWeb.instance.UserId == 0)
                {
                    HttpContext.Current.Response.Redirect(url);
                }
            }
        }
    }
    #endregion
}