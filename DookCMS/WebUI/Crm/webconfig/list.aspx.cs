using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Crm_webconfig : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    string strwhere = "1=1 ";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "px";
            ViewState["OrderDire"] = "ASC";
            BindData(strwhere);
        }
    }
    private void BindData(string strwhere)
    {
        int total = 0;
        int pagecount = 1;
        DataSet ds = BLL.DataBaseHelper.instance.GetList("webconfig", "[id]", "[id],keytext,keyvalue,px,remark", pager.PageSize, pager.CurrentPageIndex, strwhere, "px", out total, out pagecount); ;
        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        gvList.DataSource = dv;
        gvList.DataBind();
        pager.RecordCount = total;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("keytext", "这里填写英文名称");
        hs.Add("keyvalue", "这里填写值");
        hs.Add("remark", "这里填写备注");
        int px = gvList.Rows.Count+1;
        hs.Add("px", px);
        BLL.DataBaseHelper.instance.Insert(hs, "webconfig");
        BindData(strwhere);
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
        string keytext = ((TextBox)(gvList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string keyvalue = ((TextBox)(gvList.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
        string remark = ((TextBox)(gvList.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        string px = ((TextBox)(gvList.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
       Hashtable hs=new Hashtable();
        hs.Add("keytext",keytext);
        hs.Add("keyvalue",keyvalue);
        hs.Add("remark",remark);
        hs.Add("px",px);
        BLL.DataBaseHelper.instance.Update(hs, "webconfig", "id=" + id);
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
            BLL.DataBaseHelper.instance.Delete("webconfig", "id=" + id);
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

}
