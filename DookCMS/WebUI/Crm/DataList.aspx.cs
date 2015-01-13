using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.Collections;

public partial class Crm_DataList : BaseCrm
{
    public int fid = 0;
    //List<Model.Boards> list = BLL.Boards.instance.GetList("");
    private Hashtable hs = BLL.DataBaseHelper.instance.GetPrimaryKeys();
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

            DataSet ds = DataBaseHelper.instance.GetList(tablename, "*", 0, "", "");
            //DataTable  dt = ds.Tables[0];
            //dt.Columns.Remove("contents");
            gvList.DataSource = ds;
            //gvList.DataKeyNames = "";
           
            //if (hs.Contains(tablename.ToLower()))
            //{
            //    gvList.DataKeyNames = new string[] { hs[tablename.ToLower()].ToString() };     
            //}

            gvList.DataBind();

        }
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
        
        Response.Write(e.Keys);
       // var dd = e.Values;
    }
}
