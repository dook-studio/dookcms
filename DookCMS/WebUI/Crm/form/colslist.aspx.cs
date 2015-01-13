using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class colslist : System.Web.UI.Page
{
    public string cname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string formid = Request["id"];
        cname=BLL.DataBaseHelper.instance.GetSingle("form", "cname", "id=" + formid, "").ToString();
   
        if (!string.IsNullOrEmpty(formid))
        {
            rptlist.DataSource = BLL.DataBaseHelper.instance.GetList("formpara", "*", 0, "formid=" + formid, "");
            rptlist.DataBind();
        }
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            BLL.DataBaseHelper.instance.Delete("formpara", "id=" + e.CommandArgument.ToString());
            BindData();
        }
    }
}