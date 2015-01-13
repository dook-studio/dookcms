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
using System.Web.SessionState;


partial class crm : BasePage,IRequiresSessionState
{

    #region 解析模板
    private Hashtable hs = new Hashtable();
    protected override void InitPageTemplate()
    {      
        this.Document.SetValue("sys", hs);
        this.Document.SetValue("url", Request.QueryString);
        this.Document.SetValue("copyright", "<span>X1.0 © 2012-2013 <a href=\"http://www.dukeycms.com\">DukeyCMS.</a></span>");     
    }
    #endregion


    protected override void LoadTemplateFile(string fileName)
    {
        //如果session超时或者不存在
        if (Session["MyCrmUserName"] == null)
        {
            Response.Write("<script>window.top.location.replace( '/crm/login.aspx');</script>");
            Response.End();
            return;
        }

        BindCommData();
        //开始加载栏目       
        string fn =Request.QueryString["fn"]+".html";     
        fileName = this.Server.MapPath("~/templets/crm/" + fn);

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
        hs.Add("base", BaseWeb.BaseUrl + "/templets/crm/");
        hs.Add("url", BaseWeb.BaseUrl);
    }

    #region 通用获取数据方法
    /// <summary>
    /// 通用获取数据方法
    /// </summary>
    /// <returns></returns>
    public static DataView GetMenuData()
    {
        TemplateDocument td = TemplateDocument.CurrentRenderingDocument;
        if (td != null)
        {
            if (td.CurrentRenderingTag != null)
            {      
                Tag tag = td.CurrentRenderingTag;            

                try
                {
                    if (BaseWeb.instance.AdminId == 0)
                    {                        
                        return null;
                    }
                    DataTable dt;
                    DataTable mytable;
                    GetMenuByCache(out dt, out mytable);

                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "ParentId is NULL ";
                    string curid = tag.Attributes.GetValue("id");
                    string flag = tag.Attributes.GetValue("flag");
                    if (!string.IsNullOrEmpty(flag) && flag == "1")
                    {
                        string pid = tag.Attributes.GetValue("pid");
                        DataView dv1 = mytable.DefaultView;
                        dv1.RowFilter = "ParentId='"+pid+"'";      
                        return dv1;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(curid))
                        {
                            dv.RowFilter = "ParentId is NULL ";
                          
                        }
                        else
                        {
                            dv.RowFilter = "menuid='"+curid+"'";
                        }  
                        return dv;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }


        return null;
    }

    private static void GetMenuByCache(out DataTable dt, out DataTable mytable)
    {

        string menuid = "";
        string roleids = BLL.DataBaseHelper.instance.GetSingle("sys_admin", "roleids", "id=" + BaseWeb.instance.AdminId, "").ToString();
        foreach (string item in roleids.Split(','))
        {
            menuid += BLL.DataBaseHelper.instance.GetSingle("sys_roles", "menuids", "id=" + item, "") + ",";
        }
        if (menuid.Length > 0)
            menuid = menuid.Remove(menuid.Length - 1);
        string strwhere = "id in (" + menuid + ")";
        dt = BLL.DataBaseHelper.instance.GetList("sys_menu", "", 0, strwhere, " menuid").Tables[0];
        mytable = dt.Copy();
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
}