using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Crm_plugins_PluginList : System.Web.UI.Page
{
    string strwhere = " 1=1 ";
    public int total = 0, pagecount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSearch(strwhere);
        }
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindSearch(strwhere);
    }
    private string BindSearch(string strwhere)
    {        
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("plugin", "[id]", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "", out total, out pagecount);
        gvList.DataBind();
        pager.RecordCount = total;
        ViewState["wherestr"] = strwhere;
        return strwhere;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditPlugin.aspx");
    }   
}
