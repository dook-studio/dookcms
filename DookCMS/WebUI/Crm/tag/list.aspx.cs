using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Dukey.Model;


public partial class Crm_Tag : BaseCrm
{
    public string pname = "", depth = "", strwhere = " 1=1 ";
    public int fid = 0;
    public List<Tag> list= StringHelper.DataTableToList<Tag>(BLL.DataBaseHelper.instance.GetList("tag").Tables[0]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StringHelper.isNum(Request.QueryString["pid"]))
        {
            fid = int.Parse(Request.QueryString["pid"]);
        }
        strwhere += " and pid=" + fid;

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
        DataSet ds = BLL.DataBaseHelper.instance.GetList("tag", "[id]", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "px", out total, out pagecount); ;
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
        hs.Add("keytext", "");
        hs.Add("keyvalue", "");
        hs.Add("remark", "");
        hs.Add("pid", fid);
        int px =(pager.CurrentPageIndex-1)*pager.PageSize+gvList.Rows.Count + 1;
        hs.Add("px", px);
        BLL.DataBaseHelper.instance.Insert(hs, "tag");
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
        string pid = ((TextBox)(gvList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        string keytext = ((TextBox)(gvList.Rows[e.RowIndex].FindControl("txtkeytext"))).Text.ToString().Trim();
        string keyvalue = ((TextBox)(gvList.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
        string remark = ((TextBox)(gvList.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim();
        string px = ((TextBox)(gvList.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
        Hashtable hs = new Hashtable();
        hs.Add("keytext", keytext);
        hs.Add("keyvalue", keyvalue);
        hs.Add("remark", remark);
        hs.Add("pid", pid);
        hs.Add("px", px);
        BLL.DataBaseHelper.instance.Update(hs, "tag", "id=" + id);
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
            BLL.DataBaseHelper.instance.Delete("tag", "id=" + id);
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

    string str = "";
    public string GetNavLink(int id)
    {
        if (list != null)
        {
            Dukey.Model.Tag model = list.Find(item => item.id == id);
            if (model != null && model.pid == 0)
            {
                str = string.Format("<a class=\"nav\" href='list.aspx?pid={0}&pname={1}'>{1}</a>&gt;&gt;", model.id, model.keytext) + str;
                return str.ToString();
            }
            else if (model != null && model.pid != 0)
            {
                str = string.Format("<a class=\"nav\" href='list.aspx?pid={0}&pname={1}'>{1}</a>&gt;&gt;", model.id, model.keytext) + str;
                return GetNavLink(model.pid);
            }
            return "";
        }
        else
        {
            return "";
        }
    }
}
