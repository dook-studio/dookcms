using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Dukey.Model;
using System.Collections;

public partial class Crm_FetchDataList : BaseCrm
{
    public int fid = 0;
    string strwhere = "1=1 ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {       
            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {
        int total = 0;
        int pagecount = 1;
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("collectdata", "[id]", "[id],[cname], [type], [links], [encoding],[addtime],lasttime", pager.PageSize, pager.CurrentPageIndex, strwhere, "id desc", out total, out pagecount);
        gvList.DataBind();
        pager.RecordCount = total;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("edit.aspx");
    }

    protected void dropSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and cname like '%{0}%' ", txtSearch.Text.Trim());
        }
    
        BindData(strwhere);
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and cname like '%{0}%' ", txtSearch.Text.Trim());
        }
    
        BindData(strwhere);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pager.CurrentPageIndex = 1;
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and cname like '%{0}%' ", txtSearch.Text.Trim());
        }
        BindData(strwhere);
    }

}
