using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Crm_ad_sort : System.Web.UI.Page
{
    string strwhere =" 1=1 ";
    public int total = 0, pagecount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSearch(strwhere);
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();   
        hs.Add("cname", "分类名称");
        BLL.DataBaseHelper.instance.Insert(hs, "adtype");
        BindSearch(strwhere);
    }


    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindSearch(strwhere);
    }
    private string BindSearch(string strwhere)
    {
      
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("adtype", "id", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "px,id", out total, out pagecount);
        gvList.DataBind();
        pager.RecordCount = total;
        ViewState["wherestr"] = strwhere;
        return strwhere;
    }
    protected void dropPartner_SelectedIndexChanged(object sender, EventArgs e)
    {
        pager.CurrentPageIndex = 1;
        BindSearch(strwhere);
    }
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.gvList.EditIndex = e.NewEditIndex;
        BindSearch(strwhere);
    }
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gvList.DataKeys[e.RowIndex].Values[0].ToString();
        string cname = ((TextBox)(gvList.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString();
        string px = ((TextBox)(gvList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString();

        Hashtable hs = new Hashtable();
        hs.Add("cname", cname);
        hs.Add("px", px);
        BLL.DataBaseHelper.instance.Update(hs, "adtype", "id=" + id);

        gvList.EditIndex = -1;
        BindSearch(strwhere);
    }
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.gvList.EditIndex = -1;
        BindSearch(strwhere);
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gvList.DataKeys[e.RowIndex].Values[0].ToString();
        BLL.DataBaseHelper.instance.Delete("adtype", "id=" + id);
        BindSearch(strwhere);
    }
}