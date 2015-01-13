using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dukey.DBUtility;

public partial class Modules_Sys_Paramlist : BaseCrm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (dropSort.SelectedValue == "")
        {

            Common.MessageBox.Show(this, "请先选择分类");
            return;
        }
        string sql = "insert into sys_Params([code],[codevalue]) values('" + dropSort.SelectedValue + "','')";
        DbHelper.ExecuteSql(sql);
        GetData();
    }
    protected void dropSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();

    }

    private void GetData()
    {
        if (dropSort.SelectedValue != "")
            SqlDataSource1.SelectCommand = "SELECT * FROM [sys_Params] where [code]='" + dropSort.SelectedValue + "'";
        gvList.DataSourceID = "SqlDataSource1";
    }
}
