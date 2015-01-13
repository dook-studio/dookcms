using System;
using System.Web;
using Dukey.Engine;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.Collections;


partial class pitem : BasePage
{
    #region 解析模板
    private Hashtable hs = new Hashtable();
    private DataRowView theproduct;

    protected override void InitPageTemplate()
    {     
        this.Document.SetValue("sys", hs);
        this.Document.SetValue("the", theproduct);      
        this.Document.SetValue("url", Request.QueryString);
    }
    #endregion

    protected override void LoadTemplateFile(string fileName)
    {
        BindCommData();
        //开始加载栏目
        string aid = Request.QueryString["aid"];//
        if (string.IsNullOrEmpty(aid))
        {
            Response.Write("缺少参数aid!");
            Response.End();
            return;
        }
        if (!StringHelper.isNum(aid))
        {
            Response.Write("参数格式不正确!");
            Response.End();
            return;
        }

        DataRowView item = BLL.DataBaseHelper.instance.GetModelView("product", "*", "id=" + aid);
        if (item != null)
        {
            theproduct = item;

            //获取模板文件目录-------------------------------------------------------------------------------------------------------------
            string template = "default";
            if (hs["template"] == null || hs["template"] == "")
            {
                throw new Exception("webconfig表缺少template参数");
            }
            else
            {
                template = hs["template"].ToString();
            }
            //------------------------------------------------------------------------------------------------------------------------------
            DataRowView model = BLL.DataBaseHelper.instance.GetModelView("channel", "itemfileurl", "bid=" + item["typeid"].ToString());
            if (model == null)
            {
                fileName = this.Server.MapPath("~/templets/" + template + "/pitem.html");
                base.LoadTemplateFile(fileName);
                return;
            }
            string templatefile = model["itemfileurl"].ToString();//模板文件

            if (string.IsNullOrEmpty(templatefile))//判断是否设置了模板.
            {
                Response.Write("你还没有设置该栏目(编号为:" + item["typeid"] + ",类型:商品列表)下的模板文件喔~^~!请在后台栏目管理->高级->内容模板中设置.设置示例:pitem.html");
                Response.End();
                return;
            }
            fileName = this.Server.MapPath("~/templets/" + template + "/" + templatefile);
            base.LoadTemplateFile(fileName); 
        }
        else
        {
            Response.Write("该商品已下架或者已经被删除!");
            Response.End();
        }
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
                    // strwhere = strwhere.Replace("?", para);
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

    #region 上一篇,下一篇
    /// <summary>
    /// 通用获取数据方法
    /// </summary>
    /// <returns></returns>
    public static object PreNext()
    {
        TemplateDocument td = TemplateDocument.CurrentRenderingDocument;
        if (td != null)
        {
            if (td.CurrentRenderingTag != null)
            {
                Tag tag = td.CurrentRenderingTag;
                string get = tag.Attributes.GetValue("get");
                string aid = HttpContext.Current.Request.QueryString["aid"];
                try
                {
                    if (get == "pre")
                    {

                        DataRowView o= BLL.DataBaseHelper.instance.GetList("product", "id,title", 1, "id>" + aid, "id desc").Tables[0].DefaultView[0];
                        if (o != null)
                        {
                            return string.Format("<a href=\"/item.ashx?aid={0}\">{1}</a>",o["id"].ToString(),o["title"].ToString());
                        }
                    }
                    else
                    {
                        DataRowView o= BLL.DataBaseHelper.instance.GetList("product", "id,title", 1, "id<" + aid, "id").Tables[0].DefaultView[0];
                        if (o != null)
                        {
                            return string.Format("<a href=\"/item.ashx?aid={0}\">{1}</a>", o["id"].ToString(), o["title"].ToString());
                        }
                        
                    }                   
                }
                catch
                {
                    return "已经没有了";
                }
            }
        }
        return "已经没有了";
    }
    #endregion

    
}