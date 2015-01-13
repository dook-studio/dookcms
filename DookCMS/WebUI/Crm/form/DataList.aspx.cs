using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Common;

public partial class Crm_form_DataList : System.Web.UI.Page
{

    private string cols = string.Empty, strwhere = "1=1";
    public string cname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["formid"]))
            {
                hidformid.Value = Request.QueryString["formid"];
                BindHeader(hidformid.Value.ObjToInt());
                BindData(strwhere);
            }

        }
    }
    private void BindHeader(int formid)
    {
        string strwhere = string.Empty;
        switch (Common.MyWeb.DbType)
        {
            case 0: strwhere = "formid=" + formid + " and isshow=True";
                break;
            case 1: strwhere = "formid=" + formid + " and isshow=1";//sqlserver
                break;
            case 2: strwhere = "formid=" + formid + " and isshow=1";//sqlite
                break;
        }
        DataSet ds = BLL.DataBaseHelper.instance.GetList("FormPara", "colname,cname", 0, strwhere, "");
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = item["cname"].ToString();
                bf.DataField = item["colname"].ToString();
                bf.SortExpression = item["colname"].ToString();
                gvList.Columns.Add(bf);
                cols += item["colname"].ToString() + ",";
            }
            CommandField cf = new CommandField();
            cf.HeaderText = "操作";
            cf.ShowEditButton = false;
            cf.ShowDeleteButton = true;
            cf.ButtonType = ButtonType.Button;
            gvList.Columns.Add(cf);

            if (cols.Length > 0)
                cols = cols.Remove(cols.Length - 1, 1);
            hidcols.Value = cols;
        }
    }
    private void BindData(string strwhere)
    {
        DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("form", "pk,cname,tablename", "id=" + hidformid.Value);
        if (dr != null)
        {
            cname = dr["cname"].ToString();
            string tablename = dr["tablename"].ToString();
            string pk = dr["pk"].ToString();
            gvList.DataKeyNames = new string[] { pk };
            cols = pk + "," + hidcols.Value;
            //绑定数据源
            int total = 0;
            int pageCount = 1;
            DataSet ds = BLL.DataBaseHelper.instance.GetList(tablename, pk, cols, pager.PageSize, pager.CurrentPageIndex, strwhere, "", out total, out pageCount);
            DataView dtv = ds.Tables[0].DefaultView;
            string sort = string.Empty;
            try
            {
                sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                dtv.Sort = sort;
            }
            catch { }
            gvList.DataSource = dtv;
            gvList.DataBind();
            pager.RecordCount = total;
        }
        else
        {
            Response.Write("参数不正确");
            Response.End();
        }
    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        BindData(strwhere);
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gvList.DataKeys[e.RowIndex].Value.ToString();

        DataRowView dr = BLL.DataBaseHelper.instance.GetModelView("form", "pk,tablename", "id=" + hidformid.Value);
        if (dr != null)
        {
            string tablename = dr["tablename"].ToString();
            string pk = dr["pk"].ToString();
            BLL.DataBaseHelper.instance.Delete(tablename, pk + "=" + id);
            BindData(strwhere);
        }
    }
    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].IsEmpty())
        {
            ViewState["SortOrder"] = sPage;
            ViewState["OrderDire"] = "ASC";
        }
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
