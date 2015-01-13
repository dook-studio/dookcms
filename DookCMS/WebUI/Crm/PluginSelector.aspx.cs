using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text.RegularExpressions;

public partial class Crm_PluginSelector : System.Web.UI.Page
{
    string strwhere = "1=1";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {         
            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("plugins", "*", 0, strwhere, "");
        gvList.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindData(strwhere);
    }
    //private string BindSearch(string strwhere)
    //{
    //    if (dropList.SelectedValue != "" && dropList.SelectedValue != "-1")
    //    {
    //        strwhere = string.Format(" ename='{0}'", dropList.SelectedValue);
    //    }
    //    gvList.DataSource = BLL.DataBaseHelper.instance.GetList("plugins", "id", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "", out total, out pagecount);
    //    gvList.DataBind();
    //    pager.RecordCount = total;
    //    ViewState["wherestr"] = strwhere;
    //    return strwhere;
    //}
    [WebMethod]
    public static string SelectPlugins(string originstr,string type, string fn)//选择插件
    {
        try
        {
            Dukey.Model.WebConfig mysite = BLL.WebConfig.instance.GetModelByCache();//网站配置信息
            string path = HttpContext.Current.Server.MapPath("~/template/" + mysite.folder + "/" + fn + ".html");
            string htmlstr = Common.FileHelper.ReadFile(path, "utf-8");
            string tempid = type + "" + DateTime.Now.ToString("fff");
            htmlstr = htmlstr.Replace("edit=\"" + originstr + "\"", "edit=\"" + type + "." + tempid + "\"");
            htmlstr = htmlstr.Replace("<!--" + originstr + "-->", "{$plugin." + type + "." + tempid + "}");
            //MatchCollection mc = Regex.Matches(htmlstr, @"(?<=<div[^>]*>)[\s\S]*?(?=</div>)", RegexOptions.IgnoreCase);
            //htmlstr = htmlstr.Replace("edit=\"" + type + "." + pluginid + "\"", "edit=\"placeholder-" + DateTime.Now.ToString("fff") + "\"");
            Common.FileHelper.WriteFile(path, htmlstr, "utf-8");
            return "ok";
        }
        catch(Exception ex) {
            return ex.Message;
        }
    }
}
