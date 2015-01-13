using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Common;
public partial class Crm_adselector : BaseCrm
{
    string strwhere = " 1=1 ";
    public int total = 0, pagecount = 0;
    public string fromtype = "article";//推送方式
    public int fromid = 0;//推送来源编号
    private static DataSet ds=BLL.DataBaseHelper.instance.GetList("adtype");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.QueryString["fromtype"].IsEmpty())
            {
                fromtype = Request.QueryString["fromtype"];
            }
            if (!Request.QueryString["fromid"].IsEmpty())
            {
                fromid = Request.QueryString["fromid"].ObjToInt();
            }
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
