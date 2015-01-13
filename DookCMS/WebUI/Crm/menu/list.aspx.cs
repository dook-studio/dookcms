using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dukey.DBUtility;
using System.Configuration;
using System.Collections;
using System.Data;

public partial class Crm_MenuList : BaseCrm
{
    public string strwhere = " 1=1 ";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "menuid";
            ViewState["OrderDire"] = "";
            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {
        int total = 0;
        int pagecount = 1;

        DataSet ds = BLL.DataBaseHelper.instance.GetList("sys_menu", "[id]", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "menuid", out total, out pagecount); ;
        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        gvList.DataSource = dv;
        gvList.DataBind();
        pager.RecordCount = total;
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindData(strwhere);
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.gvList.EditIndex = e.NewEditIndex;
        BindData(strwhere);
    }
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        var id = gvList.DataKeys[e.RowIndex].Value.ToString();
        string menuid = ((TextBox)(gvList.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        string ParentId = ((TextBox)(gvList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string MenuName = ((TextBox)(gvList.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        string Path = ((TextBox)(gvList.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        string target = ((TextBox)(gvList.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
        string MenuType = ((TextBox)(gvList.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
        
        Hashtable hs = new Hashtable();
        hs.Add("menuid", menuid);
        hs.Add("ParentId", ParentId);
        hs.Add("MenuName", MenuName);
        hs.Add("Path", Path);
        hs.Add("target", target);
        hs.Add("MenuType", MenuType);       
        BLL.DataBaseHelper.instance.Update(hs, "sys_menu", "id=" + id);
        this.gvList.EditIndex = -1;
        BindData(strwhere);
    }
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvList.EditIndex = -1;
        BindData(strwhere);
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string id = e.CommandArgument.ToString();
            BLL.DataBaseHelper.instance.Delete("sys_menu", "id=" + id);
            BindData(strwhere);
        }

    }
    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }
        BindData(strwhere);
    }
   


    protected void btnAddNew_Click(object sender, EventArgs e)
    {        
        Hashtable hs=new Hashtable();
        hs.Add("MenuID","100");
        hs.Add("MenuName","新菜单");
        BLL.DataBaseHelper.instance.Insert(hs, "sys_menu");
        BindData(strwhere);  
    }
}
