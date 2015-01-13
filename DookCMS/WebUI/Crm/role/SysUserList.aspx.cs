using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

public partial class Crm_SysUserList : BaseCrm
{
    string strwhere = "1=1 ";    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "id";
            ViewState["OrderDire"] = "ASC";
            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {      
        int total = 0;
        int pagecount = 1;
        DataSet ds = BLL.DataBaseHelper.instance.GetList("Sys_Admin", "id", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "", out total, out pagecount);
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
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string id = e.CommandArgument.ToString();
            BLL.DataBaseHelper.instance.Delete("Sys_Admin", "[id]=" + id);
            BindData(strwhere);
        }
    }
   
   
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditSysUser.aspx");
    }
    public string GetRoles(string roleids)
    {
        string str = "";
        if (!string.IsNullOrEmpty(roleids))
        {
            //string sql="select roleName from sys_Roles where roleid in (select roleid from Sys_AdminRole where userid="+adminid+")";
            //DataSet ds=BLL.DataBaseHelper.instance.GetList("sys_roles", "rolename",0, "roleid in (select roleid from Sys_AdminRole where userid=" + adminid + ")","");
            string[] arrrole = roleids.Split(',');
            foreach (string item in arrrole)
            {
                string rolename = BLL.DataBaseHelper.instance.GetSingle("sys_roles", "rolename", "id=" + item, "").ToString();
                str += rolename + " ";
            }
        }
        return str;
    }


}
