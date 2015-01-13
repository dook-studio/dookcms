using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.Collections;

public partial class Mdbw_DataList : BaseCrm
{
    public int fid = 0;
    private Hashtable hs = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }
    private void BindData()
    {
        if (!StringHelper.IsBadChar(Request.QueryString["tablename"]))
        {
            gvList.AutoGenerateColumns = true;
            string tablename = Request.QueryString["tablename"];
            DataSet ds = BLL.DataBaseHelper.instance.GetList(tablename);
            string pk = "id";
            gvList.DataKeyNames = new string[1] {"id"};

            gvList.DataSource = ds;
            gvList.DataBind();
            hidPk.Value = pk;
            foreach (GridViewRow item in gvList.Rows)
            {
                foreach (TableCell c in item.Cells)
                {
                    if (c.Text.Length > 100)
                    {
                        c.Text = "<a href=\"javascript:\" title=\"" + c.Text + "\">详情</a>";
                    }
                }
            }         
        }
    }
    public string PrimaryKeys(string tablename)
    {
        if (hs.Contains(tablename.ToLower()))
        {
            return hs[tablename.ToLower()].ToString();
        }
        return "";
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditDBTable.aspx");
    }

    protected void gvList_PageIndexChanged(object sender, EventArgs e)
    {
        //if (ViewState["wherestr"] != null && !string.IsNullOrEmpty(ViewState["wherestr"].ToString()))
        //{
        //    //SqlDataSource1.SelectCommand = "select * from Products where " + ViewState["wherestr"].ToString();
        //}
        //gvList.DataSourceID = "SqlDataSource1";
        //BindData();
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvList.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //gvList.Rows[1].
            //Common.MessageBox.Show(this, e.CommandName);
            //DataControlField f;           
            //string dd=gvList.Columns["
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gvList.DataKeys[e.RowIndex].Values[0].ToString();
        string key = gvList.DataKeyNames[0];
        string tablename = Request.QueryString["tablename"];   
        //MdbWatch.DbHelper.Delete(tablename, "" + key + "=" + id);
        BLL.DataBaseHelper.instance.Delete(tablename, "" + key + "=" + id);
        BindData();

    }
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //添加访问权限的逻辑,session和server.avariable["refer"]以及主机名称.防止跨域提交表单
            if (Request.ServerVariables["HTTP_Referer"] == null || Request.ServerVariables["REMOTE_HOST"] == null || Request.ServerVariables["SERVER_NAME"] != Request.Url.Host)
            {
                Response.Write("非法访问!");
                Response.End();
                return;
            }
        }
    }
    protected void btnSql_Click(object sender, EventArgs e)
    {
        string sql = txtsql.Text.Trim();
        BLL.DataBaseHelper.instance.ExecuteSql(sql,null);
        Label1.Text = "执行成功" + DateTime.Now.ToString("HH:mm:ss");
        BindData();
    }
}
