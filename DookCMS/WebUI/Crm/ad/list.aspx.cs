using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
public partial class Crm_ADList : BaseCrm
{
    string strwhere = " 1=1 ";
    public int total = 0, pagecount = 0;
    private static DataSet ds=BLL.DataBaseHelper.instance.GetList("adtype");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绑定广告分类
            droptype.DataSource = ds;
            droptype.DataBind();
            BindSearch(strwhere);
        }
    }
    public string GetTypeName(string typeid)
    {
        if (typeid != "0")
        {
            return ds.Tables[0].Select("id=" + typeid).FirstOrDefault()["cname"].ToString();
        }
        return "";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Hashtable hs = new Hashtable();
        hs.Add("adno", "code");
        hs.Add("cname", "中文名称");
        hs.Add("adtype", string.IsNullOrEmpty(droptype.SelectedValue) ? "0" : droptype.SelectedValue);
        hs.Add("remark", "新建的广告位");
        hs.Add("addtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        hs.Add("uptime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        BLL.DataBaseHelper.instance.Insert(hs, "ad");
        BindSearch(strwhere);
    }


    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindSearch(strwhere);
    }
    private string BindSearch(string strwhere)
    {
        if (droptype.SelectedValue != "" && droptype.SelectedValue != "-1")
        {
            strwhere = string.Format(" adtype={0}", droptype.SelectedValue);
        }
        gvList.DataSource = BLL.DataBaseHelper.instance.GetList("ad", "id", "*", pager.PageSize, pager.CurrentPageIndex, strwhere, "", out total, out pagecount);
        gvList.DataBind();
        pager.RecordCount = total;
        ViewState["wherestr"] = strwhere;
        return strwhere;
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
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
        string type_code = ((TextBox)(gvList.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString();
        string type = ((TextBox)(gvList.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString();

        Hashtable hs = new Hashtable();
        hs.Add("ename", type_code);
        hs.Add("cname", type);
        BLL.DataBaseHelper.instance.Update(hs, "ad", "id=" + id);

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
        BLL.DataBaseHelper.instance.Delete("ad", "id=" + id);
        BindSearch(strwhere);
    }
}
