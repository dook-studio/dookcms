using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Crm_userlist : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
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
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("[user]", "[id]", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "id desc", out total, out pagecount);
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
            strwhere += string.Format(" and email like '%{0}%' ", txtSearch.Text.Trim());
        }
        //if (dropSort.SelectedValue != "")
        //{
        //    strwhere += string.Format(" and islock=" + dropSort.SelectedValue);
        //}
        BindData(strwhere);
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and email like '%{0}%' ", txtSearch.Text.Trim());
        }
        //if (dropSort.SelectedValue != "")
        //{
        //    strwhere += string.Format(" and bid=" + dropSort.SelectedValue);
        //}   
        BindData(strwhere);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pager.CurrentPageIndex = 1;
        if (txtSearch.Text.Trim() != "")
        {
            strwhere += string.Format(" and email like '%{0}%' ", txtSearch.Text.Trim());
        }
        //if (dropSort.SelectedValue != "")
        //{
        //    strwhere += string.Format(" and bid=" + dropSort.SelectedValue);
        //}        
        BindData(strwhere);
    }
}
