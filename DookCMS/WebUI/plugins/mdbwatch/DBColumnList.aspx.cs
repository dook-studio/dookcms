using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using MdbWatch;

public partial class Mdbw_DBColumnList : BaseCrm
{
    public int fid = 0;

    private Hashtable hs = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (!StringHelper.IsBadChar(Request.QueryString["tablename"]))
            {
                string tablename = Request.QueryString["tablename"];
                hidtbname.Value = tablename;
                BindColumn(tablename);
                listTable.SelectedValue = hidtbname.Value;
            }
            BindData();
        }
    }
    private void BindColumn(string tablename)
    {
        DataTable dt = BLL.DataBaseHelper.instance.GetShemaColumnName(tablename);//cid,name,type,notnull,dflt_value,pk
        DataView dtv = dt.DefaultView;
        dtv.Sort = "cid";
        gvList.DataSource = dtv;
        gvList.DataBind();
    }

    private void BindData()
    {
        DataTable dt = BLL.DataBaseHelper.instance.GetShemaTable();
        DataView dtv = dt.DefaultView;
        dtv.RowFilter = "name not like  'sqlite_%'";
        listTable.DataSource = dt;
        listTable.DataTextField = "name";
        listTable.DataValueField = "name";
        listTable.DataBind();
    }

    public bool IsPrimaryKey(string column)
    {
        string tablename = hidtbname.Value;
        if (hs.Contains(tablename.ToLower()))
        {
            if (column.ToLower() == hs[tablename.ToLower()].ToString())
                return true;
        }
        return false;
    }


    protected void listTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtbname.Value = listTable.SelectedValue;
        BindColumn(hidtbname.Value);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindColumn(hidtbname.Value);
        listTable.SelectedValue = hidtbname.Value;
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Delete":
                {
                    string colname = e.CommandArgument.ToString();
                    //删除
                    string tablename = Request.QueryString["tablename"];

                    BLL.DataBaseHelper.instance.DeleteColumn(tablename, colname);
                    BindColumn(hidtbname.Value);
                    break;
                }
        }
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
}
